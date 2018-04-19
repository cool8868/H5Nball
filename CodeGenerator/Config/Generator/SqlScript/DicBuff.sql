
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicbuff_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBuff_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBuff_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicBuff_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBuff_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBuff_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicBuff_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBuff_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBuff_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicBuff_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBuff_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBuff_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicBuff_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBuff_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBuff_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicBuff_Delete
	@BuffId int
AS

DELETE FROM [dbo].[Dic_Buff]
WHERE
	[BuffId] = @BuffId

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

CREATE PROCEDURE [dbo].P_DicBuff_GetById
	@BuffId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Buff] with(nolock)
WHERE
	[BuffId] = @BuffId
	
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

CREATE PROCEDURE [dbo].P_DicBuff_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Buff] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicBuff_Insert
	@BuffId int
	,@BuffName nvarchar(80)
	,@BuffType int
	,@BaseFlag bit
	,@BaseBuffMap varchar(200)
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Buff] (
	[BuffId]
	,[BuffName]
	,[BuffType]
	,[BaseFlag]
	,[BaseBuffMap]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @BuffId
    ,@BuffName
    ,@BuffType
    ,@BaseFlag
    ,@BaseBuffMap
    ,@Icon
    ,@Memo
    ,@RowTime
)

select @BuffId

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

CREATE PROCEDURE [dbo].P_DicBuff_Update
	@BuffId int, -- BuffId
	@BuffName nvarchar(80), -- BuffName
	@BuffType int, -- BuffType
	@BaseFlag bit, -- 基础Buff
	@BaseBuffMap varchar(200), -- 基础Buff列表
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_Buff] SET
	[BuffName] = @BuffName
	,[BuffType] = @BuffType
	,[BaseFlag] = @BaseFlag
	,[BaseBuffMap] = @BaseBuffMap
	,[Icon] = @Icon
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[BuffId] = @BuffId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



