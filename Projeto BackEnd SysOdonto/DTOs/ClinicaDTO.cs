namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class ClinicaDTO
    {
        public int? ID { get; set; }
        public string Nome { get; set; }

        public string Endereco { get; set;}

        public int Telefone { get; set; }

        public string Base64 { get; set; }

        public string? ImgURL { get; set; }

        public AdministradorDTO? Administrador { get; set;}
    }
}
