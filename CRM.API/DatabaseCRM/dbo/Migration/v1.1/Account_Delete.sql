CREATE procedure Account_Delete
	@id bigint
as
begin
	update Account
	set IsDeleted = 1
	where Id = @id;
end;