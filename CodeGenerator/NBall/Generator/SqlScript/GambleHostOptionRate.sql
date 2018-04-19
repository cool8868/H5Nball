
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gamblehostoptionrate_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHostoptionrate_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHostoptionrate_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleHostoptionrate_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHostoptionrate_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHostoptionrate_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleHostoptionrate_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHostoptionrate_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHostoptionrate_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleHostoptionrate_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHostoptionrate_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHostoptionrate_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleHostoptionrate_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleHostoptionrate_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleHostoptionrate_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleHostoptionrate_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Gamble_HostOptionRate]
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

CREATE PROCEDURE [dbo].P_GambleHostoptionrate_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_HostOptionRate] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleHostoptionrate_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_HostOptionRate] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleHostoptionrate_Insert
	@HostId int , 
	@OptionId uniqueidentifier , 
	@WinRate decimal(10, 2) , 
	@GambleMoney int , 
	@AttendPeopleCount int , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_HostOptionRate] (
	[HostId]
	,[OptionId]
	,[WinRate]
	,[GambleMoney]
	,[AttendPeopleCount]
	,[Status]
	,[RowTime]
) VALUES (
    @HostId
    ,@OptionId
    ,@WinRate
    ,@GambleMoney
    ,@AttendPeopleCount
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

CREATE PROCEDURE [dbo].P_GambleHostoptionrate_Update
	@Idx int, 
	@HostId int, -- 庄家ID
	@OptionId uniqueidentifier, -- 选项ID
	@WinRate decimal(10, 2), -- 赔率
	@GambleMoney int, -- 玩家押注总额
	@AttendPeopleCount int, -- 参与人数
	@Status int, -- 状态
	@RowTime datetime, -- 创建时间
	@RowVersion timestamp -- Version
AS



UPDATE [dbo].[Gamble_HostOptionRate] SET
	[HostId] = @HostId
	,[OptionId] = @OptionId
	,[WinRate] = @WinRate
	,[GambleMoney] = @GambleMoney
	,[AttendPeopleCount] = @AttendPeopleCount
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


