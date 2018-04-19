
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configbuffengine_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffengine_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffengine_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigBuffengine_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffengine_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffengine_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigBuffengine_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffengine_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffengine_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigBuffengine_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffengine_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffengine_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigBuffengine_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffengine_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffengine_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigBuffengine_Delete
	@Id int
AS

DELETE FROM [dbo].[Config_BuffEngine]
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

CREATE PROCEDURE [dbo].P_ConfigBuffengine_GetById
	@Id int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_BuffEngine] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigBuffengine_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_BuffEngine] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigBuffengine_Insert
	@SkillCode varchar(20) , 
	@SkillLevel int , 
	@BuffSrcType int , 
	@BuffUnitType int , 
	@LiveFlag int , 
	@CheckMode int , 
	@CheckKey varchar(200) , 
	@CalcMode int , 
	@SrcDir int , 
	@SrcMode int , 
	@SrcKey varchar(200) , 
	@DstDir int , 
	@DstMode int , 
	@DstKey varchar(200) , 
	@BuffMap varchar(200) , 
	@BuffVal money , 
	@BuffPer money , 
	@BuffArg varchar(100) , 
	@Memo nvarchar(500) , 
	@RowTime datetime , 
    @Id int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_BuffEngine] (
	[SkillCode]
	,[SkillLevel]
	,[BuffSrcType]
	,[BuffUnitType]
	,[LiveFlag]
	,[CheckMode]
	,[CheckKey]
	,[CalcMode]
	,[SrcDir]
	,[SrcMode]
	,[SrcKey]
	,[DstDir]
	,[DstMode]
	,[DstKey]
	,[BuffMap]
	,[BuffVal]
	,[BuffPer]
	,[BuffArg]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillCode
    ,@SkillLevel
    ,@BuffSrcType
    ,@BuffUnitType
    ,@LiveFlag
    ,@CheckMode
    ,@CheckKey
    ,@CalcMode
    ,@SrcDir
    ,@SrcMode
    ,@SrcKey
    ,@DstDir
    ,@DstMode
    ,@DstKey
    ,@BuffMap
    ,@BuffVal
    ,@BuffPer
    ,@BuffArg
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

CREATE PROCEDURE [dbo].P_ConfigBuffengine_Update
	@Id int, -- Id
	@SkillCode varchar(20), -- SkillCode
	@SkillLevel int, -- SkillLevel
	@BuffSrcType int, -- 来源类型
	@BuffUnitType int, -- 效果类型
	@LiveFlag int, -- LiveFlag
	@CheckMode int, -- 检查模式
	@CheckKey varchar(200), -- 检查Key
	@CalcMode int, -- 运算模式
	@SrcDir int, -- 源方
	@SrcMode int, -- 源模式
	@SrcKey varchar(200), -- 源Key
	@DstDir int, -- 目标方
	@DstMode int, -- 目标模式
	@DstKey varchar(200), -- 目标Key
	@BuffMap varchar(200), -- BuffMap
	@BuffVal money, -- Buff值
	@BuffPer money, -- Buff百分比
	@BuffArg varchar(100), -- Buff系数
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Config_BuffEngine] SET
	[SkillCode] = @SkillCode
	,[SkillLevel] = @SkillLevel
	,[BuffSrcType] = @BuffSrcType
	,[BuffUnitType] = @BuffUnitType
	,[LiveFlag] = @LiveFlag
	,[CheckMode] = @CheckMode
	,[CheckKey] = @CheckKey
	,[CalcMode] = @CalcMode
	,[SrcDir] = @SrcDir
	,[SrcMode] = @SrcMode
	,[SrcKey] = @SrcKey
	,[DstDir] = @DstDir
	,[DstMode] = @DstMode
	,[DstKey] = @DstKey
	,[BuffMap] = @BuffMap
	,[BuffVal] = @BuffVal
	,[BuffPer] = @BuffPer
	,[BuffArg] = @BuffArg
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



