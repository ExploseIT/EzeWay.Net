
ALTER TABLE [dbo].[tblBlogOrder]
ADD CONSTRAINT [FK_tblBlogOrder_tblBlog]
FOREIGN KEY ([blgo_uid]) REFERENCES [dbo].[tblBlog]([blog_id])
ON DELETE CASCADE
ON UPDATE CASCADE;