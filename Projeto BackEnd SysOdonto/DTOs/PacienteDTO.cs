namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class PacienteDTO

    {
            public int? ID { get; set; }
            public string Nome { get; set; } //
            public DateTime DataNascimento { get; set; }
            public string Genero { get; set; } //
            public string RG { get; set; } //
            public string CPF { get; set; } //
            public string Email { get; set; } //
            public string Telefone { get; set; }
            public string Profissao { get; set; }
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string CEP { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string? NomeResponsavel { get; set; }
            public string? NumeroResponsavel { get; set; }
            public string? DocumentoResponsavel { get; set; }
            public string? GrauDeParentesco { get; set; }
            public ClinicaDTO? Clinica { get; set; }
    }


    }