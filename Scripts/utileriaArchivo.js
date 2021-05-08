var instanciaClaseArchivo = null,
	int_grabar = 0;
var int_Error = 0;
var str_TipoError = "";
var str_MensajeError = "";

class class_arch {
    
    constructor(param_idReferencia = 0, param_idMultilista = 0, param_strUrl = "", param_strApiUrl = "", param_strArchivoUrl = "", param_strParametros = "", param_intCargarArchivo = 1, param_intEmbebida = 0, param_strEntidad = "", param_strTipoModal = "pgwModal")
    {
		this._idReferencia = param_idReferencia;
		this._idMultilista = param_idMultilista;
		this._strUrl = param_strUrl;
		this._strApiUrl = param_strApiUrl;
		this._strArchivoUrl = param_strArchivoUrl;
        this._strParametros = JSON.parse(param_strParametros);
        this._intCargaArchivo = param_intCargarArchivo;
        this._intEmbebida = param_intEmbebida;
        this._strEntidad = param_strEntidad;
        this._strTipoModal = param_strTipoModal;
        fn_CargarEventos(this);
        //
	}

    set strTipoModal(para_strTipoModal) {
        this._strTipoModal = para_strTipoModal;
    }
    get strTipoModal() {
        return this._strTipoModal;
    }

    set strEntidad(para_strEntidad) {
        this._strEntidad = para_strEntidad;
    }
    get strEntidad() {
        return this._strEntidad;
    }

    set intEmbebida(para_intEmbebida) {
        this._intEmbebida = para_intEmbebida;
    }
    get intEmbebida() {
        return this._intEmbebida;
    }

    set intError(para_intError) {
        int_Error = para_intError;
    }
    get intError() {
        return int_Error;
    }

    set strTipoError(para_strTipoError) {
        str_TipoError = para_strTipoError;
    }
    get strTipoError() {
        return str_TipoError;
    }

    set strMensajeError(para_strMensajeError) {
        str_MensajeError = para_strMensajeError;
    }
    get strMensajeError() {
        return str_MensajeError;
    }

    set intIdReferenciaArchivo(para_IdReferencia) {
        this._idReferencia = para_IdReferencia;
    }

    set intCargaArchivo(para_intCargarArchivo) {
        this._intCargaArchivo = para_intCargarArchivo;
    }

	get intIdReferenciaArchivo() {
        return this._idReferencia;
    }

    get intIdMultilista() {
        return this._idMultilista;
    }

    get strUrl() {
        return this._strUrl;
    }

	get strApiUrl() {
        return this._strApiUrl;
    }
	
	get strArchivoUrl() {
        return this._strArchivoUrl;
    }

	get strParametro() {
        return this._strParametros;
    }

    get intCargaArchivo() {
        return this._intCargaArchivo;
    }
}

function fn_CargarEventos(param_obj) {
	instanciaClaseArchivo = param_obj;
    fn_EliminarEventos();
    let isPgwModal = false;

	// Evento para cargar el modal
    $("body").on("click", "#btn_AgregarArchivo", function () {
        debugger;
        switch (instanciaClaseArchivo.strTipoModal) {
            case 'pgwModal':
                fn_mostrarModal(instanciaClaseArchivo);
                isPgwModal = true;
                break;
            case 'bootstrap':
                fn_mostrarModalBootstrap(instanciaClaseArchivo);
                isPgwModal = false;
                break;
            default:
                fn_mostrarModal(instanciaClaseArchivo);
        }
    });

	// Evento para agregar una nueva fila
	//$("#btn_AgregarNuevoArchivo").on("click", function () {
	$("body").on("click","#btn_AgregarNuevoArchivo", function () {
		fn_AgregarFilaArchivo();
    });

	// Evento para eliminar una fila
	//$("#btn_EliminarFilaGridArchivo").on("click", function () {
	$("body").on("click","#btn_EliminarFilaGridArchivo", function () {
		fn_EliminarFilaArchivo($(this));
    });

	// Evento para guardar los archivos en la BD y el su carpeta
	//$("#btn_GrabarArchivo").on("click", function () {
    $("body").on("click", "#btn_GrabarArchivo", function () {
        $("#btn_GrabarArchivo").prop("disabled", true);
		fn_GrabarArchivo(1);
		$("#btn_GrabarArchivo").prop("disabled", false);
    });

	// Evento para cerrar la ventana de archivos
	//$("#btn_CancelarArchivo").on("click", function () {
	$("body").on("click","#btn_CancelarArchivo", function () {
        if (isPgwModal) {
            $.pgwModal('close');
        } else {
            $("#modalCargaArchivos").modal("toggle");
        }
        
    });

	// Evento para eliminar una imagen de la BD
	//$("#btn_EliminarArchivoServidor").on("click", function () {
	$("body").on("click","tbody #btn_EliminarArchivoServidor", function () {
		var int_Id = $(this).data("id");
		fn_EliminarArchivo(int_Id);
    });
}

