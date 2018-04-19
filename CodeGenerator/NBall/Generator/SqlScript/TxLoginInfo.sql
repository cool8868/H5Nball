
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Txlogininfo_Delete    Script Date: 2016年12月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxLogininfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxLogininfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].TxLogininfo_GetById    Script Date: 2016年12月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxLogininfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxLogininfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TxLogininfo_GetAll    Script Date: 2016年12月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxLogininfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxLogininfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TxLogininfo_Insert    Script Date: 2016年12月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxLogininfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxLogininfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].TxLogininfo_Update    Script Date: 2016年12月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxLogininfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxLogininfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TxLogininfo_Delete
	@OpenId varchar(200)
AS

DELETE FROM [dbo].[Tx_LoginInfo]
WHERE
	[OpenId] = @OpenId

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

CREATE PROCEDURE [dbo].P_TxLogininfo_GetById
	@OpenId varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Tx_LoginInfo] with(nolock)
WHERE
	[OpenId] = @OpenId
	
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

CREATE PROCEDURE [dbo].P_TxLogininfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Tx_LoginInfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TxLogininfo_Insert
	@OpenId varchar(200)
	,@OpenKey varchar(200)
	,@Pf varchar(50)
	,@Format varchar(50)
	,@Ext varchar(2000)
	,@Ext1 varchar(2000)
	,@UpdateTime datetime
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Tx_LoginInfo] (
	[OpenId]
	,[OpenKey]
	,[Pf]
	,[Format]
	,[Ext]
	,[Ext1]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @OpenId
    ,@OpenKey
    ,@Pf
    ,@Format
    ,@Ext
    ,@Ext1
    ,@UpdateTime
    ,@RowTime
)

select @OpenId

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

CREATE PROCEDURE [dbo].P_TxLogininfo_Update
	@OpenId varchar(200), 
	@OpenKey varchar(200), 
	@Pf varchar(50), 
	@Format varchar(50), 
	@Ext varchar(2000), 
	@Ext1 varchar(2000), 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Tx_LoginInfo] SET
	[OpenKey] = @OpenKey
	,[Pf] = @Pf
	,[Format] = @Format
	,[Ext] = @Ext
	,[Ext1] = @Ext1
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[OpenId] = @OpenId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


