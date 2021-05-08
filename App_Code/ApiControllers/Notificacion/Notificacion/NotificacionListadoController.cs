using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

[AutorizacionRequerida]
public class NotificacionListadoController : ApiController
{
    [HttpPost]
    public List<Notificacion> ObtenerListado(FiltrosDTNotificacion ObjFiltrosNotificacion)
    {
        Notificacion objNotificacion = new Notificacion();
        return objNotificacion.ObtenerNotificacionListado(ObjFiltrosNotificacion);
    }

    [HttpGet]
    public List<Notificacion> ObtenerNotificacionPopUpListado()
    {
        Notificacion objNotificacion = new Notificacion();
        return objNotificacion.ObtenerNotificacionPopUpListado();
    }
}
