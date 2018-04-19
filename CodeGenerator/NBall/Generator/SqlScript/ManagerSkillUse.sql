
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managerskilluse_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillUse_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillUse_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillUse_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillUse_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillUse_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerskillUse_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillUse_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillUse_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerskillUse_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillUse_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillUse_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillUse_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillUse_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillUse_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerskillUse_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[ManagerSkill_Use]
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

CREATE PROCEDURE [dbo].P_ManagerskillUse_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_Use] with(nolock)
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

CREATE PROCEDURE [dbo].P_ManagerskillUse_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_Use] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerskillUse_Insert
	@SyncFlag int , 
	@PlayerSkills varchar(500) , 
	@ManagerSkills varchar(800) , 
	@CoachSkill varchar(20) , 
	@Talents varchar(200) , 
	@Wills varchar(200) , 
	@Combs varchar(200) , 
	@Suits varchar(200) , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ManagerSkill_Use] (
	[ManagerId],
	[SyncFlag]
	,[PlayerSkills]
	,[ManagerSkills]
	,[CoachSkill]
	,[Talents]
	,[Wills]
	,[Combs]
	,[Suits]
	,[RowTime]
) VALUES (
	@ManagerId,
    @SyncFlag
    ,@PlayerSkills
    ,@ManagerSkills
    ,@CoachSkill
    ,@Talents
    ,@Wills
    ,@Combs
    ,@Suits
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

CREATE PROCEDURE [dbo].P_ManagerskillUse_Update
	@ManagerId uniqueidentifier, -- ManagerId
	@SyncFlag int, -- SyncFlag
	@PlayerSkills varchar(500), -- 球员技能
	@ManagerSkills varchar(800), -- 经理技能
	@CoachSkill varchar(20), -- 教练技能
	@Talents varchar(200), -- 主动天赋
	@Wills varchar(200), -- 主动意志
	@Combs varchar(200), -- 组合
	@Suits varchar(200), -- 套装
	@RowTime datetime, -- RowTime
	@RowVersion timestamp -- RowVersion
AS



UPDATE [dbo].[ManagerSkill_Use] SET
	[SyncFlag] = @SyncFlag
	,[PlayerSkills] = @PlayerSkills
	,[ManagerSkills] = @ManagerSkills
	,[CoachSkill] = @CoachSkill
	,[Talents] = @Talents
	,[Wills] = @Wills
	,[Combs] = @Combs
	,[Suits] = @Suits
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



