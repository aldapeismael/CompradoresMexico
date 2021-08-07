using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

/// <summary>
/// Descripción breve de Acceso
/// </summary>
public class Acceso
{
    #region parametros privados
    string _Strusuario;
    string _StrPassword;
    string _StrDescEmpresa;
    string _StrCveEmpresa;
    string _StrDescTerminal;
    string _StrCveTerminal;
    string _StrMenuUsuario;
    string _StrImagen;
    string _StrToken;
    private string _StrDescUsuario;
    private string _StrDescPerfil;
    private string _StrDescEmpleado;
    private string _StrCveUsuario;

    int _IntIdUsuario;
    int _IntIdEmpleado;
    int _BoolActivo;
    int _IntBRobot;
    int _IntIdPerfil;
    int _IntIdEmpresa;
    int _IntIdTerminalDefault;
    int _BoolValido;
    int _IntTipoUsuario;
    string _StrLocalStorage;
    string _StrSqlServerName;
    string _StrEmailUsuario;

    RespuestaBD _ObjRespuestaBdValidar;

    #endregion

    #region parametros publicos
    public string Strusuario
    {
        get { return _Strusuario; }
        set { _Strusuario = value; }
    }
    public string StrPassword
    {
        get { return _StrPassword; }
        set { _StrPassword = value; }
    }

    public int IntIdUsuario
    {
        get { return _IntIdUsuario; }
        set { _IntIdUsuario = value; }
    }

    public string StrMenuUsuario
    {
        get { return _StrMenuUsuario; }
        set { _StrMenuUsuario = value; }
    }

    public RespuestaBD ObjRespuestaBdValidar
    {
        get { return _ObjRespuestaBdValidar; }
        set { _ObjRespuestaBdValidar = value; }
    }

    public int BoolActivo
    {
        get { return _BoolActivo; }
        set { _BoolActivo = value; }
    }

    public int IntBRobot
    {
        get { return _IntBRobot; }
        set { _IntBRobot = value; }
    }

    public string StrDescEmpresa
    {
        get { return _StrDescEmpresa; }
        set { _StrDescEmpresa = value; }
    }

    public string StrCveEmpresa
    {
        get { return _StrCveEmpresa; }
        set { _StrCveEmpresa = value; }
    }

    public string StrDescTerminal
    {
        get { return _StrDescTerminal; }
        set { _StrDescTerminal = value; }
    }

    public string StrCveTerminal
    {
        get { return _StrCveTerminal; }
        set { _StrCveTerminal = value; }
    }

    public int IntIdEmpresa
    {
        get { return _IntIdEmpresa; }
        set { _IntIdEmpresa = value; }
    }

    public int IntIdTerminalDefault
    {
        get { return _IntIdTerminalDefault; }
        set { _IntIdTerminalDefault = value; }
    }

    public int IntIdPerfil
    {
        get { return _IntIdPerfil; }
        set { _IntIdPerfil = value; }
    }

    public int BoolValido
    {
        get { return _BoolValido; }
        set { _BoolValido = value; }
    }

    public string StrToken
    {
        get { return _StrToken; }
        set { _StrToken = value; }
    }

    public string StrDescUsuario
    {
        get { return _StrDescUsuario; }
        set { _StrDescUsuario = value; }
    }

    public string StrCveUsuario
    {
        get { return _StrCveUsuario; }
        set { _StrCveUsuario = value; }
    }

    public int IntTipoUsuario
    {
        get { return _IntTipoUsuario; }
        set { _IntTipoUsuario = value; }
    }

    public string StrDescEmpleado
    {
        get { return _StrDescEmpleado; }

        set { _StrDescEmpleado = value; }
    }

    public int IntIdEmpleado
    {
        get { return _IntIdEmpleado; }

        set
        {
            _IntIdEmpleado = value;
        }
    }

    public string StrDescPerfil
    {
        get { return _StrDescPerfil; }

        set { _StrDescPerfil = value; }
    }

    public string StrLocalStorage
    {
        get { return _StrLocalStorage; }

        set { _StrLocalStorage = value; }
    }

    public string StrSqlServerName
    {
        get { return _StrSqlServerName; }

        set { _StrSqlServerName = value; }
    }

    public string StrEmailUsuario
    {
        get { return _StrEmailUsuario; }

        set { _StrEmailUsuario = value; }
    }

