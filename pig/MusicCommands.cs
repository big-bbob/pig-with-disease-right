namespace Pig;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Lavalink4NET;
using Lavalink4NET.Players;
using Lavalink4NET.Players.Queued;
using Lavalink4NET.Rest.Entities.Tracks;
using Microsoft.Extensions.Options;

[SlashCommandGroup("music", "Music related commands")]
public class MusicCommands : ApplicationCommandModule
{
    List<ulong> cringeList = new List<ulong>();

    private readonly IAudioService _audioService;

    public MusicCommands(IAudioService audioService)
    {
        ArgumentNullException.ThrowIfNull(audioService);

        _audioService = audioService;
    }

    [SlashCommand("play", description: "Plays music")]
    public async Task Play(InteractionContext interactionContext, [Option("query", "Track to play")] string query)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        var track = await _audioService.Tracks
            .LoadTrackAsync(query, TrackSearchMode.YouTube)
            .ConfigureAwait(false);

        if (track is null)
        {
            var errorResponse = new DiscordFollowupMessageBuilder()
                .WithContent("No results.")
                .AsEphemeral();

            await interactionContext
                .FollowUpAsync(errorResponse)
                .ConfigureAwait(false);

            return;
        }

        var position = await player
            .PlayAsync(track)
            .ConfigureAwait(false);

