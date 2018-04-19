
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configtxchargeid_Delete    Script Date: 2016年12月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTxchargeid_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTxchargeid_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigTxchargeid_GetById    Script Date: 2016年12月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTxchargeid_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTxchargeid_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigTxchargeid_GetAll    Script Date: 2016年12月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTxchargeid_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTxchargeid_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigTxchargeid_Insert    Script Date: 2016年12月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTxchargeid_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTxchargeid_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigTxchargeid_Update    Script Date: 2016年12月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTxchargeid_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTxchargeid_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigTxchargeid_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_TxChargeId]
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

CREATE PROCEDURE [dbo].P_ConfigTxchargeid_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TxChargeId] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigTxchargeid_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TxChargeId] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigTxchargeid_Insert
	@Idx int
	,@ZoneType int
	,@MallCode int
	,@TxItemId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_TxChargeId] (
	[Idx]
	,[ZoneType]
	,[MallCode]
	,[TxItemId]
) VALUES (
    @Idx
    ,@ZoneType
    ,@MallCode
    ,@TxItemId
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

CREATE PROCEDURE [dbo].P_ConfigTxchargeid_Update
	@Idx int, 
	@ZoneType int, 
	@MallCode int, 
	@TxItemId int 
AS



UPDATE [dbo].[Config_TxChargeId] SET
	[ZoneType] = @ZoneType
	,[MallCode] = @MallCode
	,[TxItemId] = @TxItemId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