function fn_EliminarEventos() {
	// Evento para cargar el modal
	$("#btn_AgregarArchivo").off("click");
	$("#btn_AgregarNuevoArchivo").off("click");
	$("#btn_EliminarFilaGridArchivo").off("click");
	$("#btn_GrabarArchivo").off("click");
	$("#btn_CancelarArchivo").off("click");
	$("#btn_EliminarArchivoServidor").off("click");

	// Evento para cargar el modal
	$("body").on('click','#btn_AgregarArchivo', function () {});

	// Evento para agregar una nueva fila
	$("body").on('click','#btn_AgregarNuevoArchivo', function () {});

	// Evento para eliminar una fila
	$("body").on('click','#btn_EliminarFilaGridArchivo', function () {});

	// Evento para guardar los archivos en la BD y el su carpeta
	$("body").on('click','#btn_GrabarArchivo', function () {});

	// Evento para eliminar una imagen de la BD
	$("body").on('click','#btn_EliminarArchivoServidor', function () {});
}

/********************************************************************
* Esta funcion muestra el modal para la captura de archivos
********************************************************************/
function fn_mostrarEmbebida(param_Int_BConsulta = 0) {
    var self = instanciaClaseArchivo;
    var strUrl = self.strUrl + "/Views/Catalogos/Archivo/archivoAbc.cshtml?CargaArchivo=" + self.intCargaArchivo + "&bMostrarAcciones=0&bEmbebida=1&strEntidad=" + self.strEntidad;

    $.ajax({
        url: strUrl,
        type: 'GET',
        success: function (data) {
            if (data != "") {
                if (data.indexOf("tbl_SubirArchivo") > 0) {
                    $('#div_DocumentosRelacionados').html(data);

                    if (typeof self.strParametro.ArchivoPermitido !== 'undefined' && self.strParametro.ArchivoPermitido.length > 0) {
                        var strExtensionPermitida = "", strExtensionPermitida2 = "";

                        $.each(self.strParametro.ArchivoPermitido, function (index, value) {
                            strExtensionPermitida += value.Archivo + ",";
                            strExtensionPermitida2 += value.Archivo + " (" + value.Tamano + " kb),";
                        });

                        if (strExtensionPermitida !== "") {
                            $(".it_Archivo").prop("accept", strExtensionPermitida);
                        }

                        $("#span_ArchivosPermitidos").text(strExtensionPermitida2.slice(0, -1));

                        fn_CargarFilaArchivo();
                    }

                } else {
                    toastr["error"]("Ocurrio un error al cargar los documentos relacionados");
                }

            }
        }, complete: function (data) {
            if (param_Int_BConsulta == 1) {
                $("#div_ContenedorPrincipal :input").prop("disabled", true);
            }
        }
    });

}

