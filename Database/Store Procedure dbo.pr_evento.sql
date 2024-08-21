--USE PruebaNEXTI
--GO

/****** Object:  Table dbo.evento    Script Date: 20/7/2024 10:50:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM sys.all_objects WHERE object_id=OBJECT_ID('dbo.pr_evento') AND type='P')
   DROP PROCEDURE dbo.pr_evento
GO

CREATE PROCEDURE dbo.pr_evento (
	@i_accion         char(1)      = 'L',
	@i_ci_id          bigint       = null,
	@i_fx_fecha       datetime     = null,
	@i_tx_lugar       varchar(100) = null,
	@i_tx_descripcion varchar(500) = null,
	@i_va_precio      money        = null,
	@i_te_estado      char(1)      = 'A',
	@o_msgerror       varchar(200) = '' OUTPUT
)
AS 
BEGIN
    DECLARE @w_ret int = 0
	BEGIN TRY
	    IF @i_accion = 'L'
		BEGIN
		    SELECT ci_id,
				   fx_fecha,
				   tx_lugar,
				   tx_descripcion,
				   va_precio,
				   te_estado
			  FROM dbo.evento
			 WHERE te_estado = 'A'
		END
	    IF @i_accion = 'C'
		BEGIN
		    SELECT ci_id,
				   fx_fecha,
				   tx_lugar,
				   tx_descripcion,
				   va_precio,
				   te_estado
			  FROM dbo.evento
			 WHERE ci_id = @i_ci_id
			   AND te_estado = 'A'
		END
	    IF @i_accion = 'I'
		BEGIN
		    IF @i_fx_fecha IS NULL
			BEGIN
			    SELECT @w_ret = -1,
				       @o_msgerror = 'Debe especificar fecha por favor'
				RETURN @w_ret
			END
		    IF LTRIM(RTRIM(ISNULL(@i_tx_lugar, ''))) IS NULL
			BEGIN
			    SELECT @w_ret = -1,
				       @o_msgerror = 'Debe especificar lugar por favor'
				RETURN @w_ret
			END
		    IF LTRIM(RTRIM(ISNULL(@i_tx_descripcion,''))) = ''
			BEGIN
			    SELECT @w_ret = -1,
				       @o_msgerror = 'Debe especificar descripcion por favor'
				RETURN @w_ret
			END
		    IF ISNULL(@i_va_precio, 0) = 0
			BEGIN
			    SELECT @w_ret = -1,
				       @o_msgerror = 'Debe especificar valor por favor'
				RETURN @w_ret
			END
		    INSERT INTO dbo.evento 
			     ( fx_fecha,
				   tx_lugar,
				   tx_descripcion,
				   va_precio,
				   te_estado )
			SELECT fx_fecha       = @i_fx_fecha,
				   tx_lugar       = @i_tx_lugar,
				   tx_descripcion = @i_tx_descripcion,
				   va_precio      = @i_va_precio,
				   te_estado      = 'A'
			SELECT @o_msgerror = 'Ingreso exitoso'
		END	
	    IF @i_accion = 'U'
		BEGIN
		    UPDATE dbo.evento 
			   SET fx_fecha       = @i_fx_fecha,
				   tx_lugar       = @i_tx_lugar,
				   tx_descripcion = @i_tx_descripcion,
				   va_precio      = @i_va_precio
			 WHERE ci_id          = @i_ci_id
			 SELECT @o_msgerror = 'Actualizacion exitosa'
		END	
	    IF @i_accion = 'E'
		BEGIN
		    UPDATE dbo.evento 
			   SET te_estado = 'I'
			 WHERE ci_id     = @i_ci_id
			 SELECT @o_msgerror = 'Eliminacion exitosa'
		END	
	END TRY
	BEGIN CATCH
	    SELECT @w_ret = -9,
		       @o_msgerror = ERROR_MESSAGE()
	END CATCH
END
GO

