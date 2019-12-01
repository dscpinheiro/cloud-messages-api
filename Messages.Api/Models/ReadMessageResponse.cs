using Newtonsoft.Json;
using System;

namespace Messages.Api.Models
{
    public class ReadMessageResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("isPalindrome")]
        public bool IsPalindrome { get; set; }
    }
}
