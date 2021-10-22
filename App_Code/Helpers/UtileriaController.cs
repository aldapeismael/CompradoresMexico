using System;
using System.Web.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Text;
using System.Reflection;

/// <summary>
/// Creado por: Joaquin Hernandez
/// FechaCreacion: 31/08/2018
/// Descripcion: Clase donde estaran las funciones genericas  para el uso en el sip
/// </summary>



[AutorizacionRequerida]
public class UtileriaController : ApiController
{
    #region parametros
    private string _StrNombre;
    private string _StrUrl;
    private int _IntId;

    public string StrNombre
    {
        get
        {
            return _StrNombre;
        }

        set
        {
            _StrNombre = value;
        }
    }
    public string StrUrl
    {
        get
        {
            return _StrUrl;
        }

        set
        {
            _StrUrl = value;
        }
    }
    public int IntId
    {
        get
        {
            return _IntId;
        }

        set
        {
            _IntId = value;
        }
    }
    #endregion parametros

    public UtileriaController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [HttpGet]
    public void ProcessRequest()
    {
        var adress = "http://23.253.102.254:8301/";
        //var fileName = "F:/php/htdocs/ala_cfditraslado_desa/archivos/pdf_logo/201810/SIP2020_ALA590112UQ6_A6_0_20180807.pdf";
        //var strRuta = "http://23.253.102.254:8301/ala_cfditraslado_desa/ARCHIVOS/PDF_LOGO/";
        var strFile = "SIP2020_ALA590112UQ6_A6_0_20180807.pdf";

        string strRuta = "http://23.253.102.254:8301/ala_cfditraslado_desa/ARCHIVOS/PDF_LOGO/";
        string fileName = "SIP2020_ALA590112UQ6_A6_0_20180807.pdf", myStringWebResource = null;
        // Create a new WebClient instance.
        WebClient myWebClient = new WebClient();
        // Concatenate the domain with the Web resource filename.
        myStringWebResource = strRuta + fileName;
        // Download the Web resource and save it into the current filesystem folder.
        myWebClient.DownloadFile(myStringWebResource, fileName);

    }


    [HttpGet]//metodo que descarga archivo
    public string DescargarArchivoGenerico(string rutaArchivo)
    {
        //var adress = "http://23.253.102.254:8301/ala_cfditraslado_desa/ARCHIVOS/PDF_LOGO/201810/SIP2020_ALA590112UQ6_CF7_125_20180920.PDF";
        var adress = "http://23.253.102.254:8301/";
        var fileName = "F:/php/htdocs/ala_cfditraslado_desa/archivos/pdf_logo/201810/SIP2020_ALA590112UQ6_A6_0_20180807.pdf";

        WebClient webClient = new WebClient();
        webClient.DownloadFile(adress, fileName);

        return "";

        ////obtenemos la extencion del archivo
        ////var tipoArchivo = Path.GetExtension(nombreArchivo);
        //var tipoArchivo = "";

        //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        ////obtenemos la ruta completa para la descarga del archivo
        //string fileLocation = rutaArchivo;

        //if (!File.Exists(fileLocation))
        //{
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Error");
        //    RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", -1, "Error archivo no encontrado: " + fileLocation, (int)Severidad.BAJA);
        //    return response;
        //}

        //Stream fileStream = File.Open(fileLocation, FileMode.Open);
        //result.Content = new StreamContent(fileStream);

        //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //result.Content.Headers.ContentDisposition.FileName = "nombre";
        //result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
        //return result;
    }//termina funcion DescargarArchivo

