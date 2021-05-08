using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


/// <summary>
/// Creado por: Joaquin hernandez Santos
/// Fecha Creacion: 10/09/2018 
/// Descripcion: Esta clase se llama ante la peticion de cualquier controlador sea de tipo ajax o indicando directamente la URL en el navegador
/// </summary>
public class AutorizacionRequeridaAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Metodo que se ejecuta antes de cualquier peticion al controlador 
    /// </summary>
    /// <param name="actionContext"></param>
    public override void OnActionExecuting(HttpActionContext actionContext)
    {

        int int_IdUsuario = VariableGlobal.SessionIntIdUsuario;//obtenemos el id de la sesion 
        var str_pathPuerto = HttpContext.Current.Request.Url.Authority;//obtenemos el dominio
        var str_urlLogin = $"http://{str_pathPuerto}/Acceso.cshtml";//obtenemos la URL del login
        var obj_origenPeticion = HttpContext.Current.Request.UrlReferrer;

        //si la peticion es llamada desde la URL
        if (obj_origenPeticion == null)
        {
            //redireccionamos a login
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Location = new Uri(str_urlLogin);
            actionContext.Response = response;
        }

        if (int_IdUsuario == 0)//si no existe sesion
        {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response = response;
        }


        base.OnActionExecuting(actionContext);
    }
    
}


