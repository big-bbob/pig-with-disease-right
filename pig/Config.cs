using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pig
{
    public class ResponseData
    {
        [JsonPropertyName("prompts")]
        public List<string> Prompts { get; set; }

        [JsonPropertyName("reponses")]
        public List<string> Responses { get; set; }
    };

    public class BotConfig
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("cool_servers")]
        public List<ulong> CoolServers { get; set; }

        [JsonPropertyName("based_list")]
        public List<ulong> BasedList { get; set; }

        [JsonPropertyName("cringe_list")]
        public List<ulong> CringeList { get; set; }

        [JsonPropertyName("owner")]
        public ulong Owner { get; set; }

        [JsonPropertyName("responses")]
        public List<ResponseData> Responses { get; set; }

        [JsonPropertyName("chance_responses")]
        public List<string> ChanceResponses { get; set; }

        [JsonPropertyName("gamble_victims")]
        public List<ulong> GambleVictims { get; set; }
    }

    public class PublicConfig
    {
        public static BotConfig Config = new BotConfig();
    };
}
