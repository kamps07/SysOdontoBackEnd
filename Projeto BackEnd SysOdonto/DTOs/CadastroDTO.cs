namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class CadastroDTO
    {
        public int? ID { get; set; }
        public string? Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Funcao { get; set; }
    }
}
