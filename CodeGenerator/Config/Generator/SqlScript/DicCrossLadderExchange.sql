
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Diccrossladderexchange_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderexchange_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderexchange_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicCrossladderexchange_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderexchange_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderexchange_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicCrossladderexchange_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderexchange_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderexchange_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicCrossladderexchange_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderexchange_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderexchange_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicCrossladderexchange_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderexchange_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderexchange_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicCrossladderexchange_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_CrossLadderExchange]
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

CREATE PROCEDURE [dbo].P_DicCrossladderexchange_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CrossLadderExchange] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicCrossladderexchange_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CrossLadderExchange] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicCrossladderexchange_Insert
	@Idx int
	,@ItemCode int
	,@CostHonor int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_CrossLadderExchange] (
	[Idx]
	,[ItemCode]
	,[CostHonor]
) VALUES (
    @Idx
    ,@ItemCode
    ,@CostHonor
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

CREATE PROCEDURE [dbo].P_DicCrossladderexchange_Update
	@Idx int, 
	@ItemCode int, 
	@CostHonor int 
AS



UPDATE [dbo].[Dic_CrossLadderExchange] SET
	[ItemCode] = @ItemCode
	,[CostHonor] = @CostHonor
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


