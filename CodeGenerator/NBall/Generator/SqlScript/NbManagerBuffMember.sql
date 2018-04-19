
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerbuffmember_Delete    Script Date: 2016年4月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffmember_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffmember_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerbuffmember_GetById    Script Date: 2016年4月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffmember_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffmember_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerbuffmember_GetAll    Script Date: 2016年4月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffmember_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffmember_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerbuffmember_Insert    Script Date: 2016年4月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffmember_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffmember_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerbuffmember_Update    Script Date: 2016年4月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffmember_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffmember_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerbuffmember_Delete
	@Id bigint
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[NB_ManagerBuffMember]
WHERE
	[Id] = @Id
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_NbManagerbuffmember_GetById
	@Id bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerBuffMember] with(nolock)
WHERE
	[Id] = @Id
	
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

CREATE PROCEDURE [dbo].P_NbManagerbuffmember_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerBuffMember] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerbuffmember_Insert
	@Id bigint
	,@ManagerId uniqueidentifier
	,@Tid uniqueidentifier
	,@Pid int
	,@PPos int
	,@PPosOn int
	,@Kpi int
	,@Level int
	,@Strength int
	,@ShowOrder int
	,@IsMain bit
	,@ReadySkills varchar(200)
	,@LiveSkills varchar(200)
	,@SpeedConst float
	,@SpeedCount float
	,@ShootConst float
	,@ShootCount float
	,@FreeKickConst float
	,@FreeKickCount float
	,@BalanceConst float
	,@BalanceCount float
	,@PhysiqueConst float
	,@PhysiqueCount float
	,@PowerConst float
	,@PowerCount float
	,@AggressionConst float
	,@AggressionCount float
	,@DisturbConst float
	,@DisturbCount float
	,@InterceptionConst float
	,@InterceptionCount float
	,@DribbleConst float
	,@DribbleCount float
	,@PassConst float
	,@PassCount float
	,@MentalityConst float
	,@MentalityCount float
	,@ResponseConst float
	,@ResponseCount float
	,@PositioningConst float
	,@PositioningCount float
	,@HandControlConst float
	,@HandControlCount float
	,@AccelerationConst float
	,@AccelerationCount float
	,@BounceConst float
	,@BounceCount float
	,@RowTime datetime
	,@RowVersion timestamp
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[NB_ManagerBuffMember] (
	[Id]
	,[ManagerId]
	,[Tid]
	,[Pid]
	,[PPos]
	,[PPosOn]
	,[Kpi]
	,[Level]
	,[Strength]
	,[ShowOrder]
	,[IsMain]
	,[ReadySkills]
	,[LiveSkills]
	,[SpeedConst]
	,[SpeedCount]
	,[ShootConst]
	,[ShootCount]
	,[FreeKickConst]
	,[FreeKickCount]
	,[BalanceConst]
	,[BalanceCount]
	,[PhysiqueConst]
	,[PhysiqueCount]
	,[PowerConst]
	,[PowerCount]
	,[AggressionConst]
	,[AggressionCount]
	,[DisturbConst]
	,[DisturbCount]
	,[InterceptionConst]
	,[InterceptionCount]
	,[DribbleConst]
	,[DribbleCount]
	,[PassConst]
	,[PassCount]
	,[MentalityConst]
	,[MentalityCount]
	,[ResponseConst]
	,[ResponseCount]
	,[PositioningConst]
	,[PositioningCount]
	,[HandControlConst]
	,[HandControlCount]
	,[AccelerationConst]
	,[AccelerationCount]
	,[BounceConst]
	,[BounceCount]
	,[RowTime]
) VALUES (
    @Id
    ,@ManagerId
    ,@Tid
    ,@Pid
    ,@PPos
    ,@PPosOn
    ,@Kpi
    ,@Level
    ,@Strength
    ,@ShowOrder
    ,@IsMain
    ,@ReadySkills
    ,@LiveSkills
    ,@SpeedConst
    ,@SpeedCount
    ,@ShootConst
    ,@ShootCount
    ,@FreeKickConst
    ,@FreeKickCount
    ,@BalanceConst
    ,@BalanceCount
    ,@PhysiqueConst
    ,@PhysiqueCount
    ,@PowerConst
    ,@PowerCount
    ,@AggressionConst
    ,@AggressionCount
    ,@DisturbConst
    ,@DisturbCount
    ,@InterceptionConst
    ,@InterceptionCount
    ,@DribbleConst
    ,@DribbleCount
    ,@PassConst
    ,@PassCount
    ,@MentalityConst
    ,@MentalityCount
    ,@ResponseConst
    ,@ResponseCount
    ,@PositioningConst
    ,@PositioningCount
    ,@HandControlConst
    ,@HandControlCount
    ,@AccelerationConst
    ,@AccelerationCount
    ,@BounceConst
    ,@BounceCount
    ,@RowTime
)

