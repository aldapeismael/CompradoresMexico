//variables globales
var str_MensajeCargando = '<span style="text-align:center">Cargando pagina, por favor espere.</span>';
var reg_str_FormatoFecha = /^(0[1-9]|[12][0-9]|3[01])[- \/.](0[1-9]|1[012])[- \/.](19|20)\d\d$/;
var hex_str_ColorBotonConfirmar = "#000000";
var obj_respuestaCodigoAutorizacion;
var int_MenuAlturaInicial = 0; //Altura inicial del navbar
var int_bCalculaTamano = 0; //variable para saber si es de menor a mayor tamaño o viceversa
var str_CaracteresPermitidos = '^[A-Z a-z0-9ÑñáéíóúÁÉÍÓÚ.:;_$.,%&@?¿(){}+"/\\-\\#*]+$';


//configuraciones globales para elementos de javascript
// Eventos Load
// --------------------------------------------------------------------------------------------------------

$(document).ready(function () {

    // Carga las funciones principales necesarias para las páginas
    //instanciamos todos los objetos
    obj_ClaseGlobal = new class_global();
    obj_ClaseToastr = new class_toastr();
    obj_ClaseDataTable = new class_datatable();

    // Agrega los estilos del template inspinia a los checkbox
    obj_ClaseIcheck = new class_iCheck();

    obj_ClaseGlobal.fn_CargaInicial();
    obj_ClaseToastr.fn_cargaOpcionesToastr();
});


$(window).resize(function () {
    var int_MargenEncabezado = 0;
    var int_AltoHeading = $(".page-heading").css("position") === "fixed" ? $(".page-heading").height() : 0;
    var int_MenuNuevaAltura = $('.menu-operativo').outerHeight();

    // Agrega un margin superior a la clase contenido-inicial
    if ($(".page-heading").css("position") !== "fixed") {
        $(".contenido-inicial").css("margin-top", "0");
    } else {
        $(".contenido-inicial").css("margin-top", int_AltoHeading + "px");
    }

    if (int_MenuAlturaInicial < int_MenuNuevaAltura) {
        int_bCalculaTamano = 1
    } else if (int_MenuAlturaInicial >= int_MenuNuevaAltura) {
        int_bCalculaTamano = 2
    }

    var intCalculoNavbar = Math.abs(int_MenuNuevaAltura - int_MenuAlturaInicial);

    if (int_bCalculaTamano == 1) { // De mayor a menor tamaño
        $(".wrapper").css("margin-top", intCalculoNavbar + "px");
    } else if (int_bCalculaTamano == 2) { // de menor a mayor tamaño
        $(".wrapper").css("margin-top", "0");
    }

    // Seteamos el margen para el encabezado fijo de un datatable
    int_MargenEncabezado = $("body").width() <= 768 ?
        $('.navbar').outerHeight() :
        $('.navbar').outerHeight() + $('.page-heading').outerHeight();

    // Revisa si existe un menu fijo de un datatable para recalcular su ancho
    if ($(".table:not(.fixedHeader-floating, .treetable, .tabla-grid) tbody").length) {
        var tbl_tabla = $('.table:not(.fixedHeader-floating):not(.treetable):not(.tabla-grid)').DataTable();
        setTimeout(function () {
            tbl_tabla.fixedHeader.headerOffset(int_MargenEncabezado);
            tbl_tabla.fixedHeader.adjust();
        }, 1000);
    }

	if ($(".ejecutivo").length) {
		$('body.fixed-nav #wrapper .navbar-static-side, body.fixed-nav #wrapper #page-wrapper').css('margin-top', '0');
		if ($(".navbar-fixed-top").length) {
			var marginTop = ($(".navbar-fixed-top").height() || 0);
			$(".fixed-nav #wrapper .page-heading").css("margin-top", marginTop + "px");
			$(".wrapper-content").css("margin-top", marginTop + "px");
		}
	}
});
// Fin Resize

//funciones globales
class class_global {
    constructor() {

    }

    fn_DataGridGenericoImprimir(p_str_IdControl, p_str_JsonDatos, p_str_json_PiePagina, p_str_JsonFiltros) {
        var obj_DataGrid = $(p_str_IdControl).dxDataGrid('instance');
        var obj_DataSource = obj_DataGrid.option('dataSource');
        var obj_QueryDatos = DevExpress.data.query(obj_DataSource);
        var obj_DatosFiltrados = obj_QueryDatos.filter(obj_DataGrid.getCombinedFilter()).toArray();
        var int_ContadorCabecera = 0;
        var str_HtmlTabla = '';
        var int_ContadorFiltros = 0;
        if (obj_DatosFiltrados.length <= 0) {
            obj_DatosFiltrados = p_str_JsonDatos;
        }
        var int_ContColumna = obj_DataGrid.columnCount(),
            obj_ColumnasOcultas = [],
            obj_ColumnasVisibles = [];

        if (typeof p_str_JsonFiltros !== 'undefined') {
            str_HtmlTabla += '<table align=center border=0 cellpadding=0 cellspacing=1 class="table table-bordered table-hover tabla-grid" style="max-width: 1000px">';
            str_HtmlTabla += '<tr>';
            for (var key in p_str_JsonFiltros) {
                if (p_str_JsonFiltros.hasOwnProperty(key)) {
                    str_HtmlTabla += '<td class=titulo-header>' + key + ':</td>';
                    str_HtmlTabla += '<td class="titulo-detalle">' + p_str_JsonFiltros[key] + '</td>';
                    int_ContadorFiltros++;
                    if (int_ContadorFiltros > 2) {
                        int_ContadorFiltros = 0;
                        str_HtmlTabla += '</tr><tr>';
                    }
                }
            }
            str_HtmlTabla += '</tr>';
            str_HtmlTabla += '</table>';
        }
        //Obtener las columnas visibles en los filtros y no visibles
        for (var i = 0; i < int_ContColumna; i++) {
            if (!obj_DataGrid.columnOption(i, "visible")) {
                obj_ColumnasOcultas.push(obj_DataGrid.columnOption(i));
            } else {
                obj_ColumnasVisibles.push(obj_DataGrid.columnOption(i));
            }
        }
        str_HtmlTabla += '<table id="tblrep" align=center cellpadding=0 cellspacing=0 border="1" ';
        str_HtmlTabla += '<tr><thead>';
        //Cargamos el header
        for (var i = 0; i < obj_ColumnasVisibles.length; i++) {
            if (obj_ColumnasVisibles[i].dataField != "NULL") {
                str_HtmlTabla += '<th align="center" class="contenido-header">' + obj_ColumnasVisibles[i].caption + '</th>';
                int_ContadorCabecera++;
            }
        }
        str_HtmlTabla += '</thead></tr>';
        //Cargamos el contenido de la tabla
        let bool_ValorNoNulo = false;
        for (var i = 0; i < obj_DatosFiltrados.length; i++) {
            str_HtmlTabla += '<tr>';
            for (var j = 0; j < obj_ColumnasVisibles.length; j++) {
                if (obj_ColumnasVisibles[j].dataField != "NULL") {
                    bool_ValorNoNulo = (obj_DatosFiltrados[i][obj_ColumnasVisibles[j].dataField] != null);
                    if (obj_ColumnasVisibles[j].dataType == "date" && bool_ValorNoNulo) {
                        str_HtmlTabla += '<td class="contenido-detalle">' + obj_ClaseGlobal.fn_ConvertirFecha(obj_DatosFiltrados[i][obj_ColumnasVisibles[j].dataField], 3, 2) + '</td>';
                    } else if (obj_ColumnasVisibles[j].dataType == "money" && bool_ValorNoNulo) {
                        str_HtmlTabla += '<td class="contenido-detalle" align="right">' + "$" + (obj_DatosFiltrados[i][obj_ColumnasVisibles[j].dataField]).formatMoney(2) + '</td>';
                    } else if (!bool_ValorNoNulo) {
                        str_HtmlTabla += '<td class="contenido-detalle"></td>';
                    }
                    else {
                        str_HtmlTabla += '<td class="contenido-detalle">' + obj_DatosFiltrados[i][obj_ColumnasVisibles[j].dataField] + '</td>';
                    }
                }
            }
            str_HtmlTabla += '</tr>';
        }

        for (var i = 0; i < p_str_json_PiePagina.length; i++) {
            let obj_Temp = $.grep(obj_DatosFiltrados, function (value, index) {
                return value[p_str_json_PiePagina[i].dataField] != null;
            });
            if (p_str_json_PiePagina[i].accion == "sumatoria") {
                p_str_json_PiePagina[i].valor = obj_Temp.reduce(function (sum, temp) {
                    return sum + temp[p_str_json_PiePagina[i].dataField];
                }, 0);
            }
            if (p_str_json_PiePagina[i].accion == "contador") {
                p_str_json_PiePagina[i].valor = obj_Temp.reduce(function (sum, temp) {
                    return sum + 1;
                }, 0);
            }
        }
        str_HtmlTabla += '<tr>';
        for (var i = 0; i < obj_ColumnasVisibles.length; i++) {
            let obj_Temp = $.grep(p_str_json_PiePagina, function (value, index) {
                return value.dataField == obj_ColumnasVisibles[i].dataField;
            });
            if (obj_ColumnasVisibles[i].dataField != "NULL") {
                if (obj_Temp.length > 0 && obj_ColumnasVisibles[i].dataType == "date") {
                    str_HtmlTabla += '<td class="contenido-detalle"><strong>' + obj_ClaseGlobal.fn_ConvertirFecha(obj_DatosFiltrados[i][obj_ColumnasVisibles[i].dataField], 3, 2) + '</strong></td>';
                } else if (obj_Temp.length > 0 && obj_ColumnasVisibles[i].dataType == "money") {
                    str_HtmlTabla += '<td class="contenido-detalle" align="right"><strong>' + obj_Temp[0].texto + "$" + (obj_Temp[0].valor).formatMoney(2) + '</strong></td>';
                } else if (obj_Temp.length > 0) {
                    str_HtmlTabla += '<td class="contenido-detalle"><strong>' + obj_Temp[0].texto + obj_Temp[0].valor + '</strong></td>';
                } else {
                    str_HtmlTabla += '<td class="contenido-detalle"></td>';
                }
            }
        }
        str_HtmlTabla += '</tr>';
        str_HtmlTabla += '<tr><td colspan="' + int_ContadorCabecera + '"><strong>Registros: ' + obj_DatosFiltrados.length + '</strong></td></tr>';
        str_HtmlTabla += '</table>';
        //str_HtmlTabla += '<div style="page-break-after: always;">.</div>';
        return str_HtmlTabla;
    }

