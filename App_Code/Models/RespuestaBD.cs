using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DatabaseResponse
/// </summary>
public class RespuestaBD
{
    public int IntError { get; set; }
    public string StrMensajeError { get; set; }
    public string StrTipoError { get; set; }
    public int IntIdRespuesta { get; set; }
    public int IntRespuesta { get; set; }
    public int IntRespuesta2 { get; set; }
    public string StrRespuesta { get; set; }
    public string StrRespuesta2 { get; set; }
    public string StrJsonRespuesta { get; set; }

    public RespuestaBD()
    {

    }

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
    }

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError, int ParamIntIdRespuesta, string ParamStrRespuesta, string ParamStrRespuesta2)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
        this.IntIdRespuesta = ParamIntIdRespuesta;
        this.StrRespuesta = ParamStrRespuesta;
        this.StrRespuesta2 = ParamStrRespuesta2;
    }

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError, int ParamIntIdRespuesta)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
        this.IntIdRespuesta = ParamIntIdRespuesta;
    }

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError, int ParamIntIdRespuesta, int ParamIntRespuesta)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
        this.IntIdRespuesta = ParamIntIdRespuesta;
        this.IntRespuesta = ParamIntRespuesta;
    }

    /**
     * EJEMPLO DE USO DEL StrJsonRespuesta
     * 
     * EJEMPLO 1:
     * --OBTIENE TODOS LOS DATOS DE LA TABLA CON UNA COLUMNA MAS DONDE SE OBTIENEN LOS MISMOS DATOS PERO EN FORMATO JSON
     * select * , json = (select * from corporativo.cliente for json auto) from corporativo.cliente where idCliente = 9
     * 
     * EJEMPLO 2:
     * --TODO LOS DATOS DE UNA TABLA EN UN SOLO CAMPO CON LA ESTRUCTURA JSON
     * select json = (select * from corporativo.cliente for json auto) from corporativo.cliente where idCliente = 9
     * 
     * EJEMPLO 3:
     * --OBTENIENDO DATOS EN FORMATO JSON MANUALMENTE
     * select 
	        dato1 = 'dato1'
	        ,dato2 = 50
	        ,dato3 = 'dato3'
        for json path 
     *
     * EJEMPLO 4:
     * --ASIGNANDOLE UN ALIAS
     * select (
	        select * from 
	        corporativo.cliente 
	        for json auto
        ) as datosJson
     * 
     
         */

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError, int ParamIntIdRespuesta, string ParamStrJsonRespuesta)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
        this.IntIdRespuesta = ParamIntIdRespuesta;
        this.StrJsonRespuesta = ParamStrJsonRespuesta;
    }

    public RespuestaBD(int ParamIntError, string ParamStrMensajeError, string ParamStrTipoError, int ParamIntIdRespuesta, int ParamIntRespuesta, int ParamIntRespuesta2, string ParamStrRespuesta, string ParamStrRespuesta2, string ParamStrJsonRespuesta)
    {
        this.IntError = ParamIntError;
        this.StrMensajeError = ParamStrMensajeError;
        this.StrTipoError = ParamStrTipoError;
        this.IntIdRespuesta = ParamIntIdRespuesta;
        this.IntRespuesta = ParamIntRespuesta;
        this.IntRespuesta2 = ParamIntRespuesta2;
        this.StrRespuesta = ParamStrRespuesta;
        this.StrRespuesta2 = ParamStrRespuesta2;
        this.StrJsonRespuesta = ParamStrJsonRespuesta;
    }

}