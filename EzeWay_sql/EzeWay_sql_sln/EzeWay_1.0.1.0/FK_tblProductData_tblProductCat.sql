

-- Alter tblProductData to add a FOREIGN KEY constraint
ALTER TABLE [dbo].[tblProductData]
ADD CONSTRAINT [FK_tblProductData_tblProductCat]
FOREIGN KEY ([prdCategory]) REFERENCES [dbo].[tblProductCat]([prdcId])
ON DELETE CASCADE
ON UPDATE CASCADE;
