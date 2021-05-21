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

    [HttpPost]
    public HttpResponseMessage SubirImagen()
    {
        HttpResponseMessage result = null;
        try
        {
            System.IO.Directory.CreateDirectory(VariableGlobal.StrUrlArchivos + Publicacion.StrUrlArchivoPublicacion);

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = VariableGlobal.StrUrlArchivos + Publicacion.StrUrlArchivoPublicacion + postedFile.FileName;
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        catch (Exception e)
        {

        }

        return result;
    }
}