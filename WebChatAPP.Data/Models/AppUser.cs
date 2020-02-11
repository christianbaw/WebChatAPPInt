using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebChatAPP.Data.Models
{
    public class AppUser : IdentityUser
    {

        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        //relate user to messages
        public virtual ICollection<Message> Messages { get; set; }

        
    }
}
