
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladderhonorrecord_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHonorrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHonorrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderHonorrecord_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHonorrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHonorrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderHonorrecord_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHonorrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHonorrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderHonorrecord_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHonorrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHonorrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderHonorrecord_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHonorrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHonorrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderHonorrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossLadder_HonorRecord]
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

CREATE PROCEDURE [dbo].P_CrossladderHonorrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_HonorRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderHonorrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_HonorRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderHonorrecord_Insert
	@ManagerId uniqueidentifier , 
	@Honor int , 
	@CurHonor int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossLadder_HonorRecord] (
	[ManagerId]
	,[Honor]
	,[CurHonor]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@Honor
    ,@CurHonor
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

CREATE PROCEDURE [dbo].P_CrossladderHonorrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@Honor int, 
	@CurHonor int, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossLadder_HonorRecord] SET
	[ManagerId] = @ManagerId
	,[Honor] = @Honor
	,[CurHonor] = @CurHonor
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


