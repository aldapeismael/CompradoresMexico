
// ------------------------------------------------------------------------------------------------
// Esta función se encarga de cancelar un documento completo (documentacion, cansus, pedidos, etc)
// ------------------------------------------------------------------------------------------------
function fn_Eliminar(par_str_idDocumento, par_int_idDocumento, par_int_idpedido, par_int_paso) {

	// Seteamos la variable paso si no trae valor
	if (typeof par_int_paso === 'undefined' || par_int_paso === null) {
		par_int_paso = 0;
	}

	// Seteamos la idPedido si no trae valor
	if (typeof par_int_idpedido === 'undefined' || par_int_idpedido === null) {
		par_int_idpedido = 0;
	}

	if (par_int_paso === 0) {
		swal({//metodo para el mensage de confirm
			title: "Cancelar Documento",
			text: "¿Desea Cancelar el Documento con la referencia " + par_str_idDocumento +"?",
			type: "warning",//tipo de mensage =>  opciones: warning, error, info y success
			showCancelButton: true,//mostrar boton de cancelacion
			confirmButtonColor: hex_str_ColorBotonConfirmar,//color de boton de confirmacion
			confirmButtonText: "Sí!",//texto en el boton de confirmacion
			cancelButtonText: "No!",//texto en el boton de cancelacion
			closeOnConfirm: true,//la ventana de cerrara al momento de seleccionar la opcion de confirmacion
			closeOnCancel: true,//la ventana de cerrara al momento de seleccionar la opcion de cancelacion
		},
		function (isConfirm) {
			if (isConfirm) { //si el usuario confirma eliminar
				fn_Eliminar(par_str_idDocumento, par_int_idDocumento, par_int_idpedido, 1);
			}
		});
	} else if (par_int_paso === 1){
		swal.close();
		setTimeout(function () {
				if (par_int_idpedido > 0) {
					swal({//metodo para el mensage de confirm
						title: "Cancelar Pedido",
						text: "¿Desea Cancelar el Pedido relacionado al documento " + par_str_idDocumento +"?",
						type: "warning",//tipo de mensage =>  opciones: warning, error, info y success
						showCancelButton: true,//mostrar boton de cancelacion
						confirmButtonColor: hex_str_ColorBotonConfirmar,//color de boton de confirmacion
						confirmButtonText: "Sí!",//texto en el boton de confirmacion
						cancelButtonText: "No!",//texto en el boton de cancelacion
						closeOnConfirm: true,//la ventana de cerrara al momento de seleccionar la opcion de confirmacion
						closeOnCancel: true,//la ventana de cerrara al momento de seleccionar la opcion de cancelacion
					},
					function (isConfirm) {
						if (isConfirm) {//si el usuario confirma eliminar
							fn_Cancelar(true, par_int_idDocumento, par_int_idpedido);
						} else {
							fn_Cancelar(false,par_int_idDocumento,par_int_idpedido);
						}
					});
				} else {
					fn_Cancelar(false,par_int_idDocumento,0);
				}
		}, 400)
	}
}

