using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

public class SubirDocumentosController : ApiController
{
    [HttpPost]
    public async Task<HttpResponseMessage> UploadFile(string id, string nameFile, int tipo)
    {
        if (!Request.Content.IsMimeMultipartContent())
        {
            return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType,
                "The request doesn't contain valid content!");
        }
        try
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var pathFile = VariableGlobal.StrUrlArchivos;
                var dataStream = await file.ReadAsStreamAsync();
                //if (tipo == 11)
                //{
                //    pathFile = HttpContext.Current.Server.MapPath("~/Files/Imagenes/Puesto/");
                //}
                //else if (tipo == 12)
                //{
                //    pathFile = HttpContext.Current.Server.MapPath("~/Files/Imagenes/Empleado/");
                //}
                //else if (tipo == 13)
                //{
                //    pathFile = HttpContext.Current.Server.MapPath("~/Files/Imagenes/Operador/");
                //}
                //else if (tipo == 14)
                //{
                //    pathFile = HttpContext.Current.Server.MapPath("~/Files/Imagenes/Evento/");
                //}
                //else
                //{
                //    if (tipo== 1)
                //    {
                //        pathFile = HttpContext.Current.Server.MapPath("~/Files/Candidato/");
                //    }
                //    if (tipo == 2)
                //    {
                //                pathFile = HttpContext.Current.Server.MapPath("~/Files/Empleado/");
                //    }
                //    if (tipo == 3)
                //    {
                //        pathFile = HttpContext.Current.Server.MapPath("~/Files/Operador/");
                //    }
                //    if (tipo == 4)
                //    {
                //        pathFile = HttpContext.Current.Server.MapPath("~/Files/ArchivosEvaluacion/");
                //    }
                //    if (tipo == 5)
                //    {
                //        pathFile = HttpContext.Current.Server.MapPath("~/Files/ArchivosDemanda/");
                //    }
                //}
                
                if (!Directory.Exists(pathFile))
                    Directory.CreateDirectory(pathFile);
                if (tipo == 5)
                {
                    using (var fileStream = File.Create(Path.Combine(pathFile, $"Usuario{id}_{Path.GetFileName(nameFile)}")))
                    {
                        dataStream.Seek(0, SeekOrigin.Begin);
                        dataStream.CopyTo(fileStream);
                        fileStream.Dispose();
                    }
                }
                else if (tipo < 11)
                {
                    using (var fileStream = File.Create(Path.Combine(pathFile, $"Usuario{id}_{Path.GetFileNameWithoutExtension(nameFile)}.pdf")))
                    {
                        dataStream.Seek(0, SeekOrigin.Begin);
                        dataStream.CopyTo(fileStream);
                        fileStream.Dispose();
                    }
                }else
                    {
                    using (var fileStream = File.Create(Path.Combine(pathFile, $"imagen_{Path.GetFileNameWithoutExtension(nameFile)}.png")))
                    {
                        dataStream.Seek(0, SeekOrigin.Begin);
                        dataStream.CopyTo(fileStream);
                        fileStream.Dispose();
                    }
                }
                
                dataStream.Dispose();

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent("Successful upload", Encoding.UTF8, "text/plain");
                response.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(@"text/html");
                return response;
            }
        }
        catch (Exception e)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
        }
        finally
        {
            GC.Collect();
        }
        return null;
    }
}
