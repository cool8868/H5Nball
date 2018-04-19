
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationhistoryofthegap_Delete    Script Date: 2017年2月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHistoryofthegap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHistoryofthegap_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationHistoryofthegap_GetById    Script Date: 2017年2月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHistoryofthegap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHistoryofthegap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationHistoryofthegap_GetAll    Script Date: 2017年2月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHistoryofthegap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHistoryofthegap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationHistoryofthegap_Insert    Script Date: 2017年2月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHistoryofthegap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHistoryofthegap_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationHistoryofthegap_Update    Script Date: 2017年2月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHistoryofthegap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHistoryofthegap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationHistoryofthegap_Delete
	@CustomsPass int,
	@Schedule int
AS

DELETE FROM [dbo].[Revelation_HistoryOfTheGap]
WHERE
	[CustomsPass] = @CustomsPass
	AND [Schedule] = @Schedule

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

CREATE PROCEDURE [dbo].P_RevelationHistoryofthegap_GetById
	@CustomsPass int,
	@Schedule int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_HistoryOfTheGap] with(nolock)
WHERE
	[CustomsPass] = @CustomsPass
	AND [Schedule] = @Schedule
	
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

CREATE PROCEDURE [dbo].P_RevelationHistoryofthegap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_HistoryOfTheGap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationHistoryofthegap_Insert
	@CustomsPass int
	,@Schedule int
	,@ManagerName nvarchar(50)
	,@Goals int
	,@ToConcede int
	,@HistoryOfTheGap int
	,@States int
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Revelation_HistoryOfTheGap] (
	[CustomsPass]
	,[Schedule]
	,[ManagerName]
	,[Goals]
	,[ToConcede]
	,[HistoryOfTheGap]
	,[States]
	,[UpdateTime]
) VALUES (
    @CustomsPass
    ,@Schedule
    ,@ManagerName
    ,@Goals
    ,@ToConcede
    ,@HistoryOfTheGap
    ,@States
    ,@UpdateTime
)

select @CustomsPass

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

CREATE PROCEDURE [dbo].P_RevelationHistoryofthegap_Update
	@CustomsPass int, -- 关卡
	@Schedule int, -- 进度
	@ManagerName nvarchar(50), -- 经理名
	@Goals int, -- 进球数
	@ToConcede int, -- 失球数
	@HistoryOfTheGap int, -- 最大分差
	@States int, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Revelation_HistoryOfTheGap] SET
	[ManagerName] = @ManagerName
	,[Goals] = @Goals
	,[ToConcede] = @ToConcede
	,[HistoryOfTheGap] = @HistoryOfTheGap
	,[States] = @States
	,[UpdateTime] = @UpdateTime
WHERE
	[CustomsPass] = @CustomsPass
	AND [Schedule] = @Schedule

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


