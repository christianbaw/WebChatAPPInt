using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChatAPP.Data;
using WebChatAPP.Data.Interfaces;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Hubs
{
    public class Helper
    {
        public readonly ApplicationDbContext _context;
        public Helper(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public Helper(Message message)
        {

            try { 
                _context.Messages.Add(message);
                _context.SaveChanges();
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
