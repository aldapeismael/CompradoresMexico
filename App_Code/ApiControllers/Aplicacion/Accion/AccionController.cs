using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class AccionController : ApiController
{
    // Obtiene una lista para usar en un select
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Accion.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro);
    }

    public JsonResult<List<Accion>> GET(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Json(Accion.ObtenerListaAnidada(ParamTipoBusqueda, ParamTipoFiltro));
    }

    // GET api/<controller>/5
    public JsonResult<Accion> Get(int id)//metodo para obtener por ID
    {
        Accion ObjAccion = new Accion(id);
        ObjAccion.ObtenerPorId();
        return Json(ObjAccion);
    }

    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] Accion value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] Accion value)//metodo para actualizar
    {
        // Se actualizan los datos y se regresan los datos en JSON
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)//metodo para eliminar
    {
        Accion ObjAccion = new Accion(id);
        return Json(ObjAccion.Eliminar());
    }
}