    public string StrImagen
    {
        get
        {
            return _StrImagen;
        }

        set
        {
            _StrImagen = value;
        }
    }
    #endregion

    #region construcotres
    public Acceso(string ParamStrUsuario, string ParamStrPassword)
    {
        this.Strusuario = ParamStrUsuario;
        this.StrPassword = ParamStrPassword;
    }

    public Acceso()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    #endregion

    #region metodos
    public Acceso ValidarUsuario()
    {

        RespuestaBD objRespuestaBD = new RespuestaBD();
        Acceso ObjAcceso = new Acceso();
        List<Acceso> ListaAcceso = new List<Acceso>();

        var Session_IntIdEmpresa = 0;
        var Session_IntIdUsuario = 0;

        try
        {

            Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            Session_IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spAccesoObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Usuario", Strusuario);
            sqlCommand.Parameters.AddWithValue("@p_Password", StrPassword);

            sqlCommand.Parameters.AddWithValue("@p_IpCliente", VariableGlobal.StrObtenerIpCliente());
            sqlCommand.Parameters.AddWithValue("@p_PuertoCliente", VariableGlobal.StrObtenerPuertoCliente());
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");

            DataSet validarUsuario = ConexionBD.EjecutarComando(Session_IntIdEmpresa, Session_IntIdUsuario, sqlCommand, "archivo : Usuario.cs, metodo : Insertar()");

            if (validarUsuario != null && validarUsuario.Tables.Count > 0 && validarUsuario.Tables[0].Rows.Count > 0 && int.Parse(validarUsuario.Tables[0].Rows[0]["idUsuario"].ToString()) != 0)
            {


                ObjAcceso = new Acceso
                {
                    IntIdUsuario = int.Parse(validarUsuario.Tables[0].Rows[0]["idUsuario"].ToString()),
                    IntIdEmpleado = int.Parse(validarUsuario.Tables[0].Rows[0]["idEmpleado"].ToString()),
                    IntBRobot = int.Parse(validarUsuario.Tables[0].Rows[0]["bRobot"].ToString()),
                    IntTipoUsuario = int.Parse(validarUsuario.Tables[0].Rows[0]["tipoUsuario"].ToString()),
                    StrImagen = validarUsuario.Tables[0].Rows[0]["imagen"].ToString(),
                    BoolActivo = 1,
                    StrToken = "",
                    IntIdPerfil = int.Parse(validarUsuario.Tables[0].Rows[0]["idPerfil"].ToString()),
                    IntIdEmpresa = int.Parse(validarUsuario.Tables[0].Rows[0]["idEmpresa"].ToString()),
                    IntIdTerminalDefault = int.Parse(validarUsuario.Tables[0].Rows[0]["idTerminal"].ToString()),
                    StrDescTerminal = validarUsuario.Tables[0].Rows[0]["descTerminal"].ToString(),
                    StrDescUsuario = validarUsuario.Tables[0].Rows[0]["descUsuario"].ToString(),
                    StrDescPerfil = validarUsuario.Tables[0].Rows[0]["descPerfil"].ToString(),
                    StrDescEmpleado = validarUsuario.Tables[0].Rows[0]["descEmpleado"].ToString(),
                    StrDescEmpresa = validarUsuario.Tables[0].Rows[0]["descEmpresa"].ToString(),
                    StrCveEmpresa = validarUsuario.Tables[0].Rows[0]["cveEmpresa"].ToString(),
                    StrCveUsuario = validarUsuario.Tables[0].Rows[0]["cveUsuario"].ToString(),
                    StrCveTerminal = validarUsuario.Tables[0].Rows[0]["cveTerminal"].ToString(),
                    StrLocalStorage = validarUsuario.Tables[0].Rows[0]["localstorage"].ToString(),
                    StrSqlServerName = validarUsuario.Tables[0].Rows[0]["serverName"].ToString(),
                    ObjRespuestaBdValidar = new RespuestaBD(
                        short.Parse(validarUsuario.Tables[1].Rows[0]["Error"].ToString()),
                        validarUsuario.Tables[1].Rows[0]["MensajeError"].ToString(),
                        validarUsuario.Tables[1].Rows[0]["TipoError"].ToString()
                    ),
                    StrEmailUsuario = validarUsuario.Tables[0].Rows[0]["email"].ToString()
                };

            }
            else
            {
                ObjAcceso = new Acceso
                {
                    IntIdUsuario = 0,
                    IntBRobot = 0,
                    BoolActivo = 0,
                    IntIdPerfil = 0,
                    ObjRespuestaBdValidar = new RespuestaBD(
                        short.Parse(validarUsuario.Tables[1].Rows[0]["Error"].ToString()),
                        validarUsuario.Tables[1].Rows[0]["MensajeError"].ToString(),
                        validarUsuario.Tables[1].Rows[0]["TipoError"].ToString()
                    )
                };


            }

        }
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/

            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de accesar con el IdUsuario:" + Session_IntIdUsuario + ", IdEmpresa:" + Session_IntIdEmpresa, e.HResult, e.Message, (int)Severidad.ALTA);


            ObjAcceso = new Acceso
            {
                IntIdUsuario = 0,
                IntBRobot = 0,
                BoolActivo = 0,
                IntIdPerfil = 0,
                ObjRespuestaBdValidar = new RespuestaBD(
                    1,
                    "Error al tratar de accesar",
                    "Error"
                )
            };

            /********************************************************************************************************************************************************************************/
        }

