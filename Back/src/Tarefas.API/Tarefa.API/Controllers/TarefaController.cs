using GerenciadorDeTarefas.API.Dtos;
using GerenciadorDeTarefas.API.Models;
using GerenciadorDeTarefas.API.Repository.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorDeTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TarefaController : BaseController
    {
        private readonly ILogger<TarefaController> _logger;
        private readonly ITarefaRepository _tarefaRepository;
        public TarefaController(ILogger<TarefaController> logger, 
                                IUsuarioRepository usuarioRepository,
                                ITarefaRepository tarefaRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _tarefaRepository = tarefaRepository;
        }

        [HttpPost]
        public IActionResult AdicionarTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                var usuario = ReadToken();
                var erros = new List<string>();
                if (tarefa == null || usuario == null)
                {
                    erros.Add("Favor informar a tarefa ou usuário");
                }
                else
                {
                    if (string.IsNullOrEmpty(tarefa.Nome) || string.IsNullOrWhiteSpace(tarefa.Nome)
                            || tarefa.Nome.Count() < 4)
                    {
                        erros.Add("Favor informar um nome válido");
                    }

                    if (tarefa.DataPrevistaConclusao == DateTime.MinValue || tarefa.DataPrevistaConclusao.Date < DateTime.Now.Date)
                    {
                        erros.Add("Data de previsão não pode ser de data passada");
                    }
                }

                if (erros.Count > 0)
                {
                    return BadRequest(new ErroRespotaDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Erros = erros
                    });
                }
                
                
                tarefa.IdUsuario = usuario.Id;
                tarefa.DataConclusao = null;
                _tarefaRepository.AdicionarTarefa(tarefa);

                return Ok(new { msg = "Tarefa criada com sucesso" });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao salvar Tarefa. Erro: {ex.Message}");
            }
        }
    }
}
