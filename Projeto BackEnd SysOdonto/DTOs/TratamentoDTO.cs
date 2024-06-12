namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class TratamentoDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateOnly DataAdicao { get; set; }
        public PacienteDTO Paciente { get; set; }
    }
}
