CREATE procedure [dbo].[Lead_GetByLogin]
@login nvarchar(20)
as
begin
select Lead.Id, Role.Name Role, FirstName, LastName, Patronymic, Login,Password, Phone, Email,
City.Name City, Address, BirthDate, RegistrationDate, ChangeDate from Lead 
inner join Role on Role.Id=Lead.RoleId
inner join City on City.Id=Lead.CityId
where Lead.Login=@login and IsDeleted=0
end