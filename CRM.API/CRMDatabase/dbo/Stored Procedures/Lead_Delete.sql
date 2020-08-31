CREATE procedure [dbo].[Lead_Delete]
	@id bigint
as
begin
	update dbo.[Lead]
	set IsDeleted = 1
	where Id = @id;
end;