    fn_IncrementarAltoDataGrid(param_objeto) {
        //$("#gridContainer").dxDataGrid("instance").getScrollable().scrollTo(0);
        //$("#gridContainer").dxDataGrid("instance").getScrollable().scrollTo($("#gridContainer").dxDataGrid("instance").getScrollable().scrollHeight());
        //$("#gridContainer").dxDataGrid("instance").getScrollable().scrollToElement($('tr[aria-rowindex="'+$("#gridContainer").dxDataGrid("instance").totalCount()+'"]'));
        var dif = 50;
        if ((int_AltoIncremental - $(param_objeto).dxDataGrid("instance").getScrollable().scrollHeight()) > 100) {
            dif = 150;
        }
        if (int_AltoIncremental < ($(param_objeto).dxDataGrid("instance").getScrollable().scrollHeight() + dif)) {
            $(param_objeto).css("height", int_AltoIncremental + 400 + "px");
            int_AltoIncremental = int_AltoIncremental + 400;
            $(param_objeto).dxDataGrid("instance").updateDimensions();
            // $(".dx-datagrid-bottom-load-panel").remove();
        } else {
            // toastr["warning"]("Ha llegado al final de la lista.");
            //$(".dx-datagrid-bottom-load-panel").remove();
            return false;
        }
    }

    fn_DecrementarAltoDataGrid(param_objeto) {
        if ($(param_objeto).height() > int_AltoInicial) {
            $(param_objeto).css("height", int_AltoIncremental - 400 + "px");
            int_AltoIncremental = int_AltoIncremental - 400;
            $(param_objeto).dxDataGrid("instance").updateDimensions()
        } else {
            $(param_objeto).css("height", int_AltoInicial + "px");
        }
    }

    // Método para mostrar o quitar la pantalla de cargando
    fn_cargando(par_metodo, par_mensaje) {
        if (typeof par_mensaje === 'undefined' || par_mensaje === '') {
            par_mensaje = "Cargando información, espere...";
        }

        if (par_metodo === 1) {
            $("html").loading({ message: par_mensaje });
        } else {
            $("html").loading('stop');
        }
    }

    fn_generaUUID() {

        var dt = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (dt + Math.random() * 16) % 16 | 0;
            dt = Math.floor(dt / 16);
            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return uuid;
    }

    fn_modalCodigoAutorizacion(parambMensaje, paramTipoBusqueda, paramStrGenerico1, paramStrGenerico2, paramStrMensaje) {

        if (typeof parambMensaje === 'undefined') {
            parambMensaje = 1;
        }
        if (typeof paramStrGenerico1 === 'undefined') {
            paramStrGenerico1 = "";
        }
        if (typeof paramStrGenerico2 === 'undefined') {
            paramStrGenerico2 = "";
        }
        if (typeof paramTipoBusqueda === 'undefined') {
            paramTipoBusqueda = 1;
        }
        if (typeof paramStrMensaje === 'undefined') {
            paramStrMensaje = "";
        }

        var str_urlAutorizacion = window.location.protocol + "//" + window.location.host + window.location.pathname.substring(0, window.location.pathname.indexOf('/Views')) + "/Views/Catalogos/AutorizacionCodigo/autorizacionCodigoValidar.cshtml?Popup=1&bMensaje=" + parambMensaje + "&pTipoBusqueda=" + paramTipoBusqueda + "&pStrGenerico1=" + paramStrGenerico1 + "&pStrGenerico2=" + paramStrGenerico2 + "&pStrMensaje=" + paramStrMensaje;
        //console.log(str_urlAutorizacion);

        $.pgwModal({
            url: str_urlAutorizacion,
            loadingContent: str_MensajeCargando,
            closeOnBackgroundClick: false,
            title: "Codigo de Autorizacion",
            maxWidth: "80%"
        });

    }


    fn_DescargarArchivo(paramUrlArchivo) {

        //console.log(paramUrlArchivo);

		$.ajax({//primero validamos que el archivo exista
			cache: false,
			url: paramUrlArchivo,
			data: "",
			success: function (data) {
				if (data === 'Error') {//si no existe mandamos el error
					toastr["error"]("Error al descargar archivo.");
				} else {//si existe procedemos a descargarlo
					window.location = paramUrlArchivo;
				}
			}
		});
    }

    fn_CargarAutocompletePosicionAbsoluta() {
        $("body").on("focus", ".autocompletePosicionAbsoluta", function () {
            var objPadre = $(this);
            var offset = objPadre.offset();
            var width = objPadre.width();
            var dec_posicionY = offset.top - $(window).scrollTop();
            var dec_posicionX = offset.left - $(window).scrollLeft();
            var objAutocomplete = $(this).closest('div').find('.easy-autocomplete-container');

            objAutocomplete.css('position', 'fixed')
                .css('width', width + 'px')
                .css('top', dec_posicionY + 35 + 'px')
                .css('left', dec_posicionX + 'px');

            $(window).scroll(function () {
                if (objAutocomplete.find(".eac-item").css("display") !== "none") {
                    offset = objAutocomplete.closest(".easy-autocomplete").find(".autocompletePosicionAbsoluta").offset();
                    dec_posicionY = offset.top - $(window).scrollTop();
                    dec_posicionX = offset.left - $(window).scrollLeft();

                    objAutocomplete
                        .css('position', 'fixed')
                        .css('top', dec_posicionY + 35 + 'px')
                        .css('left', dec_posicionX + 'px'
                        );
                }
            });

            $(".div_vScrollTablaBody").scroll(function () {
                objPadre.blur();
            });
        });
    }

    fn_ObtenerFechaActual(param_int_FormatoFecha, param_str_UrlObtenerFechaActual) {
        var str_FechaActual = '';
        $.ajax({
            url: param_str_UrlObtenerFechaActual
            , cache: false
            , async: false
            , data: {
                paramIntFormatoHora: param_int_FormatoFecha
            }
            , success: function (obj_Datos) {
                str_FechaActual = obj_Datos;
            }
        });
        return str_FechaActual;
    }

