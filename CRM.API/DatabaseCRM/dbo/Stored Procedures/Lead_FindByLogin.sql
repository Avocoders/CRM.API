create procedure [dbo].[Lead_FindByLogin]
	@login nvarchar(20)
as
begin
	select COUNT(*) FROM dbo.Lead WHERE [Login] = @login
end;