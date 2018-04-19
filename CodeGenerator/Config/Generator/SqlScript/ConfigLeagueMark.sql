
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleaguemark_Delete    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguemark_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguemark_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguemark_GetById    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguemark_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguemark_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeaguemark_GetAll    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguemark_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguemark_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeaguemark_Insert    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguemark_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguemark_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguemark_Update    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguemark_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguemark_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeaguemark_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Config_LeagueMark]
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

CREATE PROCEDURE [dbo].P_ConfigLeaguemark_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueMark] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeaguemark_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueMark] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeaguemark_Insert
	@TeamId int , 
	@TeamName nvarchar(50) , 
	@LeagueId int , 
	@Buff int , 
	@ManagerLevel int , 
	@Formation int , 
	@PlayerLevel int , 
	@PlayerCardLevel int , 
	@Coach int , 
	@Talent varchar(50) , 
	@Player1Id int , 
	@Equipment1Id int , 
	@Skill1Id varchar(50) , 
	@Player2Id int , 
	@Equipment2Id int , 
	@Skill2Id varchar(50) , 
	@Player3Id int , 
	@Equipment3Id int , 
	@Skill3Id varchar(50) , 
	@Player4Id int , 
	@Equipment4Id int , 
	@Skill4Id varchar(50) , 
	@Player5Id int , 
	@Equipment5Id int , 
	@Skill5Id varchar(50) , 
	@Player6Id int , 
	@Equipment6Id int , 
	@Skill6Id varchar(50) , 
	@Player7Id int , 
	@Equipment7Id int , 
	@Skill7Id varchar(50) , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_LeagueMark] (
	[Idx],
	[TeamId]
	,[TeamName]
	,[LeagueId]
	,[Buff]
	,[ManagerLevel]
	,[Formation]
	,[PlayerLevel]
	,[PlayerCardLevel]
	,[Coach]
	,[Talent]
	,[Player1Id]
	,[Equipment1Id]
	,[Skill1Id]
	,[Player2Id]
	,[Equipment2Id]
	,[Skill2Id]
	,[Player3Id]
	,[Equipment3Id]
	,[Skill3Id]
	,[Player4Id]
	,[Equipment4Id]
	,[Skill4Id]
	,[Player5Id]
	,[Equipment5Id]
	,[Skill5Id]
	,[Player6Id]
	,[Equipment6Id]
	,[Skill6Id]
	,[Player7Id]
	,[Equipment7Id]
	,[Skill7Id]
) VALUES (
	@Idx,
    @TeamId
    ,@TeamName
    ,@LeagueId
    ,@Buff
    ,@ManagerLevel
    ,@Formation
    ,@PlayerLevel
    ,@PlayerCardLevel
    ,@Coach
    ,@Talent
    ,@Player1Id
    ,@Equipment1Id
    ,@Skill1Id
    ,@Player2Id
    ,@Equipment2Id
    ,@Skill2Id
    ,@Player3Id
    ,@Equipment3Id
    ,@Skill3Id
    ,@Player4Id
    ,@Equipment4Id
    ,@Skill4Id
    ,@Player5Id
    ,@Equipment5Id
    ,@Skill5Id
    ,@Player6Id
    ,@Equipment6Id
    ,@Skill6Id
    ,@Player7Id
    ,@Equipment7Id
    ,@Skill7Id
)




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

CREATE PROCEDURE [dbo].P_ConfigLeaguemark_Update
	@Idx uniqueidentifier, 
	@TeamId int, -- 球队ID
	@TeamName nvarchar(50), -- 球队名
	@LeagueId int, -- 联赛ID
	@Buff int, -- Buff
	@ManagerLevel int, -- 经理开放等级
	@Formation int, -- 阵形
	@PlayerLevel int, -- 球员等级
	@PlayerCardLevel int, -- 球员卡等级
	@Coach int, -- 教练
	@Talent varchar(50), -- 天赋
	@Player1Id int, -- 球员ID1
	@Equipment1Id int, -- 球员1的装备Code
	@Skill1Id varchar(50), -- 球员1的技能
	@Player2Id int, -- 球员2
	@Equipment2Id int, 
	@Skill2Id varchar(50), 
	@Player3Id int, 
	@Equipment3Id int, 
	@Skill3Id varchar(50), 
	@Player4Id int, 
	@Equipment4Id int, 
	@Skill4Id varchar(50), 
	@Player5Id int, 
	@Equipment5Id int, 
	@Skill5Id varchar(50), 
	@Player6Id int, 
	@Equipment6Id int, 
	@Skill6Id varchar(50), 
	@Player7Id int, 
	@Equipment7Id int, 
	@Skill7Id varchar(50) 
AS



UPDATE [dbo].[Config_LeagueMark] SET
	[TeamId] = @TeamId
	,[TeamName] = @TeamName
	,[LeagueId] = @LeagueId
	,[Buff] = @Buff
	,[ManagerLevel] = @ManagerLevel
	,[Formation] = @Formation
	,[PlayerLevel] = @PlayerLevel
	,[PlayerCardLevel] = @PlayerCardLevel
	,[Coach] = @Coach
	,[Talent] = @Talent
	,[Player1Id] = @Player1Id
	,[Equipment1Id] = @Equipment1Id
	,[Skill1Id] = @Skill1Id
	,[Player2Id] = @Player2Id
	,[Equipment2Id] = @Equipment2Id
	,[Skill2Id] = @Skill2Id
	,[Player3Id] = @Player3Id
	,[Equipment3Id] = @Equipment3Id
	,[Skill3Id] = @Skill3Id
	,[Player4Id] = @Player4Id
	,[Equipment4Id] = @Equipment4Id
	,[Skill4Id] = @Skill4Id
	,[Player5Id] = @Player5Id
	,[Equipment5Id] = @Equipment5Id
	,[Skill5Id] = @Skill5Id
	,[Player6Id] = @Player6Id
	,[Equipment6Id] = @Equipment6Id
	,[Skill6Id] = @Skill6Id
	,[Player7Id] = @Player7Id
	,[Equipment7Id] = @Equipment7Id
	,[Skill7Id] = @Skill7Id
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



