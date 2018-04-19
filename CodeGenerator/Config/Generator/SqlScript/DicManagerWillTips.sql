
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmanagerwilltips_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwilltips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwilltips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwilltips_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwilltips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwilltips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicManagerwilltips_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwilltips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwilltips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicManagerwilltips_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwilltips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwilltips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwilltips_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwilltips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwilltips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicManagerwilltips_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_ManagerWillTips]
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

CREATE PROCEDURE [dbo].P_DicManagerwilltips_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWillTips] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicManagerwilltips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWillTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicManagerwilltips_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@WillRank int
	,@ActType nvarchar(80)
	,@DriveFlag int
	,@DriveFlagMemo nvarchar(80)
	,@BuffMemo nvarchar(500)
	,@BuffArg money
	,@BuffArg2 money
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ManagerWillTips] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[WillRank]
	,[ActType]
	,[DriveFlag]
	,[DriveFlagMemo]
	,[BuffMemo]
	,[BuffArg]
	,[BuffArg2]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@WillRank
    ,@ActType
    ,@DriveFlag
    ,@DriveFlagMemo
    ,@BuffMemo
    ,@BuffArg
    ,@BuffArg2
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

CREATE PROCEDURE [dbo].P_DicManagerwilltips_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillName nvarchar(80), -- SkillName
	@WillRank int, -- 意志分级
	@ActType nvarchar(80), -- 动作类型
	@DriveFlag int, -- 被动标记
	@DriveFlagMemo nvarchar(80), -- 被动标记描述
	@BuffMemo nvarchar(500), -- 效果描述
	@BuffArg money, -- BuffArg
	@BuffArg2 money, -- BuffArg2
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_ManagerWillTips] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[WillRank] = @WillRank
	,[ActType] = @ActType
	,[DriveFlag] = @DriveFlag
	,[DriveFlagMemo] = @DriveFlagMemo
	,[BuffMemo] = @BuffMemo
	,[BuffArg] = @BuffArg
	,[BuffArg2] = @BuffArg2
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



