
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationnpctemp_Delete    Script Date: 2017年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpctemp_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpctemp_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpctemp_GetById    Script Date: 2017年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpctemp_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpctemp_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationnpctemp_GetAll    Script Date: 2017年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpctemp_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpctemp_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpctemp_Insert    Script Date: 2017年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpctemp_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpctemp_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpctemp_Update    Script Date: 2017年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpctemp_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpctemp_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationnpctemp_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_RevelationNpcTemp]
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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpctemp_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationNpcTemp] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpctemp_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationNpcTemp] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationnpctemp_Insert
	@Idx int
	,@MarkId int
	,@Schedule int
	,@OpponentTeamName varchar(50)
	,@FormationID int
	,@PlayerLevel int
	,@PlayerCardStrength int
	,@Buff int
	,@P1 int
	,@P2 int
	,@P3 int
	,@P4 int
	,@P5 int
	,@P6 int
	,@P7 int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationNpcTemp] (
	[Idx]
	,[MarkId]
	,[Schedule]
	,[OpponentTeamName]
	,[FormationID]
	,[PlayerLevel]
	,[PlayerCardStrength]
	,[Buff]
	,[P1]
	,[P2]
	,[P3]
	,[P4]
	,[P5]
	,[P6]
	,[P7]
) VALUES (
    @Idx
    ,@MarkId
    ,@Schedule
    ,@OpponentTeamName
    ,@FormationID
    ,@PlayerLevel
    ,@PlayerCardStrength
    ,@Buff
    ,@P1
    ,@P2
    ,@P3
    ,@P4
    ,@P5
    ,@P6
    ,@P7
)

select @Idx

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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpctemp_Update
	@Idx int, 
	@MarkId int, 
	@Schedule int, 
	@OpponentTeamName varchar(50), 
	@FormationID int, 
	@PlayerLevel int, 
	@PlayerCardStrength int, 
	@Buff int, 
	@P1 int, 
	@P2 int, 
	@P3 int, 
	@P4 int, 
	@P5 int, 
	@P6 int, 
	@P7 int 
AS



UPDATE [dbo].[Config_RevelationNpcTemp] SET
	[MarkId] = @MarkId
	,[Schedule] = @Schedule
	,[OpponentTeamName] = @OpponentTeamName
	,[FormationID] = @FormationID
	,[PlayerLevel] = @PlayerLevel
	,[PlayerCardStrength] = @PlayerCardStrength
	,[Buff] = @Buff
	,[P1] = @P1
	,[P2] = @P2
	,[P3] = @P3
	,[P4] = @P4
	,[P5] = @P5
	,[P6] = @P6
	,[P7] = @P7
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


