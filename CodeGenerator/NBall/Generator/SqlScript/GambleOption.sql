
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gambleoption_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleOption_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleOption_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleOption_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleOption_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleOption_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleOption_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleOption_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleOption_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleOption_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleOption_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleOption_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleOption_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleOption_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleOption_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleOption_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Gamble_Option]
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

CREATE PROCEDURE [dbo].P_GambleOption_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Option] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleOption_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Option] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleOption_Insert
	@TitleId uniqueidentifier , 
	@OptionContent nvarchar(200) , 
	@Status int , 
	@Rowtime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Option] (
	[Idx],
	[TitleId]
	,[OptionContent]
	,[Status]
	,[Rowtime]
) VALUES (
	@Idx,
    @TitleId
    ,@OptionContent
    ,@Status
    ,@Rowtime
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

CREATE PROCEDURE [dbo].P_GambleOption_Update
	@Idx uniqueidentifier, 
	@TitleId uniqueidentifier, 
	@OptionContent nvarchar(200), -- 选项
	@Status int, -- 状态（1表示该选项获胜）
	@Rowtime datetime -- 创建时间
AS



UPDATE [dbo].[Gamble_Option] SET
	[TitleId] = @TitleId
	,[OptionContent] = @OptionContent
	,[Status] = @Status
	,[Rowtime] = @Rowtime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


