﻿DECLARE @currentDBVersion nvarchar(10);
set @currentDBVersion = (select top(1) DbVersion from [dbo].[DbVersion] order by Created desc)

IF @currentDBVersion >= '1.1'
set noexec on
create table [dbo].[Currency] (
    Id   tinyint  unique      NOT NULL,
    [Name] nvarchar (30) NOT NULL,
    [Code] nvarchar (3) unique NOT NULL,
	primary key (Id),
);
go
create table dbo.[Account](
		Id bigint Identity(1,1) not null, 
		LeadId bigint not null,		
		CurrencyId tinyint null,
		IsDeleted bit default (0),  
		primary key (Id),
		foreign key (LeadId)  references [Lead] (Id),
		foreign key (CurrencyId) references [Currency] (Id))
go
create procedure [dbo].[Account_GetById]
	@Id bigint
	as
		begin
		select  a.Id,
				a.IsDeleted ,
				a.CurrencyId,	
		        l.Id,
				l.FirstName, 
				l.LastName,
				l.Patronymic,
				l.BirthDate,
				l.Phone,
				l.[Address],
				ct.Id,
				ct.[Name]		
				from dbo.[Account] a
		inner join [Lead] l on l.Id=a.LeadId
		inner join [Currency] c on c.Id=a.CurrencyId
		inner join [City] ct on ct.Id=l.CityId
		where a.Id=@Id 
	end
go
create procedure Account_Add_Or_Update
	@id			bigint,
	@leadId bigint,
	@currencyId tinyint
	as
	begin
		merge  dbo.[Account] a
		using (values (@id)) n(Id) 
		on a.Id = n.Id 
		when matched and IsDeleted=0
			then update 
				set a.CurrencyId=@currencyId						
		when not matched 
			then insert (LeadId,
						CurrencyId,
						IsDeleted) 
				values (@leadId, 
						@currencyId,
						default);
		declare @inserted bigint = CAST(SCOPE_IDENTITY() as [bigint])
		if @id > 0 exec Account_GetById @id
		else exec Account_GetById @inserted
	end
go
create procedure [dbo].[UpdatePassword]
@Id			bigint,
@Password	nvarchar(64)
       
as
begin
update dbo.[Lead]
	set [Password]=@Password
	where ID = @Id and IsDeleted=0
end;
go
create procedure Account_GetByLeadId
		@leadId bigint
		as
		begin
			select  a.Id, 
					a.IsDeleted, 
					c.Id CurrencyId
					from dbo.[Account] a
			inner join [Lead] l on l.Id=a.LeadId
			inner join [Currency] c on c.Id=a.CurrencyId
			where a.LeadId=@leadId and l.IsDeleted=0
		end
