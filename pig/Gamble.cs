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

        static ulong last;

        [SlashCommand("gamble", "Timeout yourself or someone annoying")]
        public async Task GambleCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            int count = PublicConfig.Config.GambleVictims.Count + 1;
            int random = _random.Next(0, count);

            var timeDuration = DateTime.Now + TimeSpan.FromSeconds(15 * 60);

            if (!PublicConfig.Config.GambleVictims.Contains(ctx.User.Id))
            {
                last = ctx.User.Id;
            }

            if (random == count - 1)
            {
                // You loose
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("L + Bozo"));
                await ctx.Member.TimeoutAsync(timeDuration);
            }
            else
            {
                ulong userId = PublicConfig.Config.GambleVictims[random];
                timeDuration = DateTime.Now + TimeSpan.FromSeconds(8 * 60);

                if (PublicConfig.Config.GambleVictims.Contains(ctx.User.Id))
                {
                    userId = last;
                    timeDuration = DateTime.Now + TimeSpan.FromSeconds(15 * 60);
                }

                // You Win
                if (ctx.Guild.Members.ContainsKey(userId))
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Wahahaha"));
                    await ctx.Guild.Members[userId].TimeoutAsync(timeDuration);
                }
                else
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Nothing happened?"));
                }
            }

        }
    }
}
