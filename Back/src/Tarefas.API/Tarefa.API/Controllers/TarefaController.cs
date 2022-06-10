using GerenciadorDeTarefas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TarefaController : BaseController
    {
        private readonly ILogger<TarefaController> _logger; 
         
        public TarefaController(ILogger<TarefaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] Tarefa tarefa)
        {

        }
    }
}
