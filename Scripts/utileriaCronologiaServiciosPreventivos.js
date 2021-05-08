
/**
 * * Otiene la cronología de servicios preventivos para equipo componente, modelo componente y componente (mtto)
 * @param {any} param_LstIdComponentes
 */
function fn_ObtenerCronologiaServiciosPreventivos(param_LstIdComponentes, param_StrVariableApi) {
    if (param_LstIdComponentes.length > 0) {
        var lst_objComponente = [];
        for (var i = 0; i < param_LstIdComponentes.length; i++) {
            var objComponente = {
                IntIdComponente: param_LstIdComponentes[i]
            }
            lst_objComponente.push(objComponente);
        }

        var objComponentePreventivo = {
            LstObjComponente: lst_objComponente
        }

        $("#div_Kilometros").html("");
        $("#div_Dias").html("");
        $("#div_BotonesGrupos").html("");
        var str_MensajeError = "Los siguientes servicios tienen errores: ";
        var int_ContadorComponentesCronologia = param_LstIdComponentes.length;

        $.ajax({
            url: param_StrVariableApi + "/ComponenteListado/ObtenerCronologiaServicioPreventivo/0/"
            , data: JSON.stringify(objComponentePreventivo)
            , type: "POST"
            , contentType: "application/json; charset=utf-8"
            , dataType: "json"
            , async: false
            , success: function (obj_Datos) {
                $(obj_Datos).each(function (index, value) {
                    var kilometrosGrupo = obj_Datos[index].LstObjCronologiaKilometrosGrupo;
                    var kilometrosDatos = obj_Datos[index].LstObjCronologiaKilometros;
                    var diasGrupo = obj_Datos[index].LstObjCronologiaDiasGrupo;
                    var diasDatos = obj_Datos[index].LstObjCronologiaDias;
                    var str_Fuente = "";

                    $("#lbl_CantidadGruposKilometros").html(kilometrosGrupo.length);
                    $("#lbl_CantidadGruposDias").html(diasGrupo.length);
                    if ($("#lbl_CantidadGrupos").length > 0) {
                        $("#lbl_CantidadGrupos").text(kilometrosGrupo.length + diasGrupo.length);
                    }

                    $("#div_BotonesGrupos").append('<div class="col-lg-12" id="div_BtnGruposKm"></div>');

                    for (var i = 0; i < kilometrosGrupo.length; i++) {
                        $("#div_BtnGruposKm").append('<a id="btn_GrupoKm_' + kilometrosGrupo[i].IntKilometros + '" style="margin-bottom: 10px; margin-right: 10px;" onclick="fn_ResaltarTituloCronologia(' + kilometrosGrupo[i].IntKilometros + ', 1)" class="btn btn-primary btn-sm" href="#tbl_Kilometros_' + kilometrosGrupo[i].IntKilometros + '">S' + (i + 1) + ' ' + kilometrosGrupo[i].IntKilometros.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' Kilómetros</a>');
                        $("#div_Kilometros").append('<table id="tbl_Kilometros_' + kilometrosGrupo[i].IntKilometros + '" data-id="' + kilometrosGrupo[i].IntKilometros + '" class="table table-bordered tabla-grid tbl_Kilometros"><thead>' +
                            '<tr style="color: #676a6c; font-weight: bold;"><th colspan="4" style="text-align:center;">S' + (i + 1) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + kilometrosGrupo[i].IntKilometros.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' Kilómetros</th>' +
                            '<tr style="background-color:#e0e0e0; font-weight: bold;"><th style="width: 30px;"></th><th style="width: 30px;"></th><th style="width: 300px;">Servicio</th><th style="width: 80px;">Dias</th></tr></tr></thead><tbody></tbody></table>');
                    }

                    for (var x = 0; x < kilometrosDatos.length; x++) {
                        var dias = "";
                        var componentePadre = $('input[data-id="' + kilometrosDatos[x].IntIdComponente + '"]').closest("td").prev("td").text();
                        if (kilometrosDatos[x].IntBError == 1) {
                            dias = '<a style="color: red;" href="#" onclick="fn_AbrirMensajeErrorCronologiaServicios(' + "'" + kilometrosDatos[x].StrMensajeError + "'" + ');">' + kilometrosDatos[x].IntDias.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "</a>";
                            str_MensajeError += "Grupo: " + kilometrosDatos[x].IntKilometrosGrupo.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " Kilómetros<br> Servicio: " + kilometrosDatos[x].StrDescServicio + "<br>";
                            $("#btn_GrupoKm_" + kilometrosDatos[x].IntKilometrosGrupo).removeClass("btn-primary");
                            $("#btn_GrupoKm_" + kilometrosDatos[x].IntKilometrosGrupo).addClass("btn-danger");
                        } else {
                            dias = kilometrosDatos[x].IntDias.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }

                        if (kilometrosDatos[x].IntBIndicador == 1 && kilometrosDatos[x].IntKilometrosGrupo == kilometrosDatos[x].IntKilometros) {
                            str_Fuente = "bold";
                        } else {
                            str_Fuente = "normal";
                        }

                        if (componentePadre == "") {
                            componentePadre = "C" + int_ContadorComponentesCronologia;
                        }

                        $("#tbl_Kilometros_" + kilometrosDatos[x].IntKilometrosGrupo + " tbody").append('<tr style="font-weight: ' + str_Fuente + '"><td style="font-weight: bold; text-align: center;">' + componentePadre + '</td><td style="font-weight: bold; text-align: center;">' + kilometrosDatos[x].StrNumeroServicio + '</td><td>' + kilometrosDatos[x].StrDescServicio + '</td><td>' + dias + '</td></tr>');
                    }

                    $("#div_BotonesGrupos").append('<div class="col-lg-12" id="div_BtnGruposDias"></div>');

                    for (var i = 0; i < diasGrupo.length; i++) {
                        $("#div_BtnGruposDias").append('<a id="btn_GrupoDias_' + diasGrupo[i].IntDias + '" data-id="' + diasGrupo[i].IntDias + '" style="margin-bottom: 10px; margin-right: 10px;" onclick="fn_ResaltarTituloCronologia(' + diasGrupo[i].IntDias + ', 2)" class="btn btn-primary btn-sm" href="#tbl_Dias_' + diasGrupo[i].IntDias + '">S' + (i + 1) + ' ' + diasGrupo[i].IntDias.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' Dias</a>');
                        $("#div_Dias").append('<table id="tbl_Dias_' + diasGrupo[i].IntDias + '" class="table table-bordered tabla-grid tbl_Dias" data-id="' + diasGrupo[i].IntDias + '"><thead>' +
                            '<tr style="color: #676a6c; font-weight: bold;"><th colspan="4" style="text-align:center;">S' + (i + 1) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + diasGrupo[i].IntDias.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' Dias</th>' +
                            '<tr style="background-color:#e0e0e0; font-weight: bold;"><th style="width: 30px;"></th><th style="width: 30px;"></th><th style="width: 300px;">Servicio</th><th style="width: 80px;">Kilómetros</th></tr></tr></thead><tbody></tbody></table>');
                    }

                    for (var x = 0; x < diasDatos.length; x++) {
                        var kilometros = "";
                        var componentePadre = $('input[data-id="' + diasDatos[x].IntIdComponente + '"]').closest("td").prev("td").text();

                        if (diasDatos[x].IntBError == 1) {
                            if (str_MensajeError != "Los siguientes servicios tienen errores: ") {
                                str_MensajeError += "<br>";
                            }
                            kilometros = '<a style="color: red;" href="#" onclick="fn_AbrirMensajeErrorCronologiaServicios(' + "'" + diasDatos[x].StrMensajeError + "'" +  ');">' + diasDatos[x].IntKilometros.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "</a>";
                            str_MensajeError += "Grupo: " + diasDatos[x].IntDiasGrupo + " Días <br>Servicio: " + diasDatos[x].StrDescServicio + "<br>";
                            $("#btn_GrupoDias_" + diasDatos[x].IntDiasGrupo).removeClass("btn-primary");
                            $("#btn_GrupoDias_" + diasDatos[x].IntDiasGrupo).addClass("btn-danger");
                        } else {
                            kilometros = diasDatos[x].IntKilometros.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }

                        if (diasDatos[x].IntBIndicador == 1 && diasDatos[x].IntDiasGrupo == diasDatos[x].IntDias) {
                            str_Fuente = "bold";
                        } else {
                            str_Fuente = "normal";
                        }

                        if (componentePadre == "") {
                            componentePadre = "C" + int_ContadorComponentesCronologia;
                        }

                        $("#tbl_Dias_" + diasDatos[x].IntDiasGrupo + " tbody").append('<tr style="font-weight: ' + str_Fuente + '"><td style="font-weight: bold; text-align: center;">' + componentePadre + '</td><td style="font-weight: bold; text-align: center;">' + diasDatos[x].StrNumeroServicio + '</td><td>' + diasDatos[x].StrDescServicio + '</td><td>' + kilometros + '</td></tr>');
                    }

                });

                if ($('.nav-tabs a[href="#li_TabKilometros"]').parent().hasClass("active")) {
                    $("#div_BtnGruposDias").hide();
                    $("#div_BtnGruposKm").show();
                }

                if ($('.nav-tabs a[href="#li_TabDias"]').parent().hasClass("active")) {
                    $("#div_BtnGruposDias").show();
                    $("#div_BtnGruposKm").hide();
                }
                
                if (str_MensajeError != "Los siguientes servicios tienen errores: ") {
                    toastr["warning"](str_MensajeError, '', {
                        "closeButton": true,
                        "showDuration": "0",
                        "hideDuration": "1000",
                        "timeOut": "0",
                        "extendedTimeOut": "0",
                        "positionClass": "toast-middle-center"
                    });
                }
            }, error: function () {
                toastr["error"]("Hubo un error al cargar la Cronología de Servicios Preventivos, intentelo de nuevo");
            }
        });
    }
}

