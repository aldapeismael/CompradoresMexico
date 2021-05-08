﻿var instanciaClase = null;
class class_notificacion {
    constructor(param_strUrl = "", param_strApiUrl = "", param_intFrecuenciaBusqueda = 300000, param_intMinutosDiferencia = 20, param_strAltoNotificacion = '300px', param_intIdUsuario = 0) {
        this._strUrl = param_strUrl;
        this._strApiUrl = param_strApiUrl;
        this._intFrecuenciaBusqueda = param_intFrecuenciaBusqueda;
        this._intMinutosDiferencia = param_intMinutosDiferencia;
        this._strAltoNotificacion = param_strAltoNotificacion;
        this._intIdUsuario = param_intIdUsuario;
        this.fn_CargarInstanciaNotificacion(this);
    }
    
    get strUrl() {
        return this._strUrl;
    }
    get strApiUrl() {
        return this._strApiUrl;
    }
    get intFrecuenciaBusqueda() {
        return this._intFrecuenciaBusqueda;
    }
    get intMinutosDiferencia() {
        return this._intMinutosDiferencia;
    }
    get strAltoNotificacion() {
        return this._strAltoNotificacion;
    }
    fn_CargarInstanciaNotificacion(param_obj) {
        instanciaClase = param_obj;

        $("#btn_Notificaciones").click(function () {
            instanciaClase.fn_RevisarNotificacion();
        });

        fn_CargarNotificacion(true);
        setInterval(function () {
            fn_CargarNotificacion(false);
        }, this._intFrecuenciaBusqueda);
    };
    fn_RevisarNotificacion() {
        var str_Metodo = "";
        var str_Url = "";
        str_Metodo = "GET";
        str_Url = this._strApiUrl + "/NotificacionListado/ObtenerNotificacionPopUpListado/0";
        $.ajax({
            url: str_Url,
            async: true,
            type: str_Metodo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (obj_Datos) {
                $("#ul_ListaNotificacion").html("");
                var objTemp = $.grep(obj_Datos, function (value, index) {
                    return value.StrTipo == "1";
                });
                $.each(objTemp, function (index, value) {
                    var str_Fecha = moment(value.DtFechaNotificacion).format('DD/MM/YYYY HH:mm:ss');
                    var str_FechaHace = "Hace " + moment().diff(value.DtFechaNotificacion, 'hour') + " hora(s)";
                    var strNotificacion =
                        '<li>' +
                        '    <div class="dropdown-messages-box">' +
                        '	    <a href="#" class="pull-left">' +
                        '		    <div>' +
                        '			    <i class="fa ' + value.StrIcono.toLowerCase() + ' fa-2x"></i>' +
                        '		    </div>' +
                        '	    </a>' +
                        '	    <div class="media-body ">' +
                        '		    <small class="pull-right text-navy">Hace ' + moment().diff(value.DtFechaNotificacion, 'hour') + 'h</small>' + value.StrTituloNotificacion +
                        '		    <br>' +
                        '			<small class="text-muted">' + str_FechaHace + ' - ' + str_Fecha + '</small>' +
                        '		</div>' +
                        '	</div>' +
                        '</li>' +
                        '<li class="divider"></li>';
                    $("#ul_ListaNotificacion").append(strNotificacion);
                });

                objTemp = $.grep(obj_Datos, function (value, index) {
                    return value.StrTipo == "2";
                });
                $.each(objTemp, function (index, value) {
                    var strGrupo =
                        '<li>' +
                        '	<a href="#">' +
                        '		<div style="color:#948b96 !important">' +
                        '			<i class="fa ' + value.StrIcono.toLowerCase() + '"></i> ' + value.StrTituloNotificacion +
                        '			<span class="pull-right text-muted small"></span>' +
                        '		</div>' +
                        '	</a>' +
                        '</li>' +
                        '<li class="divider"></li>';
                    $("#ul_ListaNotificacion").append(strGrupo);

                });

                var str_VerTodo = '<li>' +
                    '	<div class="text-center link-block">' +
                    '		<a href="' + instanciaClase.strUrl + '/Views/Aplicacion/Notificacion/notificacionLista.cshtml" style="color: #716b6b !important;">' +
                    '			<strong>Ver todas las notificaciones</strong>' +
                    '			<i class="fa fa-angle-right"></i>' +
                    '		</a>' +
                    '	</div>' +
                    '</li>';
                $("#ul_ListaNotificacion").append(str_VerTodo);
                fn_CargarNotificacion(true);
            }
            , error: function (xhr, ajaxOptions, thrownError) {
                toastr["error"]("Ocurrió un error al tratar de obtener las notificaciones " + thrownError);
            },
            global: false
        });
    };
}

function fn_CargarNotificacion(param_boolRevisa) {
    let int_BHora = (localStorage.getItem("StrHoraNotificacion") ? true : false);
    let dt_FechaActual = new Date();
    if (!int_BHora) {
        localStorage.setItem("StrHoraNotificacion", dt_FechaActual);
    }
    if (moment().diff(localStorage.getItem("StrHoraNotificacion"), 'minute') >= 1 || param_boolRevisa == true) {
        localStorage.setItem("StrHoraNotificacion", dt_FechaActual);
        var signal_Notificacion = $.connection.notificacionHub;
        signal_Notificacion.client.ObtenerNotificacion = function (intContNotificacion) {
            // Html encode display name and message.
            $("#spandNotificacion").text(intContNotificacion);
        };

        $.connection.hub.start().done(function () {
            // Call the Send method on the hub.
            signal_Notificacion.server.obtenerCantidadNotificacion(instanciaClase._intIdUsuario);
        });
    }
};

