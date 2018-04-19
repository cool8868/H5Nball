
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladderinfo_Delete    Script Date: 2016年8月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderInfo_GetById    Script Date: 2016年8月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderInfo_GetAll    Script Date: 2016年8月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderInfo_Insert    Script Date: 2016年8月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderInfo_Update    Script Date: 2016年8月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderInfo_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[CrossLadder_Info]
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

CREATE PROCEDURE [dbo].P_CrossladderInfo_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderInfo_Insert
	@AvgWaitTime int , 
	@PlayerNumber int , 
	@Groups int , 
	@Season int , 
	@StartTime datetime , 
	@GroupingTime datetime , 
	@CountdownTime datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossLadder_Info] (
	[Idx],
	[AvgWaitTime]
	,[PlayerNumber]
	,[Groups]
	,[Season]
	,[StartTime]
	,[GroupingTime]
	,[CountdownTime]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
	@Idx,
    @AvgWaitTime
    ,@PlayerNumber
    ,@Groups
    ,@Season
    ,@StartTime
    ,@GroupingTime
    ,@CountdownTime
    ,@Status
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_CrossladderInfo_Update
	@Idx uniqueidentifier, 
	@AvgWaitTime int, -- 等待时间
	@PlayerNumber int, -- 玩家数量
	@Groups int, -- 分组数量
	@Season int, -- 所属赛季
	@StartTime datetime, -- 开始时间
	@GroupingTime datetime, -- 分组时间
	@CountdownTime datetime, -- 倒计时时间
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[CrossLadder_Info] SET
	[AvgWaitTime] = @AvgWaitTime
	,[PlayerNumber] = @PlayerNumber
	,[Groups] = @Groups
	,[Season] = @Season
	,[StartTime] = @StartTime
	,[GroupingTime] = @GroupingTime
	,[CountdownTime] = @CountdownTime
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