function fn_mostrarModal() {

    var self = instanciaClaseArchivo;
	//var strUrl = self.strUrl + "/Views/Catalogos/Archivo/archivoAbc.cshtml?CargaArchivo=" + self.intCargaArchivo;
    var strUrl = self.strUrl + "/Views/Catalogos/Archivo/archivoAbc.cshtml?CargaArchivo=" + self.intCargaArchivo + "&bMostrarAcciones=1&bEmbebida=0&strEntidad=" + self.strEntidad;

	var strAncho = "";
	if ($(window).width() < 800) {
		strAncho = $(window).width();
	} else {
		strAncho = 1000;
	}

	$.pgwModal({
		url: strUrl,
		loadingContent: '<span style="text-align:center">Cargando</span>',
		closeOnBackgroundClick: false,
		title: self.strParametro.Encabezado,
		width: strAncho,
		maxWidth: strAncho,
		ajaxOptions : {
			success : function(data) {
				if (data.indexOf("tbl_SubirArchivo") > 0) {
						
					$.pgwModal({ pushContent: data });
						
					if (typeof self.strParametro.ArchivoPermitido !== 'undefined' && self.strParametro.ArchivoPermitido.length > 0) {
						var strExtensionPermitida = "", strExtensionPermitida2 = "";

						$.each(self.strParametro.ArchivoPermitido, function (index, value) {
							strExtensionPermitida += value.Archivo + ",";
							strExtensionPermitida2 += value.Archivo + " (" + value.Tamano +" kb),";
						});

						if (strExtensionPermitida !== "") {
							$(".it_Archivo").prop("accept", strExtensionPermitida);
						}

						$("#span_ArchivosPermitidos").text(strExtensionPermitida2.slice(0, -1));
						
						fn_CargarFilaArchivo();
					}

				} else {
					$.pgwModal({ pushContent: 'No se pudo cargar la pantalla de carga de archivos' });
				}
			}
		}
	});
}

function fn_mostrarModalBootstrap() {
    var strAncho = "";
    if ($(window).width() < 800) {
        strAncho = '100%';
    } else {
        strAncho = '95%';
    }

    var template = '<div class="modal fade" id="modalCargaArchivos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">' +
        '    <div class="modal-dialog modal-lg" role="document" style="width: ' + strAncho+'">' +
        '        <div class="modal-content" style="width: 100%">' +
        '            <div class="modal-header" style="text-align: center">' +
        '                <h5 class="modal-title" id="exampleModalLabel">Carga de archivos</h5>' +
        '                <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
        '                    <span aria-hidden="true">&times;</span>' +
        '                </button>' +
        '            </div>' +
        '            <div class="modal-body">' +
        '                <div id="modal_Cuerpo" class="row"></div>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>';

    //Se elimina el modal en caso de que exista
    $('#modalCargaArchivos').remove();
    //Se carga el template para poder cargar la pantalla
    $("body").append(template);

    var self = instanciaClaseArchivo;
    var strUrl = self.strUrl + "/Views/Catalogos/Archivo/archivoAbc.cshtml?CargaArchivo=" + self.intCargaArchivo + "&bMostrarAcciones=1&bEmbebida=0&strEntidad=" + self.strEntidad;

    $("#modalCargaArchivos #modal_Cuerpo").load(strUrl);
    $("#modalCargaArchivos").modal("show");

    if (("#tbl_SubirArchivo").length > 0) {

        if (typeof self.strParametro.ArchivoPermitido !== 'undefined' && self.strParametro.ArchivoPermitido.length > 0) {
            var strExtensionPermitida = "", strExtensionPermitida2 = "";

            $.each(self.strParametro.ArchivoPermitido, function (index, value) {
                strExtensionPermitida += value.Archivo + ",";
                strExtensionPermitida2 += value.Archivo + " (" + value.Tamano + " kb),";
            });

            if (strExtensionPermitida !== "") {
                $(".it_Archivo").prop("accept", strExtensionPermitida);
            }

            $("#span_ArchivosPermitidos").text(strExtensionPermitida2.slice(0, -1));

            setTimeout(function () {
                fn_CargarFilaArchivo();
            }, 1000);
        }

    } else {
        $("#modalCargaArchivos #modal_Cuerpo").empty();
        $("#modalCargaArchivos #modal_Cuerpo").html('<p>No se pudo cargar la pantalla de carga de archivos.</p>');
    }
}

