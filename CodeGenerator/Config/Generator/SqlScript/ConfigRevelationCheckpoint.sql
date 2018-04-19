
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationcheckpoint_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationcheckpoint_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationcheckpoint_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationcheckpoint_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationcheckpoint_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationcheckpoint_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationcheckpoint_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationcheckpoint_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationcheckpoint_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationcheckpoint_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationcheckpoint_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationcheckpoint_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationcheckpoint_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationcheckpoint_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationcheckpoint_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationcheckpoint_Delete
	@Mark int,
	@SmallClearance int
AS

DELETE FROM [dbo].[Config_RevelationCheckpoint]
WHERE
	[Mark] = @Mark
	AND [SmallClearance] = @SmallClearance

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

CREATE PROCEDURE [dbo].P_ConfigRevelationcheckpoint_GetById
	@Mark int,
	@SmallClearance int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationCheckpoint] with(nolock)
WHERE
	[Mark] = @Mark
	AND [SmallClearance] = @SmallClearance
	
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

CREATE PROCEDURE [dbo].P_ConfigRevelationcheckpoint_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationCheckpoint] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationcheckpoint_Insert
	@Mark int
	,@SmallClearance int
	,@CheckpointPlayers nvarchar(50)
	,@Describe nvarchar(50)
	,@TheStory nvarchar(500)
	,@Team nvarchar(50)
	,@AgainstTheTeam nvarchar(50)
	,@Formation int
	,@TheGoalkeeperID int
	,@TheGoalkeeperName nvarchar(50)
	,@PlayersID1 int
	,@PlayersName1 nvarchar(50)
	,@PlayersID2 int
	,@PlayersName2 nvarchar(50)
	,@PlayersID3 int
	,@PlayersName3 nvarchar(50)
	,@PlayersID4 int
	,@PlayersName4 nvarchar(50)
	,@PlayersID5 int
	,@PlayersName5 nvarchar(50)
	,@PlayersID6 int
	,@PlayersName6 nvarchar(50)
	,@PlayersID7 int
	,@PlayersName7 nvarchar(50)
	,@PlayersID8 int
	,@PlayersName8 nvarchar(50)
	,@PlayersID9 int
	,@PlayersName9 nvarchar(50)
	,@PlayersID10 int
	,@PlayersName10 nvarchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationCheckpoint] (
	[Mark]
	,[SmallClearance]
	,[CheckpointPlayers]
	,[Describe]
	,[TheStory]
	,[Team]
	,[AgainstTheTeam]
	,[Formation]
	,[TheGoalkeeperID]
	,[TheGoalkeeperName]
	,[PlayersID1]
	,[PlayersName1]
	,[PlayersID2]
	,[PlayersName2]
	,[PlayersID3]
	,[PlayersName3]
	,[PlayersID4]
	,[PlayersName4]
	,[PlayersID5]
	,[PlayersName5]
	,[PlayersID6]
	,[PlayersName6]
	,[PlayersID7]
	,[PlayersName7]
	,[PlayersID8]
	,[PlayersName8]
	,[PlayersID9]
	,[PlayersName9]
	,[PlayersID10]
	,[PlayersName10]
) VALUES (
    @Mark
    ,@SmallClearance
    ,@CheckpointPlayers
    ,@Describe
    ,@TheStory
    ,@Team
    ,@AgainstTheTeam
    ,@Formation
    ,@TheGoalkeeperID
    ,@TheGoalkeeperName
    ,@PlayersID1
    ,@PlayersName1
    ,@PlayersID2
    ,@PlayersName2
    ,@PlayersID3
    ,@PlayersName3
    ,@PlayersID4
    ,@PlayersName4
    ,@PlayersID5
    ,@PlayersName5
    ,@PlayersID6
    ,@PlayersName6
    ,@PlayersID7
    ,@PlayersName7
    ,@PlayersID8
    ,@PlayersName8
    ,@PlayersID9
    ,@PlayersName9
    ,@PlayersID10
    ,@PlayersName10
)

select @Mark

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

CREATE PROCEDURE [dbo].P_ConfigRevelationcheckpoint_Update
	@Mark int, -- 大关
	@SmallClearance int, -- 小关卡
	@CheckpointPlayers nvarchar(50), -- 关卡球员
	@Describe nvarchar(50), -- 描述
	@TheStory nvarchar(500), -- 故事
	@Team nvarchar(50), -- 所在球队
	@AgainstTheTeam nvarchar(50), -- 对阵球队
	@Formation int, -- 阵形
	@TheGoalkeeperID int, -- 门将ID
	@TheGoalkeeperName nvarchar(50), -- 门将名字
	@PlayersID1 int, -- 球员ID1
	@PlayersName1 nvarchar(50), -- 球员名字1
	@PlayersID2 int, -- 球员ID2
	@PlayersName2 nvarchar(50), -- 球员名字2
	@PlayersID3 int, -- 球员ID3
	@PlayersName3 nvarchar(50), -- 球员名字3
	@PlayersID4 int, -- 球员ID4
	@PlayersName4 nvarchar(50), -- 球员名字4
	@PlayersID5 int, -- 球员ID5
	@PlayersName5 nvarchar(50), -- 球员名字5
	@PlayersID6 int, -- 球员ID6
	@PlayersName6 nvarchar(50), -- 球员名字6
	@PlayersID7 int, -- 球员ID7
	@PlayersName7 nvarchar(50), -- 球员名字7
	@PlayersID8 int, -- 球员ID8
	@PlayersName8 nvarchar(50), -- 球员名字8
	@PlayersID9 int, -- 球员ID9
	@PlayersName9 nvarchar(50), -- 球员名字9
	@PlayersID10 int, -- 球员ID10
	@PlayersName10 nvarchar(50) -- 球员名字10
AS



UPDATE [dbo].[Config_RevelationCheckpoint] SET
	[CheckpointPlayers] = @CheckpointPlayers
	,[Describe] = @Describe
	,[TheStory] = @TheStory
	,[Team] = @Team
	,[AgainstTheTeam] = @AgainstTheTeam
	,[Formation] = @Formation
	,[TheGoalkeeperID] = @TheGoalkeeperID
	,[TheGoalkeeperName] = @TheGoalkeeperName
	,[PlayersID1] = @PlayersID1
	,[PlayersName1] = @PlayersName1
	,[PlayersID2] = @PlayersID2
	,[PlayersName2] = @PlayersName2
	,[PlayersID3] = @PlayersID3
	,[PlayersName3] = @PlayersName3
	,[PlayersID4] = @PlayersID4
	,[PlayersName4] = @PlayersName4
	,[PlayersID5] = @PlayersID5
	,[PlayersName5] = @PlayersName5
	,[PlayersID6] = @PlayersID6
	,[PlayersName6] = @PlayersName6
	,[PlayersID7] = @PlayersID7
	,[PlayersName7] = @PlayersName7
	,[PlayersID8] = @PlayersID8
	,[PlayersName8] = @PlayersName8
	,[PlayersID9] = @PlayersID9
	,[PlayersName9] = @PlayersName9
	,[PlayersID10] = @PlayersID10
	,[PlayersName10] = @PlayersName10
WHERE
	[Mark] = @Mark
	AND [SmallClearance] = @SmallClearance

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



