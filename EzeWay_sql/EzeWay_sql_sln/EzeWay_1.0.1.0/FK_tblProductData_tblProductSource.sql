

-- Alter tblProductData to add a FOREIGN KEY constraint
ALTER TABLE [dbo].[tblProductData]
ADD CONSTRAINT [FK_tblProductData_tblProductSource]
FOREIGN KEY ([prdSource]) REFERENCES [dbo].[tblProductSource]([prdsId])
ON DELETE CASCADE
ON UPDATE CASCADE;

