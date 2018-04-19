
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmanagerwillparttips_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwillparttips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwillparttips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwillparttips_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwillparttips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwillparttips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicManagerwillparttips_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwillparttips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwillparttips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicManagerwillparttips_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwillparttips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwillparttips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicManagerwillparttips_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicManagerwillparttips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicManagerwillparttips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicManagerwillparttips_Delete
	@Id int
AS

DELETE FROM [dbo].[Dic_ManagerWillPartTips]
WHERE
	[Id] = @Id

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

CREATE PROCEDURE [dbo].P_DicManagerwillparttips_GetById
	@Id int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWillPartTips] with(nolock)
WHERE
	[Id] = @Id
	
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

CREATE PROCEDURE [dbo].P_DicManagerwillparttips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ManagerWillPartTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicManagerwillparttips_Insert
	@SkillId int , 
	@SkillCode varchar(20) , 
	@ItemCode int , 
	@Pid int , 
	@PName nvarchar(80) , 
	@PNickName nvarchar(80) , 
	@PColor int , 
	@PColorMemo nvarchar(80) , 
	@ReqStrength int , 
	@BuffMemo nvarchar(500) , 
	@BuffArg money , 
	@BuffArg2 money , 
	@Icon varchar(20) , 
	@Memo nvarchar(500) , 
	@RowTime datetime , 
    @Id int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Dic_ManagerWillPartTips] (
	[SkillId]
	,[SkillCode]
	,[ItemCode]
	,[Pid]
	,[PName]
	,[PNickName]
	,[PColor]
	,[PColorMemo]
	,[ReqStrength]
	,[BuffMemo]
	,[BuffArg]
	,[BuffArg2]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@ItemCode
    ,@Pid
    ,@PName
    ,@PNickName
    ,@PColor
    ,@PColorMemo
    ,@ReqStrength
    ,@BuffMemo
    ,@BuffArg
    ,@BuffArg2
    ,@Icon
    ,@Memo
    ,@RowTime
)


SET @Id = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_DicManagerwillparttips_Update
	@Id int, -- Id
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@ItemCode int, -- ItemCode
	@Pid int, -- Pid
	@PName nvarchar(80), -- PName
	@PNickName nvarchar(80), -- PNickName
	@PColor int, -- PColor
	@PColorMemo nvarchar(80), -- PColorMemo
	@ReqStrength int, -- ReqStrength
	@BuffMemo nvarchar(500), -- 效果描述
	@BuffArg money, -- BuffArg
	@BuffArg2 money, -- BuffArg2
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_ManagerWillPartTips] SET
	[SkillId] = @SkillId
	,[SkillCode] = @SkillCode
	,[ItemCode] = @ItemCode
	,[Pid] = @Pid
	,[PName] = @PName
	,[PNickName] = @PNickName
	,[PColor] = @PColor
	,[PColorMemo] = @PColorMemo
	,[ReqStrength] = @ReqStrength
	,[BuffMemo] = @BuffMemo
	,[BuffArg] = @BuffArg
	,[BuffArg2] = @BuffArg2
	,[Icon] = @Icon
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



