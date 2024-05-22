namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class DocumentosDTO
    {
        public class DocumentoDTO
{
    public int ID { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public string? PDF { get; set; }
    public string? Base64 { get; set; }
        }


    }
}