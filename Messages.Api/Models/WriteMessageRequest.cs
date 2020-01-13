using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Messages.Api.Models
{
    public class WriteMessageRequest
    {
        [Required, MaxLength(512), JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
