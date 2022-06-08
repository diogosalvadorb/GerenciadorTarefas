using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarefa.API.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
