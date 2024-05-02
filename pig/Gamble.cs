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

        static Dictionary<ulong, List<DateTime>> idiots = new Dictionary<ulong, List<DateTime>>();

        static ulong last;

        [SlashCommand("gamble", "Timeout yourself or someone annoying")]
        public async Task GambleCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            if (!PublicConfig.ArgumentStatus)
            {
                if (!idiots.ContainsKey(ctx.Member.Id))
                {
                    idiots.Add(ctx.Member.Id, new List<DateTime>());
                }

                idiots[ctx.Member.Id].Add(DateTime.Now);
                idiots[ctx.Member.Id].RemoveAll(x => DateTime.Now.Subtract(x).Hours > 1);

                if (idiots[ctx.Member.Id].Count >= 5)
                {
                    await ctx.Member.TimeoutAsync(DateTime.Now + TimeSpan.FromHours(2));
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Fine  have it your way fucko"));
                }
                else
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Please gamble responsibly, imagine what you could be buying instead"));
                }

                return;
            }

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
                timeDuration = DateTime.Now + TimeSpan.FromSeconds(10 * 60);

                if (PublicConfig.Config.GambleVictims.Contains(ctx.User.Id))
                {
                    userId = last;
                    timeDuration = DateTime.Now + TimeSpan.FromSeconds(15 * 60);
                }

                // You Win
                if (ctx.Guild.Members.ContainsKey(userId))
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Average Hero Main"));
                    await ctx.Guild.Members[userId].TimeoutAsync(timeDuration);
                }
                else
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Oops"));
                }
            }

        }
    }
}
