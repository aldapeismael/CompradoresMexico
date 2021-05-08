var UtileriaPeriodo = function (str_Url) {
    this.str_Url = str_Url;

};

UtileriaPeriodo.prototype.PeriodoObtener = function (Int_IsPost) {
    let obj_Data = {};

    $.ajax({
        url: this.str_Url,
        data: JSON.stringify(obj_Data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (obj_Datos) {
            if (obj_Datos) {

                var obj_Periodo = JSON.parse(obj_Datos.StrJsonPeriodo);

                $('.periodoDate').datepicker({
                    format: "yyyy-MM",
                    startDate: obj_Periodo[1].periodoFecha,
                    endDate: obj_Periodo[2].periodoFecha,
                    startView: 1,
                    minViewMode: 1,
                    maxViewMode: 2,
                    language: 'es',
                    keyboardNavigation: false,
                    forceParse: false,
                    autoclose: true
                });

                if (Int_IsPost === 0) {
                    $('#it_PeriodoInicioFiltro').val(obj_Periodo[0].Periodo).trigger('change');
                    $('#it_PeriodoFinFiltro').val(obj_Periodo[0].Periodo).trigger('change');
                }
                if (Int_IsPost === 2) {
                    $('#it_PeriodoFiltro').val(obj_Periodo[0].Periodo).trigger('change');
                }

            }
        }
    });
};



UtileriaPeriodo.prototype.PeriodoAnioObtener = function (Int_IsPost) {
    let obj_Data = {};

    $.ajax({
        url: this.str_Url,
        data: JSON.stringify(obj_Data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (obj_Datos) {
            if (obj_Datos) {

                var obj_Periodo = JSON.parse(obj_Datos.StrJsonPeriodo);

                $('.periodoAnioDate').datepicker({
                    format: "yyyy",
                    startDate: obj_Periodo[1].periodoFecha.substring(0,4),
                    endDate: obj_Periodo[2].periodoFecha.substring(0, 4),
                    startView: 1,
                    minViewMode: 2,
                    maxViewMode: 2,
                    language: 'es',
                    keyboardNavigation: false,
                    forceParse: false,
                    autoclose: true
                });
                $('#it_PeriodoAnio').val(obj_Periodo[0].periodoFecha.substring(0, 4)).trigger('change');
                $('#ddl_MesInicioPeriodo option[value="' + obj_Periodo[0].periodoFecha.substring(5, 7) + '"]').prop('selected', true);
                $('#ddl_MesFinPeriodo option[value="' + obj_Periodo[0].periodoFecha.substring(5, 7) + '"]').prop('selected', true);
            }
        }
    });
};