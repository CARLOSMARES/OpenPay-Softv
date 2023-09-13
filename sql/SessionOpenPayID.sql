USE [NewsoftvWEB]
GO

/****** Object:  Table [dbo].[SessionOpenPayID]    Script Date: 13/09/2023 11:16:09 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SessionOpenPayID](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Clv_Session] [bigint] NULL,
	[Contrato] [bigint] NULL,
	[IdOpenPay] [varchar](150) NULL,
	[JsonNotificacion] [varchar](max) NULL,
	[Clv_Factura] [bigint] NULL,
	[Fecha] [date] NULL,
	[MsgError] [varchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


