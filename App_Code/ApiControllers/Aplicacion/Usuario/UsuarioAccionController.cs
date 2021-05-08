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
public class UsuarioAccionController : ApiController
{
    // GET api/<controller>/5
    public JsonResult<UsuarioAccion> Get(int id)//metodo para obtener por ID
    {
        UsuarioAccion ObjPerfilAccion = new UsuarioAccion(id);
        ObjPerfilAccion.ObtenerPorId();
        return Json(ObjPerfilAccion);
    }


    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] UsuarioAccion value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] UsuarioAccion value)//metodo para actualizar
    {
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)
    {
        UsuarioAccion ObjCliente = new UsuarioAccion(id);
        return Json(ObjCliente.Eliminar());
    }
}
