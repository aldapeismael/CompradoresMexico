using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class MapaEquipoGpsController: ApiController
{
    [HttpPost]
    public JsonResult<MapaEquipoGps> MapaPosionObtener([FromBody]MapaEquipoGps value)
    {
        return Json(value.MapaPosionObtener());
    }

    [HttpPost]
    public JsonResult<MapaEquipoGps> MapaGoogleRuta([FromBody]MapaEquipoGps value)
    {
        return Json(value.MapaPosionObtener());
    }
}