        return ObjAcceso;
    }

    /// <summary>
    /// Metodo para realizar el cambio de terminal 
    /// </summary>
    /// <param name="Param_IntIdTerminal"></param>
    /// <param name="Param_StrDescTerminal"></param>
    /// <returns></returns>
    public static Acceso CambiarTerminal(int Param_IntIdTerminal, string Param_StrDescTerminal, string Param_StrCveTerminal)
    {
        Acceso objAcceso = new Acceso();

        //--------------------------
        // AGREGAR VALIDACIÓN POR BD
        //--------------------------

        try
        {
            if (Param_IntIdTerminal > 0)
            {
                objAcceso.BoolValido = 1;
                HttpContext.Current.Session["IntIdTermial"] = Param_IntIdTerminal;
                HttpContext.Current.Session["StrCveTerminal"] = Param_StrCveTerminal;
                HttpContext.Current.Session["StrDescTerminal"] = Param_StrDescTerminal;

                objAcceso.IntIdTerminalDefault = Param_IntIdTerminal;
                objAcceso.StrDescTerminal = Param_StrDescTerminal;
                objAcceso.StrCveTerminal = Param_StrCveTerminal;
            }
        }
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/

            var Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var Session_IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, objAcceso.GetType().Name + ".cs", objAcceso.GetType().Name + "Controller.cs", "Error al tratar de realizar el cambio de terminal con el IdUsuario:" + Session_IntIdUsuario + ", IdEmpresa:" + Session_IntIdEmpresa, e.HResult, e.Message, (int)Severidad.ALTA);


            objAcceso = new Acceso
            {
                IntIdUsuario = 0,
                IntBRobot = 0,
                BoolActivo = 0,
                IntIdPerfil = 0,
                ObjRespuestaBdValidar = new RespuestaBD(
                    1,
                    "Error al tratar de realizar el cambio de terminal ",
                    //e.Message(),
                    "Error"
                )
            };

            /********************************************************************************************************************************************************************************/
        }

        return objAcceso;
    }

    public static Acceso AgregarMenu(string Param_StrMenu)
    {
        var strLocalStorage = new MultiListaController().ObtenerListaParametros(0, "localstorage", 0, 0, "", "", 1).First().valor1char;

        Acceso objAcceso = new Acceso();

        HttpContext.Current.Session[strLocalStorage] = Param_StrMenu;

        objAcceso.StrMenuUsuario = Param_StrMenu;

        return objAcceso;
    }

    public static Acceso Robot()
    {
        Acceso objAcceso = new Acceso();

        HttpContext.Current.Session["IntBRobot"] = 1;
        HttpContext.Current.Session["BoolActivo"] = true;
        HttpContext.Current.Session["IntCompaniaTransportista"] = 1;
        HttpContext.Current.Session["IntIdEmpresa"] = 1;
        HttpContext.Current.Session["IntIdTermial"] = 8;
        HttpContext.Current.Session["IntIdPerfil"] = 1;
        HttpContext.Current.Session["IntIdUsuario"] = 1;

        return objAcceso;
    }

    #endregion
}