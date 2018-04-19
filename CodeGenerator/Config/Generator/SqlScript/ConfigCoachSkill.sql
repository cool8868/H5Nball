
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcoachskill_Delete    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachskill_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachskill_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachskill_GetById    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachskill_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachskill_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCoachskill_GetAll    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachskill_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachskill_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCoachskill_Insert    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachskill_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachskill_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachskill_Update    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachskill_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachskill_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCoachskill_Delete
	@CoachId int
AS

DELETE FROM [dbo].[Config_CoachSkill]
WHERE
	[CoachId] = @CoachId

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

CREATE PROCEDURE [dbo].P_ConfigCoachskill_GetById
	@CoachId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachSkill] with(nolock)
WHERE
	[CoachId] = @CoachId
	
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

CREATE PROCEDURE [dbo].P_ConfigCoachskill_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachSkill] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCoachskill_Insert
	@CoachId int
	,@SkillName nvarchar(20)
	,@TriggerCondition varchar(500)
	,@CD int
	,@TimeOfDuration varchar(50)
	,@TriggerProbability int
	,@Description varchar(500)
	,@PlusDescription varchar(500)
	,@Base0 decimal(4, 2)
	,@Base1 decimal(4, 2)
	,@Plus0 decimal(4, 2)
	,@Plus1 decimal(4, 2)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CoachSkill] (
	[CoachId]
	,[SkillName]
	,[TriggerCondition]
	,[CD]
	,[TimeOfDuration]
	,[TriggerProbability]
	,[Description]
	,[PlusDescription]
	,[Base0]
	,[Base1]
	,[Plus0]
	,[Plus1]
) VALUES (
    @CoachId
    ,@SkillName
    ,@TriggerCondition
    ,@CD
    ,@TimeOfDuration
    ,@TriggerProbability
    ,@Description
    ,@PlusDescription
    ,@Base0
    ,@Base1
    ,@Plus0
    ,@Plus1
)

select @CoachId

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

CREATE PROCEDURE [dbo].P_ConfigCoachskill_Update
	@CoachId int, 
	@SkillName nvarchar(20), 
	@TriggerCondition varchar(500), 
	@CD int, 
	@TimeOfDuration varchar(50), 
	@TriggerProbability int, 
	@Description varchar(500), 
	@PlusDescription varchar(500), 
	@Base0 decimal(4, 2), 
	@Base1 decimal(4, 2), 
	@Plus0 decimal(4, 2), 
	@Plus1 decimal(4, 2) 
AS



UPDATE [dbo].[Config_CoachSkill] SET
	[SkillName] = @SkillName
	,[TriggerCondition] = @TriggerCondition
	,[CD] = @CD
	,[TimeOfDuration] = @TimeOfDuration
	,[TriggerProbability] = @TriggerProbability
	,[Description] = @Description
	,[PlusDescription] = @PlusDescription
	,[Base0] = @Base0
	,[Base1] = @Base1
	,[Plus0] = @Plus0
	,[Plus1] = @Plus1
WHERE
	[CoachId] = @CoachId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


