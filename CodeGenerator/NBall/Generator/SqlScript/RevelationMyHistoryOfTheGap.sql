
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationmyhistoryofthegap_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistoryofthegap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistoryofthegap_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationMyhistoryofthegap_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistoryofthegap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistoryofthegap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationMyhistoryofthegap_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistoryofthegap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistoryofthegap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationMyhistoryofthegap_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistoryofthegap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistoryofthegap_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationMyhistoryofthegap_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistoryofthegap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistoryofthegap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationMyhistoryofthegap_Delete
	@Idx int
AS

DELETE FROM [dbo].[Revelation_MyHistoryOfTheGap]
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

CREATE PROCEDURE [dbo].P_RevelationMyhistoryofthegap_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_MyHistoryOfTheGap] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationMyhistoryofthegap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_MyHistoryOfTheGap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationMyhistoryofthegap_Insert
	@ManagerId uniqueidentifier , 
	@Mark int , 
	@Schedule int , 
	@Goals int , 
	@ToConcede int , 
	@HistoryOfTheGap int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_MyHistoryOfTheGap] (
	[ManagerId]
	,[Mark]
	,[Schedule]
	,[Goals]
	,[ToConcede]
	,[HistoryOfTheGap]
) VALUES (
    @ManagerId
    ,@Mark
    ,@Schedule
    ,@Goals
    ,@ToConcede
    ,@HistoryOfTheGap
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

CREATE PROCEDURE [dbo].P_RevelationMyhistoryofthegap_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理ID
	@Mark int, -- 关卡ID
	@Schedule int, -- 小关卡ID
	@Goals int, -- 进球数
	@ToConcede int, -- 失球数
	@HistoryOfTheGap int -- 最大分差
AS



UPDATE [dbo].[Revelation_MyHistoryOfTheGap] SET
	[ManagerId] = @ManagerId
	,[Mark] = @Mark
	,[Schedule] = @Schedule
	,[Goals] = @Goals
	,[ToConcede] = @ToConcede
	,[HistoryOfTheGap] = @HistoryOfTheGap
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



