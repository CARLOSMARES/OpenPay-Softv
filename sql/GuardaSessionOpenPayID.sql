USE [NewsoftvWEB]
GO

/****** Object:  StoredProcedure [dbo].[GuardaSessionOpenPayID]    Script Date: 13/09/2023 11:13:34 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GuardaSessionOpenPayID](@ID varchar(150),@Clv_Session bigint,@Contrato bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into SessionOpenPayID(Clv_Session,Contrato,IdOpenPay,JsonNotificacion,Clv_Factura, Fecha) values(@Clv_Session,@Contrato,@ID,'',0, GetDate())
END
GO


