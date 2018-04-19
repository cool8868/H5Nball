
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenateammember_Delete    Script Date: 2016年8月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaTeammember_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaTeammember_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaTeammember_GetById    Script Date: 2016年8月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaTeammember_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaTeammember_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaTeammember_GetAll    Script Date: 2016年8月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaTeammember_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaTeammember_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaTeammember_Insert    Script Date: 2016年8月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaTeammember_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaTeammember_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaTeammember_Update    Script Date: 2016年8月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaTeammember_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaTeammember_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaTeammember_Delete
	@Idx int
AS

DELETE FROM [dbo].[Arena_Teammember]
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

CREATE PROCEDURE [dbo].P_ArenaTeammember_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Teammember] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaTeammember_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Teammember] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaTeammember_Insert
	@ManagerId uniqueidentifier , 
	@ArenaType int , 
	@TeammemberString varbinary(max) , 
	@SkillString varchar(400) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_Teammember] (
	[ManagerId]
	,[ArenaType]
	,[TeammemberString]
	,[SkillString]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ArenaType
    ,@TeammemberString
    ,@SkillString
    ,@UpdateTime
    ,@RowTime
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ArenaTeammember_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ArenaType int, -- 竞技场类型
	@TeammemberString varbinary(max), -- 球员数据
	@SkillString varchar(400), 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Arena_Teammember] SET
	[ManagerId] = @ManagerId
	,[ArenaType] = @ArenaType
	,[TeammemberString] = @TeammemberString
	,[SkillString] = @SkillString
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


