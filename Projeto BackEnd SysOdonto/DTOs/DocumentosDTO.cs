namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class DocumentosDTO
    {
        public class DocumentoDTO
        {
            public int Id { get; set; }
            public int Paciente { get; set; }
            public string Titulo { get; set; }
            public string Descricao { get; set; }
            public string? Link { get; set; }
            public DateTime? DataUpload { get; set; }
        }
    }
}