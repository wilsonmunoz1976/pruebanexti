--USE PruebaNEXTI
--GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM sys.all_objects WHERE object_id=OBJECT_ID('dbo.evento') AND type='U')
   DROP TABLE dbo.evento
GO

CREATE TABLE dbo.evento (
	ci_id bigint IDENTITY(1,1) NOT NULL,
	fx_fecha datetime NOT NULL,
	tx_lugar varchar(100) NOT NULL,
	tx_descripcion varchar(500) NOT NULL,
	va_precio money NOT NULL,
	te_estado char(1) NOT NULL,
 CONSTRAINT PK_evento PRIMARY KEY CLUSTERED 
(
	ci_id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) 
) 
GO

ALTER TABLE dbo.evento ADD  CONSTRAINT DF_evento_te_estado  DEFAULT ('A') FOR te_estado
GO
