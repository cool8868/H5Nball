
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dailycupinfo_Delete    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].DailycupInfo_GetById    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DailycupInfo_GetAll    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DailycupInfo_Insert    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].DailycupInfo_Update    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DailycupInfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[DailyCup_Info]
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

CREATE PROCEDURE [dbo].P_DailycupInfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_DailycupInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DailycupInfo_Insert
	@Idx int
	,@Round int
	,@OpenGambleRound int
	,@AttendDate datetime
	,@RunDate datetime
	,@Status int
	,@RowTime datetime
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[DailyCup_Info] (
	[Idx]
	,[Round]
	,[OpenGambleRound]
	,[AttendDate]
	,[RunDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @Idx
    ,@Round
    ,@OpenGambleRound
    ,@AttendDate
    ,@RunDate
    ,@Status
    ,@RowTime
    ,@UpdateTime
)

select @Idx

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

CREATE PROCEDURE [dbo].P_DailycupInfo_Update
	@Idx int, -- 第几届
	@Round int, -- 该杯赛共有几轮
	@OpenGambleRound int, -- 开奖到第几轮了
	@AttendDate datetime, -- 报名日期
	@RunDate datetime, -- 运行日期
	@Status int, -- 0，初始；1，报名截止；2，比赛结束；3，已发送金币奖励；4，已发送积分奖励
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[DailyCup_Info] SET
	[Round] = @Round
	,[OpenGambleRound] = @OpenGambleRound
	,[AttendDate] = @AttendDate
	,[RunDate] = @RunDate
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



