using System;

namespace Messages.Api.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool IsPalindrome { get; set; }
    }
}
