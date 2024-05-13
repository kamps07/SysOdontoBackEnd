CREATE DATABASE Sysodonto;
USE Sysodonto;

CREATE TABLE Clinica(
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Telefone VARCHAR(255),
    Endereco  VARCHAR(255),
    ImgURL varchar(255) DEFAULT NULL
);

CREATE TABLE Administrador (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome varchar(255) NOT NULL,
  email varchar(255) NOT NULL,
  senha varchar(255) NOT NULL,
  cpf varchar(255) NOT NULL 
);

CREATE TABLE Paciente (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255),
    DataNascimento datetime,
    Genero varchar(255),
    RG VARCHAR (255),
	CPF VARCHAR (255),
    Email VARCHAR(255),
    Telefone VARCHAR(255),
    Profissao VARCHAR(255),
    Logradouro VARCHAR(255),
    Numero VARCHAR(255),
    Complemento VARCHAR(255),
    CEP VARCHAR(255),
    Bairro VARCHAR(255),
    Cidade VARCHAR(255),
    Estado VARCHAR(255),
    NomeResponsavel VARCHAR(255),
    NumeroResponsavel VARCHAR(255),
    DocumentoResponsavel VARCHAR(255),
    GrauDeParentesco VARCHAR(255)
);

CREATE TABLE Dentista (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Email VARCHAR(100),
    Senha VARCHAR(100)
);

CREATE TABLE Recepcionista (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Email VARCHAR(100),
    Senha VARCHAR(100)
);

CREATE TABLE Pagamentos (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Paciente INT,
    Valor DECIMAL(10, 2),
    DataPagamento DATE,
    Metodo ENUM('Dinheiro', 'Cartão de Crédito', 'Cartão de Débito'),
    FOREIGN KEY (Paciente) REFERENCES Paciente(ID)
);


CREATE TABLE Documentos (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Tipo VARCHAR(100),
    NomeArquivo VARCHAR(255),
    CaminhoArquivo VARCHAR(255),
    DataUpload DATE
);

CREATE TABLE Anamnese (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    DataAnamnese DATE,
    Pergunta TEXT,
    Resposta TEXT
);


CREATE TABLE Arquivos (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Tipo VARCHAR(100),
    NomeArquivo VARCHAR(255),
    CaminhoArquivo VARCHAR(255),
    DataUpload DATE
);


CREATE TABLE Evolucao (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255),
    DataEvolucao DATETIME,
    Descricao TEXT
);


CREATE TABLE Tratamentos (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Descricao TEXT
);

CREATE TABLE Consulta (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Tratamento INT NOT NULL,
    Descricao TEXT,
    Paciente INT NOT NULL,
    DataHorário DATETIME,
    Status ENUM('Agendada', 'Cancelada', 'Realizada'),
    FOREIGN KEY (Tratamento) REFERENCES Tratamentos(ID),
    FOREIGN KEY (Paciente) REFERENCES Paciente(ID)
);


CREATE TABLE Agenda (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Consulta INT NOT NULL,
    Dentista INT NOT NULL,
    FOREIGN KEY (Consulta) REFERENCES Consulta(ID),
    FOREIGN KEY (Dentista) REFERENCES Dentista(ID)
);

CREATE TABLE Prontuario (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Anamnese INT NOT NULL,
    Evolucao INT NOT NULL,
    Arquivos INT NOT NULL,
    Documentos INT NOT NULL,
    Descricao TEXT,
    FOREIGN KEY (Anamnese) REFERENCES Anamnese(ID),
    FOREIGN KEY (Evolucao) REFERENCES Evolucao(ID),
    FOREIGN KEY (Arquivos) REFERENCES Arquivos(ID),
    FOREIGN KEY (Documentos) REFERENCES Documentos(ID)
);




