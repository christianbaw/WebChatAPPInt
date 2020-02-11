using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebChatAPP.Data.Interfaces;

namespace WebChatAPP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatRepository _repository;

        public HomeController(ILogger<HomeController> logger, IChatRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
      
        public async Task<IActionResult> Index()
        {
            var currentLoggedUser = await _repository.currentUser();
            ViewBag.CurrentUserName = currentLoggedUser;
            var getMessages = _repository.getMessages();
            return View(getMessages);
        }
    }
}
