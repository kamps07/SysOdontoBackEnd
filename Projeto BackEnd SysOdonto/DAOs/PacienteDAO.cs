using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx;
using Projeto_BackEnd_SysOdonto.DTOs;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

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
                        DocumentoResponsavel, GrauDeParentesco, Clinica
                      ) VALUES (
                        @nome, @dataNascimento, @genero, @rg, @cpf, @email, @telefone,
                        @profissao, @logradouro, @numero, @complemento, @cep,
                        @bairro, @cidade, @estado, @nomeResponsavel, @numeroResponsavel,
                        @documentoResponsavel, @grauDeParentesco, @clinica
                      )";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", paciente.Nome);
            comando.Parameters.AddWithValue("@dataNascimento", paciente.DataNascimento);
            comando.Parameters.AddWithValue("@genero", paciente.Genero);
            comando.Parameters.AddWithValue("@rg", paciente.RG);
            comando.Parameters.AddWithValue("@cpf", paciente.CPF);
            comando.Parameters.AddWithValue("@email", paciente.Email);
            comando.Parameters.AddWithValue("@telefone", paciente.Telefone.Replace(" ", ""));
            comando.Parameters.AddWithValue("@profissao", paciente.Profissao);
            comando.Parameters.AddWithValue("@logradouro", paciente.Logradouro);
            comando.Parameters.AddWithValue("@numero", paciente.Numero);
            comando.Parameters.AddWithValue("@complemento", paciente.Complemento);
            comando.Parameters.AddWithValue("@cep", paciente.CEP);
            comando.Parameters.AddWithValue("@bairro", paciente.Bairro);
            comando.Parameters.AddWithValue("@cidade", paciente.Cidade);
            comando.Parameters.AddWithValue("@estado", paciente.Estado);
            comando.Parameters.AddWithValue("@nomeResponsavel", paciente.NomeResponsavel);
            comando.Parameters.AddWithValue("@numeroResponsavel", paciente.NumeroResponsavel.Replace(" ", ""));
            comando.Parameters.AddWithValue("@documentoResponsavel", paciente.DocumentoResponsavel);
            comando.Parameters.AddWithValue("@grauDeParentesco", paciente.GrauDeParentesco);
            comando.Parameters.AddWithValue("@clinica", paciente.Clinica.ID);

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

        public List<PacienteDTO> ListarPacientes(int clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "SELECT*FROM Paciente WHERE Clinica = @clinica";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();

            var pacientes = new List<PacienteDTO>();

            while (dataReader.Read())
            {
                var paciente = new PacienteDTO();
                paciente.Clinica = new ClinicaDTO();

                paciente.ID = int.Parse(dataReader["ID"].ToString());
                paciente.Nome = dataReader["Nome"].ToString();
                paciente.DataNascimento = (DateTime)dataReader["DataNascimento"];
                paciente.Genero = dataReader["Genero"].ToString();
                paciente.RG = dataReader["RG"].ToString();
                paciente.CPF = dataReader["CPF"].ToString();
                paciente.Email = dataReader["Email"].ToString();
                paciente.Telefone = dataReader["Telefone"].ToString();
                paciente.Profissao = dataReader["Profissao"].ToString();
                paciente.Logradouro = dataReader["Logradouro"].ToString();
                paciente.Numero = dataReader["Numero"].ToString();
                paciente.Complemento = dataReader["Complemento"].ToString();
                paciente.CEP = dataReader["CEP"].ToString();
                paciente.Bairro = dataReader["Bairro"].ToString();
                paciente.Cidade = dataReader["Cidade"].ToString();
                paciente.Estado = dataReader["Estado"].ToString();
                paciente.NomeResponsavel = dataReader["NomeResponsavel"].ToString();
                paciente.NumeroResponsavel = dataReader["NumeroResponsavel"].ToString();
                paciente.DocumentoResponsavel = dataReader["DocumentoResponsavel"].ToString();
                paciente.GrauDeParentesco = dataReader["GrauDeParentesco"].ToString();
                paciente.Clinica.ID = int.Parse(dataReader["Clinica"].ToString());

                pacientes.Add(paciente);
            }

            conexao.Close();
            return pacientes;
        }


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
            WHERE CPF = @cpf AND clinica =@clinica";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@nome", paciente.Nome);
            comando.Parameters.AddWithValue("@dataNascimento", paciente.DataNascimento);
            comando.Parameters.AddWithValue("@genero", paciente.Genero);
            comando.Parameters.AddWithValue("@rg", paciente.RG);
            comando.Parameters.AddWithValue("@cpf", paciente.CPF);
            comando.Parameters.AddWithValue("@email", paciente.Email);
            comando.Parameters.AddWithValue("@telefone", paciente.Telefone.Replace(" ", ""));
            comando.Parameters.AddWithValue("@profissao", paciente.Profissao);
            comando.Parameters.AddWithValue("@logradouro", paciente.Logradouro);
            comando.Parameters.AddWithValue("@numero", paciente.Numero);
            comando.Parameters.AddWithValue("@complemento", paciente.Complemento);
            comando.Parameters.AddWithValue("@cep", paciente.CEP);
            comando.Parameters.AddWithValue("@bairro", paciente.Bairro);
            comando.Parameters.AddWithValue("@cidade", paciente.Cidade);
            comando.Parameters.AddWithValue("@estado", paciente.Estado);
            comando.Parameters.AddWithValue("@nomeResponsavel", paciente.NomeResponsavel);
            comando.Parameters.AddWithValue("@numeroResponsavel", paciente.NumeroResponsavel.Replace(" ", ""));
            comando.Parameters.AddWithValue("@documentoResponsavel", paciente.DocumentoResponsavel);
            comando.Parameters.AddWithValue("@grauDeParentesco", paciente.GrauDeParentesco);
            comando.Parameters.AddWithValue("@clinica", paciente.Clinica.ID);

            //comando.Parameters.AddWithValue("@prontuario", paciente.Prontuario);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        //public void RemoverPaciente(string CPF)
        //{
        //    var conexao = ConnectionFactory.Build();
        //    conexao.Open();
        //    var query = "DELETE FROM Paciente WHERE CPF = @cpf";

        //    var comando = new MySqlCommand(query, conexao);

        //    comando.Parameters.AddWithValue("@cpf", CPF);

        //    var dataReader = comando.ExecuteReader();

        //    conexao.Close();
        //}

        public bool EmailValido(string Email)
        {
            try
            {
                var mailAddress = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool CPFValido(string CPF)
        {
            // Tornar o CPF em maiúsculas
            CPF = CPF.ToUpper();

            // Remover caracteres não numéricos do CPF
            CPF = CPF.Replace(".", "").Replace("-", "");

            // Verificar se o CPF possui 11 dígitos
            if (CPF.Length != 11)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais (CPF inválido)
            if (CPF[0] == CPF[1] && CPF[1] == CPF[2] && CPF[2] == CPF[3] &&
                CPF[3] == CPF[4] && CPF[4] == CPF[5] && CPF[5] == CPF[6] &&
                CPF[6] == CPF[7] && CPF[7] == CPF[8] && CPF[8] == CPF[9] &&
                CPF[9] == CPF[10])
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(CPF[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verifica o primeiro dígito verificador
            if (int.Parse(CPF[9].ToString()) != digitoVerificador1)
            {
                return false;
            }

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(CPF[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verifica o segundo dígito verificador
            if (int.Parse(CPF[10].ToString()) != digitoVerificador2)
            {
                return false;
            }

            // CPF é válido
            return true;




        }


        public bool DocumentoResponsavelNecessario(DateTime dataNascimento, string documentoResponsavel)
        {
            // Calcula a idade com base na data de nascimento
            int idade = DateTime.Now.Year - dataNascimento.Year;

            // Ajusta a idade caso o aniversário ainda não tenha ocorrido no ano atual
            if (dataNascimento.Date > DateTime.Now.AddYears(-idade).Date)
            {
                idade--;
            }

            // Retorna true se a idade for menor que 18 anos e o documento do responsável for válido
            if (idade < 18)
            {
                return DocumentoResponsavelValido(documentoResponsavel);
            }

            // Retorna false se a idade for maior ou igual a 18 anos
            return true;
        }

        public bool DocumentoResponsavelValido(string documentoResponsavel)
        {
            // Tornar o CPF em maiúsculas
            documentoResponsavel = documentoResponsavel.ToUpper();

            // Remover caracteres não numéricos do CPF
            documentoResponsavel = documentoResponsavel.Replace(".", "").Replace("-", "");

            // Verificar se o CPF possui 11 dígitos
            if (documentoResponsavel.Length != 11)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais (CPF inválido)
            if (documentoResponsavel.Distinct().Count() == 1)
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(documentoResponsavel[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verifica o primeiro dígito verificador
            if (int.Parse(documentoResponsavel[9].ToString()) != digitoVerificador1)
            {
                return false;
            }

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(documentoResponsavel[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verifica o segundo dígito verificador
            if (int.Parse(documentoResponsavel[10].ToString()) != digitoVerificador2)
            {
                return false;
            }

            // CPF é válido
            return true;
        }




        public bool RGValido(string RG)
        {
            // Verificar se o RG possui 9 dígitos
            if (RG.Length != 9)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais (RG inválido)
            char firstDigit = RG[0];
            for (int i = 1; i < RG.Length; i++)
            {
                if (RG[i] != firstDigit)
                {
                    // Se encontrarmos um dígito diferente do primeiro, o RG é válido
                    return true;
                }
            }

            // Se todos os dígitos forem iguais, o RG é inválido
            return false;
        }





        internal List<PacienteDTO> BuscarPorCPF(string cpf, int clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "SELECT * FROM Paciente WHERE CPF = @cpf AND clinica= @clinica";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@cpf", cpf);
            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();
            var pacientes = new List<PacienteDTO>(); 

            while (dataReader.Read()) 
            {
                var paciente = new PacienteDTO();
                paciente.Clinica = new ClinicaDTO();

                paciente.ID = int.Parse(dataReader["ID"].ToString());
                paciente.Nome = dataReader["Nome"].ToString();
                paciente.DataNascimento = (DateTime)dataReader["DataNascimento"];
                paciente.Genero = dataReader["Genero"].ToString();
                paciente.RG = dataReader["RG"].ToString();
                paciente.CPF = dataReader["CPF"].ToString();
                paciente.Email = dataReader["Email"].ToString();
                paciente.Telefone = dataReader["Telefone"].ToString();
                paciente.Profissao = dataReader["Profissao"].ToString();
                paciente.Logradouro = dataReader["Logradouro"].ToString();
                paciente.Numero = dataReader["Numero"].ToString();
                paciente.Complemento = dataReader["Complemento"].ToString();
                paciente.CEP = dataReader["CEP"].ToString();
                paciente.Bairro = dataReader["Bairro"].ToString();
                paciente.Cidade = dataReader["Cidade"].ToString();
                paciente.Estado = dataReader["Estado"].ToString();
                paciente.NomeResponsavel = dataReader["NomeResponsavel"].ToString();
                paciente.NumeroResponsavel = dataReader["NumeroResponsavel"].ToString();
                paciente.DocumentoResponsavel = dataReader["DocumentoResponsavel"].ToString();
                paciente.GrauDeParentesco = dataReader["GrauDeParentesco"].ToString();
                paciente.Clinica.ID = int.Parse(dataReader["Clinica"].ToString());


                pacientes.Add(paciente); 
            }

            conexao.Close();
            return pacientes; 
        }

        internal List<PacienteDTO> BuscarPorNome(string nome, int clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "SELECT * FROM Paciente WHERE Nome LIKE CONCAT('%', @nome, '%') AND clinica = @clinica";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();
            var pacientes = new List<PacienteDTO>(); 

            while (dataReader.Read()) 
            {
                var paciente = new PacienteDTO();
                paciente.Clinica = new ClinicaDTO();

                paciente.ID = int.Parse(dataReader["ID"].ToString());
                paciente.Nome = dataReader["Nome"].ToString();
                paciente.DataNascimento = (DateTime)dataReader["DataNascimento"];
                paciente.Genero = dataReader["Genero"].ToString();
                paciente.RG = dataReader["RG"].ToString();
                paciente.CPF = dataReader["CPF"].ToString();
                paciente.Email = dataReader["Email"].ToString();
                paciente.Telefone = dataReader["Telefone"].ToString();
                paciente.Profissao = dataReader["Profissao"].ToString();
                paciente.Logradouro = dataReader["Logradouro"].ToString();
                paciente.Numero = dataReader["Numero"].ToString();
                paciente.Complemento = dataReader["Complemento"].ToString();
                paciente.CEP = dataReader["CEP"].ToString();
                paciente.Bairro = dataReader["Bairro"].ToString();
                paciente.Cidade = dataReader["Cidade"].ToString();
                paciente.Estado = dataReader["Estado"].ToString();
                paciente.NomeResponsavel = dataReader["NomeResponsavel"].ToString();
                paciente.NumeroResponsavel = dataReader["NumeroResponsavel"].ToString();
                paciente.DocumentoResponsavel = dataReader["DocumentoResponsavel"].ToString();
                paciente.GrauDeParentesco = dataReader["GrauDeParentesco"].ToString();
                paciente.Clinica.ID = int.Parse(dataReader["Clinica"].ToString());

                pacientes.Add(paciente);
            }

            conexao.Close();
            return pacientes;
        }

    }
}

