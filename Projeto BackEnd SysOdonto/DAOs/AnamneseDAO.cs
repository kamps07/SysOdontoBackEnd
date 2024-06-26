using MySql.Data.MySqlClient;
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
                    var query = @"INSERT INTO RespostaAnamnese (DataRealizada, Resposta, Pergunta, Anamnese) 
                          VALUES (@dataRealizada, @resposta, @pergunta, @anamnese)";

                    var comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@dataRealizada", DateTime.Now);
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

                var query = @"INSERT INTO Anamnese (Paciente) VALUES (@idPaciente);
              SELECT LAST_INSERT_ID();";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@idPaciente", anamnese.Paciente);

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
                resposta.Pergunta= dataReader["Valor"].ToString();

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
        RespostaAnamnese.ID AS ID_RespostaAnamnese, 
        RespostaAnamnese.DataRealizada, 
        RespostaAnamnese.Resposta, 
        RespostaAnamnese.Pergunta, 
        Anamnese.ID AS ID_Anamnese
    FROM 
        RespostaAnamnese 
        INNER JOIN Anamnese ON RespostaAnamnese.anamnese = Anamnese.ID
    WHERE 
        Anamnese.paciente = @paciente
        
        ORDER BY 
    RespostaAnamnese.DataRealizada DESC;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@paciente", pacienteId);

            var dataReader = comando.ExecuteReader();
            var anamneses = new List<RespostaAnamneseDTO>();

            while (dataReader.Read())
            {
                var anamnese = new RespostaAnamneseDTO();
                var respostas = new List<RespostaDTO>();

                anamnese.ID = Convert.ToInt32(dataReader["ID_RespostaAnamnese"]);
                anamnese.DataRealizada = Convert.ToDateTime(dataReader["DataRealizada"]);

                // Verifica se o valor de ID_Anamnese não é DBNull (representa um valor nulo do banco de dados)
                if (dataReader["ID_Anamnese"] != DBNull.Value)
                {
                    anamnese.Anamnese = Convert.ToInt32(dataReader["ID_Anamnese"]);
                    respostas = ListarRespostas(anamnese.Anamnese);
                }
                else
                {
                    // Lidar com a situação em que ID_Anamnese é nulo, se necessário
                    // Aqui você pode atribuir um valor padrão ou decidir o que fazer com anamnese.Anamnese
                    anamnese.Anamnese = 0; // Exemplo: atribuir 0 como valor padrão
                }

                anamnese.Respostas = respostas;
                anamneses.Add(anamnese);
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
