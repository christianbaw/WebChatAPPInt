using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Data.Interfaces
{
    public interface IChatRepository
    {
        Task<string> currentUser();

        List<Message> getMessages();

        Task<string> getUserID();

        void insertrecord(Message message);
    }
}
