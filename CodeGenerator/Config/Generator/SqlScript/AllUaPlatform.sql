
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Alluaplatform_Delete    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllUaplatform_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllUaplatform_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllUaplatform_GetById    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllUaplatform_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllUaplatform_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllUaplatform_GetAll    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllUaplatform_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllUaplatform_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllUaplatform_Insert    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllUaplatform_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllUaplatform_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllUaplatform_Update    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllUaplatform_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllUaplatform_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllUaplatform_Delete
	@Idx int
AS

DELETE FROM [dbo].[All_UaPlatform]
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

CREATE PROCEDURE [dbo].P_AllUaplatform_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_UaPlatform] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllUaplatform_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_UaPlatform] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllUaplatform_Insert
	@Idx int
	,@FactoryCode varchar(50)
	,@PlatformCode varchar(50)
	,@ExchangeRate int
	,@CashRate int
	,@ChargeUrl varchar(100)
	,@PlatformUrl varchar(100)
	,@LoginKey varchar(50)
	,@ChargeKey varchar(50)
	,@ClientVersion varchar(50)
	,@UserActionUrl varchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[All_UaPlatform] (
	[Idx]
	,[FactoryCode]
	,[PlatformCode]
	,[ExchangeRate]
	,[CashRate]
	,[ChargeUrl]
	,[PlatformUrl]
	,[LoginKey]
	,[ChargeKey]
	,[ClientVersion]
	,[UserActionUrl]
) VALUES (
    @Idx
    ,@FactoryCode
    ,@PlatformCode
    ,@ExchangeRate
    ,@CashRate
    ,@ChargeUrl
    ,@PlatformUrl
    ,@LoginKey
    ,@ChargeKey
    ,@ClientVersion
    ,@UserActionUrl
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

CREATE PROCEDURE [dbo].P_AllUaplatform_Update
	@Idx int, 
	@FactoryCode varchar(50), -- 工厂编码
	@PlatformCode varchar(50), -- 平台编码
	@ExchangeRate int, -- 兑换点券比例
	@CashRate int, -- 现金比例
	@ChargeUrl varchar(100), 
	@PlatformUrl varchar(100), -- 平台地址
	@LoginKey varchar(50), -- 登录key
	@ChargeKey varchar(50), -- 充值key
	@ClientVersion varchar(50), -- 客户端版本号
	@UserActionUrl varchar(100) 
AS



UPDATE [dbo].[All_UaPlatform] SET
	[FactoryCode] = @FactoryCode
	,[PlatformCode] = @PlatformCode
	,[ExchangeRate] = @ExchangeRate
	,[CashRate] = @CashRate
	,[ChargeUrl] = @ChargeUrl
	,[PlatformUrl] = @PlatformUrl
	,[LoginKey] = @LoginKey
	,[ChargeKey] = @ChargeKey
	,[ClientVersion] = @ClientVersion
	,[UserActionUrl] = @UserActionUrl
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


