//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Web;
//using System.Web.Http;

///// <summary>
///// Creado por: Joaquin Hernandez Santos
///// Fecha creacion: 31/08/2018
///// Descripcion: clase para descargar archivos
///// </summary>
//public class DescargarArchivoController : ApiController
//{
//    //contructor
//	public DescargarArchivoController()
//	{
//		//
//		// TODO: Add constructor logic here
//		//
//	}

//    [HttpGet]//metodo que descarga archivo
//    public HttpResponseMessage Get(string nombreArchivo, string rutaArchivo)
//    {
//        //obtenemos la extencion del archivo
//        var tipoArchivo = Path.GetExtension(nombreArchivo);
    
//        switch (tipoArchivo)//obtenemos el header para la descarga
//        {
//            case ".txt": tipoArchivo = "text/plain";
//                break;
//            case ".csv":tipoArchivo = "application/vnd.ms-excel";
//                break;

//        }
//        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
//        //obtenemos la ruta completa para la descarga del archivo
//        string fileLocation = VariableGlobal.StrUrlArchivos + rutaArchivo+ nombreArchivo;

//        if (!File.Exists(fileLocation))
//        {
//            throw new HttpResponseException(HttpStatusCode.OK);
//        }

//        Stream fileStream = File.Open(fileLocation, FileMode.Open);
//        result.Content = new StreamContent(fileStream);

//        result.Content.Headers.ContentType = new MediaTypeHeaderValue(tipoArchivo);
//        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
//        return result;
//    }

//}