$('body').delegate('.nav-tabs a[href="#li_TabKilometros"]', 'click', function () {
    if (!$(this).parent().hasClass("active")) {
        $("#div_BtnGruposDias").hide();
        $("#div_BtnGruposKm").show();
    }
});

$('body').delegate('.nav-tabs a[href="#li_TabDias"]', 'click', function () {
    if (!$(this).parent().hasClass("active")) {
        $("#div_BtnGruposDias").show();
        $("#div_BtnGruposKm").hide();
    }
});

function fn_ResaltarTituloCronologia(param_intIdGrupo, param_IntKmDias) {
    if (param_IntKmDias == 1) {
        $('table[id^="tbl_Kilometros_"]').each(function () {
            if ($(this).data("id") == param_intIdGrupo) {
                $(this).children("thead").children("tr").first("th").css("color", "#249aff").css("font-size", "15px");
            } else {
                $(this).children("thead").children("tr").first("th").css("color", "#676a6c").css("font-size", "12px");
            }
        });
    } else if (param_IntKmDias == 2) {
        $('table[id^="tbl_Dias_"]').each(function () {
            if ($(this).data("id") == param_intIdGrupo) {
                $(this).children("thead").children("tr").first("th").css("color", "#249aff").css("font-size", "15px");
            } else {
                $(this).children("thead").children("tr").first("th").css("color", "#676a6c").css("font-size", "12px");
            }
        });
    }
}

function fn_AbrirMensajeErrorCronologiaServicios(paramStr_MsgError) {
    swal("Error del componente", paramStr_MsgError, "info");
}