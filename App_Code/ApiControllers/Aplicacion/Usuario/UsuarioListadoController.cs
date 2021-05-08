using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class UsuarioListadoController : ApiController
{
    


    [HttpPost]
    public JsonResult<RespuestaDataTable<Usuario>> ObtenerListado(FiltrosDTUsuario filtrosDtUsuario)//metodo post para datatable
    {
        Usuario ObjUsuario = new Usuario();
        return Json(ObjUsuario.ResultadoDataTables(JsonConvert.SerializeObject(filtrosDtUsuario)));
    }

    [HttpPost]
    public JsonResult<List<Usuario>> ObtenerListadoGenerico(ObjetoBusqueda objFiltro)
    {
        Usuario objUsuario = new Usuario();

        return Json(objUsuario.ObtenerListaGenerica(objFiltro));
    }

    [HttpPost]
    public JsonResult<RespuestaDataTable<Usuario>> ObtenerListadoAutorizacionCodigo(FiltrosDTUsuario filtrosDtUsuario)//metodo post para datatable
    {
        Usuario ObjUsuario = new Usuario();
        return Json(ObjUsuario.ResultadoDataTablesAutorizacionCodigo(JsonConvert.SerializeObject(filtrosDtUsuario)));
    }

    [HttpGet]
    public JsonResult<ResultadoBusqueda> ObtenerAutoComplete([FromUri] int IntLimite, int IntTipoBusqueda, string StrBuscar)
    {
        return Json(Usuario.ObtenerAutoComplete(IntLimite, IntTipoBusqueda, StrBuscar));
    }


    [HttpPost]
    public JsonResult<ResultadoBusqueda> ObtenerAutoComplete([FromBody] ListaParametrosBusqueda objListaBusqueda)
    {
        return Json(Usuario.ObtenerAutoComplete(objListaBusqueda));
    }

}
