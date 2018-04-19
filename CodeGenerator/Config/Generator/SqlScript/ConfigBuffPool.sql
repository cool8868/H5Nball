
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configbuffpool_Delete    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffpool_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffpool_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigBuffpool_GetById    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffpool_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffpool_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigBuffpool_GetAll    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffpool_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffpool_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigBuffpool_Insert    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffpool_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffpool_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigBuffpool_Update    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigBuffpool_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigBuffpool_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigBuffpool_Delete
	@Id int
AS

DELETE FROM [dbo].[Config_BuffPool]
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

CREATE PROCEDURE [dbo].P_ConfigBuffpool_GetById
	@Id int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_BuffPool] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigBuffpool_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_BuffPool] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigBuffpool_Insert
	@SkillCode varchar(20) , 
	@SkillLevel int , 
	@BuffSrcType int , 
	@BuffUnitType int , 
	@LiveFlag int , 
	@BuffNo int , 
	@DstDir int , 
	@DstMode int , 
	@DstKey varchar(200) , 
	@BuffMap varchar(200) , 
	@BuffVal money , 
	@BuffPer money , 
	@BuffArg varchar(100) , 
	@ExpiryMinutes int , 
	@LimitTimes int , 
	@TotalTimes int , 
	@RepeatBuffFlag bit , 
	@RepeatTimeFlag bit , 
	@RepeatTimesFlag bit , 
	@CoverSkillCode varchar(200) , 
	@Memo nvarchar(500) , 
	@RowTime datetime , 
    @Id int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_BuffPool] (
	[SkillCode]
	,[SkillLevel]
	,[BuffSrcType]
	,[BuffUnitType]
	,[LiveFlag]
	,[BuffNo]
	,[DstDir]
	,[DstMode]
	,[DstKey]
	,[BuffMap]
	,[BuffVal]
	,[BuffPer]
	,[BuffArg]
	,[ExpiryMinutes]
	,[LimitTimes]
	,[TotalTimes]
	,[RepeatBuffFlag]
	,[RepeatTimeFlag]
	,[RepeatTimesFlag]
	,[CoverSkillCode]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillCode
    ,@SkillLevel
    ,@BuffSrcType
    ,@BuffUnitType
    ,@LiveFlag
    ,@BuffNo
    ,@DstDir
    ,@DstMode
    ,@DstKey
    ,@BuffMap
    ,@BuffVal
    ,@BuffPer
    ,@BuffArg
    ,@ExpiryMinutes
    ,@LimitTimes
    ,@TotalTimes
    ,@RepeatBuffFlag
    ,@RepeatTimeFlag
    ,@RepeatTimesFlag
    ,@CoverSkillCode
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

CREATE PROCEDURE [dbo].P_ConfigBuffpool_Update
	@Id int, -- Id
	@SkillCode varchar(20), -- SkillCode
	@SkillLevel int, -- SkillLevel
	@BuffSrcType int, -- 来源类型
	@BuffUnitType int, -- 效果类型
	@LiveFlag int, -- LiveFlag
	@BuffNo int, -- BuffNo
	@DstDir int, -- 目标方
	@DstMode int, -- 目标模式
	@DstKey varchar(200), -- 目标Key
	@BuffMap varchar(200), -- BuffMap
	@BuffVal money, -- Buff值
	@BuffPer money, -- Buff百分比
	@BuffArg varchar(100), -- Buff系数
	@ExpiryMinutes int, -- 有效时间
	@LimitTimes int, -- 限制次数
	@TotalTimes int, -- 有效场次 -1位不计算场次 大于0为持续场次
	@RepeatBuffFlag bit, -- 累计Buff
	@RepeatTimeFlag bit, -- 累计有效时间
	@RepeatTimesFlag bit, -- 累计有效次数
	@CoverSkillCode varchar(200), -- 覆盖技能
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Config_BuffPool] SET
	[SkillCode] = @SkillCode
	,[SkillLevel] = @SkillLevel
	,[BuffSrcType] = @BuffSrcType
	,[BuffUnitType] = @BuffUnitType
	,[LiveFlag] = @LiveFlag
	,[BuffNo] = @BuffNo
	,[DstDir] = @DstDir
	,[DstMode] = @DstMode
	,[DstKey] = @DstKey
	,[BuffMap] = @BuffMap
	,[BuffVal] = @BuffVal
	,[BuffPer] = @BuffPer
	,[BuffArg] = @BuffArg
	,[ExpiryMinutes] = @ExpiryMinutes
	,[LimitTimes] = @LimitTimes
	,[TotalTimes] = @TotalTimes
	,[RepeatBuffFlag] = @RepeatBuffFlag
	,[RepeatTimeFlag] = @RepeatTimeFlag
	,[RepeatTimesFlag] = @RepeatTimesFlag
	,[CoverSkillCode] = @CoverSkillCode
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



