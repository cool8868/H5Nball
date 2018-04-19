
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicequipmentsuit_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipmentsuit_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipmentsuit_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicEquipmentsuit_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipmentsuit_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipmentsuit_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicEquipmentsuit_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipmentsuit_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipmentsuit_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicEquipmentsuit_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipmentsuit_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipmentsuit_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicEquipmentsuit_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipmentsuit_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipmentsuit_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicEquipmentsuit_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_EquipmentSuit]
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

CREATE PROCEDURE [dbo].P_DicEquipmentsuit_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_EquipmentSuit] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicEquipmentsuit_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_EquipmentSuit] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicEquipmentsuit_Insert
	@Idx int
	,@SuitType int
	,@Name nvarchar(50)
	,@Memo1 nvarchar(100)
	,@Memo2 nvarchar(100)
	,@Memo3 nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_EquipmentSuit] (
	[Idx]
	,[SuitType]
	,[Name]
	,[Memo1]
	,[Memo2]
	,[Memo3]
) VALUES (
    @Idx
    ,@SuitType
    ,@Name
    ,@Memo1
    ,@Memo2
    ,@Memo3
)

select @Idx

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

CREATE PROCEDURE [dbo].P_DicEquipmentsuit_Update
	@Idx int, 
	@SuitType int, -- 套装类型
	@Name nvarchar(50), -- 套装名称
	@Memo1 nvarchar(100), -- 3件套描述
	@Memo2 nvarchar(100), -- 5件套描述
	@Memo3 nvarchar(100) -- 7件套描述
AS



UPDATE [dbo].[Dic_EquipmentSuit] SET
	[SuitType] = @SuitType
	,[Name] = @Name
	,[Memo1] = @Memo1
	,[Memo2] = @Memo2
	,[Memo3] = @Memo3
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