/********************************************************************
* Esta funcion graba los archivos 
********************************************************************/
function fn_GrabarArchivo(param_intValidar) {
	int_grabar++;
	var self = instanciaClaseArchivo;
    var obj_archivos = $('tbody .it_Archivo');

    // si param_intValidar = 1 entonces funciona realiza validaciones
    if (param_intValidar == 1) {
        // Validamos que los archivos tengan el peso adecuado antes de continuar
        if (!fn_validarArchivos()) {
            return false;
        }
    }

	var form_data = new FormData();
	var intIndice = 0, intIndice2 = 0;

	var lst_obj_archivo = [];
	var obj_archivo = {};

    if (self.intEmbebida == 1 && (obj_archivos.length <= 1)) {
        self.IntError = 0;
        self.strTipoError = "warning";
        self.StrMensajeError = "Sin Archivos por subir";
    }
    else {
        $("tbody .it_Archivo").each(function () {
            intIndice2 = 0;
            var file_Archivo = $(this);
            $.each(file_Archivo[0].files, function (index2, value2) {
                var strNombre = obj_ClaseGlobal.fn_GenerarCadena() + +intIndice + intIndice2,
                    strNombreOriginal = value2.name;
                let int_BLeerArchivo = 0;
                strNombreOriginal = strNombreOriginal.replace("." + value2.name.split('.').pop(), "");
                form_data.append("Archivos_" + intIndice + "_" + intIndice2, value2);

                //Se verifica si el archivo se tiene que leer
                $.each(self.strParametro.ArchivoPermitido, function (i, v) {
                    if (v.Archivo.toLowerCase() === "." + value2.name.split('.').pop().toLowerCase()) {
                        int_BLeerArchivo = v.BLeerArchivo;
                        return false;
                    }
                });

                obj_archivo = {
                    StrNombreArchivoOriginal: strNombreOriginal,
                    StrNombreArchivo: strNombre,
                    StrExtensionArchivo: value2.name.split('.').pop().toLowerCase(),
                    StrRutaArchivo: self.strParametro.RutaArchivo.substring(1),
                    StrRutaArchivoFisica: self.strArchivoUrl + self.strParametro.RutaArchivo.substring(1),
                    StrCveReferencia: file_Archivo.closest("tr").find("#it_CveArchivo").val(),
                    StrDescReferencia: file_Archivo.closest("tr").find("#it_DescArchivo").val(),
                    IntIdMultilista_TipoCargaArchivo: self.intIdMultilista,
                    IntIdReferencia1: self.intIdReferenciaArchivo,
                    IntIdMultilista_TipoCategoriaArchivo: file_Archivo.closest("tr").find("#ddl_Categoria_Archivo option:selected").val(),
                    IntBLeerArchivo: int_BLeerArchivo,
                    BObtenerUltimoId: 0
                };
                lst_obj_archivo.push(obj_archivo);
                intIndice2++;
            });
            //form_data.append("clave_" + intIndice, file_Archivo.closest("tr").find("#it_CveArchivo").val());
            //form_data.append("Descripcion_" + intIndice, file_Archivo.closest("tr").find("#it_DescArchivo").val());
            intIndice++;
        });

        form_data.append("id", self.intIdReferenciaArchivo);
        form_data.append("ultimoId", 0);
        form_data.append("idMultilista", self.intIdMultilista);
        form_data.append("lstArchivo", JSON.stringify(lst_obj_archivo));
        form_data.append("json", JSON.stringify(self.strParametro));

        $.ajax({
            type: 'post',
            url: self.strApiUrl + "/Archivo/",
            data: form_data,
            dataType: "json",
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            timeout: 600000,
            success: function (obj_Datos) {
                if (self.intEmbebida == 0) {
                    toastr[obj_Datos["StrTipoError"]](obj_Datos["StrMensajeError"]);
                }

                if (obj_Datos["IntError"] === 0) {
                    self.IntError = obj_Datos["IntError"];
                    self.strTipoError = obj_Datos["StrTipoError"];
                    self.StrMensajeError = obj_Datos["StrMensajeError"];

                    fn_LimpiarFilaArchivo();
                    fn_CargarFilaArchivo();
                    if ($(".lbl_AgregarArchivo[data-id='" + self.intIdReferenciaArchivo + "']").length > 0) {
                        $(".lbl_AgregarArchivo[data-id='" + self.intIdReferenciaArchivo + "']").text(obj_Datos["IntIdRespuesta"]);
                    }
                }
            },
            error: function (response) {
                if (self.intEmbebida == 0) {
                    toastr["error"]("Ocurrió un error al subir los archivos.");
                }
                else {
                    self.IntError = 1;
                    self.strTipoError = "error";
                    self.StrMensajeError = "Ocurrió un error al subir los archivos.";
                }

            }
        });
    }
}

