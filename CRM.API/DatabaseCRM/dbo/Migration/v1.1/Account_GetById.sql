create procedure Account_GetById
	@accoundId bigint
	as
	begin
		select a.Id, l.FirstName, l.LastName, l.BirthDate, a.Balance, a.СurrencyId, a.IsDeleted from dbo.[Account] a
		inner join [Lead] l on l.Id=a.LeadId
		where a.Id=@accoundId 
	end