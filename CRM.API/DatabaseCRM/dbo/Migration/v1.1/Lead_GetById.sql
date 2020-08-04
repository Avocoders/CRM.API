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
			a.IsDeleted AccountState 
			from dbo.[Lead] l
	inner join [Role] r on r.Id=l.RoleId
	inner join City c on c.Id=l.CityId
	inner join Account a on a.LeadId=l.Id
	where l.Id=@leadid and l.IsDeleted=0
end