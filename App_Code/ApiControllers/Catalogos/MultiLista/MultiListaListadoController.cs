using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class MultiListaListadoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaDataTable<MultiLista>> ObtenerListado(FiltrosDTMultiLista ObjFiltrosMultiLista)//metodo post para datatable
    {
        MultiLista ObjMultiLista = new MultiLista();
        return Json(ObjMultiLista.ResultadoDataTables(JsonConvert.SerializeObject(ObjFiltrosMultiLista)));
    }
}
