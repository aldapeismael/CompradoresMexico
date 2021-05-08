
// ----------------------------------------------------------------------------------------------------------
// Esta función calcula los totales de todos los conceptos finales en base a la moneda, y los impuestos.
// Todos los campos y objetos deben de llamarse igual en los abc para que funcione correctamente.
// ----------------------------------------------------------------------------------------------------------
function fn_CalcularValoresConceptos() {
	// Validamos el arreglo de documentos. Si no hay documentos regresamos.
	if (typeof lst_documentosRelacionados === "undefined" || lst_documentosRelacionados.length === 0) {
		return false;
	}

	// Validaciones previas de impuestos y moneda
	if (parseInt($("#ddl_TipoIva").val() || 0) === 0) {
		toastr["error"]("Debe especificar un tipo de Iva");
		$("#ddl_TipoIva").focus();
		return;
	}

	if (parseInt($("#ddl_TipoRetencion").val() || 0) === 0) {
		toastr["error"]("Debe especificar un tipo de Iva Retenido");
		$("#ddl_TipoRetencion").focus();
		return;
	}

	if (parseInt($("#ddl_Moneda").val() || 0) === 0) {
		toastr["error"]("Debe especificar una Moneda");
		$("#ddl_Moneda").focus();
		return;
	}

	// ----------------------------------------------------------------------------
	// Parametros 
	// ----------------------------------------------------------------------------
	// Listado de Documentos
	var obj_concepto = {};

	// Moneda antigua
	var int_IdMonedaOriginal = obj_monedaActual.IntIdMultilista_moneda;
	var dec_TipoCambioOriginal = obj_monedaActual.DecTipoCambio;

	var int_IdMoneda = parseInt($("#ddl_Moneda").val()) || 0;
	var decTipoCambio = parseFloat($("#ddl_Moneda option:selected").data("tipocambio") || 0);

	// Seteo de valores a cero
	// ----------------------------------------------------------------------------
	var dec_Importe =
		dec_ImporteUnitario =
		dec_ValorUnitario =
		dec_Cantidad =
		int_BAplicarDescuento =
		dec_DescuentoUnitario =
		dec_Descuento =
		dec_SubtotalUnitario =
		dec_SubTotal =
		dec_IvaUnitario =
		dec_Iva =
		int_bIva =
		int_bIvaRetenido =
		dec_IvaRetenidoUnitario =
		dec_IvaRetenido =
		dec_TotalUnitario =
		dec_Total = 0;

	// Factores de impuestos
	// ----------------------------------------------------------------------------
	var factorIva = parseFloat($("#ddl_TipoIva option:selected").data("porcentaje") || 0);
	var factorIvaRetenido = (parseFloat($("#ddl_TipoRetencion option:selected").data("porcentaje")) || 0);

	if (factorIva === 0) {
		factorIvaRetenido = 0;
	}

	// Recorremos todos los documentos para cambiar sus valores por los nuevos segun impuesto y moneda
	// ----------------------------------------------------------------------------
	$.each(lst_documentosRelacionados, function (index, value) {
		if (typeof value.LstObjConvenioConcepto === "undefined") {
			return true;
		}
		// Validamos el arreglo de documentos. Si no hay documentos regresamos.
		if (typeof value.LstObjConvenioConcepto === "undefined" || value.LstObjConvenioConcepto.length === 0) {
			return true;
		}
		dec_ImporteUnitario = 0;
		dec_SubtotalUnitario = 0;
		dec_Importe		= 0;
		dec_SubTotal	= 0;
		dec_Iva			= 0;
		dec_Retencion	= 0;
		dec_Total		= 0;

		$.each(value.LstObjConvenioConcepto, function (index2, value2) {
			// Seteamos los parámetros a cero
			int_bIva = 0;
			int_bIvaRetenido = 0;
			dec_SubtotalUnitario = 0;
			dec_IvaUnitario = 0;
			dec_IvaRetenidoUnitario = 0;
			dec_ValorUnitario = 0;

			// Valores Unitarios
			dec_Cantidad = obj_ClaseGlobal.fn_truncarRedondear(parseFloat(value2.DecCantidad|| 0) , 3, 3);
			dec_ValorUnitario = value2.DecValorUnitario;

			dec_ValorUnitario = fnCalcularValorMoneda(
				int_IdMonedaOriginal,
				dec_TipoCambioOriginal,
				int_IdMoneda,
				decTipoCambio,
				dec_ValorUnitario,
				0)

			dec_ImporteUnitario = obj_ClaseGlobal.fn_truncarRedondear(dec_Cantidad * dec_ValorUnitario, 3, 3); // obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_Cantidad * dec_ValorUnitario, 4), 4);

			dec_SubtotalUnitario = dec_ImporteUnitario;

			// Valores globales del documento
			dec_Importe += dec_ImporteUnitario;
			dec_SubTotal += dec_SubtotalUnitario;

			if (value2.IntBIva === 1) {
				//dec_IvaUnitario = factorIva * dec_SubtotalUnitario;
				dec_IvaUnitario = obj_ClaseGlobal.fn_truncarRedondear(factorIva * dec_SubtotalUnitario, 3, 3); //obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(factorIva * dec_SubtotalUnitario, 6), 4);

				//dec_Iva += dec_IvaUnitario;

				int_bIva = 1;

				// Calculamos el Iva Retenido sólo si existe un IVA Trasladado
				if (value2.IntRetenerIva === 1) {
					//dec_IvaRetenidoUnitario = factorIvaRetenido * dec_SubtotalUnitario;
					dec_IvaRetenidoUnitario = obj_ClaseGlobal.fn_truncarRedondear(factorIvaRetenido * dec_SubtotalUnitario, 3, 3); // obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(factorIvaRetenido * dec_SubtotalUnitario, 6), 4);
					//dec_IvaRetenido += dec_IvaRetenidoUnitario;
					int_bIvaRetenido = 1;
				}
			}

			// Totales
			dec_Iva			+= dec_IvaUnitario;
			dec_Retencion	+= dec_IvaRetenidoUnitario;
			
			dec_TotalUnitario = obj_ClaseGlobal.fn_truncarRedondear(dec_SubtotalUnitario + dec_IvaUnitario - dec_IvaRetenidoUnitario, 3, 3); // obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_SubtotalUnitario + dec_IvaUnitario - dec_IvaRetenidoUnitario,6), 4);

			dec_Total		+= dec_TotalUnitario;

			// Cargamos el arreglo de conceptos
			obj_concepto = {
				IntIdConvenioConcepto:				0,
				IntIdDocumentacionConcepto:			value2.IntIdDocumentacionConcepto,
				DecCantidad:						obj_ClaseGlobal.fn_truncarRedondear(dec_Cantidad, 3, 3),			//obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_Cantidad, 6), 4),
				DecValorUnitario:					obj_ClaseGlobal.fn_truncarRedondear(dec_ValorUnitario, 3, 3),		//obj_ClaseGlobal.fn_truncar(dec_ValorUnitario, 6),
				DecImporte:							obj_ClaseGlobal.fn_truncarRedondear(dec_ImporteUnitario, 3, 3),		//obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_ImporteUnitario, 6), 4),
				DecDescuento:						obj_ClaseGlobal.fn_truncarRedondear(dec_DescuentoUnitario, 3, 3),	//obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_DescuentoUnitario, 6), 4),
				DecSubtotal:						obj_ClaseGlobal.fn_truncarRedondear(dec_SubtotalUnitario, 3, 3),	//obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_SubtotalUnitario, 6), 4),
				IntRetenerIva:						int_bIvaRetenido,
				IntBIva:							int_bIva,
				IntBIeps:							0,
				IntBIsr:							0,
				IntAplicarDescuento:				int_BAplicarDescuento,
				IntIdMultilista_ConceptoUnidad:		value2.IntIdMultilista_ConceptoUnidad,
				IntSatCatalogoCfdi_ClaveUnidad:		value2.IntSatCatalogoCfdi_ClaveUnidad,
				IntSatCatalogoCfdi_ClaveProdServ:	value2.IntSatCatalogoCfdi_ClaveProdServ,

				// Id Impuestos
				IntIdTipoImpuesto_Iva:				$("#ddl_TipoIva option:selected").val(),
				IntIdTipoImpuesto_Ieps:				0,
				IntIdTipoImpuesto_IvaRetencion:		$("#ddl_TipoRetencion option:selected").val(),
				IntIdTipoImpuesto_Isr:				0,

				// Tasas
				DecTasaIva:							obj_ClaseGlobal.fn_truncarRedondear(factorIva, 3, 3),			// obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(factorIva, 6), 4),
				DecTasaRetencion:					obj_ClaseGlobal.fn_truncarRedondear(factorIvaRetenido, 3, 3),	// obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(factorIvaRetenido, 6), 4),
				DecTasaIeps:						0,
				DecTasaIsr:							0,

				// Importes
				DecIva:								obj_ClaseGlobal.fn_truncarRedondear(dec_IvaUnitario, 3, 3),			//obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_IvaUnitario, 6), 4),
				DecRetencion:						obj_ClaseGlobal.fn_truncarRedondear(dec_IvaRetenidoUnitario, 3, 3), //obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_IvaRetenidoUnitario, 6), 4),
				DecIeps:							0,
				DecIsr:								0,
				DecTotal:							obj_ClaseGlobal.fn_truncarRedondear(dec_TotalUnitario, 3, 3), //obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_RoundNumber(dec_TotalUnitario, 6), 4),
				IntBActivo:							1,

				StrDescDocumentacionConcepto:		value2.StrDescDocumentacionConcepto
			}

			lst_documentosRelacionados[index].LstObjConvenioConcepto[index2] = obj_concepto;
		});
		// Fin $.each Conceptos
		//----------------------------------------

		dec_Total		= obj_ClaseGlobal.fn_RoundNumber(dec_SubTotal + dec_Iva - dec_Retencion,2);

		dec_Importe		= obj_ClaseGlobal.fn_RoundNumber(dec_Importe,2);
		dec_SubTotal	= obj_ClaseGlobal.fn_RoundNumber(dec_SubTotal,2);
		dec_Iva			= obj_ClaseGlobal.fn_RoundNumber(dec_Iva,2);
		dec_Retencion	= obj_ClaseGlobal.fn_RoundNumber(dec_Retencion,2);

		if (dec_Total !== obj_ClaseGlobal.fn_RoundNumber(dec_SubTotal + dec_Iva - dec_Retencion,2)) {
			dec_Total		= obj_ClaseGlobal.fn_RoundNumber(dec_SubTotal + dec_Iva - dec_Retencion,2);
		}


		$("#tbl_Conceptos tbody").find("[data-posicion='" + index + "'] .SustituyeGrid").text(dec_Total.formatMoney(2)); //obj_ClaseGlobal.fn_truncar(dec_Total, 4)

		lst_documentosRelacionados[index].DecImporte		= dec_Importe;
		lst_documentosRelacionados[index].DecSubTotal		= dec_SubTotal;
		lst_documentosRelacionados[index].DecIva			= dec_Iva;
		lst_documentosRelacionados[index].DecRetencion		= dec_Retencion;
		lst_documentosRelacionados[index].DecTotal			= dec_Total;

	});

	obj_monedaActual =	{
							IntIdMultilista_moneda: parseInt($("#ddl_Moneda").val())||0,
							DecTipoCambio:			parseFloat($("#ddl_Moneda option:selected").data("tipocambio"))||0
						}
}

