
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerbuffpool_Delete    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffpool_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffpool_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerbuffpool_GetById    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffpool_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffpool_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerbuffpool_GetAll    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffpool_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffpool_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerbuffpool_Insert    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffpool_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffpool_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerbuffpool_Update    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerbuffpool_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerbuffpool_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerbuffpool_Delete
	@Id bigint
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[NB_ManagerBuffPool]
WHERE
	[Id] = @Id
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

CREATE PROCEDURE [dbo].P_NbManagerbuffpool_GetById
	@Id bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerBuffPool] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagerbuffpool_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerBuffPool] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerbuffpool_Insert
	@ManagerId uniqueidentifier , 
	@SkillCode varchar(20) , 
	@SkillLevel int , 
	@BuffSrcType int , 
	@BuffSrcId varchar(50) , 
	@BuffUnitType int , 
	@LiveFlag int , 
	@BuffNo int , 
	@DstDir int , 
	@DstMode int , 
	@DstKey varchar(200) , 
	@BuffMap varchar(200) , 
	@BuffVal money , 
	@BuffPer money , 
	@ExpiryTime datetime , 
	@LimitTimes int , 
	@RemainTimes int , 
	@RowTime datetime , 
    @Id bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerBuffPool] (
	[ManagerId]
	,[SkillCode]
	,[SkillLevel]
	,[BuffSrcType]
	,[BuffSrcId]
	,[BuffUnitType]
	,[LiveFlag]
	,[BuffNo]
	,[DstDir]
	,[DstMode]
	,[DstKey]
	,[BuffMap]
	,[BuffVal]
	,[BuffPer]
	,[ExpiryTime]
	,[LimitTimes]
	,[RemainTimes]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@SkillCode
    ,@SkillLevel
    ,@BuffSrcType
    ,@BuffSrcId
    ,@BuffUnitType
    ,@LiveFlag
    ,@BuffNo
    ,@DstDir
    ,@DstMode
    ,@DstKey
    ,@BuffMap
    ,@BuffVal
    ,@BuffPer
    ,@ExpiryTime
    ,@LimitTimes
    ,@RemainTimes
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

CREATE PROCEDURE [dbo].P_NbManagerbuffpool_Update
	@Id bigint, -- Id
	@ManagerId uniqueidentifier, -- 经理Id
	@SkillCode varchar(20), -- SkillCode
	@SkillLevel int, -- SkillLevel
	@BuffSrcType int, -- BuffSrcType
	@BuffSrcId varchar(50), -- BuffSrcId
	@BuffUnitType int, -- 效果类型
	@LiveFlag int, -- LiveFlag
	@BuffNo int, -- BuffNo
	@DstDir int, -- 目标方
	@DstMode int, -- 目标模式
	@DstKey varchar(200), -- 目标Key
	@BuffMap varchar(200), -- BuffMap
	@BuffVal money, -- Buff值
	@BuffPer money, -- Buff百分比
	@ExpiryTime datetime, -- 失效时间
	@LimitTimes int, -- 限制次数
	@RemainTimes int, -- 剩余场次 -1为不计算场次 大于等于0需计算场次
	@RowTime datetime, -- RowTime
	@RowVersion timestamp -- RowVersion
AS



UPDATE [dbo].[NB_ManagerBuffPool] SET
	[ManagerId] = @ManagerId
	,[SkillCode] = @SkillCode
	,[SkillLevel] = @SkillLevel
	,[BuffSrcType] = @BuffSrcType
	,[BuffSrcId] = @BuffSrcId
	,[BuffUnitType] = @BuffUnitType
	,[LiveFlag] = @LiveFlag
	,[BuffNo] = @BuffNo
	,[DstDir] = @DstDir
	,[DstMode] = @DstMode
	,[DstKey] = @DstKey
	,[BuffMap] = @BuffMap
	,[BuffVal] = @BuffVal
	,[BuffPer] = @BuffPer
	,[ExpiryTime] = @ExpiryTime
	,[LimitTimes] = @LimitTimes
	,[RemainTimes] = @RemainTimes
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



