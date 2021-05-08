using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class RegistroProcesoListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<RegistroProceso>> ObtenerListado(FiltrosDTRegistroProceso filtrosRegistroProceso)//metodo post para datatable
    {
        RegistroProceso ObjRegistroProceso = new RegistroProceso();
        return Json(ObjRegistroProceso.ResultadoDataTables(JsonConvert.SerializeObject(filtrosRegistroProceso)));
    }
}
