create procedure [dbo].[Lead_Update]
@Id			bigint,
@FirstName	nvarchar(30),
@LastName	nvarchar(30),
@Patronymic	nvarchar(30),
@Password	nvarchar(64),
@Phone		nvarchar(20),
@CityId		nvarchar(30),
@Address	nvarchar(90),
@BirthDate	date
       
as
begin
update dbo.[Lead]
	set FirstName=@FirstName, LastName=@LastName, Patronymic=@Patronymic, [Password]=@Password, Phone=@Phone,
		CityId=@CityId, [Address]=@Address, BirthDate=@BirthDate
	where ID = @Id and IsDeleted=0

exec Lead_GetById @Id
end;