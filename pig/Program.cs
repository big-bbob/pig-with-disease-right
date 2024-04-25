using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Pig;
using Lavalink4NET.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

var config = JsonSerializer.Deserialize<BotConfig>(
    File.ReadAllText(new FileInfo("config.json").FullName)
);

if (config == null)
{
    throw new Exception("Invalid configuration");
}

PublicConfig.Config = config;

var builder = new HostApplicationBuilder(args);

// DSharpPlus
builder.Services.AddHostedService<ApplicationHost>();
builder.Services.AddSingleton<DiscordClient>();
builder.Services.AddSingleton(new DiscordConfiguration { Token = PublicConfig.Config.Token, Intents = DiscordIntents.All });

#if DEBUG

#else
builder.Services.AddLavalink();
#endif

// Logging
builder.Services.AddLogging(s => s.AddConsole().SetMinimumLevel(LogLevel.Trace));

builder.Build().Run();

file sealed class ApplicationHost : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DiscordClient _discordClient;

    public BotConfig Config;

    public ApplicationHost(IServiceProvider serviceProvider, DiscordClient discordClient)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(discordClient);

        _serviceProvider = serviceProvider;
        _discordClient = discordClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cmds = _discordClient.UseSlashCommands(new SlashCommandsConfiguration { Services = _serviceProvider });
        cmds.RegisterCommands<MusicCommands>();
        cmds.RegisterCommands<Hero>();
        cmds.RegisterCommands<Gamble>();

        // connect to discord gateway and initialize node connection
        await _discordClient
            .ConnectAsync()
            .ConfigureAwait(false);

        var readyTaskCompletionSource = new TaskCompletionSource();

        Task SetResult(DiscordClient client, ReadyEventArgs eventArgs)
        {
            readyTaskCompletionSource.TrySetResult();
            return Task.CompletedTask;
        }

        _discordClient.Ready += SetResult;
        await readyTaskCompletionSource.Task.ConfigureAwait(false);
        _discordClient.Ready -= SetResult;
        _discordClient.MessageCreated += MessageCreated;

        Responses.discordClient = _discordClient;

        DSharpPlus.Entities.DiscordActivity activity = new DSharpPlus.Entities.DiscordActivity("Bad Piggies", DSharpPlus.Entities.ActivityType.Playing);
        await _discordClient.UpdateStatusAsync(activity, DSharpPlus.Entities.UserStatus.Online);
        await cmds.RefreshCommands();

        await Task
            .Delay(Timeout.InfiniteTimeSpan, stoppingToken)
            .ConfigureAwait(false);
    }
    public async Task MessageCreated(DiscordClient client, MessageCreateEventArgs eventArgs)
    {
        if (PublicConfig.Config.CoolServers.Contains(eventArgs.Guild.Id) && eventArgs.Author.Id != _discordClient.CurrentUser.Id)
        {
            PublicConfig.msgs.Add(new MsgHist() { time = DateTime.Now, user = eventArgs.Author.Id });
            PublicConfig.msgs.RemoveAll(x => DateTime.Now.Subtract(x.time).Hours >= 1);

            if (PublicConfig.msgs.FindAll(x => PublicConfig.Config.GambleVictims.Contains(x.user)).Count > 30)
            {
                PublicConfig.ArgumentStatus = true;
            }
            else
            {
                PublicConfig.ArgumentStatus = false;
            }

            await Responses.CheckResponses(client, eventArgs);
        }
    }
}