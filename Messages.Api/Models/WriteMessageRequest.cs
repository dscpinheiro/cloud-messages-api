using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Messages.Api.Models
{
    public class WriteMessageRequest
    {
        [Required, MaxLength(512), JsonProperty("message")]
        public string Message { get; set; }
    }
}