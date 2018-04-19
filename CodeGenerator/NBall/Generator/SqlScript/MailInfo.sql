
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Mailinfo_Delete    Script Date: 2016年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MailInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MailInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].MailInfo_GetById    Script Date: 2016年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MailInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MailInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].MailInfo_GetAll    Script Date: 2016年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MailInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MailInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].MailInfo_Insert    Script Date: 2016年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MailInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MailInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].MailInfo_Update    Script Date: 2016年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MailInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MailInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_MailInfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[Mail_Info]
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

CREATE PROCEDURE [dbo].P_MailInfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mail_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_MailInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mail_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_MailInfo_Insert
	@ManagerId uniqueidentifier , 
	@MailType int , 
	@ContentString nvarchar(500) , 
	@Attachment varbinary(1000) , 
	@HasAttach bit , 
	@IsRead bit , 
	@Status int , 
	@RowTime datetime , 
	@ExpiredTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Mail_Info] (
	[ManagerId]
	,[MailType]
	,[ContentString]
	,[Attachment]
	,[HasAttach]
	,[IsRead]
	,[Status]
	,[RowTime]
	,[ExpiredTime]
) VALUES (
    @ManagerId
    ,@MailType
    ,@ContentString
    ,@Attachment
    ,@HasAttach
    ,@IsRead
    ,@Status
    ,@RowTime
    ,@ExpiredTime
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

CREATE PROCEDURE [dbo].P_MailInfo_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@MailType int, -- 邮件类型，对应静态表获取静态数据
	@ContentString nvarchar(500), -- 内容串，对应静态表拼接
	@Attachment varbinary(1000), -- 附件串，参数以逗号分隔，多个附件用竖线分隔
	@HasAttach bit, -- 是否有附件,领取后变为false
	@IsRead bit, -- 阅读标识
	@Status int, -- 状态，当mailtype=5时，status=0表示未抽卡
	@RowTime datetime, 
	@ExpiredTime datetime 
AS



UPDATE [dbo].[Mail_Info] SET
	[ManagerId] = @ManagerId
	,[MailType] = @MailType
	,[ContentString] = @ContentString
	,[Attachment] = @Attachment
	,[HasAttach] = @HasAttach
	,[IsRead] = @IsRead
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[ExpiredTime] = @ExpiredTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



