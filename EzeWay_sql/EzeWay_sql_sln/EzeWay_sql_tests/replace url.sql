use ezeway_db
go

declare @urllive nvarchar(50) = 'https://ezeway.co.uk'
declare @urllocal nvarchar(50) = 'http://localhost/ezeway'
declare @rep nvarchar(50) = '[url]'
declare @reprep nvarchar(50) = '[[]url]'
declare @lenurllive int = len(@urllive)
declare @lenurllocal int = len(@urllocal)
declare @lenrep int = len(@rep)
declare @lenreprep int = len(@reprep)
declare @lenrepbefore int = 20
declare @lenrepafter int = 20


select stuff(blog_content, patindex('%'+@urllive+'%',blog_content), @lenurllive, @rep), patindex('%'+@urllive+'%',blog_content)
from tblBlog
where patindex('%'+@urllive+'%',blog_content) > 0

select stuff(blog_content, patindex('%'+@urllocal+'%',blog_content), @lenurllocal, @rep), patindex('%'+@urllocal+'%',blog_content)
from tblBlog
where patindex('%'+@urllocal+'%',blog_content) > 0

update tblBlog set blog_content= stuff(blog_content, patindex('%'+@urllive+'%',blog_content), @lenurllive, @rep)
where patindex('%'+@urllive+'%',blog_content) > 0

update tblBlog set blog_content= stuff(blog_content, patindex('%'+@urllocal +'%',blog_content), @lenurllocal, @rep)
where patindex('%'+@urllocal+'%',blog_content) > 0

select substring(blog_content, patindex('%'+@reprep+'%',blog_content)-@lenrepbefore, @lenreprep+@lenrepbefore+@lenrepafter),  patindex('%'+@reprep+'%',blog_content) from tblBlog
where patindex('%'+@reprep+'%',blog_content) > 0

