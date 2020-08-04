create procedure [dbo].[CreateStrings_Account]
@rowValue bigint 
as
	begin
		declare @length int = 0
		while @length < @rowValue
		begin
		declare @leadId bigint
		declare @balance bigint
		declare @currencyId int

		set @leadId=(SELECT RAND()*(1001-1)+1)

		set @balance = (select round((rand()*100000),1))

		set @currencyId =(SELECT RAND()*(5-1)+1)

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