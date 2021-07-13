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
public class PublicacionAccionController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaBD> EliminaPublicacion(Publicacion objPublicacion)
    {
        return Json(objPublicacion.Eliminar());
    }
}