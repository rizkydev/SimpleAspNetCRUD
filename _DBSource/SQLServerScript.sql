USE [TestDB]
GO
/****** Object:  StoredProcedure [dbo].[ContactViewByID] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactViewByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactViewByID]
GO
/****** Object:  StoredProcedure [dbo].[ContactViewAll] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactViewAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactViewAll]
GO
/****** Object:  StoredProcedure [dbo].[ContactDeleteByID] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactDeleteByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactDeleteByID]
GO
/****** Object:  StoredProcedure [dbo].[ContactCreateOrUpdate] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactCreateOrUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactCreateOrUpdate]
GO
/****** Object:  Table [dbo].[Contact] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
DROP TABLE [dbo].[Contact]
GO
/****** Object:  Table [dbo].[Contact] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Address] [varchar](250) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactCreateOrUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ContactCreateOrUpdate] AS' 
END
GO
ALTER PROC [dbo].[ContactCreateOrUpdate]
@ContactID int,
@Name varchar(50),
@Mobile varchar(50),
@Address varchar(250)
AS
BEGIN
IF(@ContactID=0)
	BEGIN
	INSERT INTO Contact(Name,Mobile,Address)
	VALUES(@Name,@Mobile,@Address)
	END
ELSE
	BEGIN
	UPDATE Contact
	SET
		Name = @Name,
		Mobile = @Mobile,
		Address = @Address
	WHERE ContactID= @ContactID
	END

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactDeleteByID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ContactDeleteByID] AS' 
END

GO
ALTER PROC [dbo].[ContactDeleteByID]
@ContactID int
AS
	BEGIN
	DELETE FROM Contact
	WHERE ContactID = @ContactID
	END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactViewAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ContactViewAll] AS' 
END

GO
ALTER PROC [dbo].[ContactViewAll]
AS
	BEGIN
	SELECT *
	FROM Contact
	END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactViewByID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ContactViewByID] AS' 
END

GO
ALTER PROC [dbo].[ContactViewByID]
@ContactID int
As
	BEGIN
	SELECT *
	FROm Contact
	WHERE ContactID = @ContactID
	END

GO
