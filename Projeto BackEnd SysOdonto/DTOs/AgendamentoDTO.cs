﻿namespace Projeto_BackEnd_SysOdonto.DTOs
{
    public class AgendamentoDTO
    {
        public int ID { get; set; }
        public DateOnly DataDaConsulta{ get; set; }
        public string Horario { get; set; }
        public DentistaDTO Dentista { get; set; }
        public PacienteDTO Paciente { get; set; }
        public ClinicaDTO Clinica { get; set; }
        public ServicoDTO Servico { get; set; }
        public string Observacoes { get; set; }
        public string Duracao { get; set; }
    }   
}
