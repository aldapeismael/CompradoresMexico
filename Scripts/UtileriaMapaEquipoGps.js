var UtileriaMapaEquipoGps = function (int_TipoEquipo, int_IdEquipo, int_TipoBusqueda, str_DescEquipo, int_IdEventoGeocerca) {
    this.int_TipoEquipo = int_TipoEquipo;
    this.int_IdEquipo = int_IdEquipo;
    this.int_TipoBusqueda = int_TipoBusqueda;
    this.str_DescEquipo = str_DescEquipo;
    this.int_IdEventoGeocerca = int_IdEventoGeocerca;
};

UtileriaMapaEquipoGps.prototype.ObtenerUltimaPosicionTractor = function (url) {
    let obj_Data = {
        IntTipoEquipo: this.int_TipoEquipo,
        IntIdEquipo: this.int_IdEquipo,
        IntIdEventoGeocerca: this.int_IdEventoGeocerca,
        IntTipoBusqueda: this.int_TipoBusqueda
    };

    var str_DescEquipo = this.str_DescEquipo;

    $.ajax({
        url: url,
        data: JSON.stringify(obj_Data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (obj_Datos) {
            if (obj_Datos) {
                
                var desc = '<h4>' + str_DescEquipo + '</h4>';
                desc += '<p><b>Fecha:</b>' + obj_ClaseGlobal.fn_ConvertirFecha(obj_Datos.DtGPSDateTime, 1, 2) + '</p>';
                desc += '<p><b>Velocidad:</b>' + obj_Datos.IntSpeedVehicle + '</p>';
                desc += '<p><b>Ubicacion:</b>' + obj_Datos.StrPlace + '</p>';

                var mapOptions = {
                    center: new google.maps.LatLng(obj_Datos.DcLatitude, obj_Datos.DcLongitude),
                    zoom: 18,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                var map = new google.maps.Map(document.getElementById("div_Mapa"), mapOptions);

                var marker = new google.maps.Marker({
                    position: map.getCenter()
                    , map: map
                    , animation: google.maps.Animation.DROP
                });

                var popup = new google.maps.InfoWindow({
                    content: desc
                });

                popup.open(map, marker);
            }
        }
    });
};