/********************************************************************
* Esta funcion agrega una fila
********************************************************************/
function fn_AgregarFilaArchivo() {
    var strIdTabla = "tbl_SubirArchivo";
    var self = instanciaClaseArchivo;
    var dt_FechaHoy = obj_ClaseGlobal.fn_ConvertirFecha(obj_ClaseGlobal.fn_ObtenerFechaActual(1, self.strApiUrl + "/Utileria/ObtenerFechaActual/0/"), 3, 2);

    if (obj_ClaseGlobal.fn_validarDatosTabla(strIdTabla)) {
        var row = '<tr>' + $("#" + strIdTabla + " .tr_template").html() + '</tr>';
        row = row.replace('value="templa_value"', 'value="' + dt_FechaHoy + '"');
	    $("#" + strIdTabla + " tbody").append(row);
	}	
}

/********************************************************************
* Esta funcion elimina una fila 
********************************************************************/
function fn_EliminarFilaArchivo(param_objBoton) {
	$(param_objBoton).closest("tr").remove();
}

/********************************************************************
* Esta funcion Limpia los datos de los archivos
********************************************************************/
function fn_LimpiarFilaArchivo() {
	$("#tbl_SubirArchivo tbody").html("");
	var row = '<tr>' + $("#tbl_SubirArchivo .tr_template").html() +'</tr>';
	$("#tbl_SubirArchivo tbody").append(row);
}

/********************************************************************
* Esta funcion carga las filas de registros ya guardados
********************************************************************/
function fn_CargarFilaArchivo() {
    var self = instanciaClaseArchivo;
	var str_AnchoArchivo = "25px"
	$.ajax({
		url: self.strApiUrl + "/Archivo/"
		, async: false
		, type: "GET" //tipo de accion
		, data: {
			parIdReferencia: self.intIdReferenciaArchivo,
			parIdMultilista: self.intIdMultilista
		}
		, contentType: "application/json; charset=utf-8"
		, dataType: "json"
        , success: function (obj_Datos) {
			if (obj_Datos !== null && typeof obj_Datos == 'object') {
				// Limpiamos la tabla
				$("#tbl_DetalleArchivo2 tbody").html("");

				// Recorremos todos los archivos para pintarlos en la tabla
				$.each(obj_Datos, function (index, value) {
						
					// Obtenemos el tr de plantilla para copiarlo y agregarlo
					var row = '<tr data-posicion="'+index+'">' + $("#tbl_DetalleArchivo .tr_template").html() +'</tr>';
					$("#tbl_DetalleArchivo2 tbody").append(row);

					// Buscamos la fila del archivo en base a su posicion
                    var trFila = $("#tbl_DetalleArchivo2 tbody").find("[data-posicion='" + index + "']");
                    $(trFila).find("#it_CategoriaArchivo").text(value.StrDescTipoCategoriaArchivo);
					$(trFila).find("#it_CveArchivo").text(value.StrCveReferencia);
					$(trFila).find("#it_DescArchivo").text(value.StrDescReferencia);
                    $(trFila).find("#it_FechaArchivo").text((value.DtFechaAlta == '1900-01-01T00:00:00') ? '' : obj_ClaseGlobal.fn_ConvertirFecha(value.DtFechaAlta, 1, 2));
					$(trFila).find("#btn_EliminarArchivoServidor").data("id",value.IntIdArchivo);
					switch (value.StrExtensionArchivo) {
						case "jpg":
						case "png":
						case "gif":
							$(trFila).find(".imagenArchivo").html("<a data-fancybox='gallery' href='"+ value.StrRutaArchivo.replace(/\\/g,"/") +"'><img style='max-width:" + str_AnchoArchivo + "' src='"+ value.StrRutaArchivo +"'></a>");
							break;
						case "txt":
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo + "' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/txt.png'></a>");
							break;
						case "doc":
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo +"' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/doc.png'></a>");
							break;
						case "pdf":
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo +"' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/pdf.png'></a>");
							break;
						case "xls":
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo +"' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/xls.png'></a>");
							break;
						case "xlsx":
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo +"' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/xlsx.png'></a>");
							break;							
						default:
							$(trFila).find(".imagenArchivo").html("<a target='_blank' download='"+ value.StrNombreArchivo + '.' + value.StrExtensionArchivo +"' href="+ value.StrRutaArchivo +">" +
								"<img style='max-width:" + str_AnchoArchivo + "' src='"+ self.strUrl + "/img/formatoArchivos/doc.png'></a>");
							break;							
					}
				});
				$("#tbl_DetalleArchivo").removeClass('hidden');
				$.pgwModal('reposition');
			}
		}
		, error: function (xhr, ajaxOptions, thrownError) {
			toastr["error"]("Ocurrió un error al procesar los archivos.");
		}
	});
}

