using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class UsuarioEmpresaListadoController : ApiController
{

    [HttpPost]
    public JsonResult<RespuestaDataTable<UsuarioEmpresa>> ObtenerListado(FiltrosDTUsuarioEmpresa filtrosDtUsuario)//metodo post para datatable
    {
        UsuarioEmpresa ObjUsuario = new UsuarioEmpresa();
        return Json(ObjUsuario.ResultadoDataTables(JsonConvert.SerializeObject(filtrosDtUsuario)));
    }
}
