namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class RespostaAnamneseDTO
    {
        public int ID { get; set; }
        public DateTime DataRealizada { get; set; }
        public string Resposta { get; set; }
        public string Pergunta { get; set; }
        public int Anamnese { get; set; }
    }
}
