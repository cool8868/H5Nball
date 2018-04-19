
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Playerkillrevenge_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillRevenge_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillRevenge_Delete]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillRevenge_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillRevenge_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillRevenge_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PlayerkillRevenge_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillRevenge_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillRevenge_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PlayerkillRevenge_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillRevenge_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillRevenge_Insert]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillRevenge_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillRevenge_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillRevenge_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PlayerkillRevenge_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[PlayerKill_Revenge]
WHERE
	[Idx] = @Idx

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

CREATE PROCEDURE [dbo].P_PlayerkillRevenge_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Revenge] with(nolock)
WHERE
	[Idx] = @Idx
	
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

CREATE PROCEDURE [dbo].P_PlayerkillRevenge_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Revenge] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PlayerkillRevenge_Insert
	@ManagerId uniqueidentifier , 
	@HomeId uniqueidentifier , 
	@HomeLogo varchar(200) , 
	@HomeName nvarchar(50) , 
	@HomeScore int , 
	@AwayScore int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[PlayerKill_Revenge] (
	[ManagerId]
	,[HomeId]
	,[HomeLogo]
	,[HomeName]
	,[HomeScore]
	,[AwayScore]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@HomeId
    ,@HomeLogo
    ,@HomeName
    ,@HomeScore
    ,@AwayScore
    ,@Status
    ,@RowTime
    ,@UpdateTime
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_PlayerkillRevenge_Update
	@Idx bigint, 
	@ManagerId uniqueidentifier, 
	@HomeId uniqueidentifier, 
	@HomeLogo varchar(200), 
	@HomeName nvarchar(50), -- 对手名称
	@HomeScore int, 
	@AwayScore int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[PlayerKill_Revenge] SET
	[ManagerId] = @ManagerId
	,[HomeId] = @HomeId
	,[HomeLogo] = @HomeLogo
	,[HomeName] = @HomeName
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