    fn_ObtenerNuevoGUID(param_str_UrlObtenerNuevoGUID) {
        var str_NuevoGuid = '';
        $.ajax({
            url: param_str_UrlObtenerNuevoGUID
            , cache: false
            , async: false
            , success: function (obj_Datos) {
                str_NuevoGuid = obj_Datos;
            }
        });
        return str_NuevoGuid;
    }




    fn_ValidarFechaMayorA(paramFechaInicial, paramFechaFinal, paramMensaje, paramIdFechaFinal, paramTipo) {
        var dt_FechaInicial = moment.utc(paramFechaInicial, "DD/MM/YYYY HH:mm");
        var dt_FechaFinal = moment.utc(paramFechaFinal, "DD/MM/YYYY HH:mm");

        if (typeof paramTipo == 'undefined' || paramTipo == null) {
            paramTipo = 1;
        }

        switch (paramTipo) {
			case 1:
				if (dt_FechaInicial.isAfter(dt_FechaFinal)) {
					toastr["warning"](paramMensaje);
					$("#" + paramIdFechaFinal).focus();
					return false;
				}
				break;
        }
    }

    fn_ValidarFechaMayorOIgualA(paramFechaInicial, paramFechaFinal, paramMensaje, paramIdFechaFinal, paramTipo) {
        var dt_FechaInicial = moment.utc(paramFechaInicial, "DD/MM/YYYY HH:mm");
        var dt_FechaFinal = moment.utc(paramFechaFinal, "DD/MM/YYYY HH:mm");

        if (typeof paramTipo === 'undefined' || paramTipo === null) {
            paramTipo = 1;
        }

        switch (paramTipo) {
			case 1:
				if (dt_FechaInicial.isSameOrAfter(dt_FechaFinal)) {
					toastr["warning"](paramMensaje);
					$("#" + paramIdFechaFinal).focus();
					return false;
				}
				break;
        }
    }

    // --------------------------------------------------------------------------------------- //
    // -- EVENTOS PARA MODIFICAR UNA FECHA INICIAL O FINAL SEGÚN LA FECHA QUE SE CAPTURE       //
    // --------------------------------------------------------------------------------------- //
    // -- SI LA FECHA INICIAL ES MAYOR A LA FINAL, LA FINAL SE PONE COMO LA INICIAL            //
    // -- SI LA FECHA FINAL ES MENOR A LA INICIAL, LA INICIAL SE PONE COMO LA FINAL            //
    // --------------------------------------------------------------------------------------- //
    fn_AjustaFechaFinaFechaInicial(paramFechaInicial, paramFechaFinal, param_IdFecha, paramTipo) {
        var dt_FechaInicial = moment.utc(paramFechaInicial, "DD/MM/YYYY HH:mm");
        var dt_FechaFinal = moment.utc(paramFechaFinal, "DD/MM/YYYY HH:mm");

        if (typeof paramTipo === 'undefined' || paramTipo === null) {
            paramTipo = 1;
        }

        switch (paramTipo) {
			case 1:
				if (dt_FechaInicial.isAfter(dt_FechaFinal)) {
					
					$("#" + param_IdFecha).val(dt_FechaInicial.format('DD/MM/YYYY'));
					return false;
				}
				break;
            case 2:
				if (dt_FechaFinal.isBefore(dt_FechaInicial)) {
					
					$("#" + param_IdFecha).val(dt_FechaFinal.format('DD/MM/YYYY'));
					return false;
				}
				break;
        }
    }


    fn_ObtenerEventosAutoComplete(objAutocomplete) {

        //var dataId = objAutocomplete.elementoAutocomplete.data("id")
        //var dataValue = objAutocomplete.elementoAutocomplete.data("vsel")
        var value = objAutocomplete.elementoAutocomplete.val()

        //console.log(dataId);
        //console.log(dataValue);
        //console.log(value);

        //objAutocomplete.elementoAutocomplete.data("id", 0).data("vsel", "").data("limite", objAutocomplete.limiteTodos);
        objAutocomplete.elementoAutocomplete.data("limite", objAutocomplete.limiteTodos);

        var e = jQuery.Event("keyup");
        e.which = 8;
        e.keyCode = 8;

        $(objAutocomplete.elementoAutocomplete).val("*").trigger(e);
        $(objAutocomplete.elementoAutocomplete).val("");

        if (!$(objAutocomplete.elementoAutocomplete).is(":focus")) {

            $(objAutocomplete.elementoAutocomplete).focus();

        }

        setTimeout(function () {
            objAutocomplete.elementoAutocomplete.data("limite", objAutocomplete.limiteDefault).val(value);
        }, 800);

    }

    fn_CalcularRangoFechas(objMultilista) {
        var dt_FechaActual = new moment().format("DD/MM/YYYY");
        var dt_NuevaFechaInicio, dt_NuevaFechaFin;
        var respuesta;
        if (objMultilista.valor1char == "MESANTERIOR") {
            dt_NuevaFechaInicio = new moment(dt_FechaActual, 'DD/MM/YYYY').subtract(1, 'months').startOf('month').format("DD/MM/YYYY");
            dt_NuevaFechaFin = new moment(dt_FechaActual, 'DD/MM/YYYY').subtract(1, 'months').endOf('month').format("DD/MM/YYYY");
        } else if (objMultilista.valor1char == "MESACTUAL") {
            dt_NuevaFechaInicio = new moment(dt_FechaActual, 'DD/MM/YYYY').startOf('month').format("DD/MM/YYYY");
            dt_NuevaFechaFin = dt_FechaActual;
        } else if (objMultilista.valor1char == "DOSMESESATRAS") {
            dt_NuevaFechaInicio = new moment(dt_FechaActual, "DD/MM/YYYY").subtract(2, 'months').startOf('month').format("DD/MM/YYYY");
            dt_NuevaFechaFin = new moment(dt_FechaActual, 'DD/MM/YYYY').subtract(1, 'months').endOf('month').format("DD/MM/YYYY");
        } else if (objMultilista.valor1char == "ANIOACTUAL") {
            dt_NuevaFechaInicio = new moment(dt_FechaActual, "DD/MM/YYYY").startOf('year').format("DD/MM/YYYY");
            dt_NuevaFechaFin = dt_FechaActual;
        } else if (objMultilista.valor1char == "DOSANIOS") {
            dt_NuevaFechaInicio = new moment(dt_FechaActual, "DD/MM/YYYY").subtract(2, 'years').startOf('year').format("DD/MM/YYYY");
            dt_NuevaFechaFin = new moment(dt_FechaActual, 'DD/MM/YYYY').subtract(1, 'years').endOf('year').format("DD/MM/YYYY");
        }
        respuesta = {
            nuevaFechaInicio: dt_NuevaFechaInicio,
            nuevaFechaFin: dt_NuevaFechaFin
        }
        return respuesta;
    }

    // Funcion para ampliar el alto de los divs de cada tabs
    fn_AmpliarAltoTab(param_objeto) {
        var obj_padre = $(param_objeto).closest(".ibox");
        var int_altoTabla = $(obj_padre).find(".pane-vScroll > table").height() || 0;
        var int_altoDiv = $(obj_padre).find(".pane-vScroll").height() || 0;
        if (int_altoDiv > 0 && int_altoDiv < int_altoTabla) {
            $(obj_padre).find(".pane-vScroll").stop(true, true).animate({
                height: (int_altoDiv + 120) + 'px'
            }, 500);
        }
    }

    // Funcion para ampliar el alto de los divs de cada tabs
    fn_DecrementarAltoTab(param_objeto) {
        var obj_padre = $(param_objeto).closest(".ibox");
        var int_altoTabla = $(obj_padre).find(".pane-vScroll > table").height() || 0;
        var int_altoDiv = $(obj_padre).find(".pane-vScroll").height() || 0;
        if (int_altoDiv > 0 && int_altoDiv > 150 && (int_altoDiv - 120) > 150) {
            $(obj_padre).find(".pane-vScroll").stop(true, true).animate({
                height: (int_altoDiv - 120) + 'px'
            }, 500);
        }
    }


