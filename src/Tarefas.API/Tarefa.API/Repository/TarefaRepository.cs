using GerenciadorDeTarefas.API.Data;
using GerenciadorDeTarefas.API.Enums;
using GerenciadorDeTarefas.API.Models;
using GerenciadorDeTarefas.API.Repository.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorDeTarefas.API.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaContext _contexto;

        public TarefaRepository(TarefaContext contexto)
        {
            _contexto = contexto;
        }
        public void AdicionarTarefa(Tarefa tarefa)
        {
            _contexto.Tarefas.Add(tarefa);
            _contexto.SaveChanges();
        }

        public void AtualizarTarefa(Tarefa tarefa)
        {
            _contexto.Entry(tarefa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            _contexto.Entry(tarefa).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public List<Tarefa> BuscarTarefas(int idUsuario, DateTime? periodoDe, DateTime? periodoAte, StatusTarefaEnum status)
        {
            return _contexto.Tarefas.Where(tarefa => tarefa.IdUsuario == idUsuario
                             && (periodoDe == null || periodoDe == DateTime.MinValue || tarefa.DataPrevistaConclusao >= ((DateTime)periodoDe).Date)
                             && (periodoAte == null || periodoAte == DateTime.MinValue || tarefa.DataPrevistaConclusao <= ((DateTime)periodoAte).Date)
                             && (status == StatusTarefaEnum.Todos || (status == StatusTarefaEnum.Ativos && tarefa.DataConclusao == null)
                                        || (status == StatusTarefaEnum.Concluidos && tarefa.DataConclusao != null)))
                        .ToList();
        }

        public Tarefa GetById(int idTarefa)
        {
            return _contexto.Tarefas.FirstOrDefault(tarefa => tarefa.Id == idTarefa);
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            _contexto.Tarefas.Remove(tarefa);
            _contexto.SaveChanges();
        }
    }
}
