
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbsolution_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbSolution_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbSolution_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbSolution_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbSolution_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbSolution_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbSolution_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbSolution_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbSolution_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbSolution_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbSolution_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbSolution_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbSolution_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbSolution_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbSolution_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbSolution_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[NB_Solution]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_NbSolution_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_Solution] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_NbSolution_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_Solution] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbSolution_Insert
	@FormationId int , 
	@PlayerString char(65) , 
	@SkillString varchar(800) , 
	@FormationData varbinary(200) , 
	@VeteranCount int , 
	@Status int , 
	@RowTime datetime , 
	@OrangeCount int , 
	@CombCount int , 
	@HirePlayerString char(65) , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_Solution] (
	[ManagerId],
	[FormationId]
	,[PlayerString]
	,[SkillString]
	,[FormationData]
	,[VeteranCount]
	,[Status]
	,[RowTime]
	,[OrangeCount]
	,[CombCount]
	,[HirePlayerString]
) VALUES (
	@ManagerId,
    @FormationId
    ,@PlayerString
    ,@SkillString
    ,@FormationData
    ,@VeteranCount
    ,@Status
    ,@RowTime
    ,@OrangeCount
    ,@CombCount
    ,@HirePlayerString
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

CREATE PROCEDURE [dbo].P_NbSolution_Update
	@ManagerId uniqueidentifier, 
	@FormationId int, -- 阵型id
	@PlayerString char(65), -- 球员串,11个球员的pid，从守门员开始，以逗号分隔
	@SkillString varchar(800), -- 技能串，11个位置，从守门员开始，没有填空,以逗号分隔
	@FormationData varbinary(200), -- 阵型等级信息
	@VeteranCount int, -- 元老数量
	@Status int, 
	@RowTime datetime, 
	@OrangeCount int, 
	@CombCount int, 
	@HirePlayerString char(65) 
AS



UPDATE [dbo].[NB_Solution] SET
	[FormationId] = @FormationId
	,[PlayerString] = @PlayerString
	,[SkillString] = @SkillString
	,[FormationData] = @FormationData
	,[VeteranCount] = @VeteranCount
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[OrangeCount] = @OrangeCount
	,[CombCount] = @CombCount
	,[HirePlayerString] = @HirePlayerString
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



