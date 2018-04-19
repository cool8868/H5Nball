
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdpairrecord_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPairrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPairrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdPairrecord_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPairrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPairrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdPairrecord_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPairrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPairrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdPairrecord_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPairrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPairrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdPairrecord_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPairrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPairrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdPairrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossCrowd_PairRecord]
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

CREATE PROCEDURE [dbo].P_CrosscrowdPairrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_PairRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdPairrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_PairRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdPairrecord_Insert
	@CrossCrowdId int , 
	@PairIndex int , 
	@PlayerCount int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_PairRecord] (
	[CrossCrowdId]
	,[PairIndex]
	,[PlayerCount]
	,[RowTime]
) VALUES (
    @CrossCrowdId
    ,@PairIndex
    ,@PlayerCount
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

CREATE PROCEDURE [dbo].P_CrosscrowdPairrecord_Update
	@Idx int, 
	@CrossCrowdId int, 
	@PairIndex int, 
	@PlayerCount int, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossCrowd_PairRecord] SET
	[CrossCrowdId] = @CrossCrowdId
	,[PairIndex] = @PairIndex
	,[PlayerCount] = @PlayerCount
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


