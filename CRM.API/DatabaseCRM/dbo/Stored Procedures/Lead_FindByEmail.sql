create procedure [dbo].[Lead_FindByEmail]
	@email nvarchar(30)
as
begin
	select COUNT(*) FROM dbo.Lead WHERE [Email] = @email
end;