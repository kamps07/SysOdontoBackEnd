namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class EvolucaoDTO
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public List<string> Tratamento { get; set; }
        public DateTime DataEvolucao { get; set; }
        public int Paciente { get; set; }
        public string? Status { get; set; }

    }
}
