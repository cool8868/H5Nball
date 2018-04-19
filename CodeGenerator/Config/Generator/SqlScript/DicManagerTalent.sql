
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmanagertalent_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalent_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalent_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicManagertalent_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalent_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalent_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicManagertalent_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalent_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalent_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicManagertalent_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalent_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalent_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicManagertalent_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagertalent_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagertalent_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicManagertalent_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_ManagerTalent]
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

CREATE PROCEDURE [dbo].P_DicManagertalent_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerTalent] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicManagertalent_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerTalent] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicManagertalent_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@SkillLevel int
	,@StepNo int
	,@SectNo int
	,@DriveFlag int
	,@ReqManagerLevel int
	,@DenyFlag bit
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ManagerTalent] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[SkillLevel]
	,[StepNo]
	,[SectNo]
	,[DriveFlag]
	,[ReqManagerLevel]
	,[DenyFlag]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@SkillLevel
    ,@StepNo
    ,@SectNo
    ,@DriveFlag
    ,@ReqManagerLevel
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

CREATE PROCEDURE [dbo].P_DicManagertalent_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillName nvarchar(80), -- SkillName
	@SkillLevel int, -- 等级
	@StepNo int, -- 阶段号
	@SectNo int, -- 分支号
	@DriveFlag int, -- 被动标记
	@ReqManagerLevel int, -- 要求经理等级
	@DenyFlag bit, -- 失效标记
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_ManagerTalent] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[SkillLevel] = @SkillLevel
	,[StepNo] = @StepNo
	,[SectNo] = @SectNo
	,[DriveFlag] = @DriveFlag
	,[ReqManagerLevel] = @ReqManagerLevel
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



