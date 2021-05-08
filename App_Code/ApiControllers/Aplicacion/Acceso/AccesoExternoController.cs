using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models.Aplicacion.Acceso;
using Newtonsoft.Json;

[AllowAnonymous]
public class AccesoExternoController : ApiController
{
    [HttpPost]
    public HttpResponseMessage Post([FromBody]AccesoExterno usuarioAutenticar)
    {
        HttpResponseMessage responseMessage;
        try
        {
            if (usuarioAutenticar.Validar())
            {
                usuarioAutenticar.GenerarToken();
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Authenticated")
                };
                responseMessage.Headers.Add("Token", usuarioAutenticar.Token);
                responseMessage.Headers.Add("ExpiresOn", DateTime.UtcNow.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"])).ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
        catch (Exception exception)
        {
            exception.Data.Add("UsuarioAutenticar", JsonConvert.SerializeObject(usuarioAutenticar));
            
            responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error ocurred while processing your request")
            };
        }

        return responseMessage;
    }
}
