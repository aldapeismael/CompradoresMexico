using System.Web.Http;
using System.Web.Http.Results;


public class ContrasenaController : ApiController
{
	[HttpPost]
    public JsonResult<RespuestaBD> ValidarUsuario([FromBody]Contrasena value)
    {
        return Json(value.ValidarUsuario());
    }

    public JsonResult<RespuestaBD> Put([FromBody] Contrasena value)//metodo para actualizar
    {
        //Menu ObjMenu = new Menu();
        return Json(value.Actualizar());
    }
}
