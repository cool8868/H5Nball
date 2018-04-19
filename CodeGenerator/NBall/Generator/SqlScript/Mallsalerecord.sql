
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Mallsalerecord_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallSalerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallSalerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].MallSalerecord_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallSalerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallSalerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].MallSalerecord_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallSalerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallSalerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].MallSalerecord_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallSalerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallSalerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].MallSalerecord_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallSalerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallSalerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_MallSalerecord_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Mall_SaleRecord]
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

CREATE PROCEDURE [dbo].P_MallSalerecord_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mall_SaleRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_MallSalerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mall_SaleRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_MallSalerecord_Insert
	@ManagerId uniqueidentifier , 
	@MallCode int , 
	@Qty int , 
	@CurrencyType int , 
	@RawCurrency int , 
	@PayCurrency int , 
	@PackageFlag bit , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Mall_SaleRecord] (
	[Idx],
	[ManagerId]
	,[MallCode]
	,[Qty]
	,[CurrencyType]
	,[RawCurrency]
	,[PayCurrency]
	,[PackageFlag]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @ManagerId
    ,@MallCode
    ,@Qty
    ,@CurrencyType
    ,@RawCurrency
    ,@PayCurrency
    ,@PackageFlag
    ,@Status
    ,@RowTime
)




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

CREATE PROCEDURE [dbo].P_MallSalerecord_Update
	@Idx uniqueidentifier, 
	@ManagerId uniqueidentifier, -- 经理id
	@MallCode int, -- 道具code
	@Qty int, -- 购买数量
	@CurrencyType int, -- 货币类型
	@RawCurrency int, -- 应付货币数量
	@PayCurrency int, -- 实付货币数量
	@PackageFlag bit, -- 道具是否进背包，不进背包的为购买后立即消耗
	@Status int, -- 状态
	@RowTime datetime 
AS



UPDATE [dbo].[Mall_SaleRecord] SET
	[ManagerId] = @ManagerId
	,[MallCode] = @MallCode
	,[Qty] = @Qty
	,[CurrencyType] = @CurrencyType
	,[RawCurrency] = @RawCurrency
	,[PayCurrency] = @PayCurrency
	,[PackageFlag] = @PackageFlag
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



