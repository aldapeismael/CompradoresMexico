const json_LanguageDataTable = {
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
}
const array_Paginado = [[200, 500, 1000, 5000], [200, 500, 1000, 5000]];
const intLongitudPagina = 200;

/**************************************************************************** 
	* Funcion para validar una cadena de texto
    * Regresa TRUE si el valor es válido
****************************************************************************/

function fn_ValidarNulosOVacio(par_str_valor) {
	if (typeof par_str_valor === "undefined" || par_str_valor === null || par_str_valor === "") {
		return false;
	}

	return true;
}


/**************************************************************************** 
	* Función para la conversión de fechas a tipo String
	* Se recibe como parámetro una fecha de tipo DATE.
    * Par_Int_Tipo: 1 - Formato dd/mm/yyy
    * Par_Int_Tipo: 2 - Formato yyyy-mm-dd
****************************************************************************/
function fn_ConvertirFecha(par_dt_fecha, par_int_tipo, par_int_tipoString) {
        if (par_dt_fecha === "" || typeof par_dt_fecha === 'undefined') return null;
		var dtFecha = "";
        if (par_int_tipoString === 1) {
			var from = par_dt_fecha.split("/");
			dtFecha = new Date(from[2], from[1] - 1, from[0]);
        } else {
            dtFecha = new Date(par_dt_fecha);
        }
       
		var dd = dtFecha.getDate();
		var mm = dtFecha.getMonth()+1; 
		var yyyy = dtFecha.getFullYear();
		if(dd<10){
			dd='0'+dd;
		} 
		if(mm<10){
			mm='0'+mm;
		} 
		var strFecha = "";
		switch (par_int_tipo) {
			// 1 - Formato: dd/mm/yyyy
			case 1:
				strFecha = dd+'/'+mm+'/'+yyyy;
				break;
			// 2 - Formato: yyyy-mm-dd
			case 2:
				strFecha = yyyy+'-'+mm+'-'+dd;
				break;
			// 3 - Formato: dd/mm/yyyy HH:mm
			case 3:
				strFecha = dd + '/'+mm+'/'+ yyyy + ' ' + [ dtFecha.getHours().padLeft(),dtFecha.getMinutes().padLeft()].join(':');
				break;
			// 4 - Formato: yyyy-mm-dd HH:mm
			case 4:
				strFecha = [ dtFecha.getFullYear().padLeft(),dtFecha.getMonth().padLeft(),dtFecha.getDate().padLeft()].join('-') + ' ' + [ dtFecha.getHours().padLeft(),dtFecha.getMinutes().padLeft()].join(':');
				break;
			// 5 - Formato: HH:mm
			case 5:
				strFecha = [ dtFecha.getHours().padLeft(),dtFecha.getMinutes().padLeft()].join(':');
				break;
		}
		return strFecha;
	}

function fn_CargaBotones(mostrarColumnas) {
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

// funcion que valida que una fecha no sea indefinida, que sea del año 1900 o del año 0001
function fn_ValidarFechaNula(par_dt_fecha, par_int_tipo, par_int_tipoString) {
    return (typeof par_dt_fecha === 'undefined' ||
        par_dt_fecha === '1900-01-01T00:00:00' ||
        par_dt_fecha === '0001-01-01T00:00:00' ||
        par_dt_fecha === null) ? ('') : fn_ConvertirFecha(par_dt_fecha, par_int_tipo, par_int_tipoString);
}

// funcion que valida que una fecha no sea indefinida, que sea del año 1900 o del año 0001
function fn_ValidarFechaNulaFechaSinConvercion(par_dt_fecha) {
    return (typeof par_dt_fecha === 'undefined' ||
        par_dt_fecha === '1900-01-01T00:00:00' ||
        par_dt_fecha === '0001-01-01T00:00:00' ||
        par_dt_fecha === null) ? ('') : par_dt_fecha;
}

// Number.prototype.formatMoney(c, d, t)
// Da formato al dinero con dos decimales y comas.
// Se le pueden pasar 3 parámetros (Decimales, Separador de Decimales y Separador de miles)
Number.prototype.formatMoney = function(c, d, t){
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