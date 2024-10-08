﻿$(function () {
	$('#btn_nuevo').off('click');
	$('#btn_nuevo').click(
		function (e) {
			data_url = ($(this).attr("data-url"));
			Mostrar_form_nuevo(data_url);
	});

	$('#btn_regresar').off('click');
	$('#btn_regresar').click(
		function (e) {
			data_url = ($(this).attr("data-url"));
			$.get(data_url, {},
				function (result) {
					$("#div_body").html(result).fadeIn(300);
				}).fail(
					function ()
					{
						$('#success-msj').html("Error inesperado al cargar registros");
						$('#success-alert').fadeIn(300).delay(3000).slideUp(400);
					});
	});

	$('#btn_guardar').off('click');
	$('#btn_guardar').click(
		function () {
			data_url = ($(this).attr("data-url"));

			$('#btn_guardar').prop('disabled', true);
			$.ajax({
				url: data_url,
				type: "POST",
				data: {
					ci_id: 0,
				fx_fecha: $('#txtFecha').val(),
				tx_lugar: $('#txtLugar').val(),
				tx_descripcion: $('#txtDescripcion').val(),
				va_precio: $('#txtPrecio').val(),
				te_estado: "A"
							}
			}).done(
				function (result) {
					alert(result[1]);
					$('#warning-msj').html(result[1]);
					$('#warning-alert').fadeIn(300).delay(3000).slideUp(400);
					$('#txtFecha').val("").focus();
					$('#txtLugar').val("");
					$('#txtDescripcion').val("");
					$('#txtPrecio').val("");
					EnviarHome();
				});
		});


	$('.btn_editar').off('click');
	$('.btn-editar').on('click',
		function () {
			data_url = ($(this).attr("data-url"));
			codigo = ($(this).attr("id"));

			$.post(data_url, { i_ci_id: codigo },
				function (result) {
					$('#div_body').html(result).fadeIn(300);
					}).fail(
						function () {
							$('#success-msj').html("Error inesperado");
							$('#success-alert').fadeIn(300).delay(3000).slideUp(400);
						});
				});

	$('.btn-eliminar').off('click');
	$('.btn-eliminar').on('click',
		function () {
			data_url = ($(this).attr("data-url"));
			codigo = ($(this).attr("id"));

			var result = confirm("Esta seguro de eliminar el codigo " + codigo + "?");

			if (result) {
				$.post(data_url, { i_ci_id: codigo },
					function (result) {
						alert(result[1]);
					}).fail(
						function () {
							alert("Error inesperado");
							$('#success-msj').html("Error inesperado");
							$('#success-alert').fadeIn(300).delay(3000).slideUp(400);
					});
					EnviarHome();
			}
		});


	$('#btn_actualizar').off('click');
	$('#btn_actualizar').click(
		function () {
			$('#btn_actualizar').prop('disabled', true);
			data_url = ($(this).attr("data-url"));
			$.ajax({
				url: data_url,
				type: "POST",
				data: {
					ci_id: $('#txt_ci_id').val(),
					fx_fecha: $('#txtFecha').val(),
					tx_lugar: $('#txtLugar').val(),
					tx_descripcion: $('#txtDescripcion').val(),
					va_precio: $('#txtPrecio').val(),
					te_estado: "A"
				  }
			}).done(
				function (data) {
					alert(data[1]);
					$('#warning-msj').html(data[1]);
					$('#warning-alert').fadeIn(300).delay(3000).slideUp(400);
					$('#txtFecha').val("").focus();
					$('#txtLugar').val("");
					$('#txtDescripcion').val("");
					$('#txtPrecio').val("");
					EnviarHome();
				});
			});

	function Mostrar_form_nuevo(data_url)
	{
		$.get(data_url, {},
			function (result) {
			$('#div_body').html(result).fadeIn(300);
			}).fail(
				function (result) {
					$('#success-msj').html("Error inesperado al cargar registros");
					$('#success-alert').fadeIn(300).delay(3000).slideUp(400);
				});
	}

	function EnviarHome() {
		data_url = '/Home/Regresar';

		$.get(data_url, { }, function (result) {
			$('#princBody').html(result).fadeIn(300);
		}).fail(
			function (result) {
				$('#success-msj').html("Error inesperado al cargar registros");
				$('#success-alert').fadeIn(300).delay(3000).slideUp(400);
			});
	}
});
