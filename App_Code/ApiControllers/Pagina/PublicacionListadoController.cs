using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

public class PublicacionListadoController : ApiController
{
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
