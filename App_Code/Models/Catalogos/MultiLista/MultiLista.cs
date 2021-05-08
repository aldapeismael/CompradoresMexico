using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages.Html;

/// <summary>
/// Tabla con un catálogo Generico de listas que se usan en el sistema
/// </summary>

public class MultiLista : IMetodosModelos<MultiLista> //SE IMPLEMENTA LA CLASE GENERAL PARA HEREDAR SUS METODOS, ESTE METODO SE DEBE DE IMPLEMENTAR EN TODOS LOS MOEDLOS (AL MENOS EN LOS CATALOGOS)
{
    /*Definimos los atributos de la tabla, ESTOS ATRIBUTOS TIENE QUE SER LOS MISMOS DE LA BASE DE DATOS*/
    #region Atributos
    int _IntIdMultiLista;
    Guid _GuidMultiLista;
    string _StrCveMultiLista;
    string _StrObservacionMultiLista;
    string _StrDescMultiLista;
    string _StrValor1Char;
    decimal _DecValor1Num;
    string _StrValor2Char;
    decimal _DecValor2Num;
    private string _StrValor1Json;
    int _IntBActivo;
    List<MultiLista> _objMultiListaListado;
    RespuestaBD _objRespuestaDB;
	
	// Parámetros adicionales

    #region GetAndSet
    public int IntIdMultiLista
    {
        get { return _IntIdMultiLista; }
        set { _IntIdMultiLista = value; }
    }
    public Guid GuidMultiLista
    {
        get { return _GuidMultiLista; }
        set { _GuidMultiLista = value; }
    }
    public string StrCveMultiLista
    {
        get { return _StrCveMultiLista; }
        set { _StrCveMultiLista = value; }
    }

    public string StrObservacionMultiLista
    {
        get { return _StrObservacionMultiLista; }
        set { _StrObservacionMultiLista = value; }
    }

