using Microsoft.EntityFrameworkCore;
using Tarefa.API.Models;

namespace Tarefa.API.Data
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) 
            : base(options)
        {
      
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
