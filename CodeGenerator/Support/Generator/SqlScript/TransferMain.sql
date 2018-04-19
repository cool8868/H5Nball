
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Transfermain_Delete    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferMain_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferMain_Delete]
GO

/****** Object:  Stored Procedure [dbo].TransferMain_GetById    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferMain_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferMain_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TransferMain_GetAll    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferMain_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferMain_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TransferMain_Insert    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferMain_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferMain_Insert]
GO

/****** Object:  Stored Procedure [dbo].TransferMain_Update    Script Date: 2016年10月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TransferMain_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TransferMain_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TransferMain_Delete
	@TransferId uniqueidentifier
AS

DELETE FROM [dbo].[Transfer_Main]
WHERE
	[TransferId] = @TransferId

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

CREATE PROCEDURE [dbo].P_TransferMain_GetById
	@TransferId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Transfer_Main] with(nolock)
WHERE
	[TransferId] = @TransferId
	
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

CREATE PROCEDURE [dbo].P_TransferMain_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Transfer_Main] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TransferMain_Insert
	@DomainId int , 
	@ItemCode int , 
	@ItemName varchar(200) , 
	@ItemProp varbinary(max) , 
	@SellName varchar(50) , 
	@SellId uniqueidentifier , 
	@SellZoneName varchar(50) , 
	@Price int , 
	@DealEndName varchar(50) , 
	@DealEndZoneName varchar(50) , 
	@DealEndPrice int , 
	@DealEndId uniqueidentifier , 
	@TransferStartTime datetime , 
	@TransferDuration datetime , 
	@Status int , 
	@Poundage int , 
	@GetPrice int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @TransferId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Transfer_Main] (
	[TransferId],
	[DomainId]
	,[ItemCode]
	,[ItemName]
	,[ItemProp]
	,[SellName]
	,[SellId]
	,[SellZoneName]
	,[Price]
	,[DealEndName]
	,[DealEndZoneName]
	,[DealEndPrice]
	,[DealEndId]
	,[TransferStartTime]
	,[TransferDuration]
	,[Status]
	,[Poundage]
	,[GetPrice]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@TransferId,
    @DomainId
    ,@ItemCode
    ,@ItemName
    ,@ItemProp
    ,@SellName
    ,@SellId
    ,@SellZoneName
    ,@Price
    ,@DealEndName
    ,@DealEndZoneName
    ,@DealEndPrice
    ,@DealEndId
    ,@TransferStartTime
    ,@TransferDuration
    ,@Status
    ,@Poundage
    ,@GetPrice
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_TransferMain_Update
	@TransferId uniqueidentifier, -- 排名ID
	@DomainId int, -- 域ID
	@ItemCode int, 
	@ItemName varchar(200), -- 物品名称
	@ItemProp varbinary(max), -- 物品属性
	@SellName varchar(50), -- 出售人名字
	@SellId uniqueidentifier, -- 出售人ID
	@SellZoneName varchar(50), -- 出售人区 
	@Price int, -- 起拍价格
	@DealEndName varchar(50), -- 成交人名字
	@DealEndZoneName varchar(50), -- 成交人区ID
	@DealEndPrice int, -- 成交价格
	@DealEndId uniqueidentifier, -- 成交人ID
	@TransferStartTime datetime, -- 拍卖开始时间
	@TransferDuration datetime, -- 持续时间 秒
	@Status int, 
	@Poundage int, -- 手续费
	@GetPrice int, -- 得到的金条
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Transfer_Main] SET
	[DomainId] = @DomainId
	,[ItemCode] = @ItemCode
	,[ItemName] = @ItemName
	,[ItemProp] = @ItemProp
	,[SellName] = @SellName
	,[SellId] = @SellId
	,[SellZoneName] = @SellZoneName
	,[Price] = @Price
	,[DealEndName] = @DealEndName
	,[DealEndZoneName] = @DealEndZoneName
	,[DealEndPrice] = @DealEndPrice
	,[DealEndId] = @DealEndId
	,[TransferStartTime] = @TransferStartTime
	,[TransferDuration] = @TransferDuration
	,[Status] = @Status
	,[Poundage] = @Poundage
	,[GetPrice] = @GetPrice
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[TransferId] = @TransferId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


