using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for PaginasMenu
/// </summary>
public class PaginaMenu
{
    #region parametros

    string _StrDescPagina;
    string _StrUrl;
    int _IntBActivo;
    int _IntIdPagina;
    int _IntIdMenu;
    int _IntEjecuta;
    int _IntPaginaInicial;
    string _StrJsonPaginaAccion;
    string _StrJsonReglasNegocio;
    List<Accion> _LstObjAccion;
    List<PerfilAccion> _LstObjPerfilAccion;
    List<UsuarioAccion> _LstObjUsuarioAccion;

    public int IntPaginaInicial
    {
        get { return _IntPaginaInicial; }
        set { _IntPaginaInicial = value; }
    }

    public string StrJsonReglasNegocio
    {
        get { return _StrJsonReglasNegocio; }
        set { _StrJsonReglasNegocio = value; }
    }

    public string StrJsonPaginaAccion
    {
        get { return _StrJsonPaginaAccion; }
        set { _StrJsonPaginaAccion = value; }
    }

    public string StrDescPagina
    {
        get { return _StrDescPagina; }
        set { _StrDescPagina = value; }
    }

    public string StrUrl
    {
        get { return _StrUrl; }
        set { _StrUrl = value; }
    }

    public int IntEjecuta
    {
        get { return _IntEjecuta; }
        set { _IntEjecuta = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdMenu
    {
        get { return _IntIdMenu; }
        set { _IntIdMenu = value; }
    }

    public int IntIdPagina
    {
        get { return _IntIdPagina; }
        set { _IntIdPagina = value; }
    }

    public List<Accion> LstObjAccion
    {
        get { return _LstObjAccion; }
        set { _LstObjAccion = value; }
    }

    public List<PerfilAccion> LstObjPerfilAccion
    {
        get { return _LstObjPerfilAccion; }
        set { _LstObjPerfilAccion = value; }
    }

    public List<UsuarioAccion> LstObjUsuarioAccion
    {
        get { return _LstObjUsuarioAccion; }
        set { _LstObjUsuarioAccion = value; }
    }
    #endregion

    #region constructor
    public PaginaMenu()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public PaginaMenu(int ParamIntEjecuta, string ParamStrUrl)
    {
        _StrUrl = ParamStrUrl;
        _IntEjecuta = ParamIntEjecuta;
    }
    #endregion

    #region metodos
    public bool ObtenerPaginaAccion()    //Se obtienen las acciones de la pagina a la que el usuario entro
    {
        List<PaginaMenu> obj_PaginaAccionListado = new List<PaginaMenu>();
        var IntIdEmpresaSesion = 1;
        var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;
        var IntIdTerminalSesion = VariableGlobal.SessionIntIdTerminal;

        Accion objAccion = new Accion();
        RespuestaBD objRespuestaBD = new RespuestaBD();
        bool bool_Valido = false;
        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spMenuPaginaObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", IntEjecuta);
            sqlcommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlcommand.Parameters.AddWithValue("@p_StrBuscar", StrUrl);
            sqlcommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuarioSesion);
            sqlcommand.Parameters.AddWithValue("@p_IdTerminal", IntIdTerminalSesion);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, "PaginasMenu.cs", "PaginasMenu.cs");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));


            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlcommand, "archivo : PaginasMenu.cs => ObtenerPaginaAccion");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetObtener.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetObtener.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetObtener.Tables[0].Rows[0]["TipoError"].ToString(),
                    short.Parse(dataSetObtener.Tables[0].Rows[0]["IdRespuesta"].ToString())
                );
                this.IntIdPagina = int.Parse(dataSetObtener.Tables[0].Rows[0]["IdRespuesta"].ToString());       // se retornara 0 si no existe el permiso
                this.StrJsonPaginaAccion = dataSetObtener.Tables[0].Columns.Contains("StrJsonPaginaAccion") ? dataSetObtener.Tables[0].Rows[0]["StrJsonPaginaAccion"].ToString() : "[]";
                this.StrJsonReglasNegocio = dataSetObtener.Tables[0].Columns.Contains("StrJsonReglasNegocio") ? dataSetObtener.Tables[0].Rows[0]["StrJsonReglasNegocio"].ToString() : "[]";
                LstObjAccion = null;
                bool_Valido = true;
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return bool_Valido;
    }
    #endregion
}