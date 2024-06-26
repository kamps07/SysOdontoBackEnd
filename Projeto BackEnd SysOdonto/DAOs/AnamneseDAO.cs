using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AnamneseDAO
    {
        public void CadastrarResposta(RespostaAnamneseDTO anamnese)
        {
            int novoIdAnamnese = CadastrarAnamnese(anamnese);

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                foreach (var resposta in anamnese.Respostas)
                {
                    var query = @"INSERT INTO RespostaAnamnese (Resposta, Pergunta, Anamnese) 
                          VALUES (@resposta, @pergunta, @anamnese)";

                    var comando = new MySqlCommand(query, conexao);
                    
                    comando.Parameters.AddWithValue("@resposta", resposta.Resposta);
                    comando.Parameters.AddWithValue("@pergunta", resposta.Pergunta);
                    comando.Parameters.AddWithValue("@anamnese", novoIdAnamnese);

                    comando.ExecuteNonQuery();
                }
            }
        }



        public int CadastrarAnamnese(RespostaAnamneseDTO anamnese)
        {
            int novoId = 0;

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"INSERT INTO Anamnese (Paciente, dataRealizada) VALUES (@idPaciente, @dataRealizada);
              SELECT LAST_INSERT_ID();";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@idPaciente", anamnese.Paciente);
                    comando.Parameters.AddWithValue("@dataRealizada", DateTime.Now);

                    novoId = Convert.ToInt32(comando.ExecuteScalar());
                }
                conexao.Close();
            }

            return novoId;
        }



        public void CadastrarPergunta(RespostaDTO anamnese)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO pergunta (Valor) VALUES
						(@valor)";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@valor", anamnese.Pergunta);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<RespostaDTO> ListarRespostas(int? id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = @"
        SELECT 
            Pergunta.valor,
            RespostaAnamnese.Resposta 
        FROM 
            RespostaAnamnese 
            INNER JOIN Pergunta ON RespostaAnamnese.pergunta = Pergunta.ID
        WHERE 
            RespostaAnamnese.anamnese = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var dataReader = comando.ExecuteReader();
            var respostas = new List<RespostaDTO>();

            while (dataReader.Read())
            {
                var resposta = new RespostaDTO();
                resposta.Resposta = dataReader["Resposta"].ToString();
                resposta.Pergunta = dataReader["valor"].ToString();
                respostas.Add(resposta);
            }

            conexao.Close();

            return respostas;
        }

        public List<RespostaAnamneseDTO> ListarAnamneses(int? pacienteId)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = @"
                SELECT 
                    Anamnese.ID AS ID_Anamnese,
                    Anamnese.dataRealizada,
                    RespostaAnamnese.ID AS ID_RespostaAnamnese,
                    RespostaAnamnese.Pergunta,
                    Pergunta.valor,
	                RespostaAnamnese.Resposta
  
                FROM 
                    RespostaAnamnese
                    INNER JOIN Anamnese ON RespostaAnamnese.anamnese = Anamnese.ID
                    INNER JOIN Pergunta ON RespostaAnamnese.Pergunta = Pergunta.ID
                WHERE 
                    Anamnese.paciente = @paciente
                ORDER BY 
                    Anamnese.dataRealizada DESC;
";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@paciente", pacienteId);

            var dataReader = comando.ExecuteReader();
            var anamneses = new List<RespostaAnamneseDTO>();

            while (dataReader.Read())
            {
                int anamneseId = Convert.ToInt32(dataReader["ID_Anamnese"]);
                var anamnese = anamneses.FirstOrDefault(a => a.Anamnese == anamneseId);

                if (anamnese == null)
                {
                    anamnese = new RespostaAnamneseDTO
                    {
                        ID = Convert.ToInt32(dataReader["ID_RespostaAnamnese"]),
                        DataRealizada = Convert.ToDateTime(dataReader["dataRealizada"]),
                        Anamnese = anamneseId,
                        Respostas = new List<RespostaDTO>()
                    };
                    anamneses.Add(anamnese);
                }

                var resposta = new RespostaDTO
                {
                    Resposta = dataReader["Resposta"].ToString(),
                    Pergunta = dataReader["Valor"].ToString()
                };

                anamnese.Respostas.Add(resposta);
            }

            conexao.Close();

            return anamneses;
        }


        public List<PerguntaAnamneseDTO> ListarPerguntas()
        {
            var perguntas = new List<PerguntaAnamneseDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Pergunta";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var pergunta = new PerguntaAnamneseDTO
                            {
                                ID = Convert.ToInt32(dataReader["id"]),
                                Valor = dataReader["valor"].ToString()
                            };
                            perguntas.Add(pergunta);
                        }
                    }
                }
            }

            return perguntas;
        }


    }

}
