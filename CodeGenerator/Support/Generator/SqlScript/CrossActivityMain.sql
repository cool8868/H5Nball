
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossactivitymain_Delete    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityMain_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityMain_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossactivityMain_GetById    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityMain_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityMain_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossactivityMain_GetAll    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityMain_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityMain_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossactivityMain_Insert    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityMain_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityMain_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossactivityMain_Update    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityMain_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityMain_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossactivityMain_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossActivity_Main]
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

CREATE PROCEDURE [dbo].P_CrossactivityMain_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossActivity_Main] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossactivityMain_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossActivity_Main] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossactivityMain_Insert
	@Idx int
	,@DomainId int
	,@StartTime datetime
	,@EndTime datetime
	,@GoldBarNumber int
	,@GoldBarRefresh datetime
	,@UpdateTime datetime
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[CrossActivity_Main] (
	[Idx]
	,[DomainId]
	,[StartTime]
	,[EndTime]
	,[GoldBarNumber]
	,[GoldBarRefresh]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @Idx
    ,@DomainId
    ,@StartTime
    ,@EndTime
    ,@GoldBarNumber
    ,@GoldBarRefresh
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_CrossactivityMain_Update
	@Idx int, 
	@DomainId int, -- 跨服域
	@StartTime datetime, -- 活动开始时间
	@EndTime datetime, -- 活动结束时间
	@GoldBarNumber int, -- 剩余金条数量
	@GoldBarRefresh datetime, -- 金条刷新时间间隔
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossActivity_Main] SET
	[DomainId] = @DomainId
	,[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[GoldBarNumber] = @GoldBarNumber
	,[GoldBarRefresh] = @GoldBarRefresh
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


