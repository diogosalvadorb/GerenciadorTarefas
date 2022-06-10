using GerenciadorDeTarefas.API.Models;
using GerenciadorDeTarefas.API.Repository.Contrato;
using System.Linq;
using GerenciadorDeTarefas.API.Data;

namespace GerenciadorDeTarefas.API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TarefaContext _contexto;
        public UsuarioRepository(TarefaContext contexto)
        {
            _contexto = contexto;
        }
        public bool ExisteUsuarioPeloEmail(string email)
        {
            return _contexto.Usuarios.Any(usuario => usuario.Email.ToLower() == email);
        }

        public Usuario GetById(int idUsuario)
        {
            return _contexto.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
        }

        public Usuario GetUsuarioByLoginSenha(string login, string senha)
        {
            return _contexto.Usuarios.FirstOrDefault(usuario => usuario.Email == login.ToLower() && usuario.Senha == senha);
        }

        public void Salvar(Usuario usario)
        {
            _contexto.Usuarios.Add(usario);
            _contexto.SaveChanges();
        }
    }
}
