using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;

public class SubirArchivo
{
	#region parametros
	private string	_StrNombre;
	private string	_StrUrl;
	private int		_IntId;

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

	public SubirArchivo(){}

	//public HttpResponseMessage PostFile()
 //   {
 //       //esta es la función que se encarga de leer los archivos fisicos para mostrarlos en la pantalla de carga
 //       HttpResponseMessage result = null;
 //       var httpRequest = HttpContext.Current.Request;
 //       if (httpRequest.Files.Count > 0)
 //       {
 //           var docfiles = new List<string>();
 //           foreach (string file in httpRequest.Files)
 //           {
 //               var postedFile = httpRequest.Files[file];
 //               var filePath = VariableGlobal.StrUrlArchivos + postedFile.FileName;
 //               postedFile.SaveAs(filePath);
 //               docfiles.Add(filePath);
 //           }
 //           result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
 //       }
 //       else
 //       {
 //           result = Request.CreateResponse(HttpStatusCode.BadRequest);
 //       }

 //       return result;
 //   }
    
    public RespuestaBD EliminaArchivo()
    {
        //este segmento de codigo se deja aqui para su posterior uso, la segunda face sera hacer referencia en la base de datos por su Id
        //[FromUri]int IdFile

        //RespuestaBD response = new RespuestaBD { Error = 0,MsgError = " El archivo con el Id ='"+IdFile.ToString()+"' fue eliminado correctamente."+"'en la ruta = '"+ nombre };
        //AQUI SE DEBE ELIMINAR DE LA TABLA Y FISICAMENTE
        RespuestaBD response = new RespuestaBD { IntError = 0, StrMensajeError = "nombre" };

        //mandamos llamar a la función que borra el archivo
        BorrarArchivoFisicamente();

        return response;
    }

	//borrado fisico del archivo
    private void BorrarArchivoFisicamente()
    {
		try
		{
			// este string nos da la ruta completa de la carpeta donde se encuentra el archivo seleccionado
			string fullPath = VariableGlobal.StrUrlArchivos + this._StrUrl + this._StrNombre;
		
			//aqui se quita la diagonal extra que tiene la variable
			fullPath = fullPath.Replace(@"\\", @"\");

			//esta funcion sirve para borrar el archivo
			File.Delete(fullPath);
		
			} catch
		{

		}
        
    }
}

