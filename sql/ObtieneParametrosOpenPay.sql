USE [NewsoftvWEB]
GO

/****** Object:  StoredProcedure [dbo].[ObtieneParametrosOpenPay]    Script Date: 13/09/2023 11:13:39 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtieneParametrosOpenPay]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ID,LlavePrivada,LlavePublica,URL
	from CredencialesOpenPay
END
GO


