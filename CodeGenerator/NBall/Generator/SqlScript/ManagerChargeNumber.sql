
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managerchargenumber_Delete    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerChargenumber_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerChargenumber_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerChargenumber_GetById    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerChargenumber_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerChargenumber_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerChargenumber_GetAll    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerChargenumber_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerChargenumber_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerChargenumber_Insert    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerChargenumber_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerChargenumber_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerChargenumber_Update    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerChargenumber_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerChargenumber_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerChargenumber_Delete
	@Idx int
AS

DELETE FROM [dbo].[Manager_ChargeNumber]
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

CREATE PROCEDURE [dbo].P_ManagerChargenumber_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Manager_ChargeNumber] with(nolock)
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

CREATE PROCEDURE [dbo].P_ManagerChargenumber_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Manager_ChargeNumber] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerChargenumber_Insert
	@ManagerId uniqueidentifier , 
	@MallCode int , 
	@BuyNumber int , 
	@RowTime datetime , 
	@UpdateTiem datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Manager_ChargeNumber] (
	[ManagerId]
	,[MallCode]
	,[BuyNumber]
	,[RowTime]
	,[UpdateTiem]
) VALUES (
    @ManagerId
    ,@MallCode
    ,@BuyNumber
    ,@RowTime
    ,@UpdateTiem
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

CREATE PROCEDURE [dbo].P_ManagerChargenumber_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@MallCode int, -- 购买的物品code
	@BuyNumber int, -- 购买次数
	@RowTime datetime, 
	@UpdateTiem datetime 
AS



UPDATE [dbo].[Manager_ChargeNumber] SET
	[ManagerId] = @ManagerId
	,[MallCode] = @MallCode
	,[BuyNumber] = @BuyNumber
	,[RowTime] = @RowTime
	,[UpdateTiem] = @UpdateTiem
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



