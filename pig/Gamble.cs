using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Pig
{
    public class Gamble : ApplicationCommandModule
    {
        static Random _random = new Random();

        [SlashCommand("gamble", "Timeout yourself or someone annoying")]
        public async Task GambleCommand(InteractionContext ctx)
        {
            int count = PublicConfig.Config.GambleVictims.Count + 1;
            int random = _random.Next(0, count);

            if (random == count - 1)
            {
                // You loose
                await ctx.Member.TimeoutAsync(new DateTimeOffset().AddMinutes(10), "Idiot");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("L + Bozo"));
            }
            else
            {
                ulong userId = PublicConfig.Config.GambleVictims[random];
                // You Win
                if (ctx.Guild.Members.ContainsKey(userId))
                {
                    await ctx.Guild.Members[userId].TimeoutAsync(new DateTimeOffset().AddMinutes(10), "Chromosomeless behaviour");
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Wahahaha"));
                }
                else
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Nothing happened?"));
                }
            }

        }
    }
}