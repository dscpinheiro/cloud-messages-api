using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Messages.Api.Models
{
    public class WriteMessageViewModel
    {
        [Required, MaxLength(512), JsonProperty("message")]
        public string Message { get; set; }
    }
}