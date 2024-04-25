CREATE DATABASE Sysodonto;

USE Sysodonto;

CREATE TABLE Paciente (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    DataNascimento DATE,
    Genero varchar(20),
    RG VARCHAR (100),
	CPF VARCHAR (11),
    Email VARCHAR(100),
    Telefone VARCHAR(11),
    Profissao VARCHAR(20),
    Longadouro VARCHAR(20),
    Numero VARCHAR(5),
    Complemento VARCHAR(10),
    CEP VARCHAR(8),
    Bairro VARCHAR(20),
    Cidade VARCHAR(20),
    Estado VARCHAR(20),
    NomeResponsavel VARCHAR(30),
    NumeroResponsavel VARCHAR(11),
    DocumentoResponsavel VARCHAR(20),
    GrauDeParentesco VARCHAR(20),
    Prontuario INT NOT NULL,
    FOREIGN KEY (Prontuario) REFERENCES Prontuario(ID)
);




CREATE TABLE Dentista (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Email VARCHAR(100),
    Senha VARCHAR(100)
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


CREATE TABLE Recepcionista (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100),
    Email VARCHAR(100),
    Senha VARCHAR(100)
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