    //fn_ObtenerAutoComplete(element, int_LimiteDefault, int_LimiteTodos) {

    //    element.data("id", 0).data("vsel", "").data("limite", int_LimiteTodos);

    //    var e = jQuery.Event("keyup");
    //    e.which = 8;
    //    e.keyCode = 8;

    //    $(element).val("*").trigger(e);
    //    $(element).val("");
    //    if (!$(element).is(":focus")) {
    //        $(element).focus();
    //    }

    //    setTimeout(function () {
    //        element.data("limite", int_LimiteDefault);
    //    }, 800);

    //}

    //si solo se llama la funcion, el elemento clockpicker se posicionara en top y tomara todos los elementos que tengan la clase .clockpicker por default
    fn_Clockpicker(paramPlacement, paramObjetoClockpicker) {
        if (typeof paramPlacement === 'undefined') {
            paramPlacement = 'top';
        }
        if (typeof paramObjetoClockpicker === 'undefined') {
            paramObjetoClockpicker = '.clockpicker';
        }

        $(paramObjetoClockpicker).clockpicker({
            placement: paramPlacement,
            align: 'top',
            //donetext: 'Done',
            autoclose: true
        }).focusout(function (event) {
            setTimeout(function () {
                var element = event.target;
                var value = element.value;

                if (value != '' || value != '__:__')//si el campo hora esta vacia no se valida
                {
                    var formatoHora = /^(0[1-9]|1[0-9]|2[0-3]|00)[\:](0[1-9]|[1-5][0-9]|00)$/;

                    //validamos si es correcta la hora
                    if (!value.match(formatoHora)) {
                        element.value = '';//si es incorrecta entonces vaciamos el campo
                    }
                }
            });

        })
    }

