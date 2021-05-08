using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class RegistroErrorListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<RegistroError>> ObtenerListado(FiltrosDTRegistroError ObjFiltrosRegistroError)
    {
        RegistroError objRegistroError = new RegistroError();
        return Json(objRegistroError.ResultadoDataTables(JsonConvert.SerializeObject(ObjFiltrosRegistroError)));
    }
}
