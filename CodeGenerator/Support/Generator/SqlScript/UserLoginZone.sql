
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Userloginzone_Delete    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_UserloginZone_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_UserloginZone_Delete]
GO

/****** Object:  Stored Procedure [dbo].UserloginZone_GetById    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_UserloginZone_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_UserloginZone_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].UserloginZone_GetAll    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_UserloginZone_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_UserloginZone_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].UserloginZone_Insert    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_UserloginZone_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_UserloginZone_Insert]
GO

/****** Object:  Stored Procedure [dbo].UserloginZone_Update    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_UserloginZone_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_UserloginZone_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_UserloginZone_Delete
	@Account varchar(50)
AS

DELETE FROM [dbo].[UserLogin_Zone]
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

CREATE PROCEDURE [dbo].P_UserloginZone_GetById
	@Account varchar(50)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[UserLogin_Zone] with(nolock)
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

CREATE PROCEDURE [dbo].P_UserloginZone_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[UserLogin_Zone] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_UserloginZone_Insert
	@Account varchar(50)
	,@Platform varchar(50)
	,@LastLoginTime datetime
	,@LoginSties varchar(8000)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[UserLogin_Zone] (
	[Account]
	,[Platform]
	,[LastLoginTime]
	,[LoginSties]
	,[RowTime]
) VALUES (
    @Account
    ,@Platform
    ,@LastLoginTime
    ,@LoginSties
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

CREATE PROCEDURE [dbo].P_UserloginZone_Update
	@Account varchar(50), 
	@Platform varchar(50), -- 平台
	@LastLoginTime datetime, -- 最后登录时间
	@LoginSties varchar(8000), -- 登录过的区  s1,s2,s3
	@RowTime datetime 
AS



UPDATE [dbo].[UserLogin_Zone] SET
	[Platform] = @Platform
	,[LastLoginTime] = @LastLoginTime
	,[LoginSties] = @LoginSties
	,[RowTime] = @RowTime
WHERE
	[Account] = @Account

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



