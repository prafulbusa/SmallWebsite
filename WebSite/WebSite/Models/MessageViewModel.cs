using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string MessageBody { get; set; }
    }
}