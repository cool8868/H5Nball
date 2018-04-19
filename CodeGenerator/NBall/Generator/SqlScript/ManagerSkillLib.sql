
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managerskilllib_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillLib_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillLib_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillLib_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillLib_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillLib_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerskillLib_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillLib_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillLib_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerskillLib_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillLib_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillLib_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillLib_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillLib_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillLib_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerskillLib_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[ManagerSkill_Lib]
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_ManagerskillLib_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_Lib] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_ManagerskillLib_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_Lib] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerskillLib_Insert
	@SyncTalentPoint int , 
	@MaxTalentPoint int , 
	@MaxWillNumber int , 
	@TodoTalents varchar(200) , 
	@NodoTalents varchar(200) , 
	@TodoWills varchar(1000) , 
	@NodoWills varchar(500) , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ManagerSkill_Lib] (
	[ManagerId],
	[SyncTalentPoint]
	,[MaxTalentPoint]
	,[MaxWillNumber]
	,[TodoTalents]
	,[NodoTalents]
	,[TodoWills]
	,[NodoWills]
	,[RowTime]
) VALUES (
	@ManagerId,
    @SyncTalentPoint
    ,@MaxTalentPoint
    ,@MaxWillNumber
    ,@TodoTalents
    ,@NodoTalents
    ,@TodoWills
    ,@NodoWills
    ,@RowTime
)




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

CREATE PROCEDURE [dbo].P_ManagerskillLib_Update
	@ManagerId uniqueidentifier, -- ManagerId
	@SyncTalentPoint int, -- SyncTalentPoint
	@MaxTalentPoint int, -- 技能点
	@MaxWillNumber int, -- 意志数
	@TodoTalents varchar(200), -- 主动天赋
	@NodoTalents varchar(200), -- 被动天赋
	@TodoWills varchar(1000), -- 主动意志
	@NodoWills varchar(500), -- 被动意志
	@RowTime datetime, -- RowTime
	@RowVersion timestamp -- RowVersion
AS



UPDATE [dbo].[ManagerSkill_Lib] SET
	[SyncTalentPoint] = @SyncTalentPoint
	,[MaxTalentPoint] = @MaxTalentPoint
	,[MaxWillNumber] = @MaxWillNumber
	,[TodoTalents] = @TodoTalents
	,[NodoTalents] = @NodoTalents
	,[TodoWills] = @TodoWills
	,[NodoWills] = @NodoWills
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



