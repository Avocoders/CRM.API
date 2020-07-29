create procedure [dbo].[Lead_UpdateEmail]
	@id bigint,
	@email nvarchar(30)
as
begin
	update dbo.[Lead]
	set Email = @email
	where ID = @Id and IsDeleted=0;
	select @email;
end;
