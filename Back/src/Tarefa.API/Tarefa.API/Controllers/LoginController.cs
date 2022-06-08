using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tarefa.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
    }
}
