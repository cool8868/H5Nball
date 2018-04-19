
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagercommonpackage_Delete    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagercommonpackage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagercommonpackage_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagercommonpackage_GetById    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagercommonpackage_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagercommonpackage_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagercommonpackage_GetAll    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagercommonpackage_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagercommonpackage_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagercommonpackage_Insert    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagercommonpackage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagercommonpackage_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagercommonpackage_Update    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagercommonpackage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagercommonpackage_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagercommonpackage_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[NB_ManagerCommonPackage]
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

CREATE PROCEDURE [dbo].P_NbManagercommonpackage_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerCommonPackage] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagercommonpackage_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerCommonPackage] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagercommonpackage_Insert
	@Common1 varchar(500) , 
	@Common2 varchar(500) , 
	@Common3 varchar(500) , 
	@Common4 varchar(500) , 
	@Rowtime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerCommonPackage] (
	[Idx],
	[Common1]
	,[Common2]
	,[Common3]
	,[Common4]
	,[Rowtime]
) VALUES (
	@Idx,
    @Common1
    ,@Common2
    ,@Common3
    ,@Common4
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

CREATE PROCEDURE [dbo].P_NbManagercommonpackage_Update
	@Idx uniqueidentifier, 
	@Common1 varchar(500), 
	@Common2 varchar(500), 
	@Common3 varchar(500), 
	@Common4 varchar(500), 
	@Rowtime datetime 
AS



UPDATE [dbo].[NB_ManagerCommonPackage] SET
	[Common1] = @Common1
	,[Common2] = @Common2
	,[Common3] = @Common3
	,[Common4] = @Common4
	,[Rowtime] = @Rowtime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