// ----------------------------------------------------------------
// Función para el cálculo de un valores en base a la moneda 
// ----------------------------------------------------------------
function fnCalcularValorMoneda(
// Parámetros que se reciben en la función
	// Original
	par_idMonedaOriginal, 
	par_tipoCambioOriginal, 
	
	// Valores Nuevos
	par_idMonedaNueva, 
	par_tipoCambioNuevo,

	// Valor a cambiar
	par_valor,

	// Tipo de cálculo
	par_tipoCalculo	
) {
	var dec_valorFinal

	if (par_tipoCalculo === 0) {
		// LA RELACION DE MONEDAS ES 1-1 --> SE MANTIENE EL TOTAL
		if (par_idMonedaOriginal === par_idMonedaNueva) {
			dec_valorFinal = (par_valor || 0);
		}
						
		// LAS MONEDAS SON DIFERENTES, PERO LA ORIGINAL ES EN PESOS --> SE DIVIDE ENTRE EL TIPO DE CAMBIO NUEVO
		else if (par_idMonedaOriginal !== par_idMonedaNueva && par_tipoCambioOriginal === 1){
			dec_valorFinal = (par_valor || 0) / par_tipoCambioNuevo;
		}
										
		// LAS MONEDAS SON DIFERENTES, LA ORIGINAL NO ESTÁ EN PESOS Y LA FINAL SÍ ES EN PESOS --> SE MULTIPLICA ENTRE EL TIPO DE CAMBIO NUEVO
		else if (par_idMonedaOriginal !== par_idMonedaNueva && par_tipoCambioOriginal > 1 && par_tipoCambioNuevo === 1){
			dec_valorFinal = ((par_valor || 0) * par_tipoCambioOriginal);
		}
										
		// LAS MONEDAS SON DIFERENTES (EJ: DOLAR-EUROS) --> SE SACA EL FACTOR DEL TIPO DE CAMBIO Y MULTIPLICA POR EL VALOR
		else if (par_idMonedaOriginal !== par_idMonedaNueva && par_tipoCambioOriginal > 1){
			dec_valorFinal = ((par_tipoCambioNuevo/par_tipoCambioOriginal) * (par_valor || 0))
		}
	}

	return dec_valorFinal
}