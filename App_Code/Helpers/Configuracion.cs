using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Esta clase se usará para la configuración de la empresa
/// Debe estar como ignore en git, ya que será exclusiva de cada sistema.
/// </summary>
public class Configuracion
{
    public static string StrClaseMenu
    {
        // Colores:
        // Produccion: "#FF7E00"
        // Preproduccion: "#9999ff"
        // Desarrollo: "#727472"
        get
        {
            var nameApplication = "#727472";
            return nameApplication;
        }
    }

    public static string StrRutaVirtual
    {
        get
        {
            var nameApplication = "";
            return nameApplication;
        }
    }

    public static string StrAmbienteSistema
    {
        // Texto:
        // Produccion: ""
        // Preproduccion: "PRE"
        // Desarrollo: "DESA"
        get
        {
            var nameApplication = "DESA";
            return nameApplication;
        }
    }
    // Ruta 
    // Mensaje de Desarrollo / Pre Produccion
    // 
}