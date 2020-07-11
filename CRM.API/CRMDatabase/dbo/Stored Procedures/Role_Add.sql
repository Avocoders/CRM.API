CREATE procedure [dbo].[Role_Add]
	@name nvarchar(30)
as
begin
	insert into dbo.[Role] ([Name]) values (@name)
	select cast(SCOPE_IDENTITY()as int);
end