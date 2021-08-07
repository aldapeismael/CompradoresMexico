using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

/// <summary>
/// Descripción breve de VariablesGlobales
/// Calse para declarar todas las variables globales que se ocuparan en el sistema *
/// </summary>
public static class VariableGlobal
{
    public static string StrUrlPrefixRelative { get { return "~/api"; } }

    //regresa la URL con las API
    public static string StrUrlApi
    {
        get
        {

            var segment = HttpContext.Current.Request.Url.Segments;
            if (segment[1].Contains("Views"))
            {
                return $"http://{HttpContext.Current.Request.Url.Authority}/api";
            }
            else if (segment[2].Contains("UtileriaPeticionExterna"))
            {
                return $"http://{HttpContext.Current.Request.Url.Authority}/api";
            }
            return $"http://{HttpContext.Current.Request.Url.Host}{segment[0]}{segment[1]}/api";
        }
    }
    //regresa la URL raiz del proyecto
    public static string StrUrlSitio
    {
        get
        {

            var segment = HttpContext.Current.Request.Url.Segments;
            if (segment[1].Contains("Views") || segment[1].Contains("Default"))
            {
                return $"http://{HttpContext.Current.Request.Url.Authority}";
            }
            //else if (segment[2].Contains("UtileriaPeticionExterna"))
            //{
            //    return $"http://{HttpContext.Current.Request.Url.Authority}";
            //}

            var url = $"http://{HttpContext.Current.Request.Url.Host}{segment[0]}{segment[1]}";

            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }


    //regresa el nombre del proyecto configurada en Web.config en la seccion appSettings
    public static string StrNombreApicacion
    {
        get
        {

            var nameApplication = ConfigurationManager.AppSettings["NombreAplicacion"];
            return nameApplication;
        }
    }
    //regresa la IdEmpresa si esque existe en las sesiones
    public static int SessionIntIdEmpresa
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["IntIdEmpresa"] != null)
                    return int.Parse(HttpContext.Current.Session["IntIdEmpresa"].ToString());
                return -1;
            }
            catch
            {
                return -1;
            }
        }
    }

    //regresa el IdUsuario si existe en las sesiones
    public static int SessionIntIdUsuario
    {
        get
        {
           try
            {
                if (HttpContext.Current.Session["IntIdUsuario"] != null)
                    return int.Parse(HttpContext.Current.Session["IntIdUsuario"].ToString());
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }

    //regresa el email de usuario si existe en las sesiones
    public static string SessionStrEmailUsuario
    {
        get
        {
            if (HttpContext.Current.Session["StrEmailUsuario"] != null)
                return HttpContext.Current.Session["StrEmailUsuario"].ToString();
            return "";
        }
    }
    //regresa el menu
    public static string SessionStrLocalStorage
    {
        get
        {
            if (HttpContext.Current.Session["StrLocalStorage"] != null)
                return HttpContext.Current.Session["StrLocalStorage"].ToString();
            return "";
        }
    }
    /// <summary>
    /// Regresa el id del empleado relacionado/asignado con el usuario en sesion
    /// </summary>
    public static int SessionIntIdEmpleado
    {
        get
        {
            if (HttpContext.Current.Session["IntIdEmpleado"] != null)
                return int.Parse(HttpContext.Current.Session["IntIdEmpleado"].ToString());
            return 0;
        }
    }

    /// <summary>
    /// Tipo de Usuario, 1 = Comprador, 2 = proveedor
    /// </summary>
    public static int SessionIntTipoUsuario
    {
        get
        {
            if (HttpContext.Current.Session["IntTipoUsuario"] != null)
                return int.Parse(HttpContext.Current.Session["IntTipoUsuario"].ToString());
            return 0;
        }
    }
    
    public static string SessionStrImagenPerfil
    {
        get
        {
            if (HttpContext.Current.Session["StrImagenPerfil"] != null)
                return HttpContext.Current.Session["StrImagenPerfil"].ToString();
            return "";
        }
    }
    //regresa el IdPerfil si existe en las sesiones
    public static int SessionIntIdPerfil
    {
        get
        {
            if (HttpContext.Current.Session["IntIdPerfil"] != null)
                return int.Parse(HttpContext.Current.Session["IntIdPerfil"].ToString());
            return 0;
        }
    }
    //Terminal 
    public static int SessionIntIdTerminal
    {
        get
        {
            if (HttpContext.Current.Session["IntIdTermial"] != null)
                return int.Parse(HttpContext.Current.Session["IntIdTermial"].ToString());
            return 0;
        }
    }
    //Clave Terminal 
    public static string SessionStrCveTerminal
    {
        get
        {
            if (HttpContext.Current.Session["StrCveTerminal"] != null)
                return HttpContext.Current.Session["StrCveTerminal"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrCveTerminal"].ToString()) ? HttpContext.Current.Session["StrCveTerminal"].ToString() : "";
        }
    }

    /// <summary>
    /// Obtiene la clave del usuario de la sesion
    /// </summary>
    public static string SessionStrCveUsuario
    {
        get
        {
            if (HttpContext.Current.Session["StrCveUsuario"] != null)
                return HttpContext.Current.Session["StrCveUsuario"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrCveUsuario"].ToString()) ? HttpContext.Current.Session["StrCveUsuario"].ToString() : "";
        }
    }

    /// <summary>
    /// Obtiene la descripcion del usuario de la sesion
    /// </summary>
    public static string SessionStrDescUsuario
    {
        get
        {
            if (HttpContext.Current.Session["StrDescUsuario"] != null)
                return HttpContext.Current.Session["StrDescUsuario"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrDescUsuario"].ToString()) ? HttpContext.Current.Session["StrDescUsuario"].ToString() : "";
        }
    }

    /// <summary>
    /// Obtiene la descripcion del perfil en sesion
    /// </summary>
    public static string SessionStrDescPerfil
    {
        get
        {
            if (HttpContext.Current.Session["StrDescPerfil"] != null)
                return HttpContext.Current.Session["StrDescPerfil"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrDescUsuario"].ToString()) ? HttpContext.Current.Session["StrDescUsuario"].ToString() : "";
        }
    }

    /// <summary>
    /// Regresa la descripcion del empleado relacionado/asignado al usuario en sesion
    /// </summary>
    public static string SessionStrDescEmpleado
    {
        get
        {
            if (HttpContext.Current.Session["StrDescEmpleado"] != null)
                return HttpContext.Current.Session["StrDescEmpleado"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrDescEmpleado"].ToString()) ? HttpContext.Current.Session["StrDescEmpleado"].ToString() : "";
        }
    }

    /// <summary>
    /// Descripcion de la empresa
    /// </summary>
    public static string SessionStrDescEmpresa
    {
        get
        {
            if (HttpContext.Current.Session["StrDescEmpresa"] != null)
                return HttpContext.Current.Session["StrDescEmpresa"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrDescEmpresa"].ToString()) ? HttpContext.Current.Session["StrDescEmpresa"].ToString() : "";
        }
    }

    /// <summary>
    /// Clave de la empresa
    /// </summary>
    public static string SessionStrCveEmpresa
    {
        get
        {
            if (HttpContext.Current.Session["StrCveEmpresa"] != null)
                return HttpContext.Current.Session["StrCveEmpresa"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrCveEmpresa"].ToString()) ? HttpContext.Current.Session["StrCveEmpresa"].ToString() : "";
        }
    }

    /// <summary>
    /// Descripcion de la terminal en sesion
    /// </summary>
    public static string SessionStrDescTermianl
    {
        get
        {
            if (HttpContext.Current.Session["StrDescTerminal"] != null)
                return HttpContext.Current.Session["StrDescTerminal"].ToString();
            return "";
            //return !String.IsNullOrEmpty(HttpContext.Current.Session["StrDescTerminal"].ToString()) ? HttpContext.Current.Session["StrDescTerminal"].ToString() : "";
        }
    }

    //es robot
    public static int SessionIntBRobot
    {
        get
        {
            if (HttpContext.Current.Session["IntBRobot"] != null)
                return int.Parse(HttpContext.Current.Session["IntBRobot"].ToString());
            return 0;
        }
    }
    //regresa true o false si esta activa la sesion
    public static bool SessionBoolActivo
    {
        get
        {
            if (HttpContext.Current.Session["BoolActivo"] != null)
                return true;
            return false;
        }
    }

    //regresa true o false si esta activa la sesion
    public static string SessionStrTipoMenu
    {
        get
        {
            if (HttpContext.Current.Session["StrMenu"] != null)
                return HttpContext.Current.Session["StrMenu"].ToString();
            return "-1";
        }
    }

    public static string StrUsuarioMenu
    {
        get
        {
            string strLocalStorage = new MultiListaController().ObtenerListaParametros(0, "localstorage", 0, 0, "", "", 1).First().valor1char;
            if (HttpContext.Current.Session[strLocalStorage] != null)
                return HttpContext.Current.Session[strLocalStorage].ToString();
            return "";
        }
    }
    //regresa el menu
    public static string SessionSqlServerName
    {
        get
        {
            if (HttpContext.Current.Session["StrSqlServerName"] != null)
                return HttpContext.Current.Session["StrSqlServerName"].ToString();
            return "";
        }
    }
    public static string StrNombreConexionBD
    {
        get
        {
            return "erp";
        }
    }
    public static string StrConexionBD
    {
        get
        {
            return ConfigurationManager.ConnectionStrings[StrNombreConexionBD].ConnectionString;
        }
    }
    public static string StrNombreConexionBDEstadistica
    {
        get
        {
            return "erpEstadistica";
        }
    }

    //regresa la IdEmpresa si esque existe en las sesiones
    public static int SessionIntIdCompaniaTransportista
    {
        get
        {
            if (HttpContext.Current.Session["IntCompaniaTransportista"] != null)
                return int.Parse(HttpContext.Current.Session["IntCompaniaTransportista"].ToString());
            return 0;
        }
    }

    public static string StrUrlArchivo
    {
        get
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Parametro obj_parametro = new Parametro
                    {
                        IntTipoBusqueda = 1,
                        StrCveParametro = "sipArchivos"
                    };

                    obj_parametro.ObtenerPorClaveParametro();

                    string str_Ip = "http:\\\\" + ip.ToString() + "\\" + obj_parametro.StrValor1char + "\\";
                    return str_Ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
            //return "http:\\\\localhost\\SIP2020ARCHIVOS\\"; 
        }
    }

    public static string SessionStrUrlArchivo
    {
        get
        {
            if (HttpContext.Current.Session["StrRutaArchivo"] != null)
                return HttpContext.Current.Session["StrRutaArchivo"].ToString();

            return "";
        }
    }
    //regresa la URL raiz de la estructura para guardar archivos
    public static string StrUrlArchivos
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "sipArchivos"
            };

            obj_parametro.ObtenerPorClaveParametro();

            var url = HttpContext.Current.Server.MapPath("~");
            url = Path.GetFullPath(Path.Combine(url, "..\\" + obj_parametro.StrValor1char + "\\"));
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }
    public static string StrUrlRaiz
    {
        get
        {

            var url = HttpContext.Current.Server.MapPath("~");
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (!url.EndsWith(@"\"))
            {
                url += @"\";
            }
            return url;
        }
    }
    //regresa la URL raiz de la estructura para guardar TEMPLATE
    public static string StrUrlArchivosHTML
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "SP2020"
            };

            obj_parametro.ObtenerPorClaveParametro();

            var url = HttpContext.Current.Server.MapPath("~");
            url = Path.GetFullPath(Path.Combine(url, "..\\" + obj_parametro.StrValor1char + "\\"));
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }
    //regresa la URL raiz de la estructura para guardar archivos de la Bolsa
    public static string StrUrlArchivosBolsa
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "bolsaArchivos"
            };

            obj_parametro.ObtenerPorClaveParametro();

            var url = HttpContext.Current.Server.MapPath("~");
            url = Path.GetFullPath(Path.Combine(url, "..\\" + obj_parametro.StrValor1char + "\\"));
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }

    //regresa la llave para el js api de google
    public static string StrLlaveMapaJsApi
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "llaveMapaJsApi"
            };

            obj_parametro.ObtenerPorClaveParametro();

            return obj_parametro.StrDescParametro;
        }
    }

    //regresa la url raiz, dependiendo si el proyecto se corrio en el servidor o localmente
    public static string StrUrlProyecto
    {
        get
        {
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        string str_Ip = "http:\\\\" + ip.ToString() + "\\";
            //        str_Ip = str_Ip.Replace(@"\", "/");
            //        return str_Ip;
            //    }
            //}
            //throw new Exception("No network adapters with an IPv4 address in the system!");
            var url = (HttpContext.Current.Request.Url.Authority.Contains("localhost")) ? HttpContext.Current.Request.Url.Authority : HttpContext.Current.Request.Url.Authority + "/compradores";
            return url;
        }
    }

    public static int IntIdEmpresaAcceso
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "empresa"
            };

            obj_parametro.ObtenerPorClaveParametro();

            return obj_parametro.IntValor1num;
        }
    }

    public static int IntIdCompaniaTransportistaAcceso
    {
        get
        {
            Parametro obj_parametro = new Parametro
            {
                IntTipoBusqueda = 1,
                StrCveParametro = "companiaTransportista"
            };

            obj_parametro.ObtenerPorClaveParametro();

            return obj_parametro.IntValor1num;
        }
    }

    public static string StrObtenerIpCliente()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public static string StrObtenerPuertoCliente()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["SERVER_PORT"];
    }

    public static object StrObtenerParametroVista()
    {
        return HttpContext.Current.Session["ObjParametro"];
    }

}