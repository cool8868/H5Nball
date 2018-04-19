
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelation_Delete    Script Date: 2017年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelation_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelation_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelation_GetById    Script Date: 2017年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelation_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelation_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelation_GetAll    Script Date: 2017年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelation_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelation_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelation_Insert    Script Date: 2017年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelation_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelation_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelation_Update    Script Date: 2017年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelation_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelation_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelation_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Revelation]
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

CREATE PROCEDURE [dbo].P_ConfigRevelation_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Revelation] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelation_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Revelation] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelation_Insert
	@Idx int
	,@MarkId int
	,@Schedule int
	,@NpcId uniqueidentifier
	,@MarkPlayerId int
	,@MarkPlayer varchar(50)
	,@Describe nvarchar(200)
	,@TeamName varchar(50)
	,@OpponentTeamName varchar(50)
	,@Formation varchar(50)
	,@OpponentFormation varchar(50)
	,@PassPrizeItem varchar(50)
	,@FirstPassItem varchar(50)
	,@CourageNumber int
	,@Story nvarchar(500)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Revelation] (
	[Idx]
	,[MarkId]
	,[Schedule]
	,[NpcId]
	,[MarkPlayerId]
	,[MarkPlayer]
	,[Describe]
	,[TeamName]
	,[OpponentTeamName]
	,[Formation]
	,[OpponentFormation]
	,[PassPrizeItem]
	,[FirstPassItem]
	,[CourageNumber]
	,[Story]
) VALUES (
    @Idx
    ,@MarkId
    ,@Schedule
    ,@NpcId
    ,@MarkPlayerId
    ,@MarkPlayer
    ,@Describe
    ,@TeamName
    ,@OpponentTeamName
    ,@Formation
    ,@OpponentFormation
    ,@PassPrizeItem
    ,@FirstPassItem
    ,@CourageNumber
    ,@Story
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

CREATE PROCEDURE [dbo].P_ConfigRevelation_Update
	@Idx int, -- 关卡ID
	@MarkId int, -- 大关卡
	@Schedule int, -- 小关卡
	@NpcId uniqueidentifier, -- NPCID
	@MarkPlayerId int, -- 关卡球星ID
	@MarkPlayer varchar(50), -- 关卡球星 
	@Describe nvarchar(200), -- 描述
	@TeamName varchar(50), -- 球队
	@OpponentTeamName varchar(50), -- 对手
	@Formation varchar(50), -- 球队阵型
	@OpponentFormation varchar(50), -- 对手阵型
	@PassPrizeItem varchar(50), -- 通关奖励物品串
	@FirstPassItem varchar(50), -- 首次通关奖励物品
	@CourageNumber int, -- 获得勇气值数量
	@Story nvarchar(500) -- 故事
AS



UPDATE [dbo].[Config_Revelation] SET
	[MarkId] = @MarkId
	,[Schedule] = @Schedule
	,[NpcId] = @NpcId
	,[MarkPlayerId] = @MarkPlayerId
	,[MarkPlayer] = @MarkPlayer
	,[Describe] = @Describe
	,[TeamName] = @TeamName
	,[OpponentTeamName] = @OpponentTeamName
	,[Formation] = @Formation
	,[OpponentFormation] = @OpponentFormation
	,[PassPrizeItem] = @PassPrizeItem
	,[FirstPassItem] = @FirstPassItem
	,[CourageNumber] = @CourageNumber
	,[Story] = @Story
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


