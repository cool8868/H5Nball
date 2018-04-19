
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbuser_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUser_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUser_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbUser_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUser_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUser_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbUser_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUser_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUser_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbUser_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUser_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUser_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbUser_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbUser_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbUser_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbUser_Delete
	@Account varchar(200)
AS

DELETE FROM [dbo].[NB_User]
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

CREATE PROCEDURE [dbo].P_NbUser_GetById
	@Account varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_User] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbUser_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_User] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbUser_Insert
	@Account varchar(200)
	,@Source nvarchar(50)
	,@LastLoginTime datetime
	,@LastLoginIp varchar(50)
	,@LastLoginDate datetime
	,@ContinuingLoginDay int
	,@RegisterIp varchar(50)
	,@RegisterDate datetime
	,@Status int
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[NB_User] (
	[Account]
	,[Source]
	,[LastLoginTime]
	,[LastLoginIp]
	,[LastLoginDate]
	,[ContinuingLoginDay]
	,[RegisterIp]
	,[RegisterDate]
	,[Status]
	,[RowTime]
) VALUES (
    @Account
    ,@Source
    ,@LastLoginTime
    ,@LastLoginIp
    ,@LastLoginDate
    ,@ContinuingLoginDay
    ,@RegisterIp
    ,@RegisterDate
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

CREATE PROCEDURE [dbo].P_NbUser_Update
	@Account varchar(200), -- Account
	@Source nvarchar(50), -- 用户来源
	@LastLoginTime datetime, -- 最近登录时间
	@LastLoginIp varchar(50), -- 最近登录ip
	@LastLoginDate datetime, 
	@ContinuingLoginDay int, 
	@RegisterIp varchar(50), -- 注册时的ip
	@RegisterDate datetime, 
	@Status int, -- Status
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[NB_User] SET
	[Source] = @Source
	,[LastLoginTime] = @LastLoginTime
	,[LastLoginIp] = @LastLoginIp
	,[LastLoginDate] = @LastLoginDate
	,[ContinuingLoginDay] = @ContinuingLoginDay
	,[RegisterIp] = @RegisterIp
	,[RegisterDate] = @RegisterDate
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


