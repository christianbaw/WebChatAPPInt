using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebChatAPP.Data.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime MessageDate { get; set; }

        public string UserID { get; set; }
        public virtual AppUser Sender { get; set; }

        public Message()
        {
            MessageDate = DateTime.Now;
        }
    }
}
