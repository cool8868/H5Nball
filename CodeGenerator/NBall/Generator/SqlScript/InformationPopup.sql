
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Informationpopup_Delete    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InformationPopup_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InformationPopup_Delete]
GO

/****** Object:  Stored Procedure [dbo].InformationPopup_GetById    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InformationPopup_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InformationPopup_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].InformationPopup_GetAll    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InformationPopup_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InformationPopup_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].InformationPopup_Insert    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InformationPopup_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InformationPopup_Insert]
GO

/****** Object:  Stored Procedure [dbo].InformationPopup_Update    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InformationPopup_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InformationPopup_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_InformationPopup_Delete
	@Idx int
AS

DELETE FROM [dbo].[Information_Popup]
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

CREATE PROCEDURE [dbo].P_InformationPopup_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Information_Popup] with(nolock)
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

CREATE PROCEDURE [dbo].P_InformationPopup_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Information_Popup] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_InformationPopup_Insert
	@ManagerId uniqueidentifier , 
	@PopType int , 
	@ContentString nvarchar(100) , 
	@IsRead bit , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Information_Popup] (
	[ManagerId]
	,[PopType]
	,[ContentString]
	,[IsRead]
	,[Status]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@PopType
    ,@ContentString
    ,@IsRead
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

CREATE PROCEDURE [dbo].P_InformationPopup_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@PopType int, 
	@ContentString nvarchar(100), -- 内容串，对应静态表拼接
	@IsRead bit, -- 阅读标识
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Information_Popup] SET
	[ManagerId] = @ManagerId
	,[PopType] = @PopType
	,[ContentString] = @ContentString
	,[IsRead] = @IsRead
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



