using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class PublicacionController : ApiController
{

    [HttpPost]
    public JsonResult<RespuestaBD> Post([FromBody]Publicacion value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // GET api/<controller>/5
    public JsonResult<Publicacion> Get(int id)
    {
        Publicacion objPublicacion = new Publicacion(id);
        objPublicacion.ObtenerPorId();
        return Json(objPublicacion);
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody]Publicacion value)
    {
        return Json(value.Actualizar());
    }

    public IEnumerable<dynamic> ObtenerLista(int ParamIntTipoLista, int ParamIntTipoTexto)
    {
        return Publicacion.ObtenerLista(ParamIntTipoLista, ParamIntTipoTexto);
    }
}