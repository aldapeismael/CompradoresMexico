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
public class UsuarioController : ApiController
{
    // Obtiene una lista para usar en un select
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Usuario.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro);
    }
    // GET api/<controller>/5
    public JsonResult<Usuario> Get(int id)//metodo para obtener por ID
    {
        Usuario ObjCliente = new Usuario(id);
        ObjCliente.ObtenerPorId();
        return Json(ObjCliente);
    }
    

    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] Usuario value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] Usuario value)//metodo para actualizar
    {
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)
    {
        Usuario ObjCliente = new Usuario(id);
        return Json(ObjCliente.Eliminar());
    }
}