go
alter procedure Lead_Search
@roleId					int = null,
@firstNameSearchMode	int = null,
@firstName				nvarchar(30) = null,
@lastNameSearchMode		int = null,
@lastName				nvarchar(30) = null,
@patronymicSearchMode	int = null,
@patronymic				nvarchar(30) = null,
@loginSearchMode		int = null,
@login					nvarchar(30) = null,
@phoneSearchMode		int = null,
@phone					nvarchar(20) = null,
@emailSearchMode		int = null,
@email					nvarchar(40) = null,
@cityId					int = null,
@addressSearchMode		int = null,
@address				nvarchar(90) = null,
@birthDateBegin			date = null,
@birthDateEnd			date = null,
@registrationDateBegin	datetime2(7)=null,
@registrationDateEnd	datetime2(7)=null,
@accountId				bigint = null,
@currencyId				tinyint = null,
@includeDeleted			bit = null
as 
	begin	
		declare @exactValue int = 1
		declare @containsValue int = 2
		declare @startValue int = 3
		declare @resultSql nvarchar(max)
		set @resultSql = 'select
			l.id,
			l.FirstName,
			l.LastName,
			l.Patronymic,
			l.[Login],
			l.Phone,
			l.Email,
			l.[Address],
			l.BirthDate,
			l.RegistrationDate,
			l.ChangeDate,
			l.Isdeleted,
			r.Id,
			r.[Name],
			c.Id,
			c.[Name],
			a.Id,
			a.CurrencyId,
			a.IsDeleted 
		from dbo.[Lead] as l
		inner join [Role] as r on r.Id=l.RoleId
		inner join City as c on c.Id=l.CityId
		inner join Account as a on a.LeadId=l.Id		
		where 1=1'

		if @roleId>0
		begin
			set @resultSql = @resultSql + ' and r.Id = ' + convert(nvarchar(1), @roleId)
		end

		if @firstNameSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.FirstName = ''' + @firstName + ''''
		end

		if @firstNameSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.FirstName like ''%' + @firstName + '%'''
		end

		if @firstNameSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.FirstName like ''' + @firstName + '%'''
		end

		if @lastNameSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.LastName = ''' + @lastName + ''''
		end

		if @lastNameSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.LastName like ''%' + @lastName + '%'''
		end

		if @lastNameSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.LastName like ''' + @lastName + '%'''
		end

		if @patronymicSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.Patronymic = ''' + @patronymic + ''''
		end

		if @patronymicSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.Patronymic like ''%' + @patronymic + '%'''
		end

		if @patronymicSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.Patronymic like ''' + @patronymic + '%'''
		end

		if @loginSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.Login = ''' + @login + ''''
		end

		if @loginSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.Login like ''%' + @login + '%'''
		end

		if @loginSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.Login like ''' + @login + '%'''
		end

		if @phoneSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.Phone = ''' + @phone + ''''
		end

		if @phoneSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.Phone like ''%' + @phone + '%'''
		end

		if @phoneSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.Phone like ''' + @phone + '%'''
		end

		if @emailSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Email = ''' + @email + ''''
		end

		if @emailSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.Email like ''%' + @email + '%'''
		end

		if @emailSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Email like ''' + @email + '%'''
		end

		if @cityId>0
		begin
			set @resultSql = @resultSql + ' and c.Id = ' + convert(nvarchar(10), @cityId)
		end

		if @addressSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + ' and l.Address = ''' + @address + ''''
		end

		if @addressSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + ' and l.Address like ''%' + @address + '%'''
		end

		if @addressSearchMode=@startValue
		begin
			set @resultSql = @resultSql + ' and l.Address like ''' + @address + '%'''
		end

		if @birthDateBegin is not null
		begin
			set @resultSql = @resultSql + 'and l.BirthDate >=''' + convert(nvarchar(10), @birthDateBegin)+''''
		end

		if @birthDateEnd is not null
		begin
			set @resultSql = @resultSql + ' and l.BirthDate <=''' + convert(nvarchar(10), @birthDateEnd)+''''
		end

		if @registrationDateBegin is not null
		begin
			set @resultSql = @resultSql + ' and l.RegistrationDate >=''' + convert(nvarchar(10), @registrationDateBegin)+''''
		end

		if @registrationDateEnd is not null
		begin
			set @resultSql = @resultSql + 'and l.RegistrationDate <=''' + convert(nvarchar(10), @registrationDateEnd)+''''
		end

		if @accountId>0
		begin
			set @resultSql = @resultSql + ' and a.Id = ''' + convert(nvarchar(15),@accountId) + ''''
		end

		if @currencyId is not null
		begin
			set @resultSql = @resultSql + ' and a.CurrencyId = ''' + convert(nvarchar(3),@currencyId) + ''''
		end

		if @includeDeleted is not null
		begin
			set @resultSql = @resultSql + ' and l.IncludeDeleted =''' + convert(nvarchar(1), @includeDeleted)+''''
		end

		print @resultSql
		exec sp_sqlexec @resultSql
	end
go
alter procedure [dbo].[Lead_GetById]
@leadId bigint
as
begin
	select  l.Id, 
			l.FirstName, 
			l.LastName, 
			l.Patronymic,
			l.[Login],
			l.Phone, 
			l.Email,
		    l.[Address],
			l.BirthDate, 
			l.RegistrationDate, 
			l.ChangeDate, 
			r.Id, 
			r.[Name],
            c.Id, 
			c.[Name], 
			a.Id , 
			a.CurrencyId,
			a.IsDeleted 
			from dbo.[Lead] l
	inner join [Role] r on r.Id=l.RoleId
	inner join City c on c.Id=l.CityId
	left join Account a on a.LeadId=l.Id
	where l.Id=@leadId and l.IsDeleted=0
end
go
create procedure CreateStrings_Account
@rowValue bigint 
as
	begin
		declare @length int = 0
		while @length < @rowValue
		begin
		declare @leadId bigint		
		declare @currencyId tinyint

		set @leadId=(select round((rand()*990000),0)+1)		

		set @currencyId = (select round((rand()*3),0)+1)

		insert into Account(
			LeadId,			
			CurrencyId,
			IsDeleted)
		values (@leadId,				
				@currencyId,
				default)
   set @length = @length+1
	end

end
go

create Procedure dbo.GetCurrencyByAccountId
@accountId bigint
as
begin
select CurrencyId from dbo.Account a
where a.Id = @accountId
end
go

create procedure [dbo].[Lead_GetByLogin]
@login nvarchar(30)
as
begin
select Lead.Id, Role.Name Role, FirstName, LastName, Patronymic, Login,Password, Phone, Email,
City.Name City, Address, BirthDate, RegistrationDate, ChangeDate from Lead 
inner join Role on Role.Id=Lead.RoleId
inner join City on City.Id=Lead.CityId
where Lead.Login=@login and IsDeleted=0
end
go

INSERT INTO dbo.[DbVersion] (Created, DbVersion) VALUES (SYSDATETIME(), '1.1')
go
set noexec off