using System.Text.Json.Serialization;
using System;

namespace Messages.Api.Models
{
    public class ReadMessageResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("isPalindrome")]
        public bool IsPalindrome { get; set; }
    }
}
