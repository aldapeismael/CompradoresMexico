using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class AccionListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<Accion>> ObtenerListado(FiltrosDTAccion ObjFiltrosAccion)
    {
        Accion objAccion = new Accion();
        return Json(objAccion.ResultadoDataTables(JsonConvert.SerializeObject(ObjFiltrosAccion)));
    }
}
