using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class PerfilController : ApiController
{
    /// <summary>
    /// Obtiene la lista de Paises para mostrarlos en el combo
    /// </summary>
    /// <param name="ParamTipoBusqueda">Tipo de busqueda a realizar</param>
    /// <param name="ParamTipoFiltro">Tipo de filtro a realizar</param>
    /// <returns></returns>
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Perfil.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro);
    }

    public JsonResult<List<Perfil>> GET(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Json(Perfil.ObtenerListaAnidada(ParamTipoBusqueda, ParamTipoFiltro));
    }

    // GET api/<controller>/5
    public JsonResult<Perfil> Get(int id)//metodo para obtener por ID
    {
        Perfil ObjPerfil = new Perfil(id);
        ObjPerfil.ObtenerPorId();
        return Json(ObjPerfil);
    }

    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] Perfil value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] Perfil value)//metodo para actualizar
    {
        // Se actualizan los datos y se regresan los datos en JSON
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)//metodo para eliminar
    {
        Perfil ObjPerfil = new Perfil(id);
        return Json(ObjPerfil.Eliminar());
    }
}
