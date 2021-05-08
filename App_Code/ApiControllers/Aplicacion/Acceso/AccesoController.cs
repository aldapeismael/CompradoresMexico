using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class AccesoController : ApiController
{

    [HttpGet]
    public JsonResult<Acceso> ValidarUsuario([FromUri] string StrUsuario,  string StrPassword)
    {
        Acceso ObjAcceso = new Acceso(StrUsuario, StrPassword);

        var validarAcceso = ObjAcceso.ValidarUsuario();
        

        return Json(validarAcceso);
    }

	[HttpGet]
    public JsonResult<Acceso> CambiarTerminal(int Param_IntIdTerminal,  string Param_StrDescTerminal, string Param_StrCveTerminal)
    {
        Acceso ObjAcceso = Acceso.CambiarTerminal(Param_IntIdTerminal, Param_StrDescTerminal, Param_StrCveTerminal);

        return Json(ObjAcceso);
    }

    //[HttpGet]
    //public HttpResponseMessage Login()
    //{
    //    Response.Redirect("Home.aspx?msg=Hello User, You have successfully registered");
    //}

    [HttpPost]
    public JsonResult<Acceso> AgregarMenu([FromBody] Acceso Param_StrMenu)
    {
        Acceso ObjAcceso = Acceso.AgregarMenu(Param_StrMenu.StrMenuUsuario);

        return Json(ObjAcceso);
    }

    [HttpPost]
    public JsonResult<Acceso> Robot()
    {
        Acceso ObjAcceso = Acceso.Robot();

        return Json(ObjAcceso);
    }
}
