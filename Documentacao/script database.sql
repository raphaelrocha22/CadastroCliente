-- Geração de Modelo físico
-- Sql ANSI 2003 - brModelo.



CREATE TABLE Cliente (
idCliente int PRIMARY KEY IDENTITY,
codCliente int,
codun int,
razaoSocial nvarchar(100) NOT NULL,
nomeFantasia nvarchar(100),
cnpj nvarchar(20) NOT NULL,
inscricaoEstadual nvarchar(15),
inscricaoMunicipal nvarchar(15),
classe nvarchar(15) NOT NULL,
idRepresentante int NOT NULL
)
GO

CREATE TABLE Endereco (
idEndereco int PRIMARY KEY IDENTITY,
logradouro nvarchar(100) NOT NULL,
numero nvarchar(50) NOT NULL,
complemento nvarchar(100),
bairro nvarchar(25) NOT NULL,
municipio nvarchar(30) NOT NULL,
uf char(2) NOT NULL,
cep nvarchar(15) NOT NULL,
telefone1 nvarchar(20),
telefone2 nvarchar(20),
email nvarchar(100),
tipo nvarchar(15) NOT NULL,
idCliente int NOT NULL,
)
GO

CREATE TABLE Representante (
idRepresentante int PRIMARY KEY,
nome nvarchar(100) NOT NULL,
tipoRepresentante nvarchar(10) NOT NULL,
ativo bit NOT NULL
)
GO

CREATE TABLE Usuario (
idUsuario int PRIMARY KEY IDENTITY,
nome nvarchar(50) NOT NULL,
login nvarchar(50) NOT NULL,
senha nvarchar(100) NOT NULL
)
GO

CREATE TABLE Representante_Usuario (
idUsuario int NOT NULL,
idRepresentante int NOT NULL
)
GO

ALTER TABLE Cliente ADD CONSTRAINT UQ_CODCLIENTE
UNIQUE(codCliente)
GO

ALTER TABLE Cliente ADD CONSTRAINT UQ_CNPJ
UNIQUE(cnpj)
GO

ALTER TABLE Usuario ADD CONSTRAINT UQ_LOGIN
UNIQUE([login])
GO

ALTER TABLE Cliente ADD CONSTRAINT FK_REPRESENTANTE_IDREPRESENTANTE
FOREIGN KEY(idRepresentante) REFERENCES Representante (idRepresentante)
GO

ALTER TABLE Cliente ADD CONSTRAINT FK_CLIENTE_IDCLIENTE
FOREIGN KEY(idCliente) REFERENCES Cliente (idCliente)
GO

ALTER TABLE Representante_Usuario ADD CONSTRAINT FK_REPRESENTANTE_USUARIO_IDREPRESENTANTE
FOREIGN KEY(idRepresentante) REFERENCES Representante (idRepresentante)
GO

ALTER TABLE Representante_Usuario ADD CONSTRAINT FK_REPRESENTANTE_USUARIO_IDUSUARIO
FOREIGN KEY(idUsuario) REFERENCES Usuario (idUsuario)
GO

ALTER TABLE Representante_Usuario ADD CONSTRAINT PK_IDUSUARIO_IDREPRESENTANTE
PRIMARY KEY(idUsuario,idRepresentante)
GO


