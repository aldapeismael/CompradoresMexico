using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

[AutorizacionRequerida]
public class UsuarioAccionListadoController : ApiController
{
    public List<UsuarioAccion> ObtenerMovimientoEspecialUsuarioPerfil(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamStrUrl)
    {
        return UsuarioAccion.ObtenerMovimientoEspecialUsuarioPerfil(ParamTipoBusqueda, ParamTipoFiltro, ParamStrUrl);
    }
}
