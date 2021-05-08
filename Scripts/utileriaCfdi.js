/**
 * Función para realizar el cuadre de los decimales para poder timbrar CFDI 3.3
 */
function fn_CuadrarImportesCfdi33(param_dec_Importe, param_dec_Iva, param_dec_IvaRetenido, param_dec_Total, param_lst_obj_concepto) {
    var obj_DatosCuadrados = {
        dec_Importe:        0, 
        dec_Iva:            0, 
        dec_IvaRetenido:    0, 
        dec_Total:          0, 
        lst_obj_concepto:   []
    }
    
    //  ------------------------------------------------------------------
    //  -- AJUSTAMOS IVA E IVARETENIDO PARA EL CUADRE CFDI 3.3
    //  ------------------------------------------------------------------
    let dec_importeTotal = 0,
        dec_importeSumado = 0,
        dec_diferencia = 0,
        bool_validaTipoDato = false;


    dec_importeTotal		= param_dec_Total;
    dec_importeSumado     = obj_ClaseGlobal.fn_truncarRedondear(param_dec_Importe + param_dec_Iva - param_dec_IvaRetenido,2,6);
	//dec_importeSumado       = obj_ClaseGlobal.fn_truncar(obj_ClaseGlobal.fn_truncar(param_dec_Importe,2) + obj_ClaseGlobal.fn_truncar(param_dec_Iva,2) - obj_ClaseGlobal.fn_truncar(param_dec_IvaRetenido,2),2);
    
    dec_diferencia = obj_ClaseGlobal.fn_truncarRedondear(dec_importeTotal-dec_importeSumado,4,6);

    if (Math.abs(dec_diferencia) > 1){
        toastr["success"]("Ocurrió un error al ajustar los importes del documento");
        return false;
    }

	if (Math.abs(dec_diferencia) === 0) {
		obj_DatosCuadrados = {
			int_ajuste:			bool_validaTipoDato,
			dec_Importe:        param_dec_Importe, 
			dec_Iva:            param_dec_Iva, 
			dec_IvaRetenido:    param_dec_IvaRetenido,
			dec_Total:			param_dec_Total,
			lst_obj_concepto:   param_lst_obj_concepto
		}

		return obj_DatosCuadrados;
	}

	// Ajustamos todos los conceptos a 2 decimales por cuadre
	//$.each(param_lst_obj_concepto, function (index, value) {
	//	param_lst_obj_concepto[index].DecImporte	= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecImporte,2),4,6);
	//	param_lst_obj_concepto[index].DecDescuento	= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecDescuento,2),4,6);
	//	param_lst_obj_concepto[index].DecSubtotal	= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecSubtotal,2),4,6);
	//	param_lst_obj_concepto[index].DecIva		= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecIva,2),4,6);
	//	param_lst_obj_concepto[index].DecRetencion	= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecRetencion,2),4,6);
	//	param_lst_obj_concepto[index].DecTotal		= obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecTotal,2),4,6);
	//});

	// Buscamos si tiene iva para hacer el ajuste
    $.each(param_lst_obj_concepto, function (index, value) { 
        if (value.DecIva > 0) {
            param_lst_obj_concepto[index].DecIva = obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecIva,2) + obj_ClaseGlobal.fn_truncar(dec_diferencia,2),4,6);
            param_dec_Iva = obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_dec_Iva,2) + dec_diferencia,4,6);

			param_dec_IvaRetenido	= obj_ClaseGlobal.fn_truncarRedondear(param_dec_IvaRetenido,4,6);
			param_dec_Importe		= obj_ClaseGlobal.fn_truncarRedondear(param_dec_Importe,4,6);

            bool_validaTipoDato = true;
            return false;
        }       

		// Buscamos si tiene ivaRetenido para hacer el ajuste
		if (!bool_validaTipoDato) {
			if (value.DecRetencion > 0) {
				param_lst_obj_concepto[index].DecRetencion	= obj_ClaseGlobal.fn_truncarRedondear(param_lst_obj_concepto[index].DecRetencion + dec_diferencia,4,6);
				param_dec_IvaRetenido	= obj_ClaseGlobal.fn_truncarRedondear(param_dec_IvaRetenido + dec_diferencia,4,6);

				param_dec_Iva			= obj_ClaseGlobal.fn_truncarRedondear(param_dec_Iva,4,6);
				param_dec_Importe		= obj_ClaseGlobal.fn_truncarRedondear(param_dec_Importe,4,6);

				bool_validaTipoDato = true;
				return false;
			}   
		}

		// Si no tiene impuestos, vamos sobre el importe
		if (!bool_validaTipoDato) {
			if (value.DecImporte > 0) {
				param_lst_obj_concepto[index].DecImporte = obj_ClaseGlobal.fn_truncarRedondear(obj_ClaseGlobal.fn_truncar(param_lst_obj_concepto[index].DecImporte,2) + dec_diferencia,4,6);
				param_dec_Importe = obj_ClaseGlobal.fn_truncarRedondear(param_dec_Importe + dec_diferencia,4,6);

				param_dec_Iva			= obj_ClaseGlobal.fn_truncarRedondear(param_dec_Iva,4,6);
				param_dec_IvaRetenido	= obj_ClaseGlobal.fn_truncarRedondear(param_dec_IvaRetenido,4,6);

				bool_validaTipoDato = true;
				return false;
			}   
		}
	 });



    obj_DatosCuadrados = {
		int_ajuste:			bool_validaTipoDato,
        dec_Importe:        param_dec_Importe, 
        dec_Iva:            param_dec_Iva, 
        dec_IvaRetenido:    param_dec_IvaRetenido,
		dec_Total:			dec_importeTotal,
        lst_obj_concepto:   param_lst_obj_concepto
    }

    return obj_DatosCuadrados;
}