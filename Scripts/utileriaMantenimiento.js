// fucnion global para obtener el objeto de un modelo dado por un id
function fn_ModeloEquipoObtenerPorId(par_str_url, par_int_id) {
	var obj_ModeloEquipo;
	$.ajax({
        url: par_str_url + "/ModeloEquipo/" + par_int_id
        , type: "GET"
		, async: false
        , contentType: "application/json; charset=utf-8"
        , dataType: "json"
        , success: function (obj_Datos) {
			obj_ModeloEquipo = obj_Datos;
        }
    });

	return obj_ModeloEquipo;
}