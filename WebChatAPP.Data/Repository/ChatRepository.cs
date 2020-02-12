using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatAPP.Data.Interfaces;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ChatRepository(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> currentUser()
        {
            return await Task.Run(() => _httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public List<Message> getMessages()
        {
            return _context.Messages.Take(50).OrderBy(o => o.MessageDate).ToList();
        }

        public async Task<string> getUserID()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var userId = user.Id;

            return userId;
        }

        public void insertrecord(Message message)
        {
         _context.Messages.Add(message);
            _context.SaveChanges();
        }

        

    }
}
