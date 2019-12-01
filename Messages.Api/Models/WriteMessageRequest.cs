using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Messages.Api.Models
{
    public class WriteMessageRequest
    {
        [Required, MaxLength(512), JsonProperty("message")]
        public string Message { get; set; }
    }
}
