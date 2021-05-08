using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class MultiListaController : ApiController
{
    /// <summary>
    /// Obtiene una lista para usar en un select
    /// </summary>
    /// <param name="ParamTipoBusqueda">Tipo de busqueda</param>
    /// <param name="ParamTipoFiltro">Tipo de filtro</param>
    /// <param name="ParamCveMultiLista">Clave a buscar en la tabla de multilista</param>
    /// <returns></returns>
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamCveMultiLista)
    {
        return MultiLista.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro, ParamCveMultiLista);
    }

	public IEnumerable<dynamic> ObtenerListaParametros(int ParamIdParametro, string ParamCveParametro, int ParamValor1num, int ParamValor2num, string ParamValor1char, string ParamValor2char, int ParamBActivo)
    {
        return MultiLista.ObtenerListaParametros(ParamIdParametro, ParamCveParametro, ParamValor1num, ParamValor2num, ParamValor1char, ParamValor2char, ParamBActivo);
    }

	public IEnumerable<dynamic> ObtenerListaChar(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamCveMultiLista, string ParamCveMultiListaChar)
    {
        return MultiLista.ObtenerListaChar(ParamTipoBusqueda, ParamTipoFiltro, ParamCveMultiLista, ParamCveMultiListaChar);
    }

    public JsonResult<List<MultiLista>> GET(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamCveMultiLista)
    {
        return Json(MultiLista.ObtenerListaAnidada(ParamTipoBusqueda, ParamTipoFiltro, ParamCveMultiLista));
    }

    // GET api/<controller>/5
    public JsonResult<MultiLista> Get(string cve)//metodo para obtener por ID
    {
        MultiLista objMultiLista = new MultiLista();
        objMultiLista.ObtenerPorClave(cve);
        return Json(objMultiLista);
    }

    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] MultiLista value)// metodo para insertar
    {
        return null;
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] MultiLista value)//metodo para actualizar
    {
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)//metodo para eliminar
    {
        return null;    
    }
}