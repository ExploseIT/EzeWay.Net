USE [ezeway_db]
GO

/****** Object:  Table [dbo].[tblPost]    Script Date: 28/01/2025 19:25:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblBlog]') AND type in (N'U'))
CREATE TABLE [dbo].[tblBlog](
	[blog_id] [uniqueidentifier] NOT NULL,
	[blog_author] [uniqueidentifier] NOT NULL,
	[blog_dt_created] [datetime] NOT NULL,
	[blog_dt_modified] [datetime] NOT NULL,
	[blog_dt_display] [datetime] NOT NULL,
	[blog_content] [text] NOT NULL,
	[blog_title] [nvarchar](500) NOT NULL,
	[blog_status] [nvarchar](20) NOT NULL,
	[blog_url] [nvarchar](100) NOT NULL
 CONSTRAINT [PK_tblBlog] PRIMARY KEY CLUSTERED 
(
	[blog_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblBlogStatus]') AND type in (N'U'))

CREATE TABLE [dbo].[tblBlogStatus](
	[blgs_id] [nvarchar](20) NOT NULL,
	[blgs_name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblBlogStatus] PRIMARY KEY CLUSTERED 
(
	[blgs_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


alter procedure spBlogUpdate
			@blog_id uniqueidentifier
           ,@blog_author uniqueidentifier
           ,@blog_dt_created datetime
           ,@blog_dt_modified datetime
		   ,@blog_dt_display datetime
           ,@blog_content text
           ,@blog_title nvarchar(500)
		   ,@blog_url nvarchar(100)
           ,@blog_status nvarchar(20)
as 
begin
if @blog_id = N'00000000-0000-0000-0000-000000000000'
 set @blog_id = NEWID()
 if exists (select * from tblBlog where blog_id=@blog_id)
 UPDATE [dbo].[tblBlog]
   SET [blog_id] = @blog_id
      ,[blog_author] = @blog_author
      ,[blog_dt_created] = @blog_dt_created
      ,[blog_dt_modified] = @blog_dt_modified
	  ,[blog_dt_display] = @blog_dt_display
      ,[blog_content] = @blog_content
      ,[blog_title] = @blog_title
      ,[blog_status] = @blog_status
	  ,[blog_url] = @blog_url
 WHERE blog_id=@blog_id
 else
INSERT INTO [dbo].[tblBlog]
           ([blog_id]
           ,[blog_author]
           ,[blog_dt_created]
           ,[blog_dt_modified]
		   ,[blog_dt_display]
           ,[blog_content]
           ,[blog_title]
           ,[blog_status]
		   ,[blog_url]
		   )
     VALUES
           (@blog_id
           ,@blog_author
           ,@blog_dt_created
           ,@blog_dt_modified
		   ,@blog_dt_display
           ,@blog_content
           ,@blog_title
           ,@blog_status
		   ,@blog_url
		   )
	select * from vwBlog where blog_id=@blog_id
end
go

alter procedure spBlogReadById
	@blog_id uniqueidentifier
as
begin
	select * from vwBlog where blog_id=@blog_id
end
go

alter procedure spBlogReadByUrl
	@blog_url nvarchar(100)
as
begin
	select * from vwBlog where blog_url=@blog_url
end
go


alter procedure spBlogList
	@is_local bit
as
begin
	select * from vwBlog
	where (blog_status = 'blgs_live' or blog_status='blgs_local' and @is_local=1)
	order by blog_dt_display
end
go


Alter View vwBlog
as
SELECT [blog_id]
      ,[blog_author]
      ,[blog_dt_created]
      ,[blog_dt_modified]
	  ,[blog_dt_display]
      ,[blog_content]
      ,[blog_title]
      ,[blog_status]
	  ,[blog_url]
	  ,[blgs_name]
  FROM [dbo].[tblBlog] blog
  inner join [dbo].[tblBlogStatus] blgs on blog.blog_status=blgs.blgs_id
  
GO

