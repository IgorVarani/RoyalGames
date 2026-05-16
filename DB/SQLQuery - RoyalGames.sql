CREATE DATABASE Royal_Games;
GO

USE Royal_Games;
GO

CREATE TABLE Usuario (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(60) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Senha VARBINARY(32) NOT NULL,
    StatusUsuario BIT DEFAULT 1
);

CREATE TABLE EstadoConservacao (
    EstadoConservacaoID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(30) NOT NULL
);

CREATE TABLE ClassificacaoIndicativa (
    ClassificacaoIndicativaID INT PRIMARY KEY IDENTITY(1,1),
    Classificacao VARCHAR(20) NOT NULL
);

CREATE TABLE Genero (
    GeneroID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(80) NOT NULL
);

CREATE TABLE Plataforma (
    PlataformaID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(50) NOT NULL
);

CREATE TABLE Promocao (
    PromocaoID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    DataExpiracao DATETIME2(0) NOT NULL,
    StatusPromocao BIT DEFAULT 1
);

CREATE TABLE Jogo (
    JogoID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    Imagem VARBINARY(MAX) NOT NULL,
    StatusJogo BIT DEFAULT 1,

    EstadoConservacaoID INT NULL,
    UsuarioID INT NULL,
    ClassificacaoIndicativaID INT NULL,

    CONSTRAINT FK_Jogo_EstadoConservacao
        FOREIGN KEY (EstadoConservacaoID)
        REFERENCES EstadoConservacao(EstadoConservacaoID),

    CONSTRAINT FK_Jogo_Usuario
        FOREIGN KEY (UsuarioID)
        REFERENCES Usuario(UsuarioID),

    CONSTRAINT FK_Jogo_ClassificacaoIndicativa
        FOREIGN KEY (ClassificacaoIndicativaID)
        REFERENCES ClassificacaoIndicativa(ClassificacaoIndicativaID)
);

CREATE TABLE JogoGenero (
    JogoID INT NOT NULL,
    GeneroID INT NOT NULL,

    CONSTRAINT PK_JogoGenero
        PRIMARY KEY (JogoID, GeneroID),

    CONSTRAINT FK_JogoGenero_Jogo
        FOREIGN KEY (JogoID)
        REFERENCES Jogo(JogoID),

    CONSTRAINT FK_JogoGenero_Genero
        FOREIGN KEY (GeneroID)
        REFERENCES Genero(GeneroID)
);

CREATE TABLE JogoPlataforma (
    JogoID INT NOT NULL,
    PlataformaID INT NOT NULL,

    CONSTRAINT PK_JogoPlataforma
        PRIMARY KEY (JogoID, PlataformaID),

    CONSTRAINT FK_JogoPlataforma_Jogo
        FOREIGN KEY (JogoID)
        REFERENCES Jogo(JogoID),

    CONSTRAINT FK_JogoPlataforma_Plataforma
        FOREIGN KEY (PlataformaID)
        REFERENCES Plataforma(PlataformaID)
);

CREATE TABLE JogoPromocao (
    JogoID INT NOT NULL,
    PromocaoID INT NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_JogoPromocao
        PRIMARY KEY (JogoID, PromocaoID),

    CONSTRAINT FK_JogoPromocao_Jogo
        FOREIGN KEY (JogoID)
        REFERENCES Jogo(JogoID),

    CONSTRAINT FK_JogoPromocao_Promocao
        FOREIGN KEY (PromocaoID)
        REFERENCES Promocao(PromocaoID)
);

CREATE TABLE Log_AlteracaoJogo (
    Log_AlteracaoJogoID INT PRIMARY KEY IDENTITY(1,1),
    DataAlteracao DATETIME2(0) NOT NULL,
    NomeAnterior VARCHAR(100) NOT NULL,
    PrecoAnterior DECIMAL(10,2) NULL,
    JogoID INT NULL,

    CONSTRAINT FK_LogAlteracao_Jogo
        FOREIGN KEY (JogoID)
        REFERENCES Jogo(JogoID)
);