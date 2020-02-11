using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebChatAPP.Data.Interfaces;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IChatRepository _repository;
        
        public MessageController(IChatRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(MessageVM messageVM)
        {
            var message = new Message
            {
                UserID = await _repository.getUserID(),
                UserName = User.Identity.Name,
                Text = messageVM.Text
            };

            _repository.insertrecord(message);
            return Ok(new { message.UserName, message.Text, message.MessageDate });
        }
    }
}