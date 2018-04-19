
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmanagertalenttips_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalenttips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalenttips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicManagertalenttips_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalenttips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalenttips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicManagertalenttips_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalenttips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalenttips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicManagertalenttips_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalenttips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalenttips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicManagertalenttips_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalenttips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalenttips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicManagertalenttips_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_ManagerTalentTips]
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

CREATE PROCEDURE [dbo].P_DicManagertalenttips_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerTalentTips] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicManagertalenttips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerTalentTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicManagertalenttips_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@ActType nvarchar(80)
	,@ReqManagerLevel int
	,@DriveFlag int
	,@DriveFlagMemo nvarchar(80)
	,@LastTime nvarchar(80)
	,@BuffMemo nvarchar(500)
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ManagerTalentTips] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[ActType]
	,[ReqManagerLevel]
	,[DriveFlag]
	,[DriveFlagMemo]
	,[LastTime]
	,[BuffMemo]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@ActType
    ,@ReqManagerLevel
    ,@DriveFlag
    ,@DriveFlagMemo
    ,@LastTime
    ,@BuffMemo
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

CREATE PROCEDURE [dbo].P_DicManagertalenttips_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillName nvarchar(80), -- SkillName
	@ActType nvarchar(80), -- 动作类型
	@ReqManagerLevel int, -- 要求经理等级
	@DriveFlag int, -- 被动标记
	@DriveFlagMemo nvarchar(80), -- 被动标记描述
	@LastTime nvarchar(80), -- 持续时间
	@BuffMemo nvarchar(500), -- 效果描述
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_ManagerTalentTips] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[ActType] = @ActType
	,[ReqManagerLevel] = @ReqManagerLevel
	,[DriveFlag] = @DriveFlag
	,[DriveFlagMemo] = @DriveFlagMemo
	,[LastTime] = @LastTime
	,[BuffMemo] = @BuffMemo
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



