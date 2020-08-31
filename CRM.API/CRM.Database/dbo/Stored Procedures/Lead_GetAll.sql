CREATE procedure [dbo].[Lead_GetAll]
as
begin
	select [Lead].Id, FirstName, LastName, Patronymic, [Login], Phone, Email,
		    [Address], BirthDate, RegistrationDate, ChangeDate, [Role].Id, [Role].[Name],
            City.Id, City.[Name] from dbo.[Lead] 
	inner join [Role] on [Role].Id=[Lead].RoleId
	inner join City on City.Id=[Lead].CityId
	where IsDeleted=0
end
