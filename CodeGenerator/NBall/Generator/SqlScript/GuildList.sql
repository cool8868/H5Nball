
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Guildlist_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GuildList_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GuildList_Delete]
GO

/****** Object:  Stored Procedure [dbo].GuildList_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GuildList_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GuildList_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GuildList_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GuildList_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GuildList_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GuildList_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GuildList_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GuildList_Insert]
GO

/****** Object:  Stored Procedure [dbo].GuildList_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GuildList_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GuildList_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GuildList_Delete
	@GuildNo int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Guild_List]
WHERE
	[GuildNo] = @GuildNo
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

CREATE PROCEDURE [dbo].P_GuildList_GetById
	@GuildNo int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Guild_List] with(nolock)
WHERE
	[GuildNo] = @GuildNo
	
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

CREATE PROCEDURE [dbo].P_GuildList_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Guild_List] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GuildList_Insert
	@GuildId uniqueidentifier , 
	@GuildName nvarchar(50) , 
	@Icon varchar(20) , 
	@Logo nvarchar(40) , 
	@Intro nvarchar(400) , 
	@Note nvarchar(400) , 
	@GuildLevel int , 
	@GuildActive int , 
	@GuildActiveCost int , 
	@CntMembers int , 
	@MaxMembers int , 
	@CreatorId uniqueidentifier , 
	@CreatorName nvarchar(100) , 
	@CreateTime datetime , 
	@LeaderId uniqueidentifier , 
	@LeaderName nvarchar(100) , 
	@LeadTime datetime , 
	@LeadTrack int , 
	@GKpi int , 
	@GRank int , 
	@LockFlag int , 
	@LockTime datetime , 
	@InvalidFlag int , 
	@RowTimeUp datetime , 
	@RowTime datetime , 
    @GuildNo int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Guild_List] (
	[GuildId]
	,[GuildName]
	,[Icon]
	,[Logo]
	,[Intro]
	,[Note]
	,[GuildLevel]
	,[GuildActive]
	,[GuildActiveCost]
	,[CntMembers]
	,[MaxMembers]
	,[CreatorId]
	,[CreatorName]
	,[CreateTime]
	,[LeaderId]
	,[LeaderName]
	,[LeadTime]
	,[LeadTrack]
	,[GKpi]
	,[GRank]
	,[LockFlag]
	,[LockTime]
	,[InvalidFlag]
	,[RowTimeUp]
	,[RowTime]
) VALUES (
    @GuildId
    ,@GuildName
    ,@Icon
    ,@Logo
    ,@Intro
    ,@Note
    ,@GuildLevel
    ,@GuildActive
    ,@GuildActiveCost
    ,@CntMembers
    ,@MaxMembers
    ,@CreatorId
    ,@CreatorName
    ,@CreateTime
    ,@LeaderId
    ,@LeaderName
    ,@LeadTime
    ,@LeadTrack
    ,@GKpi
    ,@GRank
    ,@LockFlag
    ,@LockTime
    ,@InvalidFlag
    ,@RowTimeUp
    ,@RowTime
)


SET @GuildNo = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_GuildList_Update
	@GuildNo int, 
	@GuildId uniqueidentifier, 
	@GuildName nvarchar(50), 
	@Icon varchar(20), 
	@Logo nvarchar(40), 
	@Intro nvarchar(400), 
	@Note nvarchar(400), 
	@GuildLevel int, 
	@GuildActive int, 
	@GuildActiveCost int, 
	@CntMembers int, 
	@MaxMembers int, 
	@CreatorId uniqueidentifier, 
	@CreatorName nvarchar(100), 
	@CreateTime datetime, 
	@LeaderId uniqueidentifier, 
	@LeaderName nvarchar(100), 
	@LeadTime datetime, 
	@LeadTrack int, 
	@GKpi int, 
	@GRank int, 
	@LockFlag int, 
	@LockTime datetime, 
	@InvalidFlag int, 
	@RowTimeUp datetime, 
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Guild_List] SET
	[GuildId] = @GuildId
	,[GuildName] = @GuildName
	,[Icon] = @Icon
	,[Logo] = @Logo
	,[Intro] = @Intro
	,[Note] = @Note
	,[GuildLevel] = @GuildLevel
	,[GuildActive] = @GuildActive
	,[GuildActiveCost] = @GuildActiveCost
	,[CntMembers] = @CntMembers
	,[MaxMembers] = @MaxMembers
	,[CreatorId] = @CreatorId
	,[CreatorName] = @CreatorName
	,[CreateTime] = @CreateTime
	,[LeaderId] = @LeaderId
	,[LeaderName] = @LeaderName
	,[LeadTime] = @LeadTime
	,[LeadTrack] = @LeadTrack
	,[GKpi] = @GKpi
	,[GRank] = @GRank
	,[LockFlag] = @LockFlag
	,[LockTime] = @LockTime
	,[InvalidFlag] = @InvalidFlag
	,[RowTimeUp] = @RowTimeUp
	,[RowTime] = @RowTime
WHERE
	[GuildNo] = @GuildNo
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


