-- Jhonatan Candido Matos RA: 082170016


use master
drop database Teste
create database Teste
use Teste

------------------------------------------------------------------------------------------------------
-- Criar tabelas
------------------------------------------------------------------------------------------------------

create TABLE clientes (
  id int PRIMARY KEY,
  nome varchar(255),
  nasc date,
  sexo varchar(1),
  cep varchar(255),
  endereco varchar(255),
  bairro varchar(255),
  cidade varchar(255),
  estado varchar(255),
  telefone varchar(255),
  email varchar(255),
  senha varchar(255),
  imagem varbinary(max),  
)
GO

set dateformat dmy
GO

create TABLE [funcionarios] (
  [id] int PRIMARY KEY,
  [nome] varchar(255)
)
GO

create TABLE [produtos] (
  [id] int PRIMARY KEY,
  [descricao] varchar(255),
  [preco] decimal(10,2),
  [imagem] varbinary(max)
)
GO

create TABLE [venda] (
  [id] int PRIMARY KEY,
  [valor] decimal(10,2)
)
GO

create TABLE [categoria] (
  [id] int PRIMARY KEY,
  [descricao] varchar(255)
)
GO

create table local(
  id int primary key identity(1, 1),
  cep varchar(255),
  endereco varchar(255),
  bairro varchar(255),
  cidade varchar(255),
  estado varchar(255),

)
GO

create TABLE[dbo].[Pedido]
(
 [id] [int] IDENTITY(1,1) NOT NULL primary key,
 [Data] [datetime] NULL
)
GO
create TABLE[dbo].[PedidoItem]
(
 [Id][int] IDENTITY(1,1) NOT NULL primary key,
 [ProdutoId] [int]NOT NULL,  
 [Qtde] [int] NULL,
 [Valor] decimal(10,2) NULL,
)
GO

create table Login (Id int primary key identity(1,1),
					Usuario varchar (20),
					Senha varchar (20))
go

------------------------------------------------------------------------------------------------------
-- Adicionar FKs
------------------------------------------------------------------------------------------------------


alter table PedidoItem
add PedidoId int not null
	foreign key (PedidoId) references
		Pedido (id)
GO

alter table produtos
add categoriaId int not null
	foreign key (categoriaId) references
		categoria (id)
GO

alter table venda
add produtoId int not null
	foreign key (produtoId) references
		produtos (id)
GO
alter table venda
add clienteId int not null
	foreign key (clienteId) references
		clientes (id)
GO
alter table venda
add funcId int not null
	foreign key (funcId) references
		funcionarios (id)
GO
------------------------------------------------------------------------------------------------------
-- Stored Procedure
------------------------------------------------------------------------------------------------------
create procedure spDelete
(
	@id int ,
	@tabela varchar(max)
)
as
	begin
		declare @sql varchar(max);
		set @sql = ' delete ' + @tabela +
		' where id = ' + cast(@id as varchar(max))
		exec(@sql)
	end
GO

create procedure spConsulta
(
@id int ,
@tabela varchar(max)
)
as
	begin
		declare @sql varchar(max);
		set @sql = 'select * from ' + @tabela +
		' where id = ' + cast(@id as varchar(max))
		exec(@sql)
	end
GO

create procedure spListagem
(
@tabela varchar(max),
@ordem varchar(max))
as
	begin
		exec('select * from ' + @tabela +
		' order by ' + @ordem)
	end
GO

create procedure spProximoId
(@tabela varchar(max))
as
	begin
		exec('select isnull(max(id) +1, 1) as MAIOR from '
		+@tabela)
	end
GO

create procedure spInsert_Clientes
(
	@id int,	
	@nome varchar(max),
	@nasc date,
	@sexo varchar(1),
	@cep varchar(max),
	@endereco varchar(max),
	@bairro varchar(max),
	@cidade varchar(max),
	@estado varchar(max),
	@telefone varchar(max),
	@email varchar(max),
	@senha varchar(max),
	@imagem varbinary(max)
)
as
	begin
		insert into Clientes
		(id, nome, nasc, sexo, cep, endereco, bairro, cidade, estado, telefone, email, senha, imagem)
		values
		(@id, @nome, @nasc, @sexo, @cep, @endereco, @bairro, @cidade, @estado, @telefone, @email, @senha, @imagem)

		insert into local values (@cep, @endereco, @bairro, @cidade, @estado)

	end
GO

