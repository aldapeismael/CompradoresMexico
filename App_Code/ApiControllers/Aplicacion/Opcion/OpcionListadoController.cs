using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class OpcionListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<Opcion>> ObtenerListado(FiltrosDTOpcion ObjFiltrosOpcion)
    {
        Opcion objOpcion = new Opcion();
        return Json(objOpcion.ResultadoDataTables(JsonConvert.SerializeObject(ObjFiltrosOpcion)));
    }


    [HttpPost]
    public JsonResult<List<Opcion>> ObtenerListadoGenerico(ObjetoBusqueda objFiltro)
    {
        Opcion objOpcion = new Opcion();
        return Json(objOpcion.ObtenerListaGenerica(objFiltro));
    }
}
