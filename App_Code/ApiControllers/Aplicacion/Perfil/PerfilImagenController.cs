using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

public class PerfilImagenController : ApiController
{

    [HttpPost]
    public JsonResult<RespuestaBD> Post([FromBody] Perfil value)// metodo para insertar
    {
        return Json(value.ActualizarImagen());
    }

}