    // carga los valores iniciales al entrar a un documento HTML
    fn_CargaInicial() {
        let str_Origin = window.location.origin;
        let int_Index = parseInt(str_Origin.indexOf("localhost") || 0);

        $("body").off("blur", ".touchspin:not([readonly]):not(.noValidarTouchSpin)");
        $("body").on("blur", ".touchspin:not([readonly]):not(.noValidarTouchSpin)", function (e) {
            let string_valorInput = $(this).val();
            let float_valorInput = (parseFloat($(this).val()) || 0);
            let float_valorMinimo = (parseFloat($(this).data("min")) || 0);
            let float_valorMaximo = (parseFloat($(this).data("max")) || 0);

            if (float_valorInput < float_valorMinimo && string_valorInput !== "") {
                toastr["error"]("El valor mínimo a capturar es " + float_valorMinimo + ". <br>Se ajustará el valor al mínimo automáticamente");
            } else if (float_valorInput > float_valorMaximo && string_valorInput !== "") {
                toastr["error"]("El valor máximo a capturar es " + float_valorMaximo + ". <br>Se ajustará el valor al máximo automáticamente");
            }
        });

        // bloquea los formularios cuando se presiona enter
        $("form").keypress(function (e) {
            if (e.which == 13) {
                return false;
            }
        });

        // Obtiene los eventos de Ajax y les agrega la funcionalidad de loading
        $(document).ajaxStop(function () {
            $("html").loading('stop');
            //$.pgwModal('reposition');
        });
        $(document).ajaxStart(function () {
            $("html").loading({ message: "Cargando información, espere..." });
            $("#btn_Grabar, .btn_Grabar").prop('disabled', true);
        });
        $(document).ajaxError(function () {
            $("html").loading('stop');
            $("#btn_Grabar, .btn_Grabar").prop('disabled', false);
        });
        $(document).ajaxComplete(function (event, xhr, settings) {
            if (parseInt(xhr.status) === 401) {

                window.location.replace(window.location.origin + (int_Index <= 0 ? "/compradores" : "")+ "/Acceso.cshtml?SesionTerminada=true");
            }
            setTimeout(function () {
                $("#btn_Grabar, .btn_Grabar").prop('disabled', false);
            }, 1700);
        });

        // agrega un margen al menu operativo
        if ($(".top-navigation").length) {
            var marginTop = $(".navbar-fixed-top").height();
            $(".top-navigation.fixed-nav #wrapper").css("margin-top", marginTop + "px");
        }

        if ($("body").width() > 768) {
            // Agrega un margin superior a la clase contenido-inicial
            var int_altoHeading = $(".page-heading").height();
            //var int_altoNavbar = $(".navbar-fixed-top").height();
            $(".contenido-inicial").css("margin-top", int_altoHeading + "px");
        }

        //Evento click para ampliar el div page-heading
        $(".navbar .navbar-minimalize").click(function () {
            // Revisa si existe un menu fijo de un datatable para recalcular su ancho
            if ($(".table:not(.fixedHeader-floating, .treetable, .tabla-grid) tbody").length) {
                var tbl_tabla = $('.table:not(.fixedHeader-floating,.treetable,.tabla-grid)').DataTable();
                //if ($(".table:not(.fixedHeader-floating, .treetable, .tabla-grid) tbody").length) {
                //    var tbl_tabla = $('.table:not(.fixedHeader-floating)').DataTable();
                setTimeout(function () {
                    tbl_tabla.fixedHeader.adjust();
                }, 1000);
            }
        });

        /* Menu varios niveles */
        if ($('ul.dropdown-menu [data-toggle=dropdown]').length) {
            $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
                event.preventDefault();
                event.stopPropagation();
                $(this).parent().siblings().removeClass('open');
                $(this).parent().toggleClass('open');
            });
        }

        // Elimna los botones superiores que no se vayan a usar, según si se encuentra la tabla
        if ($("#tbl_Lista").length) {
            $(".btn_Abc").remove();
            $(".btn_Lista").removeClass("hidden");
        } else if ($("#btn_Guardar").length) {
            $(".btn_Lista").remove();
            $(".btn_Abc").removeClass("hidden");
        }

        //configuracion de Datepicker
        //     $.fn.datepicker.dates['es'] = {
        //         days: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"],
        //         daysShort: ["Dom", "Lun", "Mar", "Mier", "Jue", "Vie", "Sab"],
        //         daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
        //         months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        //         monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
        //         today: "hoy",
        //         clear: "Limpiar",
        //         format: "dd/mm/yyyy",
        //         titleFormat: "MM yyyy", 
        //     };
        //     //se inicializa el datepicker
        //     $('.datepicker').datepicker(
        //{
        //	language: 'es',
        //	weekStart: 1,
        //	todayBtn: "linked",
        //	keyboardNavigation: false,
        //	//forceParse: false,
        //	//calendarWeeks: true,
        //	autoclose: true,
        //	//linked: true,
        //	todayHighlight: true
        //}
        //     );

        //obj_ClaseGlobal.fn_IniciarDatePicker();

        $("body").on("keypress", ".numerico", function (e) {
            return obj_ClaseGlobal.fn_ValidarDatosInput(e, 5);
        });

		$("body").on("keypress", ".numerico:not([readonly]),.dosDecimales:not([readonly])", function (e) {
			let decimales = parseInt($(this).data("dec") || 2 , 10);

			var texto = $(this).val();
			texto = texto.slice(0, $(this)[0].selectionStart) + texto.slice($(this)[0].selectionEnd);
			var t = texto;

            //var t = this.value;
			this.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), decimales)) : t;
        });

		$("body").on("keypress", ".validaDatos", function (e) {
            return obj_ClaseGlobal.fn_ValidarDatosInput(e, $(this).data("validacion"), $(this).data("espacio") );
        });

		// Prevenimos el poder pegar valores no permitidos
		//$("body").on("paste", ".validaDatos,.numerico,.dosDecimales", function (e) {
        //e.preventDefault();
        //});

        $("body").on("paste", ".validaDatos:not(.permiteCopiar),.numerico:not(.permiteCopiar),.dosDecimales:not(.permiteCopiar)", function (e) {
            e.preventDefault();
        });

        $("body").on("keyup blur", "input[type='text']:not(.minusculas):not(.numerico), textarea:not(.minusculas)", function (e) {
			if (!$(this).parent().hasClass('easy-autocomplete')) {
                var start = this.selectionStart;
                var end = this.selectionEnd;
                var value = $(this).val()
                $(this).val(value);
                this.setSelectionRange(start, end);
                return;
			}
        });

		//$('input, select').not('[type="checkbox"], [type="radio"], [type="button"], [type="submit"]').filter(function() {
		//  return !$(this).parent().hasClass('easy-autocomplete');
		//}).on('focus', function () {
		//	// do something
		//});

        obj_ClaseGlobal.fn_PintarAsteriscosCamposObligatorios();

    }

    fn_PintarAsteriscosCamposObligatorios() {
        $(".required").each(function () {
            if ($(this).hasClass("datepicker") && $(this).closest(".form-group").find("label:first").text() !== "") {
                $(this).closest(".form-group").find(".text-red").remove();
                $(this).closest(".form-group").find("label:first").html('<span class="text-red">* </span>' + $(this).closest(".form-group").find("label:first").html());
            } else {
                $("body label[for='" + $(this).prop("id") + "']").find(".text-red").remove();
                $("body label[for='" + $(this).prop("id") + "']").prepend("<span class='text-red'>* </span>");
            }
        })
    }

    fn_LimpiarControles(par_str_IdControl) {
        var obj_Data;
        var str_Llaves;
        //Borra por id
        if (typeof par_str_IdControl === 'undefined') {
            par_str_IdControl = 'body ';
        }

        $(par_str_IdControl + ".limpiarInput").each(function () {
            var i = 0;
            obj_Data = $(this).data();
            str_Llaves = $.map(obj_Data, function (value, key) {
                return key;
            });
            for (i = 0; i < str_Llaves.length; i++) {
                if (str_Llaves[i] == "mask") continue;
                if (isNaN($(this).data(str_Llaves[i]))) {
                    $(this).data(str_Llaves[i], "");
                } else {
                    $(this).data(str_Llaves[i], "0");
                }
            }
            $(this).val("");
        });
        $(par_str_IdControl + ".limpiarCombo").each(function () {
            $(this).html('<option value="0">-SELECCIONE-</option>');
            $(this).trigger("change");
        });
        $(par_str_IdControl + ".reiniciarCombo").each(function () {
            $(this).prop("selectedIndex", 0);
            $(this).trigger("change");
        });
    }

    // Esta funcion genera una validación dentro de una tabla a un campo dado por ID con la clase required
    fn_validarDatosTabla(tbl_id, int_tieneContador = 0) {
        var bool_valido = false;
        var obj_elemento = $("#" + tbl_id + " tbody .required");
		var leyenda = '';

		if (obj_elemento.size() > 0) {
			obj_elemento.parent().removeClass("has-error");



            $("#" + tbl_id + " tbody .required").each(function () {

				leyenda = $("body label[for='" + $(this).prop("id") + "']").text();
				leyenda = leyenda.replace("* ", "");
				var ultimoCaracter = leyenda.substr(leyenda.length - 1);
				if (ultimoCaracter === ":") {
					leyenda = leyenda.slice(0, -1);
				}

				if (leyenda === "" || leyenda === undefined) {
                    leyenda = $(this).data("leyendaerror");
                }
				if (leyenda === "" || leyenda === undefined) {
                    leyenda = $(this).prop("id");
				}
				
                if (int_tieneContador === 1) {
                    var contador = $(this).closest('tr').data('orden');
                    leyenda = leyenda + ' ' + contador;
                }
                
				if ($(this).is('select')) {
                    if ($.trim($(this).val()) === "" || $.trim($(this).val()) === "0") {
                        if ($(this).closest("div.tab-pane").length) {
                            var bool_tab = $(this).closest("div.tab-pane").prop("id");
                            $('a[href="#' + bool_tab + '"]').click();
                        }
                        $(this).parent().addClass("has-error");
						toastr["error"]("Debe seleccionar un valor de la lista " + leyenda);
                        $(this).focus();
                        bool_valido = false;
                        return bool_valido;
                    } else {
                        bool_valido = true;
                    }
                } else if ($.trim($(this).val()) === "") {
                    if ($(this).closest("div.tab-pane").length) {
                        var bool_tab = $(this).closest("div.tab-pane").prop("id");
                        $('a[href="#' + bool_tab + '"]').click();
                    }
                    $(this).parent().addClass("has-error");
					toastr["error"]("Debe seleccionar o introducir un valor " + leyenda);
                    $(this).focus();
                    bool_valido = false;
                    return bool_valido;
                } else {
                    bool_valido = true;
                }
            });
        } else {
            bool_valido = true;
        }

        return bool_valido;
    }

    fn_validarDatosTablaPuesto(tbl_id) {
        var bool_valido = false;
        var obj_elemento = $("#" + tbl_id + " tbody .required");
        if (obj_elemento.size() > 0) {
            obj_elemento.parent().removeClass("has-error");
            $("#" + tbl_id + " tbody .required").each(function () {
                if ($(this).is('select') && ($.trim($(this).val()) === "" || $.trim($(this).val()) === "0")) {
                    $(this).parent().addClass("has-error");
                    toastr["error"]("Debe seleccionar un valor de la lista");
                    $(this).focus();
                    bool_valido = false;
                    return bool_valido;
                } else if ($.trim($(this).val()) === "") {
                    $(this).parent().addClass("has-error");
                    toastr["error"]("Debe seleccionar o introducir un valor");
                    $(this).focus();
                    bool_valido = false;
                    return bool_valido;
                } else {
                    bool_valido = true;
                }
            });
        } else {
            bool_valido = true;
        }

        return bool_valido;
    }

    // Esta funcion genera una validación a un formulario.
    // 1. Se validaran que todos los campos tengan caraceters correctos.
    // 2. Se validarán los campos con la clase required

    fn_ValidarDatosFormulario(par_idDocumento) {

        if (typeof par_idDocumento === 'undefined') {
            par_idDocumento = 'body';
        }

        var bool_valido = true;
		var leyenda = '';

        $(par_idDocumento + " input[type=text]:enabled:not([readonly]), " + par_idDocumento + " textarea:not([disabled])").each(function () {
            leyenda = $("body label[for='" + $(this).prop("id") + "']").text();
            leyenda = leyenda.replace("* ", "");
            var ultimoCaracter = leyenda.substr(leyenda.length - 1);
            if (ultimoCaracter === ":") {
                leyenda = leyenda.slice(0, -1);
            }

            $(this).val($.trim($(this).val()));
            if ($(this).val() !== "" && !obj_ClaseGlobal.fn_ValidarDatosRegulares($(this).val())) {
                if ($(this).closest("div.tab-pane").length) {
                    var bool_tab = $(this).closest("div.tab-pane").prop("id");
                    $('a[href="#' + bool_tab + '"]').click();
                }
                $(this).parent().addClass("has-error");
                toastr["error"]("Valor no permitido en el campo " + leyenda + ". Valores permitidos: " + str_CaracteresPermitidos + ". Revise los carácteres");
                $(this).focus();
                bool_valido = false;
                return bool_valido;
            }
        });

        // 1. Validación de caracteres en los campos, para impedir caracteres no deseados
        //$(par_idDocumento + " :input[type=text]:not(.readonly), " +par_idDocumento + " :input[type=text]:not([readonly])," + par_idDocumento + " textarea:not([disabled])").each(function () {
        //    leyenda = $("body label[for='" + $(this).prop("id") + "']").text();
        //    leyenda = leyenda.replace("* ", "");
        //    var ultimoCaracter = leyenda.substr(leyenda.length - 1);
        //    if (ultimoCaracter === ":") {
        //        leyenda = leyenda.slice(0, -1);
        //    }

        //    $(this).val($.trim($(this).val()));
        //    if ($(this).val() !== "" && !obj_ClaseGlobal.fn_ValidarDatosRegulares($(this).val())) {
        //        if ($(this).closest("div.tab-pane").length) {
        //            var bool_tab = $(this).closest("div.tab-pane").prop("id");
        //            $('a[href="#' + bool_tab + '"]').click();
        //        }
        //        $(this).parent().addClass("has-error");
        //        toastr["error"]("Valor no permitido en el campo " + leyenda + ". Revise los carácteres");
        //        $(this).focus();
        //        bool_valido = false;
        //        return bool_valido;
        //    }
        //});

        if (bool_valido === false) {
            return bool_valido;
        }

        // 2. Validacion de campos requeridos para que no queden vacíos
        var obj_elemento = $(par_idDocumento + " .required").not("table .required").not(".modal:hidden .required");
        if (obj_elemento.size() > 0) {
            var str_tabulador = "";
            $(".has-error").removeClass("has-error");
            obj_elemento.each(function () {

				leyenda = $("body label[for='" + $(this).prop("id") + "']").text();
				leyenda = leyenda.replace("* ", "");
				var ultimoCaracter = leyenda.substr(leyenda.length - 1);
				if (ultimoCaracter === ":") {
					leyenda = leyenda.slice(0, -1);
				}

                if (leyenda === "" || leyenda === undefined) {
                    leyenda = $(this).data("leyendaerror");
                }
                if (leyenda === "" || leyenda === undefined) {
                    leyenda = $(this).prop("id");
                }

                if ($(this).prop("disabled") === true) {
                    return;
                }

                if ($(this).is('select')) {
                    //if ($.trim($(this).val()) === "" || $.trim($(this).val()) === "0") {
                    if ($.trim($(this).find('option:selected').val()) === "" || $.trim($(this).find('option:selected').val()) === "0") {
                        if ($(this).closest("div.tab-pane").length) {
                            str_tabulador = $(this).closest("div.tab-pane").prop("id");
                            $('a[href="#' + str_tabulador + '"]').click();
                        }

                        $(this).parent().addClass("has-error");
                        toastr["error"]("Debe seleccionar un valor de la lista " + leyenda);
                        $(this).focus();
                        bool_valido = false;
                        return bool_valido;
                    }
                } else if ($(this).parent(".easy-autocomplete").length && parseInt($(this).data("id")) === 0) {
                    $(this).parent().addClass("has-error");
                    toastr["error"]("Debe capturar un valor en el campo " + leyenda);
                    $(this).focus();
                    bool_valido = false;
                    return bool_valido;
                } else if ($.trim($(this).val()) === "") {
                    if ($(this).closest("div.tab-pane").length) {
                        str_tabulador = $(this).closest("div.tab-pane").prop("id");
                        $('a[href="#' + str_tabulador + '"]').click();
                    }
                    $(this).parent().addClass("has-error");
                    toastr["error"]("Debe seleccionar o introducir un valor " + leyenda);
                    $(this).focus();
                    bool_valido = false;
                    return bool_valido;
                }
            });
        }

        return bool_valido;
    }

    fn_ValidarDatosRegulares(str_valor, str_expresionRegular) {
        if (typeof str_expresionRegular === 'undefined' || str_expresionRegular === "") {
            str_expresionRegular = str_CaracteresPermitidos;
        }
        return new RegExp(str_expresionRegular).test(str_valor);
    }

    // Esta funcion genera un screenshot de la pantalla y la manda como tipo imagen para imprimir
    // parametros de funcion JS: 
    // 1. Nombre de contenedor(ejemplo: div id)
    // 2. Titulo de impresion(ejemplo: Factura)
    // 3. Mensaje de alert antes de imprimir
    fn_ImpresionRapidaPantalla(StrIdDiv, StrTituloImpresion, StrMensaje) {
        html2canvas($("#" + StrIdDiv).get(0)).then(canvas => {
            var dataURL = canvas.toDataURL("image/jpeg", 5.0);



            var windowContent = '<!DOCTYPE html>';
            windowContent += '<html>'
            windowContent += '<head><title>' + StrTituloImpresion +'</title></head>';
            windowContent += '<body>'
            windowContent += '<img width="1100" src="' + dataURL + '"/>';
            windowContent += '</body>';
            windowContent += '</html>';
            windowContent += '<script>'
            windowContent += 'alert("' + StrMensaje + '");';
            windowContent += '</scri' + 'pt>';

            var printWin = window.open('', '', 'width=700,height=900');
            printWin.document.open();
            printWin.document.write(windowContent);

            printWin.document.addEventListener('load', function () {
                printWin.focus();
                printWin.print();
                printWin.document.close();
                printWin.close();
            }, true);

        });
    }

    //Función para validar un RFC
    // Devuelve el RFC sin espacios ni guiones si es correcto
    // Devuelve false si es inválido
    // (debe estar en mayúsculas, guiones y espacios intermedios opcionales)
    fn_ValidaRfc(rfc, aceptarGenerico = true) {

        if (rfc == null)  //Coincide con el formato general del regex?
            return false;

        rfc = rfc.toUpperCase();
        const re = /^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/;
        var validado = rfc.match(re);

        if (!validado)  //Coincide con el formato general del regex?
            return false;

        //Separar el dígito verificador del resto del RFC
        const digitoVerificador = validado.pop(),
            rfcSinDigito = validado.slice(1).join(''),
            len = rfcSinDigito.length,

            //Obtener el digito esperado
            diccionario = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
            indice = len + 1;
        var suma,
            digitoEsperado;

        if (len == 12) suma = 0
        else suma = 481; //Ajuste para persona moral

        for (var i = 0; i < len; i++)
            suma += diccionario.indexOf(rfcSinDigito.charAt(i)) * (indice - i);
        digitoEsperado = 11 - suma % 11;
        if (digitoEsperado == 11) digitoEsperado = 0;
        else if (digitoEsperado == 10) digitoEsperado = "A";

        //El dígito verificador coincide con el esperado?
        // o es un RFC Genérico (ventas a público general)?
        if ((digitoVerificador != digitoEsperado) && (!aceptarGenerico || rfcSinDigito + digitoVerificador != "XAXX010101000"))
            return false;
        else if (!aceptarGenerico && rfcSinDigito + digitoVerificador == "XEXX010101000")
            return false;

        return true;
    }

	/**************************************************************************** 
	* Valida que la tecla puslada sea igual a la opcion elegida.
	* para llamar la funcion es: onkeypress="return fn_ValidarDatosInput(event,'1')"
	* donde el segundo parametro es el patron que vamos a seguir:
	****************************************************************************/
    fn_ValidarDatosInput(e, valor, espacio) {
        var tecla = document.all ? e.keyCode : e.which;
        var patron;
        if (tecla === 8 | tecla === 9 | tecla === 0 | tecla === 13) { //Tecla de retroceso y Tab
            return true;
        }
        if (espacio == undefined) {
            if (tecla === 32) {
                return false;
            }
        }
        
        if (valor === 1) {
            patron = /[A-Z a-z]/; // Solo acepta letras y espacios
        } else if (valor === 2) {
            patron = /\d/; //Solo acepta números
        } else if (valor === 3) {
            patron = /[0123456789 -]/; //Solo acepta números, espacios y guion.
        } else if (valor === 4) {
            patron = /[0123456789:]/; //Solo acepta números y dos puntos.
        } else if (valor === 5) {
            patron = /[0123456789.]/; //Solo acepta números y puntos.
        } else if (valor === 6) {
            patron = /[0123456789 .-]/; //Solo acepta números, puntos y guion.
        } else if (valor === 7) {
            patron = /[A-Z a-z 0123456789]/; //Solo acepta números y letras.
        } else if (valor === 8) {
            patron = /[0123456789 .,]/; //Solo acepta números y puntos comas.
        } else if (valor === 9) {
            patron = /[A-Za-z0123456789 -]/; //Solo acepta números, letras, espacios y guiones
        }

        return new RegExp(patron).test(String.fromCharCode(tecla));
    }
    /* Fin fn_validarDatosInput */

    fn_AgregarMargenEncabezado(par_obj_DataTable) {
        new $.fn.dataTable.FixedHeader(par_obj_DataTable,
            {
                headerOffset: $('.navbar').outerHeight() + $('.page-heading').outerHeight()
            }
        );
        $('.table.fixedHeader-floating').css('width', $('#tbl_Lista').width() + "px !important");
    }

	/**************************************************************************** 
	* Función para la conversión de fechas a tipo String
	* Se recibe como parámetro una fecha de tipo DATE.
    * Par_Int_Tipo: 1 - Formato dd/mm/yyy
    * Par_Int_Tipo: 2 - Formato yyyy-mm-dd
	****************************************************************************/
    fn_ConvertirFecha(par_dt_fecha, par_int_tipo, par_int_tipoString) {
        if (par_dt_fecha === "" || typeof par_dt_fecha === 'undefined') return null;
        var dtFecha = "";
        if (par_int_tipoString === 1) {
            var from = par_dt_fecha.split("/");
            dtFecha = new Date(from[2].split(" ")[0], from[1] - 1, from[0]);
        } else {
			if (typeof par_dt_fecha !== 'object') {
				par_dt_fecha = par_dt_fecha.replace(/-/g, "/").replace(/T/g, " ");
			}
			
            dtFecha = new Date(par_dt_fecha);
        }

        var dd = dtFecha.getDate();
        var mm = dtFecha.getMonth() + 1;
        var yyyy = dtFecha.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var strFecha = "";
        switch (par_int_tipo) {
            // 1 - Formato: dd/mm/yyyy
            case 1:
                strFecha = dd + '/' + mm + '/' + yyyy;
                break;
            // 2 - Formato: yyyy-mm-dd
            case 2:
                strFecha = yyyy + '-' + mm + '-' + dd;
                break;
            // 3 - Formato: dd/mm/yyyy HH:mm
            case 3:
                strFecha = dd + '/' + mm + '/' + yyyy + ' ' + [dtFecha.getHours().padLeft(), dtFecha.getMinutes().padLeft()].join(':');
                break;
            // 4 - Formato: yyyy-mm-dd HH:mm
            case 4:
                strFecha = [dtFecha.getFullYear().padLeft(), dtFecha.getMonth().padLeft(), dtFecha.getDate().padLeft()].join('-') + ' ' + [dtFecha.getHours().padLeft(), dtFecha.getMinutes().padLeft()].join(':');
                break;
            // 5 - Formato: HH:mm
            case 5:
                strFecha = [dtFecha.getHours().padLeft(), dtFecha.getMinutes().padLeft()].join(':');
                break;
            // 6 - Formato: yyyy-mm-dd HH:mm
            case 6:
                strFecha = [dtFecha.getFullYear().padLeft(), (dtFecha.getMonth() + 1).padLeft(), dtFecha.getDate().padLeft()].join('-') + ' ' + [dtFecha.getHours().padLeft(), dtFecha.getMinutes().padLeft()].join(':');
                break;
        }
        return strFecha;
    }

    // funcion que valida que una fecha no sea indefinida, que sea del año 1900 o del año 0001
    fn_ValidarFechaNula(par_dt_fecha, par_int_tipo, par_int_tipoString) {
        return (typeof par_dt_fecha === 'undefined' ||
            par_dt_fecha === '1900-01-01T00:00:00' ||
            par_dt_fecha === '0001-01-01T00:00:00' ||
            par_dt_fecha === null) ? ('') : obj_ClaseGlobal.fn_ConvertirFecha(par_dt_fecha, par_int_tipo, par_int_tipoString);
    }

    // funcion que valida que una fecha no sea indefinida, que sea del año 1900 o del año 0001
    fn_ValidarFechaNulaFechaSinConvercion(par_dt_fecha) {
        return (typeof par_dt_fecha === 'undefined' ||
            par_dt_fecha === '1900-01-01T00:00:00' ||
            par_dt_fecha === '0001-01-01T00:00:00' ||
            par_dt_fecha === null) ? ('') : par_dt_fecha;
    }

    // Funcion que valida datos undefined y retorna un vacio
    fn_ValidarDatoNulo(valor) {
        if (valor === undefined) {
            return '';
        } else {
            return valor;
        }
    }

    // Funcion que genera una cadena en base a la fecha, horas, minutos y segundos actuales:
    fn_GenerarCadena() {
        var now = new Date();
        var str_nombreArchivo = now.getFullYear().toString();
        str_nombreArchivo += (now.getMonth() < 9 ? '0' : '') + (now.getMonth() + 1).toString();
        str_nombreArchivo += (now.getDate() < 10 ? '0' : '') + now.getDate().toString();
        str_nombreArchivo += now.getUTCHours().toString() + now.getUTCMinutes().toString() + now.getUTCSeconds().toString() + now.getUTCMilliseconds().toString();

        return str_nombreArchivo;
    }

    //fn_IniciarDatePicker() {
    //    //configuracion de Datepicker
    //    $.fn.datepicker.dates['es'] = {
    //        days: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"],
    //        daysShort: ["Dom", "Lun", "Mar", "Mier", "Jue", "Vie", "Sab"],
    //        daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
    //        months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
    //        monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
    //        today: "hoy",
    //        clear: "Limpiar",
    //        format: "dd/mm/yyyy",
    //        titleFormat: "MM yyyy",
    //    };
    //    //se inicializa el datepicker
    //    $('.datepicker').datepicker(
    //        {
    //            language: 'es',
    //            weekStart: 1,
    //            todayBtn: "linked",
    //            keyboardNavigation: false,
    //            forceParse: false,
    //            //calendarWeeks: true,
    //            autoclose: true,
    //            //linked: true,
    //            todayHighlight: true
    //        }
    //    ).bind(
    //        'blur',
    //        function (event) {
    //            setTimeout(function () {//esperamos medio segundo para que cargue la fecha y despues la recuperamos
    //                var element = event.currentTarget;//recuperamos el elemento
    //                var valueDate = element.value;//obtenemos el valor del elemento

    //                if (valueDate != '' || valueDate != '__/__/____')//si el campo viene vacio no validamos
    //                {
    //                    //definimos la regla para la validacion

    //                    //validamos si es correcta la fecha
    //                    if (!valueDate.match(reg_str_FormatoFecha)) {
    //                        element.value = '';//si es incorrecta entonces vaciamos el campo
    //                    }
    //                }
    //            }, 500);

    //        }
    //        ).on("changeDate", function (e) {
    //            //console.log(e);
    //        });
    //}

    roundTo(num, length) {
        return +(Math.round(num + "e+" + length) + "e-" + length);
    }

    /**
     * Redondea un numero decimal con el numero de decimales especificados
     * @param {any} numero Numero a redondear
     * @param {any} dec Cantidad de decimales
     */
    fn_RoundNumber(numero, dec) {
        var flotante, flotanteRound, resultado, decimales = 2;

        if (dec) {
            decimales = dec;
        }

        flotante = parseFloat(numero);
        flotanteRound = this.roundTo(flotante, (decimales + 1));
        //resultado = Math.round(flotanteRound * Math.pow(10, decimales)) / Math.pow(10, decimales);
        resultado = Number(Math.round(flotanteRound  +'e'+decimales)+'e-'+decimales);


        return resultado;
    }

    fn_truncar(par_numero, par_longitud) {
        if (isNaN(par_numero)) {
            return parseFloat(0,10);
        }
        var string = par_numero.toString();
        var int = "";
        var dec = "";
        if (string.indexOf(".") != -1) {
            int = string.split(".")[0];
            dec = string.split(".")[1];
            dec = dec.substring(0, par_longitud);
            string = int + "." + dec;
        }
        return parseFloat(string,10);
    }

	fn_truncarRedondear(par_total, par_longitudTruncado, par_longitudRedondeo) {
        return this.fn_truncar(this.fn_RoundNumber(par_total, par_longitudRedondeo), par_longitudTruncado);
	}

    fn_ObtenerColorPopup() {
        return hex_str_ColorBotonConfirmar;
    }

    /**
     * Rellena con ceros a la izquierda
     * param number - numero a rellenar
     * param length - tamaño de la cadena a devolver
     */
    fn_RellenarNumeroCeros(numero, length) {

        var str = '' + parseInt(numero);
        while (str.length < length) {
            str = '0' + str;
        }

        return str;
    }

    /**
    * Rellena una cadena a la izquierda o a la derecha, segun definimos
    * 
    * param_cadena		- numero a rellenar
    * param_longitud	- tamaño de la cadena a devolver
    * param_carcater	- caracter que rellenará la cadena
    * param_direccion	- dirección hacia la que se rellenará:
    *							0 = hacia la izquierda
    *							n = hacia la derecha 
    * Para agregar caracteres vacios, podemos usar "\xa0"
    */
    fn_RellenarEspaciosCadena(param_cadena, param_longitud, param_caracter, param_direccion) {
        var strCadena = param_cadena + "";
        return (param_direccion === 0 ? strCadena.padStart(param_longitud, param_caracter) : strCadena.padEnd(param_longitud, param_caracter));
    }

    fn_ValidaCorreo(correo) {
        var arrcorreo = correo.split(/[,|;]/);
        var email;
        var bool_respuesta = true;

        for (var i = 0; i < arrcorreo.length; i++) {
            email = arrcorreo[i];
            if (!(email.match(/^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$/))) {
                bool_respuesta = false;
            }
        }

        return bool_respuesta;
    }

    fn_obtenerNumeroMes(Str_Mes) {

        var Str_MesNumero = '';

        var Arr_Mes = Str_Mes.split('.');

        Str_Mes = Arr_Mes[0].toUpperCase();

        if (Str_Mes === "ENERO") { Str_MesNumero = "0"; }
        else if (Str_Mes === "FEBRERO") { Str_MesNumero = "1"; }
        else if (Str_Mes === "MARZO") { Str_MesNumero = "2"; }
        else if (Str_Mes === "ABRIL") { Str_MesNumero = "3"; }
        else if (Str_Mes === "MAYO") { Str_MesNumero = "4"; }
        else if (Str_Mes === "JUNIO") { Str_MesNumero = "5"; }
        else if (Str_Mes === "JULIO") { Str_MesNumero = "6"; }
        else if (Str_Mes === "AGOSTO") { Str_MesNumero = "7"; }
        else if (Str_Mes === "SEPTIEMBRE") { Str_MesNumero = "8"; }
        else if (Str_Mes === "OCTUBRE") { Str_MesNumero = "9"; }
        else if (Str_Mes === "NOVIEMBRE") { Str_MesNumero = "10"; }
        else if (Str_Mes === "DICIEMBRE") { Str_MesNumero = "11"; }

        return Str_MesNumero;

    }

    fn_obtenerStringMes(Str_Mes) {
        var Str_MesNumero = '';

        Str_Mes = Str_Mes.toUpperCase();

        if (Str_Mes === "01") { Str_MesNumero = "Enero"; }
        else if (Str_Mes === "02") { Str_MesNumero = "Febrero"; }
        else if (Str_Mes === "03") { Str_MesNumero = "Marzo"; }
        else if (Str_Mes === "04") { Str_MesNumero = "Abril"; }
        else if (Str_Mes === "05") { Str_MesNumero = "Mayo"; }
        else if (Str_Mes === "06") { Str_MesNumero = "Junio"; }
        else if (Str_Mes === "07") { Str_MesNumero = "Julio"; }
        else if (Str_Mes === "08") { Str_MesNumero = "Agosto"; }
        else if (Str_Mes === "09") { Str_MesNumero = "Septiembre"; }
        else if (Str_Mes === "10") { Str_MesNumero = "Octubre"; }
        else if (Str_Mes === "11") { Str_MesNumero = "Noviembre"; }
        else if (Str_Mes === "12") { Str_MesNumero = "Diciembre"; }

        return Str_MesNumero;
    }

	/**
	 * Esta funcion cambia inserta un html en el objeto p de sweet alert
	 * Se usa para poder insertar html detro del plugin ya que por default no acepta.
	 * @param {any} param_cadena Cadena que vamos a insertar
	 * 
	 */
	fn_cambiarStringAHtmlSweetAlert(param_cadena) {
		$(".sweet-alert p").text("").html("").html(param_cadena);
    }

    fn_agregarDias(param_fecha, param_dias) {
        if (param_fecha === "" || typeof param_fecha === 'undefined') return null;

        var dt_Fecha = new Date(param_fecha);
        dt_Fecha.setDate(dt_Fecha.getDate() + (param_dias + 1));
        return dt_Fecha;
    }

}
//TERMINAN FUNCIONES GLOBALES

