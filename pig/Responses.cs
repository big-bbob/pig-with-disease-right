using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pig
{
    internal static class Responses
    {
        public static DiscordClient discordClient;
        
        static Random random = new Random();

        static DateTime shutup = DateTime.Now.AddHours(-1);

        public static async Task CheckResponses(DiscordClient client, MessageCreateEventArgs eventArgs)
        {
            string[] words = (eventArgs.Message.Content.ToLower().Replace('?', ' ').Replace('.', ' ').Replace(',', ' ').Replace('!', ' ')).Split(' ');

            if ((words.Contains("shut") || words.Contains("quite")) && (words.Contains("please") || words.Contains("pls")))
            {
                if (eventArgs.MentionedUsers.Contains(discordClient.CurrentUser))
                {
                    var msg = new DiscordMessageBuilder() { Content = "Don't feel like it" };
                    await discordClient.SendMessageAsync(eventArgs.Channel, msg);
                    //shutup = DateTime.Now;
                    return;
                }
            }

            // Chance
            if (random.NextDouble() <= 0.00125f)
            {
                var msg = new DiscordMessageBuilder() { Content = PublicConfig.Config.ChanceResponses[random.Next(0, PublicConfig.Config.ChanceResponses.Count)]};
                await discordClient.SendMessageAsync(eventArgs.Channel, msg);
                return;
            }

            if (random.NextDouble() <= 0.01)
            {
                string content = eventArgs.Message.Content;
                string result = "\"";

                for (int i = 0; i < content.Length; i++)
                {
                    string thingo = content[i].ToString().ToLower();
                    if (random.Next(0, 2) == 0)
                    {
                        thingo = thingo.ToUpper();
                    }

                    result += thingo;
                }
                
                result += "\"";

                var msg = new DiscordMessageBuilder() { Content = result };
                await discordClient.SendMessageAsync(eventArgs.Channel, msg);
                return;
            }

            // Word Specific
            if (DateTime.Now.Subtract(shutup).TotalMinutes >= 10)
            {
                foreach (var response in PublicConfig.Config.Responses)
                {
                    foreach (var prompt in response.Prompts)
                    {
                        if (words.Contains(prompt))
                        {
                            var msg = new DiscordMessageBuilder() { Content = response.Responses[random.Next(0, response.Responses.Count)] };
                            await discordClient.SendMessageAsync(eventArgs.Channel, msg);
                            return;
                        }
                    }
                }
            }
        }
    }
}