/********************************************************************
* Esta funcion valida los archivos antes de procesarlos
********************************************************************/
function fn_validarArchivos(paramobj) {
	var self = instanciaClaseArchivo;
	var obj_archivos = $('tbody .it_Archivo');
	var bool_valido = true;
    var bool_encontrado = false;

    if (!obj_ClaseGlobal.fn_validarDatosTabla("tbl_SubirArchivo")) {
        return false;
    }

    if (self.intEmbebida == 1 && (obj_archivos.length <= 1)) {
        bool_encontrado = true;
    }
    else {
        $.each(obj_archivos, function (index, value) {
            bool_encontrado = false;

            $.each(value.files, function (index2, value2) {
                var obj_TamanoArchivo = value2.size;
                var obj_TipoArchivo = value2.name.split('.').pop();
                bool_encontrado = false;

                $.each(self.strParametro.ArchivoPermitido, function (i, v) {
                    if (v.Archivo.toLowerCase() === "." + obj_TipoArchivo.toLowerCase()) {
                        bool_encontrado = true;
                        if ((v.Tamano * 1000) < obj_TamanoArchivo) {
                            toastr["error"]("El archivo excede al tamaño permitido (" + v.Tamano + " Kb).");
                            bool_valido = false;
                        }
                        return false;
                    }
                });

                if (bool_encontrado == false) {
                    bool_valido == false;
                    return true;
                }
            });

            if (bool_valido == false) {
                $(this).focus();
                return true;
            }
            if (bool_encontrado == false) {
                return true;
            }
        });
        if (bool_encontrado == false) {
            toastr["error"]("Los archivos seleccionados no contienen la extensión permitida para este catálogo. Favor de verificar.");
            bool_valido = false;
        }

    }
	return bool_valido;
}

//funcion que se llama al momento de dar click en el boton de eliminar una imagen
function fn_EliminarArchivo(par_int_Id) {
	var self = instanciaClaseArchivo;
    swal({								//metodo para el mensage de confirm
        title: "Eliminar Archivo",
        text: "Desea eliminar el archivo?",
        type: "warning",				//tipo de mensage => opciones: warning, error, info y success
        showCancelButton: true,			//mostrar boton de cancelacion
        confirmButtonColor: hex_str_ColorBotonConfirmar,	//color de boton de confirmacion
        confirmButtonText: "Sí!",		//texto en el boton de confirmacion
        cancelButtonText: "No!",		//texto en el boton de cancelacion
        closeOnConfirm: true,			//la ventana de cerrara al momento de seleccionar la opcion de confirmacion
        closeOnCancel: true,			//la ventana de cerrara al momento de seleccionar la opcion de cancelacion
    },
    function (isConfirm) {
		if (isConfirm) {				//si el usuario confirma eliminar
			$.ajax({
				url: self.strApiUrl + "/Archivo/" + par_int_Id				//se llama la metodo delete de ApiController
				, type: "DELETE"
				, async: false
				, contentType: "application/json; charset=utf-8"
				, dataType: "json"
				, success: function (obj_Datos) {
					toastr[obj_Datos["StrTipoError"]](obj_Datos["StrMensajeError"]); //mostramos la respuesta del ajax
					if (obj_Datos["IntError"] === 0) {
						fn_CargarFilaArchivo();
                        if ($(".lbl_AgregarArchivo[data-id='" + self.intIdReferenciaArchivo + "']").length > 0) {
                            $(".lbl_AgregarArchivo[data-id='" + self.intIdReferenciaArchivo + "']").text(obj_Datos["IntIdRespuesta"]);
                        }
					}
				}
				, error: function (result) {
					toastr["warning"]('Ocurrió un error al eliminar el archivo: ' + result.status + ' ' + result.statusText);
				}
			});
		} else {
			$("#btn_BuscarPedidos").click();
		}
    });
}