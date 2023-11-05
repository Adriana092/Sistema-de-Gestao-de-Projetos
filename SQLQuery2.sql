USE BDProjeto

/*Criação da tabela Orientador*/

CREATE TABLE Orientador(
ID_Orientador INTEGER IDENTITY PRIMARY KEY,
 Username VARCHAR(50) NOT NULL,
 Nome VARCHAR(50) NOT NULL,
 Email VARCHAR(50) NOT NULL,
 Instituicao VARCHAR(50) NOT NULL,
 Pass VARCHAR(50) NOT NULL
 );

/*Criação da tabela Orientador Externo*/
CREATE TABLE Externo(
ID_Externo INTEGER IDENTITY PRIMARY KEY,
FOREIGN KEY(ID_Externo) REFERENCES Orientador(ID_Orientador)
);

/*Criação da tabela Orientador Interno*/
CREATE TABLE Interno(
ID_Interno INTEGER IDENTITY PRIMARY KEY,
Username VARCHAR(50) NOT NULL,
FOREIGN KEY(ID_Interno) REFERENCES Orientador(ID_Orientador)
)

/*Criação da tabela Aluno*/
CREATE TABLE Aluno(
ID_Aluno INTEGER IDENTITY PRIMARY KEY,
Username VARCHAR(50) NOT NULL,
Nome VARCHAR(50) NOT NULL,
Curso VARCHAR(50) NOT NULL,
Email VARCHAR(50) NOT NULL,
Numero_Mec INTEGER NOT NULL,
Pass VARCHAR(50) NOT NULL
);

/*Criação da tabela Edição*/
CREATE TABLE Edicao(
ID_Edicao INTEGER IDENTITY PRIMARY KEY,
Nome VARCHAR(50) NOT NULL,
Descricao VARCHAR(500) NOT NULL,
Valido_de INTEGER NOT NULL,
Valido_ate INTEGER NOT NULL,
);

/*Criação da tabela Projeto*/
CREATE TABLE Projeto(
ID_Projeto INTEGER IDENTITY PRIMARY KEY,
Titulo VARCHAR(100) NOT NULL,
Num_Alunos INTEGER NOT NULL,
Area_Investigacao VARCHAR(50) NOT NULL,
Centro_Investigacao VARCHAR(50),
Apresentacao VARCHAR(1000) NOT NULL,
Objetivos VARCHAR(500) NOT NULL,
ID_Orientador INT NOT NULL,
ID_Edicao INT NOT NULL,
FOREIGN KEY (ID_Edicao) REFERENCES Edicao(ID_Edicao),
FOREIGN KEY(ID_Orientador) REFERENCES Orientador(ID_Orientador)
);



/*Criação da tabela Grupo*/
CREATE TABLE Grupo(
ID_Grupo INTEGER IDENTITY PRIMARY KEY,
ID_Edicao INT NOT NULL,
FOREIGN KEY(ID_Edicao) REFERENCES Edicao(ID_Edicao),
FOREIGN KEY(ID_Grupo) REFERENCES Aluno(ID_Aluno)
);

/*Criação da tabela Candidatura*/
CREATE TABLE Candidatura(
Seriacao INTEGER NOT NULL,
Resultado BIT,
ID_Grupo INT NOT NULL,
ID_projeto INT NOT NULL,
Ordem_Preferencia INTEGER NOT NULL,
PRIMARY KEY(ID_grupo, ID_Projeto),
FOREIGN KEY(ID_Grupo) REFERENCES Grupo(ID_Grupo),
FOREIGN KEY(ID_Projeto) REFERENCES Projeto(ID_Projeto)
);

/*Criação da tabela Orienta*/
CREATE TABLE Orienta(
ID_Orientador INT NOT NULL,
ID_Projeto INT NOT NULL,
Tipo VARCHAR(50) NOT NULL,
PRIMARY KEY(ID_Orientador, ID_Projeto),
FOREIGN KEY(ID_Orientador) REFERENCES Orientador(ID_Orientador),
FOREIGN KEY(ID_Projeto) REFERENCES Projeto(ID_Projeto)
);

/*Criação da tabela Pertence*/

CREATE TABLE Pertence(
ID_Aluno INT REFERENCES dbo.Aluno(ID_Aluno),
ID_Grupo INT REFERENCES Grupo(ID_Grupo),
PRIMARY KEY (ID_Aluno, ID_Grupo)
);