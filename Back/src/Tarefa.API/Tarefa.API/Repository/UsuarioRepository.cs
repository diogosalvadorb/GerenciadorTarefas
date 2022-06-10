using Tarefa.API.Data;
using Tarefa.API.Models;
using Tarefa.API.Repository.Contrato;

namespace Tarefa.API.Repository
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
            throw new System.NotImplementedException();
        }

        public Usuario GetById(int idUsuario)
        {
            throw new System.NotImplementedException();
        }

        public Usuario GetUsuarioByLoginSenha(string login, string senha)
        {
            throw new System.NotImplementedException();
        }

        public void Salvar(Usuario usario)
        {
            _contexto.Usuarios.Add(usario);
            _contexto.SaveChanges();
        }
    }
}