// FUNCION PARA CANCELAR UN MOVIMIENTO
function fn_Cancelar(par_bool_CancelaPedido, par_int_idDocumento, par_int_idpedido) {
	//alert("Cancelado");

	var obj_EliminarPedido = {
		IntIdDocumento: par_int_idDocumento,
		IntIdPedido: par_int_idpedido,
		IntBEliminarPedido: (par_bool_CancelaPedido ? 1 : 0),
	}


	fn_cargando(1);
	$.ajax({
		//url: "@VariableGlobal.StrUrlApi/Documento/?ParamIntIdDocumento=" + int_id + "&ParamIntIdPedido=" + int_idPedido + "&"  //se llama la metodo delete de ApiController
		url: strVariableGlobal //se llama la metodo delete de ApiController
		, type: "DELETE"
		, contentType: "application/json; charset=utf-8"
		, dataType: "json"
		//, async: false
		, data: JSON.stringify(obj_EliminarPedido)
		, success: function (obj_Datos) {

			toastr[obj_Datos["StrTipoError"]](obj_Datos["StrMensajeError"]);//mostramos la respuesta del ajax

			var indexPage = obj_DataTable.page();//obtenemos la pagina que se encuentra el datatable

			obj_DataTable.ajax.reload();//se recarga el objeto datatable con los datos actualizados
			obj_DataTable.page(indexPage).draw("page");//se mantiene en la pagina que estava
			fn_cargando(0);
		}
		, error: function (xhr, ajaxOptions, thrownError) {
			fn_cargando(0);
			toastr["error"]("Ocurrió un error al cancelar el documento. Error: " + thrownError);
		}
	});

	int_id = 0;
	int_idPedido = 0;
	str_referencia = "";
}

// ------------------------------------------------------------------------------------------------
// Esta función se encarga de cancelar una CANSUS MULTIPLE
// ------------------------------------------------------------------------------------------------
function fn_EliminarCansusMultiple(par_str_idDocumento, par_int_idDocumento, par_int_bCansus) {

		swal({//metodo para el mensage de confirm
			title: "Cancelar Cansus Múltiple",
			text: "¿Desea cancelar la Cansus Múltiple del Documento " + par_str_idDocumento +"?",
			type: "warning",//tipo de mensage =>  opciones: warning, error, info y success
			showCancelButton: true,//mostrar boton de cancelacion
			confirmButtonColor: hex_str_ColorBotonConfirmar,//color de boton de confirmacion
			confirmButtonText: "Sí!",//texto en el boton de confirmacion
			cancelButtonText: "No!",//texto en el boton de cancelacion
			closeOnConfirm: true,//la ventana de cerrara al momento de seleccionar la opcion de confirmacion
			closeOnCancel: true,//la ventana de cerrara al momento de seleccionar la opcion de cancelacion
		},
		function (isConfirm) {
			if (isConfirm) { //si el usuario confirma eliminar
				fn_CancelarCansusMultiple(par_int_idDocumento, par_int_bCansus);
			}
		});
}

// FUNCION PARA CANCELAR UN MOVIMIENTO
function fn_CancelarCansusMultiple( par_int_idDocumento, par_int_cancelacionSustitucion) {
	//alert("Cancelado");

	var obj_DocumentoEliminarControlador = {
		IntIdDocumento: par_int_idDocumento,
		IntBCancelacionSustitucionMultiple: par_int_cancelacionSustitucion,
	}
	
	//fn_cargando(1);

	$.ajax({
		url: strVariableGlobal //se llama al metodo delete de ApiController CancelacionSustitucionMultiple
		, type: "DELETE"
		, contentType: "application/json; charset=utf-8"
		, dataType: "json"
		//, async: false
		, data: JSON.stringify(obj_DocumentoEliminarControlador)
		, success: function (obj_Datos) {

			toastr[obj_Datos["StrTipoError"]](obj_Datos["StrMensajeError"]);//mostramos la respuesta del ajax

			var indexPage = obj_DataTable.page();//obtenemos la pagina que se encuentra el datatable

			obj_DataTable.ajax.reload();//se recarga el objeto datatable con los datos actualizados
			obj_DataTable.page(indexPage).draw("page");//se mantiene en la pagina que estaba
			//fn_cargando(0);
		}
		, error: function (xhr, ajaxOptions, thrownError) {
			//fn_cargando(0);
			toastr["error"]("Ocurrió un error al cancelar el documento. Error: " + thrownError);
		}
	});

	int_id = 0;
	str_referencia = "";
}





// Método para mostrar o quitar la pantalla de cargando
function fn_cargando(par_metodo) {
	if (par_metodo === 1) {
			$("html").loading({ message: "Cargando información, espere..." });
	} else {
		$("html").loading('stop');
	}
}