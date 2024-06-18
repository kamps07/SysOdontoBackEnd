using System;

namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class AnamneseDTO
    {
        public int ID { get; set; }
        public int PacienteId { get; set; }
        public DateTime Data { get; set; }
        public string Resposta { get; set; }
        public string Pergunta { get; set; }
    }
}
