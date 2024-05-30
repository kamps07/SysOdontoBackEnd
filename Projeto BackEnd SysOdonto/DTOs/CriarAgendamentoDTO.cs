namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class CriarAgendamentoDTO
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string Horario { get; set; }
        public int Dentista { get; set; }
        public int Paciente { get; set; }
        public int Servico { get; set; }
        public string Observacoes { get; set; }
    }
}