select @Id

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

CREATE PROCEDURE [dbo].P_NbManagerbuffmember_Update
	@Id bigint, -- Id
	@ManagerId uniqueidentifier, -- ManagerId
	@Tid uniqueidentifier, -- Tid
	@Pid int, -- Pid
	@PPos int, -- PPos
	@PPosOn int, -- PPosOn
	@Kpi int, -- Kpi
	@Level int, -- Level
	@Strength int, -- Strength
	@ShowOrder int, -- ShowOrder
	@IsMain bit, -- IsMain
	@ReadySkills varchar(200), -- ReadySkills
	@LiveSkills varchar(200), -- LiveSkills
	@SpeedConst float, -- SpeedConst
	@SpeedCount float, -- SpeedCount
	@ShootConst float, -- ShootConst
	@ShootCount float, -- ShootCount
	@FreeKickConst float, -- FreeKickConst
	@FreeKickCount float, -- FreeKickCount
	@BalanceConst float, -- BalanceConst
	@BalanceCount float, -- BalanceCount
	@PhysiqueConst float, -- PhysiqueConst
	@PhysiqueCount float, -- PhysiqueCount
	@PowerConst float, 
	@PowerCount float, 
	@AggressionConst float, -- AggressionConst
	@AggressionCount float, -- AggressionCount
	@DisturbConst float, -- DisturbConst
	@DisturbCount float, -- DisturbCount
	@InterceptionConst float, -- InterceptionConst
	@InterceptionCount float, -- InterceptionCount
	@DribbleConst float, -- DribbleConst
	@DribbleCount float, -- DribbleCount
	@PassConst float, -- PassConst
	@PassCount float, -- PassCount
	@MentalityConst float, -- MentalityConst
	@MentalityCount float, -- MentalityCount
	@ResponseConst float, -- ResponseConst
	@ResponseCount float, -- ResponseCount
	@PositioningConst float, -- PositioningConst
	@PositioningCount float, -- PositioningCount
	@HandControlConst float, -- HandControlConst
	@HandControlCount float, -- HandControlCount
	@AccelerationConst float, -- AccelerationConst
	@AccelerationCount float, -- AccelerationCount
	@BounceConst float, -- BounceConst
	@BounceCount float, -- BounceCount
	@RowTime datetime, -- RowTime
	@RowVersion timestamp -- RowVersion
AS



UPDATE [dbo].[NB_ManagerBuffMember] SET
	[ManagerId] = @ManagerId
	,[Tid] = @Tid
	,[Pid] = @Pid
	,[PPos] = @PPos
	,[PPosOn] = @PPosOn
	,[Kpi] = @Kpi
	,[Level] = @Level
	,[Strength] = @Strength
	,[ShowOrder] = @ShowOrder
	,[IsMain] = @IsMain
	,[ReadySkills] = @ReadySkills
	,[LiveSkills] = @LiveSkills
	,[SpeedConst] = @SpeedConst
	,[SpeedCount] = @SpeedCount
	,[ShootConst] = @ShootConst
	,[ShootCount] = @ShootCount
	,[FreeKickConst] = @FreeKickConst
	,[FreeKickCount] = @FreeKickCount
	,[BalanceConst] = @BalanceConst
	,[BalanceCount] = @BalanceCount
	,[PhysiqueConst] = @PhysiqueConst
	,[PhysiqueCount] = @PhysiqueCount
	,[PowerConst] = @PowerConst
	,[PowerCount] = @PowerCount
	,[AggressionConst] = @AggressionConst
	,[AggressionCount] = @AggressionCount
	,[DisturbConst] = @DisturbConst
	,[DisturbCount] = @DisturbCount
	,[InterceptionConst] = @InterceptionConst
	,[InterceptionCount] = @InterceptionCount
	,[DribbleConst] = @DribbleConst
	,[DribbleCount] = @DribbleCount
	,[PassConst] = @PassConst
	,[PassCount] = @PassCount
	,[MentalityConst] = @MentalityConst
	,[MentalityCount] = @MentalityCount
	,[ResponseConst] = @ResponseConst
	,[ResponseCount] = @ResponseCount
	,[PositioningConst] = @PositioningConst
	,[PositioningCount] = @PositioningCount
	,[HandControlConst] = @HandControlConst
	,[HandControlCount] = @HandControlCount
	,[AccelerationConst] = @AccelerationConst
	,[AccelerationCount] = @AccelerationCount
	,[BounceConst] = @BounceConst
	,[BounceCount] = @BounceCount
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



