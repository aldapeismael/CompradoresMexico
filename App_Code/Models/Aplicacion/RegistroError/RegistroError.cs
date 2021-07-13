using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Descripción breve de RegistroError
/// </summary>
public class RegistroError
{
    #region Propiedades
    int _IntIdRegistroError;
    int _IntIdUsuario;
    int _IntIdEmpresa;
    DateTime _DateFechaInicio;
    DateTime _DateFechaFin;
    int _IntIdMultilista_Severidad;
    int _IntIdMultilista_TipoError;
    int _IntErrorCodigo;
    int _IntErrorLinea;
    string _StrErrorMensaje;
    string _StrErrorVista;
    string _StrDescStoredProcedure;
    string _StrDescStoredProcedureParametro;
    string _StrDescNavegador;
    string _StrIP;
    string _StrVista;
    string _StrDescControlador;
    string _StrDescModelo;
    string _StrDescModeloMetodo;
    int _IntBActivo;
    Empresa _objEmpresa;
    Usuario _objUsuario;
    MultiLista _objMultiLista_Severidad;
    MultiLista _objMultiLista_TipoError;

    public int IntIdRegistroError
    {
        get { return _IntIdRegistroError; }
        set { _IntIdRegistroError = value; }
    }
    public int IntIdUsuario
    {
        get { return _IntIdUsuario; }
        set { _IntIdUsuario = value; }
    }
    public int IntIdEmpresa
    {
        get { return _IntIdEmpresa; }
        set { _IntIdEmpresa = value; }
    }
    public DateTime DateFechaInicio
    {
        get { return _DateFechaInicio; }
        set { _DateFechaInicio = value; }
    }
    public DateTime DateFechaFin
    {
        get { return _DateFechaFin; }
        set { _DateFechaFin = value; }
    }
    public int IntIdMultilista_Severidad
    {
        get { return _IntIdMultilista_Severidad; }
        set { _IntIdMultilista_Severidad = value; }
    }
    public int IntIdMultilista_TipoError
    {
        get { return _IntIdMultilista_TipoError; }
        set { _IntIdMultilista_TipoError = value; }
    }
    public int IntErrorCodigo
    {
        get { return _IntErrorCodigo; }
        set { _IntErrorCodigo = value; }
    }
    public int IntErrorLinea
    {
        get { return _IntErrorLinea; }
        set { _IntErrorLinea = value; }
    }
    public string StrErrorMensaje
    {
        get { return _StrErrorMensaje; }
        set { _StrErrorMensaje = value; }
    }
    public string StrErrorVista
    {
        get { return _StrErrorVista; }
        set { _StrErrorVista = value; }
    }
    public string StrDescStoredProcedure
    {
        get { return _StrDescStoredProcedure; }
        set { _StrDescStoredProcedure = value; }
    }
    public string StrDescStoredProcedureParametro
    {
        get { return _StrDescStoredProcedureParametro; }
        set { _StrDescStoredProcedureParametro = value; }
    }
    public string StrDescNavegador
    {
        get { return _StrDescNavegador; }
        set { _StrDescNavegador = value; }
    }
    public string StrIP
    {
        get { return _StrIP; }
        set { _StrIP = value; }
    }
    public string StrVista
    {
        get { return _StrVista; }
        set { _StrVista = value; }
    }
    public string StrDescControlador
    {
        get { return _StrDescControlador; }
        set { _StrDescControlador = value; }
    }
    public string StrDescModelo
    {
        get { return _StrDescModelo; }
        set { _StrDescModelo = value; }
    }
    public string StrDescModeloMetodo
    {
        get { return _StrDescModeloMetodo; }
        set { _StrDescModeloMetodo = value; }
    }
    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }
    public Empresa ObjEmpresa
    {
        get
        { return _objEmpresa; }
        set { _objEmpresa = value; }
    }
    public Usuario ObjUsuario
    {
        get { return _objUsuario; }
        set { _objUsuario = value; }
    }
    public MultiLista ObjMultiLista_Severidad
    {
        get { return _objMultiLista_Severidad; }
        set { _objMultiLista_Severidad = value; }
    }
    public MultiLista ObjMultiLista_TipoError
    {
        get { return _objMultiLista_TipoError; }
        set { _objMultiLista_TipoError = value; }
    }
    #endregion

    #region Constructor
    public RegistroError()
    {
    }

    /// <summary>
    /// Objeto que se envia a los STORED PROCEDURES
    /// </summary>
    /// <param name="ParamSql"></param>
    /// <param name="ParamStrDescModeloMetodo"></param>
    /// <param name="ParamStrDescModelo"></param>
    /// <param name="ParamStrDescControlador"></param>
    public RegistroError(SqlParameterCollection ParamSql, string ParamStrDescModeloMetodo, string ParamStrDescModelo, string ParamStrDescControlador)
    {
        this._StrDescStoredProcedureParametro = this.ObtenerParametros(ParamSql);
        this._StrIP = ObtenerIpLocal();
        this._StrDescNavegador = HttpContext.Current.Request.Browser.Type;
        this._StrVista = HttpContext.Current.Request.Url.AbsoluteUri;
        this._IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        this._IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        this._DateFechaInicio = DateTime.Now;
        this._StrDescModeloMetodo = ParamStrDescModeloMetodo;
        this._StrDescModelo = ParamStrDescModelo;
        this._StrDescControlador = ParamStrDescControlador;
        this._IntBActivo = 1;
    }

    /// <summary>
    /// Objeto que va y guarda directamente a la tabla
    /// </summary>
    /// <param name="ParamStrDescModeloMetodo"></param>
    /// <param name="ParamStrDescModelo"></param>
    /// <param name="ParamStrDescControlador"></param>
    /// <param name="ParamStrErrorVista"></param>
    /// <param name="ParamIntErrorCodigo"></param>
    /// <param name="ParamStrErrorMensaje"></param>
    /// <param name="ParamIntSeveridad"></param>
    public RegistroError(string ParamStrDescModeloMetodo, string ParamStrDescModelo, string ParamStrDescControlador, string ParamStrErrorVista, int ParamIntErrorCodigo, string ParamStrErrorMensaje, int ParamIntSeveridad)
    {
        this._StrIP = ObtenerIpLocal();
        this._StrDescNavegador = HttpContext.Current.Request.Browser.Type;
        this._StrVista = HttpContext.Current.Request.Url.AbsoluteUri;
        this._IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        this._IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        this._DateFechaInicio = DateTime.Now;
        this._StrDescModeloMetodo = ParamStrDescModeloMetodo;
        this._StrDescModelo = ParamStrDescModelo;
        this._StrDescControlador = ParamStrDescControlador;
        this._StrErrorVista = ParamStrErrorVista;
        this._IntBActivo = 1;
        this._IntErrorCodigo = ParamIntErrorCodigo;
        this._StrErrorMensaje = ParamStrErrorMensaje;
        this.DateFechaFin = DateTime.Now;
        this.IntIdMultilista_Severidad = ParamIntSeveridad;
        this._IntIdRegistroError = int.Parse(this.InsertarRegistroError());
    }

    public RegistroError(string ParamStrDescModeloMetodo, string ParamStrDescModelo, string ParamStrDescControlador, string ParamStrErrorVista, int ParamIntErrorCodigo, string ParamStrErrorMensaje, int ParamIntSeveridad, int paramIntIdEmpresa, int paramIntIdUsuario)
    {
        this._StrIP = ObtenerIpLocal();
        this._StrDescNavegador = HttpContext.Current.Request.Browser.Type;
        this._StrVista = HttpContext.Current.Request.Url.AbsoluteUri;
        this._IntIdEmpresa = paramIntIdEmpresa;
        this._IntIdUsuario = paramIntIdUsuario;
        this._DateFechaInicio = DateTime.Now;
        this._StrDescModeloMetodo = ParamStrDescModeloMetodo;
        this._StrDescModelo = ParamStrDescModelo;
        this._StrDescControlador = ParamStrDescControlador;
        this._StrErrorVista = ParamStrErrorVista;
        this._IntBActivo = 1;
        this._IntErrorCodigo = ParamIntErrorCodigo;
        this._StrErrorMensaje = ParamStrErrorMensaje;
        this.DateFechaFin = DateTime.Now;
        this.IntIdMultilista_Severidad = ParamIntSeveridad;
        this._IntIdRegistroError = int.Parse(this.InsertarRegistroError());
    }

	 /// <summary>
    /// Objeto que va y guarda directamente a la tabla
    /// </summary>
    /// <param name="ParamStrDescModeloMetodo"></param>
    /// <param name="ParamStrDescModelo"></param>
    /// <param name="ParamStrDescControlador"></param>
    /// <param name="ParamStrErrorVista"></param>
    /// <param name="ParamIntErrorCodigo"></param>
    /// <param name="ParamStrErrorMensaje"></param>
    /// <param name="ParamIntSeveridad"></param>
	/// <param name="ParamStrDescVista"></param>
    public RegistroError(string ParamStrDescModeloMetodo, string ParamStrDescModelo, string ParamStrDescControlador, string ParamStrErrorVista, int ParamIntErrorCodigo, string ParamStrErrorMensaje, int ParamIntSeveridad, string ParamStrDescVista)
    {
        this._StrIP = ObtenerIpLocal();
        this._StrDescNavegador = HttpContext.Current.Request.Browser.Type;
        this._StrVista = ParamStrDescVista;
        this._IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        this._IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        this._DateFechaInicio = DateTime.Now;
        this._StrDescModeloMetodo = ParamStrDescModeloMetodo;
        this._StrDescModelo = ParamStrDescModelo;
        this._StrDescControlador = ParamStrDescControlador;
        this._StrErrorVista = ParamStrErrorVista;
        this._IntBActivo = 1;
        this._IntErrorCodigo = ParamIntErrorCodigo;
        this._StrErrorMensaje = ParamStrErrorMensaje;
        this.DateFechaFin = DateTime.Now;
        this.IntIdMultilista_Severidad = ParamIntSeveridad;
        this._IntIdRegistroError = int.Parse(this.InsertarRegistroError());
    }
    #endregion

    #region Métodos
    public string ObtenerParametros(SqlParameterCollection ParamSql)
    {
        string StrParametros = String.Empty;
        foreach (SqlParameter FilaParametro in ParamSql)
        {
            var dataType = (FilaParametro.Value ?? "").GetType();

            Regex r = new Regex(@"\d{2}/\d{2}/\d{4}");
            if (r.IsMatch(FilaParametro.Value == null ? "" : FilaParametro.Value.ToString()) 
                &&  (
                        !(FilaParametro.Value.ToString().StartsWith("{") && FilaParametro.Value.ToString().EndsWith("}")) && 
                        !(FilaParametro.Value.ToString().StartsWith("[") && FilaParametro.Value.ToString().EndsWith("]"))
                    )//Y no es Json
                && (
                      dataType == typeof(DateTime)
                    )//Y no es DATE
                )


            {
                var date = DateTime.Parse(FilaParametro.Value.ToString());
                StrParametros += FilaParametro.ParameterName + "='" + date.ToString("yyyy-MM-dd HH:mm") + "',";
            }
            else
            {
                StrParametros += FilaParametro.ParameterName + "='" + FilaParametro.Value + "',";
            }
        }
        return StrParametros;
    }

    //if ((FilaParametro.Value.ToString().StartsWith("{") && FilaParametro.Value.ToString().EndsWith("}")) || //For object
    //           (FilaParametro.Value.ToString().StartsWith("[") && FilaParametro.Value.ToString().EndsWith("]"))) //For array
    //       {
    //           StrParametros += FilaParametro.ParameterName + "='" + FilaParametro.Value + "',";
    //       }

    public static string ObtenerIpLocal()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "";
    }

    public string InsertarRegistroError()
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "aplicacion.spRegistroErrorInsertar";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(this));
        sqlCommand.Parameters.AddWithValue("@p_TipoError", "CSHARP");
        sqlCommand.Parameters.AddWithValue("@p_Severidad", this.IntIdMultilista_Severidad);
        sqlCommand.Parameters.AddWithValue("@p_IdError", 0);
        DataSet dataSetInsertar = ConexionBD.EjecutarComandoEstadistica(sqlCommand);
        if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0)
        {
            return dataSetInsertar.Tables[0].Rows[0]["pError"].ToString();
        }
        else
        {
            return "0";
        }
    }

    public RespuestaDataTable<RegistroError> ResultadoDataTables(string filtros)
    {
        FiltrosDTRegistroError objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTRegistroError>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<RegistroError> objRegistroErrorListado = new List<RegistroError>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroErrorObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);
            sqlCommand.Parameters.AddWithValue("@p_IdRegistroError", objFiltroDt.IntIdRegistroError);
            if (objFiltroDt.IntIdEmpresa > 0)
            {
                sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", objFiltroDt.IntIdEmpresa);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", 0);
            }
            sqlCommand.Parameters.AddWithValue("@p_IdMultiLista_Severidad", objFiltroDt.IntIdSeveridad);
            sqlCommand.Parameters.AddWithValue("@p_IdMiltiLista_TipoError", objFiltroDt.IntIdTipoError);
            sqlCommand.Parameters.AddWithValue("@p_FechaInicio", DateTime.Parse(objFiltroDt.DateFechaInicio));
            sqlCommand.Parameters.AddWithValue("@p_FechaFin", DateTime.Parse(objFiltroDt.DateFechaFin).AddDays(1));
            sqlCommand.Parameters.AddWithValue("@p_Modelo", objFiltroDt.StrModelo);

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: RegistroError.cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaRegistroError in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    objRegistroErrorListado.Add(new RegistroError()
                    {
                        _IntIdRegistroError = int.Parse(filaRegistroError["idRegistroError"].ToString()),
                        _objEmpresa = new Empresa()
                        {
                            StrCveEmpresa = filaRegistroError["cveEmpresa"].ToString()
                        },
                        _objUsuario = new Usuario()
                        {
                            StrDescUsuario = filaRegistroError["descUsuario"].ToString()
                        },
                        _objMultiLista_Severidad = new MultiLista()
                        {
                            StrDescMultiLista = filaRegistroError["descMultiLista_Severidad"].ToString()
                        },
                        _objMultiLista_TipoError = new MultiLista()
                        {
                            StrDescMultiLista = filaRegistroError["descMultiLista_TipoError"].ToString()
                        },
                        _StrErrorMensaje = filaRegistroError["errorMensaje"].ToString(),
                        _StrErrorVista = filaRegistroError["errorVista"].ToString(),
                        _DateFechaInicio = DateTime.Parse(filaRegistroError["fechaInicio"].ToString()),
                        _DateFechaFin = DateTime.Parse(filaRegistroError["fechaFin"].ToString()),
                        _IntErrorCodigo = int.Parse(filaRegistroError["errorCodigo"].ToString()),
                        _IntErrorLinea = int.Parse(filaRegistroError["errorLinea"].ToString()),
                        _StrDescStoredProcedure = filaRegistroError["descStoredProcedure"].ToString(),
                        _StrDescStoredProcedureParametro = filaRegistroError["descStoredProcedureParametro"].ToString(),
                        _StrDescNavegador = filaRegistroError["descNavegador"].ToString(),
                        _StrIP = filaRegistroError["descIp"].ToString(),
                        _StrVista = filaRegistroError["descVista"].ToString(),
                        _StrDescControlador = filaRegistroError["descControlador"].ToString(),
                        _StrDescModelo = filaRegistroError["descModelo"].ToString(),
                        _StrDescModeloMetodo = filaRegistroError["descModeloMetodo"].ToString(),
                        _IntBActivo = int.Parse(filaRegistroError["bActivo"].ToString())
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception ex)
        {
        }
        return new RespuestaDataTable<RegistroError>
        {
            data = objRegistroErrorListado,
            recordsFiltered = IntTotalFilas,
            recordsTotal = 0,
            draw = objFiltroDt.draw
        };
    }
    #endregion
}

public class FiltrosDTRegistroError : DataTableAjaxPostModel
{
    public int IntIdRegistroError { get; set; }
    public int IntIdEmpresa { get; set; }
    public int IntIdSeveridad { get; set; }
    public int IntIdTipoError { get; set; }
    public string DateFechaInicio { get; set; }
    public string DateFechaFin { get; set; }
    public string StrModelo { get; set; }
}

public enum Severidad
{
    BAJA = 1,
    MEDIA = 2,
    ALTA = 3
}