
use ezeway_db
go

if not exists (select * from sys.objects where name='tblCustomer' and type='U')
CREATE TABLE [dbo].[tblCustomer](
	[cust_id] [uniqueidentifier] NOT NULL,
	[cust_firstname] [nvarchar](100) NULL,
	[cust_lastname] [nvarchar](100) NULL,
	[cust_email] [nvarchar](50) NULL,
	[cust_mobile] [nvarchar](20) NULL,
	[cust_pwd] [nvarchar](100) NULL,
	[cust_signup_dt] [datetime] NOT NULL,
	[cust_browserdetails] [nvarchar](300) NOT NULL,
	[cust_flags] [nvarchar](500) NULL,
 CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED 
(
	[cust_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


if exists (select * from sys.objects where name='vwCustomer' and type='V')
DROP VIEW [dbo].[vwCustomer]
GO
CREATE view [dbo].[vwCustomer] AS
SELECT [cust_id]
      ,isnull([cust_firstname],'') cust_firstname
      ,isnull([cust_lastname],'') cust_lastname
      ,isnull([cust_email],'') cust_email
      ,isnull([cust_mobile],'') cust_mobile
      ,isnull([cust_pwd],'') cust_pwd
      ,[cust_signup_dt]
	  ,isnull([cust_browserdetails],'') cust_browserdetails 
      ,cast (json_value([cust_flags], '$.cust_avon') as bit) cust_avon
	  ,cast (json_value([cust_flags], '$.cust_vivamk') as bit) cust_vivamk
	  ,cast (json_value([cust_flags], '$.cust_offers') as bit) cust_offers
	  
  FROM [dbo].[tblCustomer]
GO

if exists (select * from sys.objects where name='spCustomerListByEmail' and type='P')
Drop procedure spCustomerListByEmail
go

Create procedure spCustomerListByEmail
 @cust_email nvarchar(100)
as
SELECT *
  FROM [dbo].[vwCustomer]
  where isnull(@cust_email,'')<>'' and @cust_email=cust_email
GO

if exists (select * from sys.objects where name='spCustomerListByMobile' and type='P')
Drop procedure spCustomerListByMobile
go

Create procedure spCustomerListByMobile
 @cust_mobile nvarchar(100)
as
SELECT *
  FROM [dbo].[vwCustomer]
  where isnull(@cust_mobile,'')<>'' and @cust_mobile=cust_mobile
GO

if exists (select * from sys.objects where name='spCustomerListByEmailOrMobile' and type='P')
Drop procedure spCustomerListByEmailOrMobile
go

Create procedure spCustomerListByEmailOrMobile
 @cust_email nvarchar(100)
 ,@cust_mobile nvarchar(100)
as
SELECT *
  FROM [dbo].[vwCustomer]
  where (isnull(@cust_email,'')<>'' and @cust_email=cust_email)
  or (isnull(@cust_mobile,'')<>'' and @cust_mobile=cust_mobile)
GO

if exists (select * from sys.objects where name='spCustomerUpdate' and type='P')
Drop procedure spCustomerUpdate
go
Create procedure spCustomerUpdate
			@cust_id uniqueidentifier
           ,@cust_firstname nvarchar(100)
           ,@cust_lastname nvarchar(100)
           ,@cust_email nvarchar(50)
           ,@cust_mobile nvarchar(20)
           ,@cust_pwd nvarchar(100)
           ,@cust_signup_dt datetime
		   ,@cust_browserdetails nvarchar(300)
		   ,@cust_avon bit
		   ,@cust_vivamk bit
		   ,@cust_offers bit

as
begin
declare @cust_flags nvarchar(100)
set @cust_flags = N'{"cust_avon":false, "cust_vivamk":false, "cust_offers":false}'

set @cust_flags = JSON_MODIFY(@cust_flags, '$.cust_avon',@cust_avon)
set @cust_flags = JSON_MODIFY(@cust_flags, '$.cust_vivamk',@cust_vivamk)
set @cust_flags = JSON_MODIFY(@cust_flags, '$.cust_offers',@cust_offers)
if not exists (select * from vwCustomer where cust_id=@cust_id) 
INSERT INTO [dbo].[tblCustomer]
           ([cust_id]
           ,[cust_firstname]
           ,[cust_lastname]
           ,[cust_email]
           ,[cust_mobile]
           ,[cust_pwd]
           ,[cust_signup_dt]
		   ,[cust_browserdetails]
		   ,[cust_flags]
		   )
     VALUES
           (@cust_id
           ,@cust_firstname
           ,@cust_lastname
           ,@cust_email
           ,@cust_mobile
           ,@cust_pwd
           ,@cust_signup_dt
		   ,@cust_browserdetails
		   ,@cust_flags
		   )
else
UPDATE [dbo].[tblCustomer]
   SET [cust_id] = @cust_id
      ,[cust_firstname] = @cust_firstname
      ,[cust_lastname] = @cust_lastname
      ,[cust_email] = @cust_email
      ,[cust_mobile] = @cust_mobile
      ,[cust_pwd] = @cust_pwd
      ,[cust_signup_dt] = @cust_signup_dt
	  ,[cust_browserdetails] = @cust_browserdetails
      ,[cust_flags] = @cust_flags
 WHERE cust_id=@cust_id

 select * from vwCustomer where cust_id=@cust_id
end
GO

if exists (select * from sys.objects where name='spCustomerReadById' and type='P')
Drop procedure spCustomerReadById
go
Create procedure spCustomerReadById
	@cust_id uniqueidentifier
as
begin
 select * from vwCustomer where cust_id=@cust_id
end
GO

if exists (select * from sys.objects where name='spCustomerNextId' and type='P')
Drop procedure spCustomerNextId
go
Create procedure spCustomerNextId
as
declare @cust_id uniqueidentifier
select @cust_id = newid()
while exists (select * from tblCustomer where cust_id=@cust_id)
begin
select @cust_id = newid()
end
select @cust_id
go

if not exists (select * from sys.objects where name='tblPageData' and type='U')
CREATE TABLE [dbo].[tblPageData](
	[pd_id] [uniqueidentifier] NOT NULL,
	[pd_page] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblPageData] PRIMARY KEY CLUSTERED 
(
	[pd_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

if not exists (select * from sys.objects where name='FK_tblPageData_tblPageData' and type='F') 
ALTER TABLE [dbo].[tblPageData]  WITH CHECK ADD  CONSTRAINT [FK_tblPageData_tblPageData] FOREIGN KEY([pd_id])
REFERENCES [dbo].[tblPageData] ([pd_id])
GO

ALTER TABLE [dbo].[tblPageData] CHECK CONSTRAINT [FK_tblPageData_tblPageData]
GO

if exists (select * from sys.objects where name='spPageDataRead' and type='P')
drop procedure spPageDataRead
go
create procedure spPageDataRead
	@pd_id uniqueidentifier
as
begin
SELECT [pd_id]
      ,[pd_page]
  FROM [dbo].[tblPageData]
  where pd_id=@pd_id
end
GO

if exists (select * from sys.objects where name='spPageDataUpdate' and type='P')
drop procedure spPageDataUpdate
go
create procedure spPageDataUpdate
	@pd_id uniqueidentifier
	, @pd_page nvarchar(100)

as
begin
if not exists (select * from tblPageData where pd_id=@pd_id)

INSERT INTO [dbo].[tblPageData]
           ([pd_id]
           ,[pd_page])
     VALUES
           (@pd_id
           ,@pd_page)

else
UPDATE [dbo].[tblPageData]
   SET [pd_id] = @pd_id
      ,[pd_page] = @pd_page
 WHERE pd_id = @pd_id

select * from tblPageData where pd_id=@pd_id

end
GO

if not exists (select * from sys.objects where name='tblPageContent' and type='U')
CREATE TABLE [dbo].[tblPageContent](
	[pc_name] [nvarchar](100) NOT NULL,
	[pc_value] [nvarchar](max) NOT NULL,
	[pc_pdpd_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tblPageContent_1] PRIMARY KEY CLUSTERED 
(
	[pc_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
if not exists (select * from sys.objects where name='FK_tblPageContent_tblPageData' and type='F'
)
ALTER TABLE [dbo].[tblPageContent]  WITH CHECK ADD  CONSTRAINT [FK_tblPageContent_tblPageData] FOREIGN KEY([pc_pdpd_id])
REFERENCES [dbo].[tblPageData] ([pd_id])
GO

ALTER TABLE [dbo].[tblPageContent] CHECK CONSTRAINT [FK_tblPageContent_tblPageData]
GO

if exists (select * from sys.objects where name='spPageContentList' and type='P')
drop procedure spPageContentList
go
create procedure spPageContentList
	@pd_id uniqueidentifier
as
begin
SELECT [pc_name]
      ,[pc_value]
      ,[pc_pdpd_id]
  FROM [dbo].[tblPageContent]
  where pc_pdpd_id = @pd_id
end
GO


if exists (select * from sys.objects where name='spPageContentUpdate' and type='P')
drop procedure spPageContentUpdate
go

create procedure spPageContentUpdate
	@pc_name nvarchar(100)
	, @pc_value nvarchar(max)
	, @pc_pd uniqueidentifier

as
begin
if not exists (select * from tblPageContent where pc_name=@pc_name and pc_pdpd_id=@pc_pd)
INSERT INTO [dbo].[tblPageContent]
           ([pc_name]
           ,[pc_value]
           ,[pc_pdpd_id])
     VALUES
           (@pc_name
           ,@pc_value
           ,@pc_pd)
else
UPDATE [dbo].[tblPageContent]
   SET [pc_name] = @pc_name
      ,[pc_value] = @pc_value
      ,[pc_pdpd_id] = @pc_pd
 WHERE pc_name=@pc_name and pc_pdpd_id=@pc_pd

 select * from tblPageContent where pc_name=@pc_name and pc_pdpd_id=@pc_pd

end
GO




