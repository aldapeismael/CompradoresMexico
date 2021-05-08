using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Contacto
{
    //definimos atributos
    #region parametros
    int     _IntIdContacto;
    int     _IntIdRelacion;
    string  _StrContacto;
    int     _IntMultilistaPuesto;
    string  _StrTelefono;
	string  _StrFax;
    string  _StrEmail;
    string  _IntBActivo;
    
    public int IntIdContacto { get{ return _IntIdContacto; } set{ _IntIdContacto = value; } }
    public int IntIdRelacion { get{ return _IntIdRelacion; } set{ _IntIdRelacion = value; } }
    public string StrContacto { get{ return _StrContacto; } set{ _StrContacto = value; } }
    public int IntMultilistaPuesto { get{ return _IntMultilistaPuesto; } set{ _IntMultilistaPuesto = value; } }
    public string StrTelefono { get{ return _StrTelefono; } set{ _StrTelefono = value; } }
	public string StrFax { get{ return _StrFax; } set{ _StrFax = value; } }
    public string StrEmail { get{ return _StrEmail; } set{ _StrEmail = value; } }
    public string IntBActivo { get{ return _IntBActivo; } set{ _IntBActivo = value; } }
    
    #endregion parametros

    //creamos los constructores
    #region constructor
        public Contacto(){}

    
    #endregion constructor


    #region metodos

    #endregion metodos




}