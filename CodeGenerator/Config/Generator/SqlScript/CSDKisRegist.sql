
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Csdkisregist_Delete    Script Date: 2016年5月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Csdkisregist_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Csdkisregist_Delete]
GO

/****** Object:  Stored Procedure [dbo].Csdkisregist_GetById    Script Date: 2016年5月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Csdkisregist_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Csdkisregist_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Csdkisregist_GetAll    Script Date: 2016年5月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Csdkisregist_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Csdkisregist_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Csdkisregist_Insert    Script Date: 2016年5月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Csdkisregist_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Csdkisregist_Insert]
GO

/****** Object:  Stored Procedure [dbo].Csdkisregist_Update    Script Date: 2016年5月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Csdkisregist_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Csdkisregist_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Csdkisregist_Delete
	@idx int
AS

DELETE FROM [dbo].[CSDKisRegist]
WHERE
	[idx] = @idx

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

CREATE PROCEDURE [dbo].P_Csdkisregist_GetById
	@idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CSDKisRegist] with(nolock)
WHERE
	[idx] = @idx
	
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

CREATE PROCEDURE [dbo].P_Csdkisregist_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CSDKisRegist] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Csdkisregist_Insert
	@ret text , 
	@msg text , 
	@roleId text , 
	@roleName text , 
	@roleLevel text , 
	@serverNo text , 
	@serverId text , 
	@serverName text , 
    @idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CSDKisRegist] (
	[ret]
	,[msg]
	,[roleId]
	,[roleName]
	,[roleLevel]
	,[serverNo]
	,[serverId]
	,[serverName]
) VALUES (
    @ret
    ,@msg
    ,@roleId
    ,@roleName
    ,@roleLevel
    ,@serverNo
    ,@serverId
    ,@serverName
)


SET @idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_Csdkisregist_Update
	@idx int, 
	@ret text, 
	@msg text, 
	@roleId text, 
	@roleName text, 
	@roleLevel text, 
	@serverNo text, 
	@serverId text, 
	@serverName text 
AS



UPDATE [dbo].[CSDKisRegist] SET
	[ret] = @ret
	,[msg] = @msg
	,[roleId] = @roleId
	,[roleName] = @roleName
	,[roleLevel] = @roleLevel
	,[serverNo] = @serverNo
	,[serverId] = @serverId
	,[serverName] = @serverName
WHERE
	[idx] = @idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


