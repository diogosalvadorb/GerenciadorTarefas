using GerenciadorDeTarefas.API.Enums;
using GerenciadorDeTarefas.API.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.API.Repository.Contrato
{
    public interface ITarefaRepository
    {
        public void AdicionarTarefa(Tarefa tarefa);
        Tarefa GetById(int idTarefa);
        void RemoverTarefa(Tarefa tarefa);
        void AtualizarTarefa(Tarefa tarefa);
        List<Tarefa> BuscarTarefas(int idUsuario, DateTime? periodoDe, DateTime? periodoAte, StatusTarefaEnum status);
    }
}
