create procedure [dbo].[Lead_Add_Or_Update]
@id			bigint,
@firstName	nvarchar(30),
@lastName	nvarchar(30),
@patronymic	nvarchar(30),
@login		nvarchar(30),
@password	nvarchar(64),
@phone		nvarchar(20),
@email		nvarchar(40),
@cityId		int,
@address	nvarchar(90),
@birthDate	date
as
begin
		declare @roleUser tinyint = 3
		declare @registrationDate datetime2 = SYSDATETIME()
		declare @changeDate datetime2 = SYSDATETIME()
merge  dbo.[Lead] l
using (values (@id)) n(Id) 
on l.Id = n.Id 
when matched and l.IsDeleted=0
	then update 
		set l.FirstName=@FirstName,
			l.LastName=@LastName,
			l.Patronymic=@Patronymic,
			l.[Password]=@Password,
			l.Phone=@Phone,
			l.CityId=@CityId,
			l.[Address]=@Address,
			l.BirthDate=@BirthDate,
			l.ChangeDate=@changeDate
when not matched 
	then insert (RoleId, FirstName, LastName, Patronymic, [Login], [Password], Phone, Email, CityId, [Address], BirthDate, RegistrationDate, ChangeDate, IsDeleted) 
		values (@roleUser, 
				@firstName, 
				@lastName, 
				@patronymic, 
				@login, 
				@password, 
				@phone, 
				@email, 
				@cityId, 
				@address, 
				@birthDate, 
				@registrationDate, 
				@changeDate, 
				default);
	declare @inserted bigint = CAST(SCOPE_IDENTITY() as [bigint])
	if @id > 0 exec Lead_GetById @id
	else exec Lead_GetById @inserted
end
