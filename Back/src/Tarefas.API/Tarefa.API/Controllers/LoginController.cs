using GerenciadorDeTarefas.API.Repository.Contrato;
using GerenciadorDeTarefas.API.Services;
using GerenciadorDeTarefas.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using GerenciadorDeTarefas.API.Dtos;

namespace GerenciadorDeTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(ILogger<LoginController> logger,
                               IUsuarioRepository usuarioRepository )
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
        {
            try
            {
                if (requisicao == null
                    || string.IsNullOrEmpty(requisicao.Login) || string.IsNullOrWhiteSpace(requisicao.Login)
                    || string.IsNullOrEmpty(requisicao.Senha) || string.IsNullOrEmpty(requisicao.Senha))

                {
                    return BadRequest(new ErroRespotaDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Erro = "Parâmetros de entrada inválidos"
                    });
                }

                var usuario = _usuarioRepository.GetUsuarioByLoginSenha(requisicao.Login, MD5Utils.GerarHashMD5(requisicao.Senha));
                if (usuario == null)
                {
                    return BadRequest(new ErroRespotaDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Erro = "Usuário ou senha inválidos"
                    });
                }

                var token = TokenService.GerarToken(usuario);

                return Ok(new LoginRespostaDto() {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
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