//CLASE PARA LIBRERIA TOASTR -> libreria para mensajes instantaneos NO ALERT
class class_toastr {
    constructor() {
        // Configuración inicial para los mensajes de alerta emergentes
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "preventDuplicates": true,
            "positionClass": "toast-middle-center",//"toast-top-center",
            "onclick": null,
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "7000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    fn_cargaOpcionesToastr() {
        toastr.options.onShown = function () {
            $("#overlayToastr").fadeIn("normal", function () { });

        }
        toastr.options.onHidden = function () {
            $("#overlayToastr").fadeOut("normal", function () { });
        }
    }

    fn_cargaParametrosError() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-middle-center",//"toast-top-center",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": 0,
            "extendedTimeOut": 0,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",
            "tapToDismiss": false
        };
        obj_ClaseToastr.fn_cargaOpcionesToastr();
    }

    fn_cargaParametrosExito() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "preventDuplicates": true,
            "positionClass": "toast-middle-center",//"toast-top-center",
            "onclick": null,
            "showDuration": "400",
            "hideDuration": "1000",
            "timeOut": "7000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        obj_ClaseToastr.fn_cargaOpcionesToastr();
    }

    fn_cargaParametrosMensaje() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        //obj_ClaseToastr.fn_cargaOpcionesToastr();
    }
}

