using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using System.Web.WebPages.Html;
using System.Text.RegularExpressions;

/// <summary>
/// Descripción breve de Acceso
/// </summary>
public class Contrasena
{
    #region parametros privados
    string _StrEmail;
    string _StrContrasena;
    string _StrContrasenaNueva;
    string _StrUsuario;
    string _StrFechaVigencia;
    RespuestaBD _ObjRespuestaBdValidar;
    bool _BoolCorreo;
    #endregion

    #region parametros publicos
    public string StrEmail
    {
        get
        {
            return _StrEmail;
        }

        set
        {
            _StrEmail = value;
        }
    }

    public string StrContrasena
    {
        get { return _StrContrasena; }
        set { _StrContrasena = value; }
    }

    public string StrContrasenaNueva
    {
        get { return _StrContrasenaNueva; }
        set { _StrContrasenaNueva = value; }
    }

    public bool BoolCorreo
    {
        get { return _BoolCorreo; }
        set { _BoolCorreo = value; }
    }

    public string StrUsuario
    {
        get { return _StrUsuario; }
        set { _StrUsuario = value; }
    }

    public string StrFechaVigencia
    {
        get { return _StrFechaVigencia; }
        set { _StrFechaVigencia = value; }
    }
    #endregion

    #region construcotres
    public Contrasena() { }
    #endregion

    #region metodos
    public RespuestaBD ValidarUsuario()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_StrUsuario", this._StrEmail);

            DataSet contrasenaEnviar = ConexionBD.EjecutarComando(1, 1, sqlCommand, "Archivo : Contrasena.cs, Metdo: Validar()");

            if (contrasenaEnviar != null && contrasenaEnviar.Tables.Count > 0 && contrasenaEnviar.Tables[0].Rows.Count > 0)
            {
                StrContrasena = contrasenaEnviar.Tables[0].Columns.Contains("StrContrasena") ? contrasenaEnviar.Tables[0].Rows[0]["StrContrasena"].ToString() : "";
                StrUsuario = contrasenaEnviar.Tables[0].Columns.Contains("StrUsuario") ? contrasenaEnviar.Tables[0].Rows[0]["StrUsuario"].ToString() : "";
                StrFechaVigencia = contrasenaEnviar.Tables[0].Columns.Contains("StrFechaVigencia") ? contrasenaEnviar.Tables[0].Rows[0]["StrFechaVigencia"].ToString() : "";

                objRespuestaBD = new RespuestaBD(
                    int.Parse(contrasenaEnviar.Tables[0].Rows[0]["Error"].ToString()),
                    contrasenaEnviar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    contrasenaEnviar.Tables[0].Rows[0]["TipoError"].ToString()
                    );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }

            objRespuestaBD.IntError = 0;

            if (objRespuestaBD.IntError == 0)
            {

                objRespuestaBD = EnvioCorreo(objRespuestaBD);

            }
        }
        catch (Exception e)
        {
            objRespuestaBD = new RespuestaBD(1, e.ToString(), "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_email", this._StrEmail);
            sqlCommand.Parameters.AddWithValue("@p_Contrasena", this._StrContrasena);
            sqlCommand.Parameters.AddWithValue("@p_nuevaContrasena", this._StrContrasenaNueva);

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(1, 1, sqlCommand, "archivo : Contrasena.cs, metodo : Actualizar()");
            if (dataSetActualizar != null && dataSetActualizar.Tables.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetActualizar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetActualizar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetActualizar.Tables[0].Rows[0]["TipoError"].ToString()
                );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }

        }
        catch (Exception e)
        {
            objRespuestaBD = new RespuestaBD(1, e.ToString(), "error");
        }

        return objRespuestaBD;
    }

    private RespuestaBD EnvioCorreo(RespuestaBD objRespuestaBD)
    {

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spParametroObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", 1);
            sqlCommand.Parameters.AddWithValue("@p_CveParametro", "correo_notificacion");

            DataSet dataSetObtener = ConexionBD.EjecutarComando(1, 1, sqlCommand, "archivo: Contrasena.cs");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                string StrParametrosCorreo = dataSetObtener.Tables[0].Rows[0]["valor1char"].ToString();
                string[] StrArrayParametros = StrParametrosCorreo.Split(new string[] { "|" }, StringSplitOptions.None);        //separamos el string en pedazos, obteniendo individualmente la info de usr, correo, host y puerto
                //Como en este caso solo necesitamos el correo, solo buscamos el correo
                var StrBuscaUsr = "usuario=";
                var StrUsrParametro = Array.FindAll(StrArrayParametros, s => s.Contains(StrBuscaUsr));
                var StrUsrCorreo = StrUsrParametro[0].Replace("usuario=",string.Empty);

                Correo objCorreo = new Correo()
                {
                    StrEmailDe = StrUsrCorreo,
                    StrEmailA = this._StrEmail,
                    StrAsunto = "SIP2020|Solicitud de contraseña",
                    StrMensaje = "Estimado usuario: " + this.StrUsuario + "<br>" +
                                 "su contraseña es: " + this.StrContrasena + "<br>" +
                                 "con vigencia hasta el dia " + StrFechaVigencia,
                };

                objRespuestaBD = objCorreo.EnviarCorreo();
            }

        }
        catch (Exception ex)
        {
            objRespuestaBD = new RespuestaBD(1, ex.ToString(), "error");
        }
        return objRespuestaBD;
    }

    #endregion
}