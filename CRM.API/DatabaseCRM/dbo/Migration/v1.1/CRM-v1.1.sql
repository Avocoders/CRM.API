DECLARE @currentDBVersion nvarchar(10);
set @currentDBVersion = (select top(1) DbVersion from [dbo].[DbVersion] order by Created desc)

IF @currentDBVersion <> '1.1'

create table [dbo].[Currency] (
    Id   int  unique      NOT NULL,
    [Name] nvarchar (30) NOT NULL,
    Code nvarchar (3) unique NOT NULL,
	primary key (Id),
);
go
create table dbo.[Account](
		Id bigint Identity, 
		LeadId bigint not null,
		Balance bigint null,
		СurrencyId int null,
		IsDeleted bit default (0),  
		primary key (Id),
		foreign key (LeadId)  references [Lead] (Id),
		foreign key (СurrencyId) references [Currency] (Id))
go
create procedure Account_GetById
	@accountId bigint
	as
	begin
		select a.Id, 
				l.FirstName, 
				l.LastName, 
				l.BirthDate, 
				c.[Name], 
				a.IsDeleted from dbo.[Account] a
		inner join [Lead] l on l.Id=a.LeadId
		inner join [Currency] c on c.Id=a.СurrencyId
		where a.Id=@accountId 
	end
go
create procedure Account_Add_Or_Update
	@id			bigint,
	@leadId bigint,
	@currencyId int
	as
	begin
		merge  dbo.[Account] a
		using (values (@id)) n(Id) 
		on a.Id = n.Id 
		when matched and IsDeleted=0
			then update 
				set a.СurrencyId=@currencyId						
		when not matched 
			then insert (LeadId,
						СurrencyId,
						IsDeleted) 
				values (@leadId, 
						@currencyId,
						default);
		declare @inserted bigint = CAST(SCOPE_IDENTITY() as [bigint])
		if @id > 0 exec Account_GetById @id
		else exec Account_GetById @inserted
	end
go
create procedure Account_GetByLeadId
		@leadId bigint
		as
		begin
			select a.Id, 
					l.FirstName, 
					l.LastName, 
					l.BirthDate, 
					c.[Name], 
					a.IsDeleted from dbo.[Account] a
			inner join [Lead] l on l.Id=a.LeadId
			inner join [Currency] c on c.Id=a.СurrencyId
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
@currencyId				nvarchar(3) = null,
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
			c.[Name]
			a.Id,
			cr.[Name]
		from dbo.[Lead] as l
		inner join [Role] as r on r.Id=l.RoleId
		inner join City as c on c.Id=l.CityId
		inner join Account as a on a.LeadId=l.Id
		inner join Currency cr on cr.Id=a.СurrencyId
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
			set @resultSql = @resultSql + ' and l.FirstName lile ''' + @firstName + '%'''
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
			set @resultSql = @resultSql + ' and l.LastName lile ''' + @lastName + '%'''
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
			set @resultSql = @resultSql + ' and l.Patronymic lile ''' + @patronymic + '%'''
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
			set @resultSql = @resultSql + ' and l.Login lile ''' + @login + '%'''
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
			set @resultSql = @resultSql + ' and l.Phone lile ''' + @phone + '%'''
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
			set @resultSql = @resultSql + 'and l.Email lile ''' + @email + '%'''
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
			set @resultSql = @resultSql + ' and l.Address lile ''' + @address + '%'''
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
			set @resultSql = @resultSql + ' and a.Id = ''' + convert(nvarchar(1),@accountId) + ''''
		end

		if @currencyId is not null
		begin
			set @resultSql = @resultSql + ' and cr.Id = ''' + convert(nvarchar(1),@currencyId) + ''''
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
@leadid bigint
as
begin
	select l.Id, 
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
			a.Id AccountNumber, 
			cr.[Name],
			a.IsDeleted AccountState 
			from dbo.[Lead] l
	inner join [Role] r on r.Id=l.RoleId
	inner join City c on c.Id=l.CityId
	inner join Account a on a.LeadId=l.Id
	inner join [Currency] cr on c.Id=a.СurrencyId

	where l.Id=@leadid and l.IsDeleted=0
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
		declare @balance bigint
		declare @currencyId int

		set @leadId=(select round((rand()*100000),1))

		set @balance = (select round((rand()*100000),1))

		set @currencyId =(select round((rand()*4),1))

		insert into Account(
			LeadId,
			Balance,
			СurrencyId,
			IsDeleted)
		values (@leadId,
				@balance,
				@currencyId,
				default)
	end
set @length = @length+1
end
go

INSERT INTO dbo.[DbVersion] (Created, DbVersion) VALUES (SYSDATETIME(), '1.1')
go