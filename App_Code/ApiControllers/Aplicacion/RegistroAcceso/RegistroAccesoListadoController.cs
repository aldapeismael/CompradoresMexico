using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

[AutorizacionRequerida]
public class RegistroAccesoListadoController : ApiController
{
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamIntTipoFiltro)
    {
        return RegistroAcceso.ObtenerLista(ParamTipoBusqueda, ParamIntTipoFiltro);
    }

    [HttpPost]
    public List<RegistroAcceso> ObtenerRegistroAccesoListado(FiltrosRegitroAcceso ObjFiltros)
    {
        RegistroAcceso ObjRegistroAcceso = new RegistroAcceso();
        return ObjRegistroAcceso.ObtenerRegistroAccesoListado(JsonConvert.SerializeObject(ObjFiltros));
    }
}
