using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class PerfilListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<Perfil>> ObtenerListado(FiltrosDTPerfil filtrosPerfil)//metodo post para datatable
    {
        Perfil ObjPerfil = new Perfil();
        return Json(ObjPerfil.ResultadoDataTables(JsonConvert.SerializeObject(filtrosPerfil)));
    }
}
