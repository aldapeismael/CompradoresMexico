using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Models
{
    public class Borra_AutorizacionExternaAttribute : ActionFilterAttribute
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            IEnumerable<string> authzHeaders;
            token = null;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            string bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpStatusCode statusCode;
            string token;
            // determine whether a jwt exists or not
            if (!TryRetrieveToken(actionContext.Request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                actionContext.Response = actionContext.Request.CreateResponse(statusCode);
            }
            else
            {
                try
                {
                    string secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                    string audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                    string issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));

                    SecurityToken securityToken;
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters
                    {
                        ValidAudience = audienceToken,
                        ValidIssuer = issuerToken,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = LifetimeValidator,
                        IssuerSigningKey = securityKey
                    };

                    // Extract and assign Current Principal and user
                    ClaimsPrincipal responseValidate =
                        tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                    if (ValidarToken(responseValidate.Identity.Name,
                        actionContext.Request.RequestUri.AbsolutePath,
                        actionContext.Request.Method.Method))
                    {
                        Thread.CurrentPrincipal = responseValidate;
                        HttpContext.Current.User = responseValidate;
                    }
                    else
                    {
                        statusCode = HttpStatusCode.Unauthorized;
                        actionContext.Response = actionContext.Request.CreateResponse(statusCode);
                    }
                }
                catch (SecurityTokenValidationException tokenValidationException)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    actionContext.Response = actionContext.Request.CreateResponse(statusCode);
                }
                catch (Exception exception)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    actionContext.Response = actionContext.Request.CreateResponse(statusCode);
                }
            }
            base.OnActionExecuting(actionContext);
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires == null) return false;
            return DateTime.UtcNow < expires;
        }

        private static bool ValidarToken(string usuario, string controller, string metodo)
        {
            if (controller.StartsWith("/"))
                controller = controller.Substring(1);

            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = "corporativo.spUsuarioAutenticar",
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.AddWithValue("@p_ejecuta", 2);
            sqlCommand.Parameters.AddWithValue("@p_usuario", usuario);
            sqlCommand.Parameters.AddWithValue("@p_controlador", controller);
            sqlCommand.Parameters.AddWithValue("@p_metodo", metodo);

            const int idEmpresa = 1;

            DataSet resultado = ConexionBD.EjecutarComando(idEmpresa, 0, sqlCommand, "Autenticar");
            if (resultado.Tables.Count <= 0 || resultado.Tables[0].Rows.Count <= 0)
            {
                return false;
            }

            if (!bool.Parse(resultado.Tables[0].Rows[0][0].ToString()))
            {
                return false;
            }

            return true;
        }
    }
}