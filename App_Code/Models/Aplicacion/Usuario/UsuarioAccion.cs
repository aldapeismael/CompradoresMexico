using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de PerfilAccion
/// </summary>
public class UsuarioAccion : IMetodosModelos<PerfilAccion>
{
    #region Atributos
    private int _IntIdUsuario;
    private int _IntIdUsuarioAccion;
    private int _IntIdPagina;
    private int _IntIdAccion;
    private int _IntBActivo;
    private string _StrCveAccionReferencia;

    //Listas para grids
    List<PaginaMenu> _LstObjPagina;

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdAccion
    {
        get { return _IntIdAccion; }
        set { _IntIdAccion = value; }
    }

    public int IntIdPagina
    {
        get { return _IntIdPagina; }
        set { _IntIdPagina = value; }
    }

    public int IntIdUsuario
    {
        get { return _IntIdUsuario; }
        set { _IntIdUsuario = value; }
    }

    public int IntIdUsuarioAccion
    {
        get { return _IntIdUsuarioAccion; }
        set { _IntIdUsuarioAccion = value; }
    }

    public List<PaginaMenu> LstObjPagina
    {
        get { return _LstObjPagina; }
        set { _LstObjPagina = value; }
    }

    public string StrCveAccionReferencia
    {
        get
        {
            return _StrCveAccionReferencia;
        }

        set
        {
            _StrCveAccionReferencia = value;
        }
    }

    #endregion

    public UsuarioAccion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public UsuarioAccion(int IntIdUsuarioAccion)
    {
        _IntIdUsuarioAccion = IntIdUsuarioAccion;
    }

    #region Funciones
    public RespuestaBD Actualizar()
    {
        throw new NotImplementedException();
    }

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioAccionInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlCommand.Parameters.AddWithValue("@p_IntIdUsuario", this._IntIdUsuario);      // Usuario al cual se aplicaran las acciones!

            //objetos grid
            sqlCommand.Parameters.AddWithValue("@p_ListaPaginasAcciones", JsonConvert.SerializeObject(this._LstObjPagina));

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));


            //Empieza registro Acceso
            string StrOrigen = "";
            var ObjUrl = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer : HttpContext.Current.Request.Url; 
 string StrUrlQuery = ObjUrl.Query;
            string[] StrValorQuery = StrUrlQuery.Split(new string[] { "Origen=" }, StringSplitOptions.RemoveEmptyEntries);
            if (StrValorQuery.Length > 1)
            {
                string[] StrValorQuery2 = StrValorQuery[1].Split('&');
                StrOrigen = "?Origen=" + StrValorQuery2[0];
            }

            ConexionBD conexion = new ConexionBD();
            conexion.SqlCommandComando = sqlCommand;
            conexion.SqlParametro = sqlCommand.Parameters;
            conexion.StrDescRuta = ObjUrl.AbsolutePath + StrOrigen;
            conexion.StrDescModelo = this.GetType().Name + ".cs";
            conexion.StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name;
            conexion.IntBRegistrarAcceso = 1; //En caso de que no se quiera registrar el proceso, solo ejecutarlo, mandar en 0

            DataSet dataSetActualizar = conexion.EjecutarComandoRegistroAcceso();
            //Termina registro Acceso

            //DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : UsuarioAccion.cs, metodo : Insertar()");

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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de actualizar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    public bool ObtenerPorId()
    {
        throw new NotImplementedException();
    }

    public RespuestaDataTable<PerfilAccion> ResultadoDataTables(string filtros)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Métodos Extra
    public static List<UsuarioAccion> ObtenerMovimientoEspecialUsuarioPerfil(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamStrUrl)
    {
        List<UsuarioAccion> objMovimientosEspeciales = new List<UsuarioAccion>();
        var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;
        var IntIdTerminalSesion = VariableGlobal.SessionIntIdTerminal;
        var IntIdPerfilSesion = VariableGlobal.SessionIntIdPerfil;

        string StrOrigen = "";
        var ObjUrl = HttpContext.Current.Request.Url;
        string StrUrlQuery = ObjUrl.Query;
        string[] StrValorQuery = StrUrlQuery.Split(new string[] { "Origen=" }, StringSplitOptions.RemoveEmptyEntries);
        if (StrValorQuery.Length > 1)
        {
            string[] StrValorQuery2 = StrValorQuery[1].Split('&');
            StrOrigen = "?Origen=" + StrValorQuery2[0];
        }

        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spObtenerMovimientoEspecialUsuarioPerfil";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlcommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlcommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuarioSesion);
            sqlcommand.Parameters.AddWithValue("@p_IdPerfil", IntIdPerfilSesion);
            sqlcommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlcommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            sqlcommand.Parameters.AddWithValue("@p_StrBuscar", ObjUrl.AbsolutePath + StrOrigen);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, "UsuarioAccion.cs", "UsuarioAccion.cs");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlcommand, "archivo : UsuarioAccion.cs => ObtenerMovimientoEspecialUsuarioPerfil");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtener.Tables[0].Rows)
                {
                    objMovimientosEspeciales.Add(new UsuarioAccion()
                    {
                        IntIdUsuarioAccion = int.Parse(fila["idUsuarioPerfilAccion"].ToString()),
                        IntIdUsuario = int.Parse(fila["idUsuarioPerfil"].ToString()),
                        IntIdAccion = int.Parse(fila["idAccion"].ToString()),
                        StrCveAccionReferencia = fila["cveAccionReferencia"].ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "UsuarioAccion.cs", "UsuarioAccionListadoController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objMovimientosEspeciales;
    }
    #endregion
}