    /// <summary>
    /// Creado por: Joaquin Hernandez
    /// FechaCreacion: 31/08/2018
    /// Descripcion: Funcion para descargar archivo
    /// </summary>
    /// <param name="nombreArchivo"></param>
    /// <param name="rutaArchivo"></param>
    /// <returns>retorna el archivo</returns>
    [HttpGet]//metodo que descarga archivo
    public HttpResponseMessage DescargarArchivo(string nombreArchivo, string rutaArchivo)
    {
        //obtenemos la extencion del archivo
        var tipoArchivo = Path.GetExtension(nombreArchivo);

        switch (tipoArchivo)//obtenemos el header para la descarga
        {
            case ".txt":
                tipoArchivo = "text/plain";
                break;
            case ".csv":
                //tipoArchivo = "application/vnd.ms-excel";
                //tipoArchivo = "aplication/octet-stream";
                tipoArchivo = "text/csv";
                break;
            case ".pag":
                tipoArchivo = "text/plain";
                break;
            case ".dat":
                tipoArchivo = "text/plain";
                break;
            case ".pdf":
                tipoArchivo = "application/pdf";
                break;
            case ".xlsx":
                tipoArchivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                break;
            case ".xls":
                tipoArchivo = "application/vend.ms-excel";
                break;
            case ".xml":
                tipoArchivo = "text/xml";
                break;
            case ".zip":
                tipoArchivo = "application/zip";
                break;
            case ".xlsm":
                tipoArchivo = "application/vnd.ms-excel.template.macroEnabled.12";
                break;
        }
        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //obtenemos la ruta completa para la descarga del archivo
        string fileLocation = VariableGlobal.StrUrlArchivos + rutaArchivo + nombreArchivo;

        if (!File.Exists(fileLocation))
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Error");
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", -1, "Error archivo no encontrado: " + fileLocation, (int)Severidad.BAJA);
            return response;
        }

        Stream fileStream = File.Open(fileLocation, FileMode.Open);
        result.Content = new StreamContent(fileStream);

        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        result.Content.Headers.ContentDisposition.FileName = nombreArchivo;
        result.Content.Headers.ContentType = new MediaTypeHeaderValue(tipoArchivo);
        return result;
    }//termina funcion DescargarArchivo

    [HttpPost]
    public HttpResponseMessage SubirArchivo(string StrUrlArchivo)
    {
        HttpResponseMessage result = null;
        try
        {
            if (!(System.IO.Directory.Exists(VariableGlobal.StrUrlArchivos + StrUrlArchivo)))
            {
                System.IO.Directory.CreateDirectory(VariableGlobal.StrUrlArchivos + StrUrlArchivo);
            }
            if (!(System.IO.Directory.Exists(VariableGlobal.StrUrlArchivos + StrUrlArchivo + "Error\\"))) //revisar si existe la carpeta de archivos con error
            {
                System.IO.Directory.CreateDirectory(VariableGlobal.StrUrlArchivos + StrUrlArchivo + "Error\\");
            }
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files) //Foreach para multiples archivos
                {
                    var postedFile = httpRequest.Files[file]; //Obtener el archivo httpPostedFile
                    var filePath = VariableGlobal.StrUrlArchivos + StrUrlArchivo + postedFile.FileName; //Obterner la ruta
                    postedFile.SaveAs(filePath); //Guardar el HttpPostedFile en la ruta
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return result;
    }

    [HttpPost]
    public RespuestaBD EliminaArchivo()
    {
        //este segmento de codigo se deja aqui para su posterior uso, la segunda face sera hacer referencia en la base de datos por su Id
        //[FromUri]int IdFile

        //RespuestaBD response = new RespuestaBD { Error = 0,MsgError = " El archivo con el Id ='"+IdFile.ToString()+"' fue eliminado correctamente."+"'en la ruta = '"+ nombre };
        //AQUI SE DEBE ELIMINAR DE LA TABLA Y FISICAMENTE
        RespuestaBD response = new RespuestaBD { IntError = 0, StrMensajeError = "nombre" };

        //mandamos llamar a la función que borra el archivo
        try
        {
            // este string nos da la ruta completa de la carpeta donde se encuentra el archivo seleccionado
            string fullPath = VariableGlobal.StrUrlArchivos + this._StrUrl + this._StrNombre;

            //aqui se quita la diagonal extra que tiene la variable
            fullPath = fullPath.Replace(@"\\", @"\");

            //esta funcion sirve para borrar el archivo
            File.Delete(fullPath);

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }


        return response;
    }

    [HttpGet]
    public string ObtenerFechaActual(int paramIntFormatoHora)
    {
        string StrFormatoFecha = "";
        switch (paramIntFormatoHora)
        {
            case 1:
                StrFormatoFecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                break;
            case 2:
                StrFormatoFecha = DateTime.Now.ToString("dd/MM/yyyy");
                break;
            case 3:
                StrFormatoFecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                break;
            default:
                StrFormatoFecha = DateTime.Now.ToString();
                break;
        }
        return StrFormatoFecha;
    }

    [HttpGet]
    public string ObtenerNuevoGUID()
    {
        Guid objGuid = new Guid();
        objGuid = Guid.NewGuid();
        return objGuid.ToString();
    }

}