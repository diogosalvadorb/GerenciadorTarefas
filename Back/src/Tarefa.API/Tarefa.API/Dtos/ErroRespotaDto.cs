using System.Collections.Generic;

namespace Tarefa.API.Dtos
{
    public class ErroRespotaDto
    {
        public int Status { get; set; }
        public string Erro { get; set; }
        public List<string> Erros { get; set; }
    }
}
