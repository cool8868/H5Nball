
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Transferdropout_Delete    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferDropout_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferDropout_Delete]
GO

/****** Object:  Stored Procedure [dbo].TransferDropout_GetById    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferDropout_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferDropout_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TransferDropout_GetAll    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferDropout_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferDropout_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TransferDropout_Insert    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferDropout_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferDropout_Insert]
GO

/****** Object:  Stored Procedure [dbo].TransferDropout_Update    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferDropout_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferDropout_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TransferDropout_Delete
	@DomaId int
AS

DELETE FROM [dbo].[Transfer_DropOut]
WHERE
	[DomaId] = @DomaId

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

CREATE PROCEDURE [dbo].P_TransferDropout_GetById
	@DomaId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Transfer_DropOut] with(nolock)
WHERE
	[DomaId] = @DomaId
	
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

CREATE PROCEDURE [dbo].P_TransferDropout_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Transfer_DropOut] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TransferDropout_Insert
	@DomaId int
	,@DropOutType int
	,@DropOutNumber int
	,@RefreshTiem date
	,@RowTime datetime
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Transfer_DropOut] (
	[DomaId]
	,[DropOutType]
	,[DropOutNumber]
	,[RefreshTiem]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @DomaId
    ,@DropOutType
    ,@DropOutNumber
    ,@RefreshTiem
    ,@RowTime
    ,@UpdateTime
)

select @DomaId

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

CREATE PROCEDURE [dbo].P_TransferDropout_Update
	@DomaId int, -- 域ID
	@DropOutType int, -- 掉落类型
	@DropOutNumber int, -- 可掉落数量
	@RefreshTiem date, -- 刷新时间
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Transfer_DropOut] SET
	[DropOutType] = @DropOutType
	,[DropOutNumber] = @DropOutNumber
	,[RefreshTiem] = @RefreshTiem
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[DomaId] = @DomaId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


