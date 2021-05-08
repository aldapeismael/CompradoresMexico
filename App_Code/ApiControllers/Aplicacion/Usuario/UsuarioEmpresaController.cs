using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class UsuarioEmpresaController : ApiController
{

    // GET api/<controller>/5
    public JsonResult<UsuarioEmpresa> Get(int id)//metodo para obtener por ID
    {
        UsuarioEmpresa ObjUsuarioEmpresa = new UsuarioEmpresa(id);
        ObjUsuarioEmpresa.ObtenerPorId();
        return Json(ObjUsuarioEmpresa);
    }


    // POST api/<controller>
    public JsonResult<RespuestaBD> Post([FromBody] UsuarioEmpresa value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody] UsuarioEmpresa value)//metodo para actualizar
    {
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)
    {
        UsuarioEmpresa ObjCliente = new UsuarioEmpresa(id);
        return Json(ObjCliente.Eliminar());
    }
}