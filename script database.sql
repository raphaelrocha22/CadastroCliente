use cadastroCliente
go

create table representante(
codRepresentante int primary key not null,
nome nvarchar(50) not null,
tipoRepresentante nvarchar(20) not null,
ativo bit not null)
go

create table usuario(
id int primary key IDENTITY,
nome nvarchar(50) not null,
[login] nvarchar(50) not null,
senha nvarchar(100) not null,
codRepresentante int null,
ativo bit not null)
go

alter table usuario add constraint FK_representante_CodRepresentante
foreign key (codRepresentante) references representante(codRepresentante)
go

alter table usuario add constraint UQ_Login
unique ([login])
go


select * from usuario