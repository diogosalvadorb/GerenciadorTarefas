using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tarefa.API.Dtos;
using Tarefa.API.Models;
using Tarefa.API.Repository.Contrato;
using Tarefa.API.Utils;

namespace Tarefa.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : BaseController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger,
                                 IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromBody]Usuario usuario)
        {
            try
            {
                var erros = new List<string>();
                if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome)
                    || usuario.Nome.Length < 2)
                {
                    erros.Add("Nome inválido");
                }
                                
                if(string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha)
                    || usuario.Senha.Length < 4)
                {
                    erros.Add("Senha inválida");
                }

                Regex regex = new Regex(@"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,4})+)$");
                if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Email)
                    || !regex.Match(usuario.Email).Success)
                {
                    erros.Add("Email inválido");
                }



                if (erros.Count > 0)
                {
                    return BadRequest(new ErroRespotaDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Erros = erros
                    });
                }

                usuario.Senha = MD5Utils.GerarHashMD5(usuario.Senha);
                _usuarioRepository.Salvar(usuario);

                return Ok(new {msg = "Usuário Criado com sucesso"});
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao salvar usuario. Erro: {ex.Message}");
            }
        }
    }
}
