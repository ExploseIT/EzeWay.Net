

ALTER TABLE [dbo].[tblBlog]
ADD CONSTRAINT [FK_tblBlog_tblBlogStatus]
FOREIGN KEY ([blog_status]) REFERENCES [dbo].[tblBlogStatus]([blgs_id])
ON DELETE CASCADE
ON UPDATE CASCADE;

