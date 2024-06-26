namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class OdontogramaUnitarioDTO
    {
        public int ID { get; set; }
        public int Dente { get; set; }
        public int Paciente { get; set; }
        public string Tratamento { get; set; }
        public string Posicao { get; set; }
        public string Descricao { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
