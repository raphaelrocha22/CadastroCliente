create database cadastroCliente
go

use cadastroCliente
go

create table usuario(
idUsuario int identity not null,
nome nvarchar(50) not null,
[login] nvarchar(50) not null,
senha nvarchar(100) not null,
email nvarchar(100) not null,
perfil nvarchar(20) not null,
[status] nvarchar(10) not null,
constraint PK_Usuario_idUsuario primary key(idUsuario),
constraint UQ_Usuario_Login unique(login)
)
go

create table representante(
idRepresentante int not null,
nome nvarchar(100) not null,
tipoRepresentante nvarchar(10) not null,
email nvarchar(100) NULL,
idGerente int NULL,
ativo bit NOT NULL,
constraint PK_Representante_idRepresentante primary key(idRepresentante)
)
go

create table representante_Usuario(
idUsuario int not null,
idRepresentante int not null,
constraint PK_Representante_Usuario_idUsuario_idRepresentante primary key(idUsuario,idRepresentante)
)
go

create table cliente(
idCliente int identity not null,
codCliente int null,
codun int null,
razaoSocial nvarchar(100) not null,
nomeFantasia nvarchar(100) null,
codun_razaoSocial nvarchar(100) null,
cnpj nvarchar(20) not null,
inscricaoEstadual nvarchar(15) null,
inscricaoMunicipal nvarchar(15) null,
classe nvarchar(15) not null,
idAgente int not null,
idPromotor int not null,
observacao nvarchar(max) null,
acao nvarchar(20) not null,
[status] nvarchar(20) not null,
dataCadastro datetime not null,
idUsuario int not null,
constraint PK_Cliente_idCliente primary key(idCliente),
constraint UQ_Cliente_cnpj unique(cnpj)
)
go

create table Endereco(
idEndereco int identity not null,
tipo nvarchar(15) not null,
logradouro nvarchar(100) not null,
numero nvarchar(50) not null,
complemento nvarchar(100) null,
bairro nvarchar(25) not null,
municipio nvarchar(30) not null,
uf char(2) not null,
cep nvarchar(15) not null,
telefone1 nvarchar(20) null,
telefone2 nvarchar(20) null,
email nvarchar(100) null,
idCliente int not null,
dataCadastro datetime not null,
igualCadastro bit null,
igualCobranca bit null,
idUsuario int not null,
constraint PK_Endereco_idEndereco primary key(idEndereco)
)
go

create table Campanha(
id [int] identity not null,
campanha nvarchar(20) not null,
idCliente int null,
codCliente int null,
codun int null,
versao int not null,
nomeResponsavel nvarchar(50) null,
cpfResponsavel nvarchar(20) null,
modalidade nvarchar(20) null,
dataNegociacao datetime null,
dataInicio datetime not null,
dataFim datetime null,
mediaHistorica decimal(18, 0) null,
periodoMeses [int] null,
metaPeriodo decimal(18, 0) null,
crescimento decimal(18, 2) null,
markup decimal(18, 2) not null,
desconto decimal(18, 3) not null,
mesesPagamentoRBR int not null,
netlineHabilitado bit not null,
mesesPagamentoNetline int not null,
guelta nvarchar(10) not null,
rebatePercent decimal(18, 3) null,
rebateValor decimal(18, 2) null,
fileContrato nvarchar(100) null,
observacao nvarchar(max) null,
idUsuario int not null,
dataCadastro datetime not null,
[status] nvarchar(20) not null,
acao nvarchar(20) not null,
constraint PK_Campanha_id primary key(id)
)
go