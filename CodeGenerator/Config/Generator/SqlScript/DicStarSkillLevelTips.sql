
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicstarskillleveltips_Delete    Script Date: 2016年10月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskillleveltips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskillleveltips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicStarskillleveltips_GetById    Script Date: 2016年10月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskillleveltips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskillleveltips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicStarskillleveltips_GetAll    Script Date: 2016年10月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskillleveltips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskillleveltips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicStarskillleveltips_Insert    Script Date: 2016年10月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskillleveltips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskillleveltips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicStarskillleveltips_Update    Script Date: 2016年10月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskillleveltips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskillleveltips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicStarskillleveltips_Delete
	@SkillId int
AS

DELETE FROM [dbo].[Dic_StarSkillLevelTips]
WHERE
	[SkillId] = @SkillId

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

CREATE PROCEDURE [dbo].P_DicStarskillleveltips_GetById
	@SkillId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkillLevelTips] with(nolock)
WHERE
	[SkillId] = @SkillId
	
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

CREATE PROCEDURE [dbo].P_DicStarskillleveltips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkillLevelTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicStarskillleveltips_Insert
	@SkillId int
	,@StarSkillCode varchar(20)
	,@StarSkillName nvarchar(80)
	,@ActType int
	,@ActTypeMemo nvarchar(80)
	,@Pid int
	,@ReqStrength int
	,@TriggerAction nvarchar(80)
	,@TriggerRate nvarchar(80)
	,@CD nvarchar(80)
	,@LastTime nvarchar(80)
	,@BuffMemo nvarchar(500)
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@PlusSkillCode varchar(20)
	,@PlusSkillName nvarchar(80)
	,@PlusSkillMemo nvarchar(500)
	,@RowTime datetime
	,@OrigVal money
	,@StepVal money
	,@OrigVal2 money
	,@StepVal2 money
	,@OrigVal3 money
	,@StepVal3 money
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_StarSkillLevelTips] (
	[SkillId]
	,[StarSkillCode]
	,[StarSkillName]
	,[ActType]
	,[ActTypeMemo]
	,[Pid]
	,[ReqStrength]
	,[TriggerAction]
	,[TriggerRate]
	,[CD]
	,[LastTime]
	,[BuffMemo]
	,[Icon]
	,[Memo]
	,[PlusSkillCode]
	,[PlusSkillName]
	,[PlusSkillMemo]
	,[RowTime]
	,[OrigVal]
	,[StepVal]
	,[OrigVal2]
	,[StepVal2]
	,[OrigVal3]
	,[StepVal3]
) VALUES (
    @SkillId
    ,@StarSkillCode
    ,@StarSkillName
    ,@ActType
    ,@ActTypeMemo
    ,@Pid
    ,@ReqStrength
    ,@TriggerAction
    ,@TriggerRate
    ,@CD
    ,@LastTime
    ,@BuffMemo
    ,@Icon
    ,@Memo
    ,@PlusSkillCode
    ,@PlusSkillName
    ,@PlusSkillMemo
    ,@RowTime
    ,@OrigVal
    ,@StepVal
    ,@OrigVal2
    ,@StepVal2
    ,@OrigVal3
    ,@StepVal3
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

CREATE PROCEDURE [dbo].P_DicStarskillleveltips_Update
	@SkillId int, 
	@StarSkillCode varchar(20), 
	@StarSkillName nvarchar(80), 
	@ActType int, 
	@ActTypeMemo nvarchar(80), 
	@Pid int, 
	@ReqStrength int, 
	@TriggerAction nvarchar(80), 
	@TriggerRate nvarchar(80), 
	@CD nvarchar(80), 
	@LastTime nvarchar(80), 
	@BuffMemo nvarchar(500), 
	@Icon varchar(20), 
	@Memo nvarchar(500), 
	@PlusSkillCode varchar(20), 
	@PlusSkillName nvarchar(80), 
	@PlusSkillMemo nvarchar(500), 
	@RowTime datetime, 
	@OrigVal money, 
	@StepVal money, 
	@OrigVal2 money, 
	@StepVal2 money, 
	@OrigVal3 money, 
	@StepVal3 money 
AS



UPDATE [dbo].[Dic_StarSkillLevelTips] SET
	[StarSkillCode] = @StarSkillCode
	,[StarSkillName] = @StarSkillName
	,[ActType] = @ActType
	,[ActTypeMemo] = @ActTypeMemo
	,[Pid] = @Pid
	,[ReqStrength] = @ReqStrength
	,[TriggerAction] = @TriggerAction
	,[TriggerRate] = @TriggerRate
	,[CD] = @CD
	,[LastTime] = @LastTime
	,[BuffMemo] = @BuffMemo
	,[Icon] = @Icon
	,[Memo] = @Memo
	,[PlusSkillCode] = @PlusSkillCode
	,[PlusSkillName] = @PlusSkillName
	,[PlusSkillMemo] = @PlusSkillMemo
	,[RowTime] = @RowTime
	,[OrigVal] = @OrigVal
	,[StepVal] = @StepVal
	,[OrigVal2] = @OrigVal2
	,[StepVal2] = @StepVal2
	,[OrigVal3] = @OrigVal3
	,[StepVal3] = @StepVal3
WHERE
	[SkillId] = @SkillId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



