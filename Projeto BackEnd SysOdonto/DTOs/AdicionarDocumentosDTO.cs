namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class AdicionarDocumentosDTO
    {
        public int Paciente { get; set; }
        public string Descricao { get; set; }
        public List<AdicionarDocumentoDTO> Documentos { get; set; }
    }

    public class AdicionarDocumentoDTO
    {
        public string Base64 { get; set; }
        public string Nome { get; set; }
        public string? Titulo { get => $"{this.Nome}.{this.Extensao}"; }
        public string Extensao { get; set; }
        public string? Link { get; set; }
    }
}
