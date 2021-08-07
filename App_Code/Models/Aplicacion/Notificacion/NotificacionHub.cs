using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.AspNet.SignalR;

public class NotificacionHub : Hub
{
    #region Metodos
    public void ObtenerCantidadNotificacion(int ParamIdUsuario, int ParamIdUsuarioDestino, int ParamIdPublicacion)
    {
        try
        {
            Notificacion objNotificacion = new Notificacion();
            objNotificacion.ObtenerCantidadNotificacion(ParamIdUsuario, ParamIdUsuarioDestino, ParamIdPublicacion);
            Clients.Caller.ObtenerNotificacion(objNotificacion.StrJsonChat);
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
    }
    #endregion
}
