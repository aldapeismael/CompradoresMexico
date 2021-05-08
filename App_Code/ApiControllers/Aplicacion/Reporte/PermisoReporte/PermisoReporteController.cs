using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

public class PermisoReporteController : ApiController
{
    [HttpPost]
    public List<PermisoReporte> ObtenerPermisos(FiltrosDTPermisoReporte ObjFiltrosPermisoReporte)
    {
        PermisoReporte ObjPermisoReporte = new PermisoReporte();
        return ObjPermisoReporte.ObtenerPermisosUsuarios(JsonConvert.SerializeObject(ObjFiltrosPermisoReporte));
    }
}
