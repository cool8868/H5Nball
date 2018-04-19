
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicclubskill_Delete    Script Date: 2016年6月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicClubskill_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicClubskill_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicClubskill_GetById    Script Date: 2016年6月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicClubskill_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicClubskill_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicClubskill_GetAll    Script Date: 2016年6月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicClubskill_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicClubskill_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicClubskill_Insert    Script Date: 2016年6月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicClubskill_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicClubskill_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicClubskill_Update    Script Date: 2016年6月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicClubskill_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicClubskill_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicClubskill_Delete
	@SkillId int
AS

DELETE FROM [dbo].[Dic_ClubSkill]
WHERE
	[SkillId] = @SkillId

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

CREATE PROCEDURE [dbo].P_DicClubskill_GetById
	@SkillId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ClubSkill] with(nolock)
WHERE
	[SkillId] = @SkillId
	
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

CREATE PROCEDURE [dbo].P_DicClubskill_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ClubSkill] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicClubskill_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillLevel int
	,@SkillName nvarchar(80)
	,@ClubType int
	,@SkillKey nvarchar(100)
	,@SkillValue int
	,@BuffMemo nvarchar(500)
	,@Icon varchar(20)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ClubSkill] (
	[SkillId]
	,[SkillCode]
	,[SkillLevel]
	,[SkillName]
	,[ClubType]
	,[SkillKey]
	,[SkillValue]
	,[BuffMemo]
	,[Icon]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillLevel
    ,@SkillName
    ,@ClubType
    ,@SkillKey
    ,@SkillValue
    ,@BuffMemo
    ,@Icon
    ,@RowTime
)

select @SkillId

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

CREATE PROCEDURE [dbo].P_DicClubskill_Update
	@SkillId int, 
	@SkillCode varchar(20), 
	@SkillLevel int, 
	@SkillName nvarchar(80), 
	@ClubType int, 
	@SkillKey nvarchar(100), 
	@SkillValue int, 
	@BuffMemo nvarchar(500), 
	@Icon varchar(20), 
	@RowTime datetime 
AS



UPDATE [dbo].[Dic_ClubSkill] SET
	[SkillCode] = @SkillCode
	,[SkillLevel] = @SkillLevel
	,[SkillName] = @SkillName
	,[ClubType] = @ClubType
	,[SkillKey] = @SkillKey
	,[SkillValue] = @SkillValue
	,[BuffMemo] = @BuffMemo
	,[Icon] = @Icon
	,[RowTime] = @RowTime
WHERE
	[SkillId] = @SkillId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



