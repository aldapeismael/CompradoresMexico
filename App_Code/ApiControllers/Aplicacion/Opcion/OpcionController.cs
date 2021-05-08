using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class OpcionController : ApiController
{
    // GET api/<controller>/5
    public JsonResult<Opcion> Get(int id)//metodo para obtener por ID
    {
        Opcion ObjOpcion = new Opcion(id);
        ObjOpcion.ObtenerPorId();
        return Json(ObjOpcion);
    }

    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] Opcion value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] Opcion value)//metodo para actualizar
    {
        // Se actualizan los datos y se regresan los datos en JSON
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)//metodo para eliminar
    {
        Opcion ObjOpcion = new Opcion(id);
        return Json(ObjOpcion.Eliminar());
    }
}
