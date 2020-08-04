create procedure Account_Add_Or_Update
	@id			bigint,
	@leadId bigint,
	@balance bigint,
	@currencyId int
	as
	begin
		merge  dbo.[Account] a
		using (values (@id)) n(Id) 
		on a.Id = n.Id 
		when matched and IsDeleted=0
			then update 
				set a.Balance=@balance,
					a.СurrencyId=@currencyId						
		when not matched 
			then insert (LeadId,
						Balance,
						СurrencyId,
						IsDeleted) 
				values (@leadId, 
						@balance, 
						@currencyId,
						default);
		declare @inserted bigint = CAST(SCOPE_IDENTITY() as [bigint])
		if @id > 0 exec Account_GetById @id
		else exec Account_GetById @inserted
	end