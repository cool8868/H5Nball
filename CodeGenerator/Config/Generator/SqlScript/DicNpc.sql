
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicnpc_Delete    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNpc_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNpc_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicNpc_GetById    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNpc_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNpc_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicNpc_GetAll    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNpc_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNpc_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicNpc_Insert    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNpc_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNpc_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicNpc_Update    Script Date: 2016年1月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNpc_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNpc_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicNpc_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Dic_NPC]
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

CREATE PROCEDURE [dbo].P_DicNpc_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_NPC] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicNpc_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_NPC] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicNpc_Insert
	@Type int , 
	@Name nvarchar(50) , 
	@Logo int , 
	@FormationId int , 
	@FormationLevel int , 
	@TeammemberLevel int , 
	@PlayerCardStrength int , 
	@CoachId int , 
	@DoTalent varchar(200) , 
	@DoWill varchar(100) , 
	@ManagerSkill varchar(100) , 
	@CombLevel int , 
	@Buff int , 
	@PropertyPoint int , 
	@TP1 int , 
	@TE1 int , 
	@TS1 varchar(20) , 
	@TP2 int , 
	@TE2 int , 
	@TS2 varchar(20) , 
	@TP3 int , 
	@TE3 int , 
	@TS3 varchar(20) , 
	@TP4 int , 
	@TE4 int , 
	@TS4 varchar(20) , 
	@TP5 int , 
	@TE5 int , 
	@TS5 varchar(20) , 
	@TP6 int , 
	@TE6 int , 
	@TS6 varchar(20) , 
	@TP7 int , 
	@TE7 int , 
	@TS7 varchar(20) , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Dic_NPC] (
	[Idx],
	[Type]
	,[Name]
	,[Logo]
	,[FormationId]
	,[FormationLevel]
	,[TeammemberLevel]
	,[PlayerCardStrength]
	,[CoachId]
	,[DoTalent]
	,[DoWill]
	,[ManagerSkill]
	,[CombLevel]
	,[Buff]
	,[PropertyPoint]
	,[TP1]
	,[TE1]
	,[TS1]
	,[TP2]
	,[TE2]
	,[TS2]
	,[TP3]
	,[TE3]
	,[TS3]
	,[TP4]
	,[TE4]
	,[TS4]
	,[TP5]
	,[TE5]
	,[TS5]
	,[TP6]
	,[TE6]
	,[TS6]
	,[TP7]
	,[TE7]
	,[TS7]
) VALUES (
	@Idx,
    @Type
    ,@Name
    ,@Logo
    ,@FormationId
    ,@FormationLevel
    ,@TeammemberLevel
    ,@PlayerCardStrength
    ,@CoachId
    ,@DoTalent
    ,@DoWill
    ,@ManagerSkill
    ,@CombLevel
    ,@Buff
    ,@PropertyPoint
    ,@TP1
    ,@TE1
    ,@TS1
    ,@TP2
    ,@TE2
    ,@TS2
    ,@TP3
    ,@TE3
    ,@TS3
    ,@TP4
    ,@TE4
    ,@TS4
    ,@TP5
    ,@TE5
    ,@TS5
    ,@TP6
    ,@TE6
    ,@TS6
    ,@TP7
    ,@TE7
    ,@TS7
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

CREATE PROCEDURE [dbo].P_DicNpc_Update
	@Idx uniqueidentifier, 
	@Type int, -- npc类型：1,巡回赛;2,挑战赛
	@Name nvarchar(50), -- npc名字
	@Logo int, 
	@FormationId int, 
	@FormationLevel int, 
	@TeammemberLevel int, 
	@PlayerCardStrength int, 
	@CoachId int, 
	@DoTalent varchar(200), 
	@DoWill varchar(100), 
	@ManagerSkill varchar(100), 
	@CombLevel int, 
	@Buff int, 
	@PropertyPoint int, 
	@TP1 int, 
	@TE1 int, 
	@TS1 varchar(20), 
	@TP2 int, 
	@TE2 int, 
	@TS2 varchar(20), 
	@TP3 int, 
	@TE3 int, 
	@TS3 varchar(20), 
	@TP4 int, 
	@TE4 int, 
	@TS4 varchar(20), 
	@TP5 int, 
	@TE5 int, 
	@TS5 varchar(20), 
	@TP6 int, 
	@TE6 int, 
	@TS6 varchar(20), 
	@TP7 int, 
	@TE7 int, 
	@TS7 varchar(20) 
AS



UPDATE [dbo].[Dic_NPC] SET
	[Type] = @Type
	,[Name] = @Name
	,[Logo] = @Logo
	,[FormationId] = @FormationId
	,[FormationLevel] = @FormationLevel
	,[TeammemberLevel] = @TeammemberLevel
	,[PlayerCardStrength] = @PlayerCardStrength
	,[CoachId] = @CoachId
	,[DoTalent] = @DoTalent
	,[DoWill] = @DoWill
	,[ManagerSkill] = @ManagerSkill
	,[CombLevel] = @CombLevel
	,[Buff] = @Buff
	,[PropertyPoint] = @PropertyPoint
	,[TP1] = @TP1
	,[TE1] = @TE1
	,[TS1] = @TS1
	,[TP2] = @TP2
	,[TE2] = @TE2
	,[TS2] = @TS2
	,[TP3] = @TP3
	,[TE3] = @TE3
	,[TS3] = @TS3
	,[TP4] = @TP4
	,[TE4] = @TE4
	,[TS4] = @TS4
	,[TP5] = @TP5
	,[TE5] = @TE5
	,[TS5] = @TS5
	,[TP6] = @TP6
	,[TE6] = @TE6
	,[TS6] = @TS6
	,[TP7] = @TP7
	,[TE7] = @TE7
	,[TS7] = @TS7
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



