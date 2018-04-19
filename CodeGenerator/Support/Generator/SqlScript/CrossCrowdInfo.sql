
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdinfo_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdInfo_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdInfo_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdInfo_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdInfo_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdInfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossCrowd_Info]
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

CREATE PROCEDURE [dbo].P_CrosscrowdInfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdInfo_Insert
	@StartTime datetime , 
	@EndTime datetime , 
	@DomainId int , 
	@PlayerCount int , 
	@PairCount int , 
	@PrizePoint int , 
	@IsSendKillPrize bit , 
	@IsSendRankPrize bit , 
	@Status int , 
	@RowTime datetime , 
	@PrizeLegendCount int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_Info] (
	[StartTime]
	,[EndTime]
	,[DomainId]
	,[PlayerCount]
	,[PairCount]
	,[PrizePoint]
	,[IsSendKillPrize]
	,[IsSendRankPrize]
	,[Status]
	,[RowTime]
	,[PrizeLegendCount]
) VALUES (
    @StartTime
    ,@EndTime
    ,@DomainId
    ,@PlayerCount
    ,@PairCount
    ,@PrizePoint
    ,@IsSendKillPrize
    ,@IsSendRankPrize
    ,@Status
    ,@RowTime
    ,@PrizeLegendCount
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

CREATE PROCEDURE [dbo].P_CrosscrowdInfo_Update
	@Idx int, 
	@StartTime datetime, 
	@EndTime datetime, 
	@DomainId int, 
	@PlayerCount int, -- 参与人数
	@PairCount int, -- 配对次数
	@PrizePoint int, 
	@IsSendKillPrize bit, 
	@IsSendRankPrize bit, 
	@Status int, -- 状态：0，初始；1，已开始；2，已结束
	@RowTime datetime, 
	@PrizeLegendCount int 
AS



UPDATE [dbo].[CrossCrowd_Info] SET
	[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[DomainId] = @DomainId
	,[PlayerCount] = @PlayerCount
	,[PairCount] = @PairCount
	,[PrizePoint] = @PrizePoint
	,[IsSendKillPrize] = @IsSendKillPrize
	,[IsSendRankPrize] = @IsSendRankPrize
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[PrizeLegendCount] = @PrizeLegendCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


