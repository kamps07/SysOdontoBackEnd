namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class OdontogramaMultiploDTO
    {
        public int ID { get; set; }
        public int Paciente { get; set; }
        public List<int> Dentes { get; set; }
        public string? Tratamento { get; set; }
        public string? Posicao { get => "all"; }
        public string? Descricao { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
