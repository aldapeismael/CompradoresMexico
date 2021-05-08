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
public class PerfilAccionController : ApiController
{
    // GET api/<controller>/5
    public JsonResult<PerfilAccion> Get(int id)//metodo para obtener por ID
    {
        PerfilAccion ObjPerfilAccion = new PerfilAccion(id);
        ObjPerfilAccion.ObtenerPorId();
        return Json(ObjPerfilAccion);
    }


    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] PerfilAccion value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] PerfilAccion value)//metodo para actualizar
    {
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)
    {
        PerfilAccion ObjCliente = new PerfilAccion(id);
        return Json(ObjCliente.Eliminar());
    }
}
