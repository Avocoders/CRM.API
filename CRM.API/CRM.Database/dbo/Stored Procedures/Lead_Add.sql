CREATE procedure [dbo].[Lead_Add]
        @firstName nvarchar(30),
        @lastName nvarchar(30),
        @patronymic nvarchar(30),
        @login nvarchar(20),
        @password nvarchar(64),
        @phone nvarchar(20),
        @email nvarchar(30),
        @cityId int,
        @address nvarchar(90),
        @birthDate date
    as
    begin
        declare @roleUser tinyint = 3
		declare @registrationDate datetime2 = SYSDATETIME()
		declare @changeDate datetime2 = SYSDATETIME()
        insert into dbo.[Lead] (RoleId, FirstName, LastName, Patronymic, [Login], [Password], Phone, Email, CityId, [Address], BirthDate, RegistrationDate, ChangeDate, IsDeleted) 
		values (@roleUser, @firstName, @lastName, @patronymic, @login, @password, @phone, @email, @cityId, @address, @birthDate, @registrationDate, @changeDate, default);
        select SCOPE_IDENTITY()
    end;