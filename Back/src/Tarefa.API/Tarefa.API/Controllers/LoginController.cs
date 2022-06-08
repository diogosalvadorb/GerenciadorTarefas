using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Tarefa.API.Dtos;
using Tarefa.API.Models;
using Tarefa.API.Services;

namespace Tarefa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;

        private readonly string LoginTeste = "adim@email.com";
        private readonly string SenhaTeste = "1234";
  
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
        {
            try
            {
                if (requisicao == null
                    || string.IsNullOrEmpty(requisicao.Login) || string.IsNullOrWhiteSpace(requisicao.Login)
                    || string.IsNullOrEmpty(requisicao.Senha) || string.IsNullOrEmpty(requisicao.Senha)
                    ||  requisicao.Login != LoginTeste || requisicao.Senha != SenhaTeste)

                {
                    return BadRequest(new ErroRespotaDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Erro = "Parâmetros de entrada inválidos"
                    });
                }

                var usuarioTeste = new Usuario()
                {
                    Id = 2,
                    Nome = "Usuario Teste",
                    Email = LoginTeste,
                    Senha = SenhaTeste
                };

                var token = TokenService.GerarToken(usuarioTeste);

                return Ok(new LoginRespostaDto() {
                    Email = usuarioTeste.Email,
                    Nome = usuarioTeste.Nome,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar fazer Login. Erro: {ex.Message}");
            }
        }
    }
}