//CLASE PARA TOUCHSPIN -> libreria para validar campos numéricos
class class_touchspin {
    constructor() { }

    fn_CargaInputs() {
        $(".touchspin:not([readonly])").not("thead .touchspin").each(function () {
            var double_min = $(this).data("min");
            var double_max = $(this).data("max");
            var double_step = $(this).data("step");
            var double_dec = $(this).data("dec");
            var text_postfix = $(this).data("postfix");

            $(this).TouchSpin({
                min: double_min,
                max: double_max,
                step: double_step,
                decimals: double_dec,
                boostat: 5,
                maxboostedstep: 10,
                verticalbuttons: false,
                forcestepdivisibility: '',
                postfix: text_postfix
            });
        });
    }

}

//Clase de apoyo con funciones y propiedades generales DataTable
class class_datatable {
    constructor() {
        this.json_LanguageDataTable = {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "",//"(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            buttons: {
                copyTitle: 'Copiar',
                copySuccess: {
                    _: '%d líneas copiadas',
                    1: '1 línea copiada'
                },
                colvis: 'Columnas',
                copy: 'Copiar'
            }
        },
            this.intLongitudPagina = 200;//numero de filas por default a mostrar
        this.array_Paginado = [[200, 500, 1000, 5000], [200, 500, 1000, 5000]];
    }

    fn_CargaBotones(mostrarColumnas) {
        var json_BotonesDatatable = [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            }];

        if (!(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))) {
            json_BotonesDatatable.push({
                extend: 'excelHtml5',
                title: 'ExampleFile',
                exportOptions: {
                    columns: ':visible'
                }
            });
        }

        json_BotonesDatatable.push(
            [
                {
                    extend: 'csvHtml5',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: 'ExampleFile',
                    exportOptions: {
                        columns: ':visible'
                    }
                }
            ]);

        if (mostrarColumnas) {
            json_BotonesDatatable.push({
                extend: 'colvis',
                columns: ':not(.noVis)'
            });
        }

        return json_BotonesDatatable;
    }
}

