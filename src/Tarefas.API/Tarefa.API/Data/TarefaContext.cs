using GerenciadorDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.API.Data
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) 
            : base(options)
        {
      
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
