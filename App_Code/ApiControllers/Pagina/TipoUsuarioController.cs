using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

public class TipoUsuarioController : ApiController
{
    [HttpPost]
    public JsonResult<TipoUsuario> VerificaTipoPerfil([FromBody]TipoUsuario value)// metodo para insertar
    {
        value.VerificaTipoPerfil();
        return Json(value);
    }

    //// GET api/<controller>/5
    //public JsonResult<TipoUsuario> Get(int id)
    //{
    //    TipoUsuario objTipoUsuario = new TipoUsuario(id);
    //    objTipoUsuario.ObtenerPorId();
    //    return Json(objTipoUsuario);
    //}

    //// PUT api/<controller>/5
    //public JsonResult<RespuestaBD> Put([FromBody]TipoUsuario value)
    //{
    //    return Json(value.Actualizar());
    //}

    //public IEnumerable<dynamic> ObtenerLista(int ParamIntTipoLista, int ParamIntTipoTexto)
    //{
    //    return TipoUsuario.ObtenerLista(ParamIntTipoLista, ParamIntTipoTexto);
    //}
}