class class_iCheck {
    constructor() {
        if ($('.i-checks').length) {
            var $not = $(this).closest('.tabla-grid thead');
            $('.i-checks').not('.tabla-grid thead .i-checks').iCheck({
                checkboxClass: 'icheckbox_square-orange',
                radioClass: 'iradio_square-orange'
            });
        }
    }

    fn_ActivaDesactivaIcheck(id, valor) {
        $("#" + id).iCheck(parseInt(valor) === 1 ? "check" : "uncheck");
    }

    fn_ActivaIcheckEnTabla() {
        $('.i-checks').not('.tabla-grid thead .hidden .i-checks').iCheck({
            checkboxClass: 'icheckbox_square-orange',
            radioClass: 'iradio_square-orange'
        });
    }
    fn_ActivaIcheckDinamico(str_InputId) {
        $(str_InputId).iCheck({
            checkboxClass: 'icheckbox_square-orange',
            radioClass: 'iradio_square-orange'
        });
    }
}

// Number.prototype.formatMoney(c, d, t)
// Da formato al dinero con dos decimales y comas.
// Se le pueden pasar 3 parámetros (Decimales, Separador de Decimales y Separador de miles)
Number.prototype.formatMoney = function (c, d, t) {
    c = parseFloat(c);
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d === undefined ? "." : d,
        t = t === undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
}