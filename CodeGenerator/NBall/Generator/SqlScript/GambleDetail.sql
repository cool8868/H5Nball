
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gambledetail_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleDetail_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleDetail_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleDetail_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleDetail_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleDetail_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleDetail_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleDetail_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleDetail_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleDetail_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleDetail_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleDetail_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleDetail_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleDetail_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleDetail_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleDetail_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Gamble_Detail]
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_GambleDetail_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Detail] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleDetail_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Detail] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleDetail_Insert
	@ManagerId uniqueidentifier , 
	@ManagerName nvarchar(50) , 
	@HostOptionId int , 
	@GambleMoney int , 
	@ResultMoney int , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Detail] (
	[ManagerId]
	,[ManagerName]
	,[HostOptionId]
	,[GambleMoney]
	,[ResultMoney]
	,[Status]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ManagerName
    ,@HostOptionId
    ,@GambleMoney
    ,@ResultMoney
    ,@Status
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

CREATE PROCEDURE [dbo].P_GambleDetail_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理ID
	@ManagerName nvarchar(50), -- 经理名
	@HostOptionId int, -- 押注选项ID
	@GambleMoney int, -- 押注金额
	@ResultMoney int, -- 结算奖金
	@Status int, -- 状态 0 未开奖，1已猜中，2未猜中
	@RowTime datetime, -- 创建时间
	@RowVersion timestamp -- Version
AS



UPDATE [dbo].[Gamble_Detail] SET
	[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[HostOptionId] = @HostOptionId
	,[GambleMoney] = @GambleMoney
	,[ResultMoney] = @ResultMoney
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


