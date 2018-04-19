
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gamblehost_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHost_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHost_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleHost_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHost_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHost_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleHost_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHost_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHost_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleHost_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHost_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHost_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleHost_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHost_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHost_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleHost_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Gamble_Host]
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

CREATE PROCEDURE [dbo].P_GambleHost_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Host] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleHost_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Host] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleHost_Insert
	@ManagerId uniqueidentifier , 
	@ManagerName nvarchar(50) , 
	@TitleId uniqueidentifier , 
	@HostMoney int , 
	@TotalMoney int , 
	@AttendPeopleCount int , 
	@HostWinMoney int , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Host] (
	[ManagerId]
	,[ManagerName]
	,[TitleId]
	,[HostMoney]
	,[TotalMoney]
	,[AttendPeopleCount]
	,[HostWinMoney]
	,[Status]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ManagerName
    ,@TitleId
    ,@HostMoney
    ,@TotalMoney
    ,@AttendPeopleCount
    ,@HostWinMoney
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

CREATE PROCEDURE [dbo].P_GambleHost_Update
	@Idx int, -- 标识
	@ManagerId uniqueidentifier, -- 经理ID
	@ManagerName nvarchar(50), -- 经理名
	@TitleId uniqueidentifier, 
	@HostMoney int, -- 庄家的资金
	@TotalMoney int, -- 奖池总奖金
	@AttendPeopleCount int, -- 参与人数
	@HostWinMoney int, -- 庄家赢的钱
	@Status int, -- 状态
	@RowTime datetime, -- 创建时间
	@RowVersion timestamp -- 版本号
AS



UPDATE [dbo].[Gamble_Host] SET
	[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[TitleId] = @TitleId
	,[HostMoney] = @HostMoney
	,[TotalMoney] = @TotalMoney
	,[AttendPeopleCount] = @AttendPeopleCount
	,[HostWinMoney] = @HostWinMoney
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


