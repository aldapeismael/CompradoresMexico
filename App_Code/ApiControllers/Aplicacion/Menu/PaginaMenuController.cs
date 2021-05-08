using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class PaginaMenuController : ApiController
{
    // GET api/<controller>/5
    public JsonResult<PaginaMenu> Get(int ParamIntEjecuta, string ParamStrUrl)//metodo para obtener por Busqueda en filtro
    {
        PaginaMenu ObjPaginaMenu = new PaginaMenu(ParamIntEjecuta,ParamStrUrl);
        ObjPaginaMenu.ObtenerPaginaAccion();
        return Json(ObjPaginaMenu);
    }
}
