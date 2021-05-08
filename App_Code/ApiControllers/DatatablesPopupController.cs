using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

[AutorizacionRequerida]
public class DatatablesPopupController : ApiController
{
    [HttpPost]
    public List<List<string>> Post(DatatablesPopup cls_consulta)
    {
        List<List<string>> listGlobal = new List<List<string>>();
        string StrEjecucion = "";
        // Validamos que el objeto no es nulo, en caso contrario regresamos error.
        if (cls_consulta == null)
        {
            List<string> list = new List<string>();
            list.Add("No se especifico un id");
            listGlobal.Add(list);
            return listGlobal;
        }

        //Se pasa el nombre del controlador del cual se hace la llamada
        cls_consulta.Controlador = this.ControllerContext.RouteData.Values["controller"].ToString();

        StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.StrDescEjecuta) + " ";
        if (cls_consulta.ParamIntNombre1 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamIntNombre1) + "='" + cls_consulta.IdGenerico1 + "',";
        if (cls_consulta.ParamIntNombre2 != null)            
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamIntNombre2) + "='" + cls_consulta.IdGenerico2 + "',";
        if (cls_consulta.ParamIntNombre3 != null)            
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamIntNombre3) + "='" + cls_consulta.IdGenerico3 + "',";
        if (cls_consulta.ParamIntNombre4 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamIntNombre4) + "='" + cls_consulta.IdGenerico4 + "',";
        if (cls_consulta.ParamIntNombre5 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamIntNombre5) + "='" + cls_consulta.IdGenerico5 + "',";
        if (cls_consulta.ParamStringNombre1 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamStringNombre1) + "='" + cls_consulta.StringGenerico1 + "',";
        if (cls_consulta.ParamStringNombre2 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamStringNombre2) + "='" + cls_consulta.StringGenerico2 + "',";
        if (cls_consulta.ParamStringNombre3 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamStringNombre3) + "='" + cls_consulta.StringGenerico3 + "',";
        if (cls_consulta.ParamStringNombre4 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamStringNombre4) + "='" + cls_consulta.StringGenerico4 + "',";
        if (cls_consulta.ParamDtFechaNombre1 != null)
            StrEjecucion += Encriptado.DesencriptarDatos(cls_consulta.ParamDtFechaNombre1) + "='" + cls_consulta.DateTimeGenerico1 + "',";

        StrEjecucion = StrEjecucion.TrimEnd(',');
        cls_consulta.Query = StrEjecucion;

        // Validamos el objeto
        cls_consulta.fn_validaObjeto();

        // Si la validación regresa un error, regresamos el error en el LIST
        if (!String.IsNullOrEmpty(cls_consulta.Error))
        {
            List<string> list = new List<string>();
            list.Add(cls_consulta.Error);
            listGlobal.Add(list);
            return listGlobal;
        }

        return cls_consulta.fn_obtenerDatos();
    }
}
/// <summary>
/// Enumerado para definir el tipo de query que se va a ejecutar
/// * Solo hay que agregar los que sean necesarios
/// </summary>
public enum TipoQuery
{
    AutorizacionCodigos = 1,
    EstacionServicioCombustible = 2,
	Convenio = 3,
	PedidosDocumentos = 4,
    OrigenDestino = 5,
    RelacionEnvioLiquidacion = 6,
    Prestamo = 7,
    BancoMovimiento = 8 //Se llama desde la vista de PolizaBancoMovimientoLista.cshtml

}