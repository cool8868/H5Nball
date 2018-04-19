
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmanagerwill_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwill_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwill_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwill_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwill_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwill_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicManagerwill_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwill_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwill_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicManagerwill_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwill_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwill_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwill_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwill_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwill_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicManagerwill_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_ManagerWill]
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

CREATE PROCEDURE [dbo].P_DicManagerwill_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWill] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicManagerwill_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWill] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicManagerwill_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@WillRank int
	,@DriveFlag int
	,@PartMap varchar(200)
	,@CombSkillCode varchar(20)
	,@MaxCombLevel int
	,@BuffMemo nvarchar(500)
	,@BuffArg money
	,@BuffArg2 money
	,@SortNo int
	,@DenyFlag bit
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ManagerWill] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[WillRank]
	,[DriveFlag]
	,[PartMap]
	,[CombSkillCode]
	,[MaxCombLevel]
	,[BuffMemo]
	,[BuffArg]
	,[BuffArg2]
	,[SortNo]
	,[DenyFlag]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@WillRank
    ,@DriveFlag
    ,@PartMap
    ,@CombSkillCode
    ,@MaxCombLevel
    ,@BuffMemo
    ,@BuffArg
    ,@BuffArg2
    ,@SortNo
    ,@DenyFlag
    ,@Icon
    ,@Memo
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

CREATE PROCEDURE [dbo].P_DicManagerwill_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillName nvarchar(80), -- SkillName
	@WillRank int, -- 意志分级
	@DriveFlag int, -- 被动标记
	@PartMap varchar(200), -- 球员组成
	@CombSkillCode varchar(20), -- 组合技能
	@MaxCombLevel int, -- 组合等级
	@BuffMemo nvarchar(500), -- BuffMemo
	@BuffArg money, -- BuffArg
	@BuffArg2 money, -- BuffArg2
	@SortNo int, -- SortNo
	@DenyFlag bit, -- 失效标记
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_ManagerWill] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[WillRank] = @WillRank
	,[DriveFlag] = @DriveFlag
	,[PartMap] = @PartMap
	,[CombSkillCode] = @CombSkillCode
	,[MaxCombLevel] = @MaxCombLevel
	,[BuffMemo] = @BuffMemo
	,[BuffArg] = @BuffArg
	,[BuffArg2] = @BuffArg2
	,[SortNo] = @SortNo
	,[DenyFlag] = @DenyFlag
	,[Icon] = @Icon
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[SkillCode] = @SkillCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



