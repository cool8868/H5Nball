
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicguildskill_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGuildskill_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGuildskill_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicGuildskill_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGuildskill_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGuildskill_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicGuildskill_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGuildskill_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGuildskill_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicGuildskill_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGuildskill_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGuildskill_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicGuildskill_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGuildskill_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGuildskill_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicGuildskill_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_GuildSkill]
WHERE
	[SkillCode] = @SkillCode

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

CREATE PROCEDURE [dbo].P_DicGuildskill_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GuildSkill] with(nolock)
WHERE
	[SkillCode] = @SkillCode
	
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

CREATE PROCEDURE [dbo].P_DicGuildskill_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GuildSkill] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicGuildskill_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@MaxLevel int
	,@ReqGuildLevel int
	,@BuffMemo nvarchar(500)
	,@BaseValue int
	,@PlusValue int
	,@BuffLastMemo nvarchar(200)
	,@BuyCostActive int
	,@Icon varchar(20)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_GuildSkill] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[MaxLevel]
	,[ReqGuildLevel]
	,[BuffMemo]
	,[BaseValue]
	,[PlusValue]
	,[BuffLastMemo]
	,[BuyCostActive]
	,[Icon]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@MaxLevel
    ,@ReqGuildLevel
    ,@BuffMemo
    ,@BaseValue
    ,@PlusValue
    ,@BuffLastMemo
    ,@BuyCostActive
    ,@Icon
    ,@RowTime
)

select @SkillId

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

CREATE PROCEDURE [dbo].P_DicGuildskill_Update
	@SkillId int, 
	@SkillCode varchar(20), 
	@SkillName nvarchar(80), 
	@MaxLevel int, 
	@ReqGuildLevel int, 
	@BuffMemo nvarchar(500), 
	@BaseValue int, 
	@PlusValue int, 
	@BuffLastMemo nvarchar(200), 
	@BuyCostActive int, 
	@Icon varchar(20), 
	@RowTime datetime 
AS



UPDATE [dbo].[Dic_GuildSkill] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[MaxLevel] = @MaxLevel
	,[ReqGuildLevel] = @ReqGuildLevel
	,[BuffMemo] = @BuffMemo
	,[BaseValue] = @BaseValue
	,[PlusValue] = @PlusValue
	,[BuffLastMemo] = @BuffLastMemo
	,[BuyCostActive] = @BuyCostActive
	,[Icon] = @Icon
	,[RowTime] = @RowTime
WHERE
	[SkillCode] = @SkillCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



