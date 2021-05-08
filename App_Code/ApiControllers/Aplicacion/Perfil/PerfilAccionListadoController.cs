using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class PerfilAccionListadoController : ApiController
{

    [HttpPost]
    public string ObtenerMenu()//metodo post para datatable
    {
        //Perfil ObjPerfil = new Perfil();
        //return Json(ObjPerfil.ResultadoDataTables(JsonConvert.SerializeObject(filtrosPerfil)));

        return "joaquin";
    }
}
