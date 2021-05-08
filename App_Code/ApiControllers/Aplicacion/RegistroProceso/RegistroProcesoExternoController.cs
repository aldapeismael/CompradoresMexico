using System.Web.Http;
using Models;
using System.Web.Http.Results;

[AutorizacionExterna]
public class RegistroProcesoExternoController : ApiController
{
    [HttpPost]
    public JsonResult<RespuestaBD> Post([FromBody]RegistroProcesoExterno value)
    {
        return Json(value.Insertar());
    }
}