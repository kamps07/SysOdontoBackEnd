using Microsoft.AspNetCore.Mvc;
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
                        Profissao, Logradouro, Numero, Complemento, CEP,
                        Bairro, Cidade, Estado, NomeResponsavel, NumeroResponsavel,
                        DocumentoResponsavel, GrauDeParentesco
                      ) VALUES (
                        @nome, @dataNascimento, @genero, @rg, @cpf, @email, @telefone,
                        @profissao, @logradouro, @numero, @complemento, @cep,
                        @bairro, @cidade, @estado, @nomeResponsavel, @numeroResponsavel,
                        @documentoResponsavel, @grauDeParentesco
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
            comando.Parameters.AddWithValue("@logradouro", paciente.Logradouro);
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
            //comando.Parameters.AddWithValue("@prontuario", paciente.Prontuario);

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
            var conexao = ConnectionFactory.Build();

            conexao.Open();
            var query = "SELECT * FROM Paciente WHERE CPF = @cpf";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@cpf", CPF);

            var dataReader = comando.ExecuteReader();

            if(dataReader.Read())
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
                Logradouro = dataReader["Logradouro"].ToString(),
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
                //Prontuario = int.Parse(dataReader["Prontuario"].ToString())
            };

            conexao.Close();
            return paciente;

        }


        //Modificar função de alteração
        //Retornar valores  salvo no banco de dados 
        //Verificar campos que sofreram alteração  
        //Executar comando UDPATE de alteração apenas no campos que foram alterados 

        


        public void AlterarPaciente(PacienteDTO paciente)
        {
            var conexao = ConnectionFactory.Build();
            
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
                Logradouro = @logradouro,
                Numero = @numero,
                Complemento = @complemento,
                CEP = @cep,
                Bairro = @bairro,
                Cidade = @cidade,
                Estado = @estado,
                NomeResponsavel = @nomeResponsavel,
                NumeroResponsavel = @numeroResponsavel,
                DocumentoResponsavel = @documentoResponsavel,
                GrauDeParentesco = @grauDeParentesco
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
                comando.Parameters.AddWithValue("@logradouro", paciente.Logradouro);
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
                //comando.Parameters.AddWithValue("@prontuario", paciente.Prontuario);

                comando.ExecuteNonQuery();
                conexao.Close();


        }

        //public void RemoverProfessor(int id)
        //{
        //    var conexao = ConnectionFactory.Build();
        //    conexao.Open();

        //    var query = @"DELETE FROM Professores WHERE ID = @id";

        //    var comando = new MySqlCommand(query, conexao);
        //    comando.Parameters.AddWithValue("@id", id);

        //    comando.ExecuteNonQuery();
        //    conexao.Close();
        //}


        public void RemoverPaciente(string CPF)
        {
            var conexao = ConnectionFactory.Build();

            conexao.Open();
            var query = "DELETE FROM Paciente WHERE CPF = @cpf";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@cpf", CPF);

            var dataReader = comando.ExecuteReader();

            conexao.Close();

        }




    }


}


