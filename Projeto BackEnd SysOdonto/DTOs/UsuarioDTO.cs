namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class UsuarioDTO
    {
        public int? ID { get; set; }
        public string? Nome { get; set; }
        public string Senha { get; set; }
        public string? Funcao { get; set; }
        public string Email { get; set; }
        public ClinicaDTO? Clinica { get; set; }
    }
}