    public string StrDescMultiLista
    {
        get { return _StrDescMultiLista; }
        set { _StrDescMultiLista = value; }
    }
    public decimal DecValor1Num
    {
        get { return _DecValor1Num; }
        set { _DecValor1Num = value; }
    }
    public string StrValor1Char
    {
        get { return _StrValor1Char; }
        set { _StrValor1Char = value; }
    }
    public decimal DecValor2Num
    {
        get { return _DecValor2Num; }
        set { _DecValor2Num = value; }
    }
    public string StrValor2Char
    {
        get { return _StrValor2Char; }
        set { _StrValor2Char = value; }
    }
    public string StrValor1Json
    {
        get { return _StrValor1Json; }
        set { _StrValor1Json = value; }
    }
    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }
    public RespuestaBD ObjRespuestaDB
    {
        get { return _objRespuestaDB; }
        set { _objRespuestaDB = value; }
    }
    public List<MultiLista> objMultiListaListado
    {
        get
        {
            return _objMultiListaListado;
        }

        set
        {
            _objMultiListaListado = value;
        }
    }

	#endregion

	#endregion

	/*Se definen los constructores*/
	#region constructores
	public MultiLista() { }

    // Constructor por parámetros
    public MultiLista(int ParamIntIdMultiLista)
    {
        this._IntIdMultiLista = ParamIntIdMultiLista;
    }

    public MultiLista(int ParamIntIdMultiLista, string ParamStrCveMultiLista, string ParamStrDescMultiLista, decimal ParamDecValor1Num, string ParamStrValor1Char, decimal ParamDecValor2Num, string ParamStrValor2Char, int ParamBActivo)
    {
        this._IntIdMultiLista = ParamIntIdMultiLista;
        this._StrCveMultiLista = ParamStrCveMultiLista;
        this._StrDescMultiLista = ParamStrDescMultiLista;
        this._DecValor1Num = ParamDecValor1Num;
        this._StrValor1Char = ParamStrValor1Char;
        this._DecValor2Num = ParamDecValor2Num;
        this._StrValor2Char = ParamStrValor2Char;
        this._IntBActivo = ParamBActivo;
    }

    #endregion

    /*Definimos todos los métodos*/
    #region Métodos
    public static bool ObtenerOrigen(string StringOrigen, string CveMultilista)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "corporativo.spMultiListaObtenerLista";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
        sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", 8);
        sqlCommand.Parameters.AddWithValue("@p_CveMultiLista", CveMultilista);
        sqlCommand.Parameters.AddWithValue("@p_CveMultiListaChar", StringOrigen);

        RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "DocumentoVenta.cs", "DocumentoVentalista.cshtml");
        sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

        var dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: DocumentoVenta");

        bool Bool_IdOrigen = false;

        if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0 && dataSetObtenerLista.Tables[0].Rows.Count > 0)
        {
            Bool_IdOrigen = true;
        }

        return Bool_IdOrigen;
    }

    public RespuestaDataTable<MultiLista> ResultadoDataTables(string filtros)
    {
        FiltrosDTMultiLista objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTMultiLista>(filtros);
        int IntTotalFilasFitradas = 0;
        int IntTotalFilas = 0;
        List<MultiLista> MultiListaListado = new List<MultiLista>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMultiListaObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);
            sqlCommand.Parameters.AddWithValue("@p_CveMultiLista", objFiltroDt.StrCveMultiLista);
            sqlCommand.Parameters.AddWithValue("@p_DescMultiLista", objFiltroDt.StrDescMultiLista);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", objFiltroDt.IntBActivo);

            DataSet dataSetCatalogosSat = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : MultiLista.cs, metodo : ResultadoDataTables()");

            if (dataSetCatalogosSat != null && dataSetCatalogosSat.Tables.Count > 0 && dataSetCatalogosSat.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in dataSetCatalogosSat.Tables[0].Rows)
                {
                    MultiListaListado.Add(new MultiLista()
                    {
                        IntIdMultiLista = int.Parse(fila["idMultiLista"].ToString()),
                        StrCveMultiLista = fila["cveMultiLista"].ToString(),
                        StrDescMultiLista = fila["descMultiLista"].ToString(),
                        DecValor1Num = decimal.Parse(fila["valor1num"].ToString()),
                        StrValor1Char = fila["valor1char"].ToString(),
                        DecValor2Num = decimal.Parse(fila["valor2num"].ToString()),
                        StrValor2Char = fila["valor2char"].ToString(),
                        StrValor1Json = fila["valor1json"].ToString(),
                        IntBActivo = int.Parse(fila["bActivo"].ToString())
                    });
                }

                IntTotalFilasFitradas = int.Parse(dataSetCatalogosSat.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetCatalogosSat.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return new RespuestaDataTable<MultiLista>
        {
            data = MultiListaListado,
            recordsFiltered = IntTotalFilasFitradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    public RespuestaBD Insertar()
    {
        return null;
    }

    public bool ObtenerPorId()
    {
        bool bool_Valido = false;
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "catalogos.spDivisionObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_CveMultiLista", this._StrCveMultiLista);
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo : Division.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this._IntIdMultiLista = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMultiLista"].ToString());
                this._StrCveMultiLista = dataSetObtener.Tables[0].Rows[0]["cveMultiLista"].ToString();
                this._StrDescMultiLista = dataSetObtener.Tables[0].Rows[0]["descMultiLista"].ToString();
                this._DecValor1Num = decimal.Parse(dataSetObtener.Tables[0].Rows[0]["valor1num"].ToString());
                this._StrValor1Char = dataSetObtener.Tables[0].Rows[0]["valor1char"].ToString();
                this._DecValor2Num = decimal.Parse(dataSetObtener.Tables[0].Rows[0]["valor2num"].ToString());
                this._StrValor2Char = dataSetObtener.Tables[0].Rows[0]["valor2char"].ToString();
                this._StrValor1Json = dataSetObtener.Tables[0].Rows[0]["valor1json"].ToString();
                this.IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this._objRespuestaDB = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
        return bool_Valido;
    }

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMultiListaActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_MultiListaListado", JsonConvert.SerializeObject(this._objMultiListaListado));
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo : MultiLista.cs, metodo : Actualizar()");
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
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        return null;
    }

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamStrCveMultiLista)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        int IntBRobot = VariableGlobal.SessionIntBRobot;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMultiListaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            sqlCommand.Parameters.AddWithValue("@p_CveMultiLista", ParamStrCveMultiLista);
            sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBRobot);
			
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "Obtener Lista: MultiLista");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }

	 public static IEnumerable<dynamic> ObtenerListaChar(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamStrCveMultiLista, string ParamStrCveMultiListaChar)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMultiListaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            sqlCommand.Parameters.AddWithValue("@p_CveMultiLista", ParamStrCveMultiLista);
			sqlCommand.Parameters.AddWithValue("@p_CveMultiListaChar", ParamStrCveMultiListaChar);
			
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "Obtener Lista: MultiLista");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }

    public static List<MultiLista> ObtenerListaAnidada(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamStrCveMultiLista)
    {
        switch (ParamTipoBusqueda)
        {
            case 1:
                return MultiLista.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro, ParamStrCveMultiLista).Select(s => new MultiLista()
                {
                    IntIdMultiLista = int.Parse(s.idMultiLista.ToString()),
                    StrCveMultiLista = s.cveMultiLista.ToString(),
                    StrDescMultiLista = s.descMultiLista.ToString()
                }).ToList();
            default:
                return MultiLista.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro, ParamStrCveMultiLista).Select(s => new MultiLista() { }).ToList();
        }
    }

	 public static IEnumerable<dynamic> ObtenerListaParametros(int ParamIdParametro, string ParamCveParametro, int ParamValor1num, int ParamValor2num, string ParamValor1char, string ParamValor2char, int ParamBActivo)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spParametroLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdParametro",	ParamIdParametro);
            sqlCommand.Parameters.AddWithValue("@p_CveParametro",	ParamCveParametro);
            sqlCommand.Parameters.AddWithValue("@p_Valor1num",		ParamValor1num);
			sqlCommand.Parameters.AddWithValue("@p_Valor1char",		ParamValor1char);
			sqlCommand.Parameters.AddWithValue("@p_Valor2num",		ParamValor2num);
			sqlCommand.Parameters.AddWithValue("@p_Valor2char",		ParamValor2char);
			sqlCommand.Parameters.AddWithValue("@p_BActivo",		ParamBActivo);
			
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerLista = ConexionBD.EjecutarComandoExterno(sqlCommand, "Obtener ListaParametro: MultiLista");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Multilista.cs", "MultilistaController.cs", "Error al tratar de obtener la lista de parametros, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }
    #endregion



    #region Métodos Grid
    public void ObtenerPorClave(string ParamStrCveMultiLista)
    {
        _objMultiListaListado = new List<MultiLista>();
        try
        {
            _objMultiListaListado = ObtenerLista(3,0, ParamStrCveMultiLista).Select(cm=>new MultiLista()
            {
                _IntIdMultiLista    = int.Parse(cm.idMultiLista.ToString()),
                _StrCveMultiLista   = cm.cveMultiLista.ToString(),
                _StrDescMultiLista  = cm.descMultiLista.ToString(),
                _DecValor1Num       = decimal.Parse(cm.valor1num.ToString()),		
                _StrValor1Char      = cm.valor1char.ToString(),		
                _DecValor2Num       = decimal.Parse(cm.valor2num.ToString()),		
                _StrValor2Char      = cm.valor2char.ToString(),		
                _StrValor1Json      = cm.valor1json.ToString(),		
                _IntBActivo         = int.Parse(cm.bActivo.ToString()),
                _StrObservacionMultiLista = cm.observacion.ToString()
            }).ToList();
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this._objRespuestaDB = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
    }
    #endregion
}

public class FiltrosDTMultiLista : DataTableAjaxPostModel
{
    public string StrCveMultiLista { get; set; }
    public string StrDescMultiLista { get; set; }
    public int IntBActivo { get; set; }
}

