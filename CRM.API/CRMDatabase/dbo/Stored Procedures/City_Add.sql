CREATE procedure [dbo].[City_Add]
	@name nvarchar(30)
as
begin
	insert into dbo.City ([Name]) values (@name);
	select SCOPE_IDENTITY();
end;