using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Domain
{
    public partial class Message
    {
        public Message()
        {
        }

        public int MessageId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string MessageBody { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

   }
}
