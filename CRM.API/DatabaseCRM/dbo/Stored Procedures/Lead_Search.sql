create procedure Lead_Search
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
		from dbo.[Lead] as l
		inner join [Role] as r on r.Id=l.RoleId
		inner join City as c on c.Id=l.CityId
		where 1=1'

		if @roleId>0
		begin
			set @resultSql = @resultSql + 'and r.Id = ' + convert(nvarchar(1), @roleId)
		end

		if @firstNameSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.FirstName = ''' + @firstName + ''''
		end

		if @firstNameSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.FirstName like ''%' + @firstName + '%'''
		end

		if @firstNameSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.FirstName lile ''' + @firstName + '%'''
		end

		if @lastNameSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.LastName = ''' + @lastName + ''''
		end

		if @lastNameSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.LastName like ''%' + @lastName + '%'''
		end

		if @lastNameSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.LastName lile ''' + @lastName + '%'''
		end

		if @patronymicSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Patronymic = ''' + @patronymic + ''''
		end

		if @patronymicSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.Patronymic like ''%' + @patronymic + '%'''
		end

		if @patronymicSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Patronymic lile ''' + @patronymic + '%'''
		end

		if @loginSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Login = ''' + @login + ''''
		end

		if @loginSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.Login like ''%' + @login + '%'''
		end

		if @loginSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Login lile ''' + @login + '%'''
		end

		if @phoneSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Phone = ''' + @phone + ''''
		end

		if @phoneSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.Phone like ''%' + @phone + '%'''
		end

		if @phoneSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Phone lile ''' + @phone + '%'''
		end

		if @emailSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Email = ''' + @email + ''''
		end

		if @emailSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.Email like ''%' + @email + '%'''
		end

		if @emailSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Email lile ''' + @email + '%'''
		end

		if @cityId>0
		begin
			set @resultSql = @resultSql + 'and c.Id = ' + convert(nvarchar(10), @cityId)
		end

		if @addressSearchMode=@exactValue
		begin
			set @resultSql = @resultSql + 'and l.Address = ''' + @address + ''''
		end

		if @addressSearchMode=@containsValue
		begin
			set @resultSql = @resultSql + 'and l.Address like ''%' + @address + '%'''
		end

		if @addressSearchMode=@startValue
		begin
			set @resultSql = @resultSql + 'and l.Address lile ''' + @address + '%'''
		end

		if @birthDateBegin is not null
		begin
			set @resultSql = @resultSql + 'and l.BirthDate >=''' + convert(nvarchar(10), @birthDateBegin)+''''
		end

		if @birthDateEnd is not null
		begin
			set @resultSql = @resultSql + 'and l.BirthDate <=''' + convert(nvarchar(10), @birthDateEnd)+''''
		end

		if @registrationDateBegin is not null
		begin
			set @resultSql = @resultSql + 'and l.RegistrationDate >=''' + convert(nvarchar(10), @registrationDateBegin)+''''
		end

		if @registrationDateEnd is not null
		begin
			set @resultSql = @resultSql + 'and l.RegistrationDate <=''' + convert(nvarchar(10), @registrationDateEnd)+''''
		end

		if @includeDeleted is not null
		begin
			set @resultSql = @resultSql + 'and l.IncludeDeleted <=''' + convert(nvarchar(1), @includeDeleted)+''''
		end

		print @resultSql
		exec sp_sqlexec @resultSql
	end
