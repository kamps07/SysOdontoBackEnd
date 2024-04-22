using MySql.Data.MySqlClient;
using Mysqlx;
using Projeto_BackEnd_SysOdonto.DTOs;
using System.Runtime.ConstrainedExecution;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class PacienteDAO
    {
        public void CadastrarPaciente(PacienteDTO paciente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Paciente (
                        Nome, DataNascimento, Genero, RG, CPF, Email, Telefone,
                        Profissao, Longadouro, Numero, Complemento, CEP,
                        Bairro, Cidade, Estado, NomeResponsavel, NumeroResponsavel,
                        DocumentoResponsavel, GrauDeParentesco, Prontuario
                      ) VALUES (
                        @nome, @dataNascimento, @genero, @rg, @cpf, @email, @telefone,
                        @profissao, @longadouro, @numero, @complemento, @cep,
                        @bairro, @cidade, @estado, @nomeResponsavel, @numeroResponsavel,
                        @documentoResponsavel, @grauDeParentesco, @prontuario
                      )";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", paciente.Nome);
            comando.Parameters.AddWithValue("@dataNascimento", paciente.DataNascimento);
            comando.Parameters.AddWithValue("@genero", paciente.Genero);
            comando.Parameters.AddWithValue("@rg", paciente.RG);
            comando.Parameters.AddWithValue("@cpf", paciente.CPF);
            comando.Parameters.AddWithValue("@email", paciente.Email);
            comando.Parameters.AddWithValue("@telefone", paciente.Telefone);
            comando.Parameters.AddWithValue("@profissao", paciente.Profissao);
            comando.Parameters.AddWithValue("@longadouro", paciente.Longadouro);
            comando.Parameters.AddWithValue("@numero", paciente.Numero);
            comando.Parameters.AddWithValue("@complemento", paciente.Complemento);
            comando.Parameters.AddWithValue("@cep", paciente.CEP);
            comando.Parameters.AddWithValue("@bairro", paciente.Bairro);
            comando.Parameters.AddWithValue("@cidade", paciente.Cidade);
            comando.Parameters.AddWithValue("@estado", paciente.Estado);
            comando.Parameters.AddWithValue("@nomeResponsavel", paciente.NomeResponsavel);
            comando.Parameters.AddWithValue("@numeroResponsavel", paciente.NumeroResponsavel);
            comando.Parameters.AddWithValue("@documentoResponsavel", paciente.DocumentoResponsavel);
            comando.Parameters.AddWithValue("@grauDeParentesco", paciente.GrauDeParentesco);
            comando.Parameters.AddWithValue("@prontuario", paciente.Prontuario);

            comando.ExecuteNonQuery();
            conexao.Close();
        }


        public bool VerificarPaciente(PacienteDTO paciente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Paciente WHERE CPF = @cpf";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@cpf", paciente.CPF);

            var dataReader = comando.ExecuteReader();


            bool pacienteEncontrado = false;


            if (dataReader.Read())
            {

                pacienteEncontrado = true;
            }
            conexao.Close();


            return pacienteEncontrado;
        }


        public PacienteDTO ListarPaciente(string CPF)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Paciente WHERE CPF = @cpf";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@cpf", CPF);

                    using (var dataReader = comando.ExecuteReader())
                    {
                        if (!dataReader.Read())
                        {

                            return null;
                        }

                        var paciente = new PacienteDTO
                        {
                            Nome = dataReader["Nome"].ToString(),
                            DataNascimento = (DateTime)dataReader["DataNascimento"],
                            Genero = dataReader["Genero"].ToString(),
                            RG = dataReader["RG"].ToString(),
                            CPF = dataReader["CPF"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Telefone = dataReader["Telefone"].ToString(),
                            Profissao = dataReader["Profissao"].ToString(),
                            Longadouro = dataReader["Longadouro"].ToString(),
                            Numero = dataReader["Numero"].ToString(),
                            Complemento = dataReader["Complemento"].ToString(),
                            CEP = dataReader["CEP"].ToString(),
                            Bairro = dataReader["Bairro"].ToString(),
                            Cidade = dataReader["Cidade"].ToString(),
                            Estado = dataReader["Estado"].ToString(),
                            NomeResponsavel = dataReader["NomeResponsavel"].ToString(),
                            NumeroResponsavel = dataReader["NumeroResponsavel"].ToString(),
                            DocumentoResponsavel = dataReader["DocumentoResponsavel"].ToString(),
                            GrauDeParentesco = dataReader["GrauDeParentesco"].ToString(),
                            Prontuario = int.Parse(dataReader["Prontuario"].ToString())
                        };

                        return paciente;

                    }

                }

            }
        }

        public void AlterarPaciente(PacienteDTO paciente)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"
            UPDATE Paciente 
            SET 
                Nome = @nome,
                DataNascimento = @dataNascimento,
                Genero = @genero,
                RG = @rg,
                Email = @email,
                Telefone = @telefone,
                Profissao = @profissao,
                Longadouro = @longadouro,
                Numero = @numero,
                Complemento = @complemento,
                CEP = @cep,
                Bairro = @bairro,
                Cidade = @cidade,
                Estado = @estado,
                NomeResponsavel = @nomeResponsavel,
                NumeroResponsavel = @numeroResponsavel,
                DocumentoResponsavel = @documentoResponsavel,
                GrauDeParentesco = @grauDeParentesco,
                Prontuario = @prontuario
            WHERE CPF = @cpf";

                var comando = new MySqlCommand(query, conexao);

                comando.Parameters.AddWithValue("@nome", paciente.Nome);
                comando.Parameters.AddWithValue("@dataNascimento", paciente.DataNascimento);
                comando.Parameters.AddWithValue("@genero", paciente.Genero);
                comando.Parameters.AddWithValue("@rg", paciente.RG);
                comando.Parameters.AddWithValue("@cpf", paciente.CPF);
                comando.Parameters.AddWithValue("@email", paciente.Email);
                comando.Parameters.AddWithValue("@telefone", paciente.Telefone);
                comando.Parameters.AddWithValue("@profissao", paciente.Profissao);
                comando.Parameters.AddWithValue("@longadouro", paciente.Longadouro);
                comando.Parameters.AddWithValue("@numero", paciente.Numero);
                comando.Parameters.AddWithValue("@complemento", paciente.Complemento);
                comando.Parameters.AddWithValue("@cep", paciente.CEP);
                comando.Parameters.AddWithValue("@bairro", paciente.Bairro);
                comando.Parameters.AddWithValue("@cidade", paciente.Cidade);
                comando.Parameters.AddWithValue("@estado", paciente.Estado);
                comando.Parameters.AddWithValue("@nomeResponsavel", paciente.NomeResponsavel);
                comando.Parameters.AddWithValue("@numeroResponsavel", paciente.NumeroResponsavel);
                comando.Parameters.AddWithValue("@documentoResponsavel", paciente.DocumentoResponsavel);
                comando.Parameters.AddWithValue("@grauDeParentesco", paciente.GrauDeParentesco);
                comando.Parameters.AddWithValue("@prontuario", paciente.Prontuario);

                comando.ExecuteNonQuery(); // Executar o comando SQL
            }
        }




    }
}

     