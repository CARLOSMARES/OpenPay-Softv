USE [NewsoftvWEB]
GO

/****** Object:  StoredProcedure [dbo].[AfectaPagoNotificacionOpenPay]    Script Date: 13/09/2023 11:13:29 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AfectaPagoNotificacionOpenPay](@Clv_Session bigint,@ID varchar(150),@JsonResponse varchar(max),@type varchar(150))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare  @BndError int , @Msg varchar(250) , @Clv_FacturaSalida bigint,@Tipo varchar(10),@Contrato bigint
	set @Tipo='C'
	Declare @Cajera varchar(11)=''
	Declare @Sucursal int=0, @Caja int=0
	Declare @Fecha date = SYSDATETIME()

    if exists(Select 1 from SessionOpenPayID where IdOpenPay=@ID and Clv_Session=@Clv_Session and CLv_Factura=0) and @type = 'charge.succeeded'
	begin
		select @Contrato=Contrato from SessionOpenPayID where IdOpenPay=@ID and Clv_Session=@Clv_Session and CLv_Factura=0

		exec DameDetalle @Clv_session ,1
		
		select top 1 @Sucursal=Clv_Sucursal from SUCURSALES where Nombre like '%OpenPay%'
		if @Sucursal is null set @Sucursal = 0
		select top 1 @Caja = Clave from CatalogoCajas where Descripcion like '%OpenPay%'
		if @Caja is null set @Caja = 0
		select top 1 @Cajera = Clv_Usuario from Usuarios where Nombre like '%OpenPay%'
		if @Cajera is null set @Cajera = ''
		Exec GrabaFacturas_2 @Contrato, @clv_Session, @Cajera, @Sucursal, @Caja, @Tipo, '', 0, 0, @BndError output, @Msg output, @Clv_FacturaSalida output,0,0,0,0,0,'',0,0,'','',0,0
		if @Clv_FacturaSalida is null set @Clv_FacturaSalida = 0
		if @Clv_FacturaSalida > 0
		begin
			update SessionOpenPayID set CLv_Factura=@Clv_FacturaSalida,JsonNotificacion=@JsonResponse, Fecha=GETDATE() where Clv_Session=@Clv_Session
		end
		else
		begin
			update SessionOpenPayID set MsgError = 'No se Grabo el Pago' where Clv_Session = @Clv_Session
		end
	end
	else if exists(Select 1 from SessionOpenPayID where IdOpenPay=@ID and Clv_Session=@Clv_Session and CLv_Factura=0)
	begin
		update SessionOpenPayID  set JsonNotificacion=@JsonResponse, Fecha=GETDATE()  where Clv_Session=@Clv_Session

	end
END
GO


