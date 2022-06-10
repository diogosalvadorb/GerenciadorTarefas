using Tarefa.API.Models;

namespace Tarefa.API.Repository.Contrato
{
    public interface IUsuarioRepository
    {
        public void Salvar(Usuario usario);
        bool ExisteUsuarioPeloEmail(string email);
        Usuario GetUsuarioByLoginSenha(string login, string senha);
        Usuario GetById(int idUsuario);
    }
}
