using Org.BouncyCastle.Ocsp;
using System;
using System.Security.Cryptography.Xml;

namespace Projeto_BackEnd_SysOdonto.DTOs
{
    
        public class RespostaAnamneseDTO
        {
            public int?ID { get; set; }
            public DateTime? DataRealizada { get; set; }
            public List<RespostaDTO> Respostas { get; set; }
            public int? Anamnese { get; set; }
            public int? Paciente { get; set; }
        }

        public class RespostaDTO
        {
            public string? Resposta { get; set; }
            public string? Pergunta { get; set; }
        }

}