create procedure spUpdate_Clientes
(
	@id int,	
	@nome varchar(max),
	@nasc date,
	@sexo varchar(1),
	@cep varchar(max),
	@endereco varchar(max),
	@bairro varchar(max),
	@cidade varchar(max),
	@estado varchar(max),
	@telefone varchar(max),
	@email varchar(max),
	@senha varchar(max),
	@imagem varbinary(max)
)
as
	begin
		update clientes set
		nome = @nome,
		nasc = @nasc,
		sexo = @sexo,
		cep = @cep,
		endereco = @endereco,
		bairro = @bairro,
		cidade = @cidade,
		estado = @estado,
		telefone = @telefone,
		email = @email,
		senha = @senha,
		imagem = @imagem
		where id = @id
	end
GO

create procedure spInsert_Funcionarios
(
	@id int,	
	@nome varchar(max)
)
as
	begin
		insert into funcionarios
		(id, nome)
		values
		(@id, @nome)
	end
GO

create procedure spUpdate_Funcionarios
(
	@id int,
	@nome varchar(max)	
)
as
	begin
		update funcionarios set
		nome = @nome
		where id = @id
	end
GO

create procedure spInsert_Categoria
(
	@id int,	
	@descricao varchar(max)
)
as
	begin
		insert into categoria
		(id, descricao)
		values
		(@id, @descricao)
	end
GO

create procedure spUpdate_Categoria
(
	@id int,
	@descricao varchar(max)	
)
as
	begin
		update categoria set
		descricao = @descricao
		where id = @id
	end
GO

create procedure spInsert_Produtos
(
	@id int,	
	@descricao varchar(max),
	@preco decimal(10,2),
	@categoriaId int,
	@imagem varbinary(max)
)
as
	begin
		insert into produtos
		(id, descricao, preco, categoriaId, imagem)
		values
		(@id, @descricao, @preco, @categoriaId, @imagem)
	end
GO

create procedure spUpdate_Produtos
(
	@id int,	
	@descricao varchar(max),
	@preco decimal(10,2),
	@categoriaId int,
	@imagem varbinary(max)
)
as
	begin
		update produtos set
		descricao = @descricao,		
		preco = @preco,
		categoriaId = @categoriaId,
		imagem = @imagem
		where id = @id
	end
GO

create procedure spLogin
(
@email  varchar(max),
@senha varchar(max),
@tabela varchar(max)
)
as
	begin
		declare @sql varchar(max);
		set @sql = 'select * from ' + @tabela +
		' where email = ' + @email + 
		' and senha = ' + @senha

		exec(@sql)
	end
GO

create procedure spInsert_Pedido 
 @data datetime
as
begin
 insert into pedido(data) values (@data)
end
GO

create procedure spInsert_PedidoItem 
 @PedidoId int,
 @ProdutoId int,
 @Qtde int,
 @Valor decimal(10,2)
as
begin
 insert into pedidoItem(pedidoId, ProdutoId, Qtde, Valor)
 values (@PedidoId, @ProdutoId, @Qtde, @Valor)
end
GO 

create procedure spInsert_Login 
(
@usuario varchar(20), @senha varchar (20)
)
as
begin
insert into Login (usuario,senha) values (@usuario,@senha)
end
GO


create procedure spListagemCategorias as begin   select * from categoria order by descricao end 
GO 

------------------------------------------------------------------------------------------------------
-- Criando Function
------------------------------------------------------------------------------------------------------

create function fnc_Consulta_Login
(
@Usuario varchar(20)
)
Returns table
as
Return
(
select * from Login
where Usuario = @Usuario
)
GO


create procedure spConsulta_Login 
(
@Usuario varchar(20
))
as
begin
select * from fnc_Consulta_Login (@Usuario)
end 
GO

------------------------------------------------------------------------------------------------------
-- Criando Triggers
------------------------------------------------------------------------------------------------------

create trigger rtg_Delete_Produtos
	on produtos for delete as
begin
	-- Vai mostrar todos os dados que serão excluidos
	select * from deleted
end
GO

create trigger rtg_Delete_Cliente
	on clientes for delete as
begin
	-- Vai mostrar todos os dados que serão excluidos
	select * from deleted
end
GO

create trigger rtg_Delete_Funcionarios
	on funcionarios for delete as
begin
	-- Vai mostrar todos os dados que serão excluidos
	select * from deleted
end
GO

create trigger rtg_Delete_Categoria
	on categoria for delete as
begin
	-- Vai mostrar todos os dados que serão excluidos
	select * from deleted
end
GO

create trigger rtg_Delete_Login
	on login for delete as
begin
	-- Vai mostrar todos os dados que serão excluidos
	select * from deleted
end
GO

------------------------------------------------------------------------------------------------------
-- Inserindo valores
------------------------------------------------------------------------------------------------------

insert into categoria values(1, 'Pizzas')
GO
insert into categoria values(2, 'Bebidas')
GO
insert into funcionarios(id, nome) values(1, 'joao')
GO
insert into funcionarios(id, nome) values(2, 'maria')
GO
insert into Login (Usuario,Senha) values('usuario','senha')
GO