        if (position is 0)
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Playing: {track.Title}"))
                .ConfigureAwait(false);
        }
        else
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Added to queue: {track.Title}"))
                .ConfigureAwait(false);
        }
    }

    [SlashCommand("myway", description: "Plays My Way")]
    public async Task PlayWay(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        var track = await _audioService.Tracks
            .LoadTrackAsync("My Way (2008 Remastered)", TrackSearchMode.YouTube)
            .ConfigureAwait(false);

        if (track is null)
        {
            var errorResponse = new DiscordFollowupMessageBuilder()
                .WithContent("No results.")
                .AsEphemeral();

            await interactionContext
                .FollowUpAsync(errorResponse)
                .ConfigureAwait(false);

            return;
        }

        var position = await player
            .PlayAsync(track)
            .ConfigureAwait(false);

        if (position is 0)
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Playing: {track.Title}"))
                .ConfigureAwait(false);
        }
        else
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Added to queue: {track.Title}"))
                .ConfigureAwait(false);
        }
    }

    [SlashCommand("rizz", description: "Rizz")]
    public async Task PlayRizz(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        var track = await _audioService.Tracks
            .LoadTrackAsync(PublicConfig.Config.RizzMusic, TrackSearchMode.YouTube)
            .ConfigureAwait(false);

        if (track is null)
        {
            var errorResponse = new DiscordFollowupMessageBuilder()
                .WithContent("No results.")
                .AsEphemeral();

            await interactionContext
                .FollowUpAsync(errorResponse)
                .ConfigureAwait(false);

            return;
        }

        var position = await player
            .PlayAsync(track)
            .ConfigureAwait(false);

        if (position is 0)
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Playing: {track.Title}"))
                .ConfigureAwait(false);
        }
        else
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Added to queue: {track.Title}"))
                .ConfigureAwait(false);
        }
    }

    [SlashCommand("leave", description: "Leave VC")]
    public async Task Leave(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        await player.DisconnectAsync();

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"See ya later idiots"))
            .ConfigureAwait(false);
    }


    [SlashCommand("skip", description: "Skip Song in Queue")]
    public async Task Skip(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        await player.SkipAsync();

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Rightio"))
            .ConfigureAwait(false);
    }

    [SlashCommand("pause", description: "Pause Playback")]
    public async Task Pause(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        await player.PauseAsync();

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Rightio"))
            .ConfigureAwait(false);
    }

    [SlashCommand("resume", description: "Resume Playback")]
    public async Task Resume(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        await player.ResumeAsync();

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Rightio"))
            .ConfigureAwait(false);
    }

    [SlashCommand("current", description: "Current Track")]
    public async Task Current(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        var current = player.CurrentTrack;

        if (current == null)
        {
            return;
        }

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"{current.Title}"))
            .ConfigureAwait(false);
    }

    [SlashCommand("remove", description: "Remove Track from Queue")]
    public async Task Remove(InteractionContext interactionContext, [Option("index", "Number in Queue")] string index)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        if (cringeList.Contains(interactionContext.User.Id))
        {
            await interactionContext
                .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Nuh uh"))
                .ConfigureAwait(false);
        }

        int _index = int.Parse(index);
        await player.Queue.RemoveAtAsync(_index - 1);

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Done"))
            .ConfigureAwait(false);
    }

    [SlashCommand("queue", description: "Display the Queue")]
    public async Task Queue(InteractionContext interactionContext)
    {
        await interactionContext.DeferAsync().ConfigureAwait(false);

        var player = await GetPlayerAsync(interactionContext, connectToVoiceChannel: true).ConfigureAwait(false);

        if (player is null)
        {
            return;
        }

        string queue = "";

        foreach (var item in player.Queue)
        {
            if (item.Track != null)
                queue += item.Track.Title + ", ";
        }

        await interactionContext
            .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent(queue))
            .ConfigureAwait(false);
    }


        [SlashCommand("block", "Admin stuff")]
        public async Task Restrict(InteractionContext interactionContext, [Option("user", "block")] DiscordUser user)
        {
            await interactionContext.DeferAsync().ConfigureAwait(false);

            if (PublicConfig.Config.BasedList.Contains(interactionContext.User.Id) && !cringeList.Contains(interactionContext.User.Id))
            {
                if (user.Id != PublicConfig.Config.Owner && !cringeList.Contains(user.Id))
                {
                    cringeList.Add(user.Id);

                    await interactionContext
                        .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Yes"))
                        .ConfigureAwait(false);
                }
                else
                {
                    await interactionContext
                        .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("No"))
                        .ConfigureAwait(false);
                }
            }
            else
            {
                await interactionContext
                    .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("No"))
                    .ConfigureAwait(false);
            }
        }

        [SlashCommand("noblock", "Admin stuff")]
        public async Task Unrestrict(InteractionContext interactionContext, [Option("user", "noblock")] DiscordUser user)
        {
            await interactionContext.DeferAsync().ConfigureAwait(false);

            if (PublicConfig.Config.BasedList.Contains(interactionContext.User.Id) && !cringeList.Contains(interactionContext.User.Id))
            {
                if (user.Id != PublicConfig.Config.Owner && !cringeList.Contains(user.Id))
                {
                    cringeList.Remove(user.Id);

                    await interactionContext
                        .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Yes"))
                        .ConfigureAwait(false);
                }
                else
                {
                    await interactionContext
                        .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("No"))
                        .ConfigureAwait(false);
                }
            }
            else
            {
                await interactionContext
                    .FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("No"))
                    .ConfigureAwait(false);
            }
        }

    private async ValueTask<QueuedLavalinkPlayer?> GetPlayerAsync(InteractionContext interactionContext, bool connectToVoiceChannel = true)
    {
        ArgumentNullException.ThrowIfNull(interactionContext);

        var retrieveOptions = new PlayerRetrieveOptions(
            ChannelBehavior: connectToVoiceChannel ? PlayerChannelBehavior.Join : PlayerChannelBehavior.None);

        var playerOptions = new QueuedLavalinkPlayerOptions { HistoryCapacity = 10000 };

        var result = await _audioService.Players
            .RetrieveAsync(interactionContext.Guild.Id, interactionContext.Member?.VoiceState.Channel.Id, playerFactory: PlayerFactory.Queued, Options.Create(playerOptions), retrieveOptions)
            .ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            var errorMessage = result.Status switch
            {
                PlayerRetrieveStatus.UserNotInVoiceChannel => "You are not connected to a voice channel.",
                PlayerRetrieveStatus.BotNotConnected => "The bot is currently not connected.",
                _ => "Unknown error.",
            };

            var errorResponse = new DiscordFollowupMessageBuilder()
                .WithContent(errorMessage)
                .AsEphemeral();

            await interactionContext
                .FollowUpAsync(errorResponse)
                .ConfigureAwait(false);

            return null;
        }

        return result.Player;
    }
}