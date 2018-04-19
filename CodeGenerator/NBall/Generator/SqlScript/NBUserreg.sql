
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbuserreg_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUserreg_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUserreg_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbUserreg_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUserreg_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUserreg_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbUserreg_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUserreg_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUserreg_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbUserreg_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUserreg_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUserreg_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbUserreg_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUserreg_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUserreg_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbUserreg_Delete
	@Account varchar(200)
AS

DELETE FROM [dbo].[NB_UserReg]
WHERE
	[Account] = @Account

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbUserreg_GetById
	@Account varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_UserReg] with(nolock)
WHERE
	[Account] = @Account
	
RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbUserreg_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_UserReg] with(nolock)
	
RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE [dbo].P_NbUserreg_Insert
	@Account varchar(200)
	,@Pwd varchar(50)
	,@Name varchar(50)
	,@Cert varchar(50)
	,@Birthday datetime
	,@RecordDate datetime
	,@LastOnlineMinutes int
	,@Status int
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[NB_UserReg] (
	[Account]
	,[Pwd]
	,[Name]
	,[Cert]
	,[Birthday]
	,[RecordDate]
	,[LastOnlineMinutes]
	,[Status]
	,[RowTime]
) VALUES (
    @Account
    ,@Pwd
    ,@Name
    ,@Cert
    ,@Birthday
    ,@RecordDate
    ,@LastOnlineMinutes
    ,@Status
    ,@RowTime
)

select @Account

RETURN 0


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbUserreg_Update
	@Account varchar(200), 
	@Pwd varchar(50), 
	@Name varchar(50), 
	@Cert varchar(50), 
	@Birthday datetime, 
	@RecordDate datetime, 
	@LastOnlineMinutes int, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[NB_UserReg] SET
	[Pwd] = @Pwd
	,[Name] = @Name
	,[Cert] = @Cert
	,[Birthday] = @Birthday
	,[RecordDate] = @RecordDate
	,[LastOnlineMinutes] = @LastOnlineMinutes
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Account] = @Account

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


