
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Coachmanager_Delete    Script Date: 2017年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CoachManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CoachManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].CoachManager_GetById    Script Date: 2017年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CoachManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CoachManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CoachManager_GetAll    Script Date: 2017年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CoachManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CoachManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CoachManager_Insert    Script Date: 2017年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CoachManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CoachManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].CoachManager_Update    Script Date: 2017年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CoachManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CoachManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CoachManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Coach_Manager]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_CoachManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Coach_Manager] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_CoachManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Coach_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CoachManager_Insert
	@HaveExp int , 
	@CoachString varbinary(max) , 
	@EnableCoachId int , 
	@EnableCoachLevel int , 
	@EnableCoachStar int , 
	@EnableCoachSkillLevel int , 
	@Offensive decimal(5, 2) , 
	@Organizational decimal(5, 2) , 
	@Defense decimal(5, 2) , 
	@BodyAttr decimal(5, 2) , 
	@Goalkeeping decimal(5, 2) , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Coach_Manager] (
	[ManagerId],
	[HaveExp]
	,[CoachString]
	,[EnableCoachId]
	,[EnableCoachLevel]
	,[EnableCoachStar]
	,[EnableCoachSkillLevel]
	,[Offensive]
	,[Organizational]
	,[Defense]
	,[BodyAttr]
	,[Goalkeeping]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @HaveExp
    ,@CoachString
    ,@EnableCoachId
    ,@EnableCoachLevel
    ,@EnableCoachStar
    ,@EnableCoachSkillLevel
    ,@Offensive
    ,@Organizational
    ,@Defense
    ,@BodyAttr
    ,@Goalkeeping
    ,@Status
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_CoachManager_Update
	@ManagerId uniqueidentifier, 
	@HaveExp int, -- 可用教练经验
	@CoachString varbinary(max), 
	@EnableCoachId int, 
	@EnableCoachLevel int, 
	@EnableCoachStar int, 
	@EnableCoachSkillLevel int, 
	@Offensive decimal(5, 2), 
	@Organizational decimal(5, 2), 
	@Defense decimal(5, 2), 
	@BodyAttr decimal(5, 2), 
	@Goalkeeping decimal(5, 2), 
	@Status int, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Coach_Manager] SET
	[HaveExp] = @HaveExp
	,[CoachString] = @CoachString
	,[EnableCoachId] = @EnableCoachId
	,[EnableCoachLevel] = @EnableCoachLevel
	,[EnableCoachStar] = @EnableCoachStar
	,[EnableCoachSkillLevel] = @EnableCoachSkillLevel
	,[Offensive] = @Offensive
	,[Organizational] = @Organizational
	,[Defense] = @Defense
	,[BodyAttr] = @BodyAttr
	,[Goalkeeping] = @Goalkeeping
	,[Status] = @Status
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


