using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages.Html;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Accion
/// </summary>
public class Accion : IMetodosModelos<Accion>
{
    #region Propiedades

    private int _IntIdAccion;
    private int _IntIdPaginaAccion;
    private string _StrDescAccion;
    private string _StrCveAccion;
    private string _StrObjetoFuncion;
    private string _StrObjetoNombre;
    private string _StrObjetoId;
    private string _StrObjetoClase;
    private string _StrObjetoIcono;
    private int _IntBActivo;
    private string _StrJsonPaginaAccion;
    private int _IntBMovimientoEspecial;

    #region Getter y Setter

    public string StrJsonPaginaAccion
    {
        get { return _StrJsonPaginaAccion; }
        set { _StrJsonPaginaAccion = value; }
    }

    public int IntIdAccion
    {
        get { return _IntIdAccion; }
        set { _IntIdAccion = value; }
    }

    public string StrDescAccion
    {
        get { return _StrDescAccion; }
        set { _StrDescAccion = value; }
    }

    public string StrCveAccion
    {
        get { return _StrCveAccion; }
        set { _StrCveAccion = value; }
    }

    public string StrObjetoFuncion
    {
        get { return _StrObjetoFuncion; }
        set { _StrObjetoFuncion = value; }
    }

    public string StrObjetoNombre
    {
        get { return _StrObjetoNombre; }
        set { _StrObjetoNombre = value; }
    }

    public string StrObjetoId
    {
        get { return _StrObjetoId; }
        set { _StrObjetoId = value; }
    }

    public string StrObjetoClase
    {
        get { return _StrObjetoClase; }
        set { _StrObjetoClase = value; }
    }

    public string StrObjetoIcono
    {
        get { return _StrObjetoIcono; }
        set { _StrObjetoIcono = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }
    
    public int IntIdPaginaAccion
    {
        get { return _IntIdPaginaAccion; }
        set { _IntIdPaginaAccion = value; }
    }

    public int IntBMovimientoEspecial
    {
        get
        {
            return _IntBMovimientoEspecial;
        }

        set
        {
            _IntBMovimientoEspecial = value;
        }
    }
    #endregion


    #endregion
    #region Contructores

    public Accion()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Accion(int intIdAccion)
    {
        _IntIdAccion = intIdAccion;
    }

    /// <summary>
    /// Contructor con todos los parametros
    /// </summary>
    /// <param name="intIdAccion">id</param>
    /// <param name="strDescAccion">nombre para el boton</param>
    /// <param name="strObjetoFuncion">funcion a la que se llama</param>
    /// <param name="strObjetoNombre">nombre o id en html</param>
    /// <param name="intBActivo">bandera de activo</param>
    public Accion(int intIdAccion, string strDescAccion, string strObjetoFuncion, string strObjetoNombre, int intBActivo)
    {
        _IntIdAccion = intIdAccion;
        _StrDescAccion = strDescAccion;
        _StrObjetoFuncion = strObjetoFuncion;
        _StrObjetoNombre = strObjetoNombre;
        _IntBActivo = intBActivo;
    }

    #endregion

    #region Metodos

    public RespuestaDataTable<Accion> ResultadoDataTables(string filtros)
    {
        FiltrosDTAccion objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTAccion>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<Accion> aplicacionCompListado = new List<Accion>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);

            if (objFiltroDt.IntActivo >= 0)
                sqlCommand.Parameters.AddWithValue("@p_Activo", objFiltroDt.IntActivo);

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    aplicacionCompListado.Add(new Accion()
                    {
                        IntIdAccion = int.Parse(fila["id"].ToString()),
                        StrDescAccion = fila["descAccion"].ToString(),
                        StrObjetoNombre = fila["objetoNombre"].ToString(),
                        IntBActivo = int.Parse(fila["bActivo"].ToString())
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception ex)
        {
            // Crear tabla para agregar excepciones
        }

        return new RespuestaDataTable<Accion>
        {
            data = aplicacionCompListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_CveAccion", this.StrCveAccion);
            sqlCommand.Parameters.AddWithValue("@p_DescAccion", this.StrDescAccion);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoFuncion", this.StrObjetoFuncion);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoNombre", this.StrObjetoNombre);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoId", this.StrObjetoId);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoClase", this.StrObjetoClase);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoIcono", this.StrObjetoIcono);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Accion.cs", "Accion.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs => Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0 && dataSetInsertar.Tables[0].Rows.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetInsertar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetInsertar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetInsertar.Tables[0].Rows[0]["TipoError"].ToString()
                );
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
        return objRespuestaBD;
    }

    public bool ObtenerPorId()
    {
        bool bool_Valido = false;
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var intIdUsuario = VariableGlobal.SessionIntIdUsuario;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_IdAccion", this.IntIdAccion);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this._IntIdAccion = int.Parse(dataSetObtener.Tables[0].Rows[0]["idAccion"].ToString());
                this._StrDescAccion = dataSetObtener.Tables[0].Rows[0]["descAccion"].ToString();
                this._StrCveAccion = dataSetObtener.Tables[0].Rows[0]["cveAccionReferencia"].ToString();
                this._StrObjetoFuncion = dataSetObtener.Tables[0].Rows[0]["objetoFuncion"].ToString();
                this._StrObjetoNombre = dataSetObtener.Tables[0].Rows[0]["objetoNombre"].ToString();
                this._StrObjetoId = dataSetObtener.Tables[0].Rows[0]["ObjetoId"].ToString();
                this._StrObjetoClase = dataSetObtener.Tables[0].Rows[0]["ObjetoClase"].ToString();
                this._StrObjetoIcono = dataSetObtener.Tables[0].Rows[0]["ObjetoIcono"].ToString();
                this._IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());
                bool_Valido = true;
            }
        }
        catch (Exception e)
        {
            // Agregar excepcion a tabla
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
            sqlCommand.CommandText = "corporativo.spAccionActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdAccion", this.IntIdAccion);
            sqlCommand.Parameters.AddWithValue("@p_CveAccion", this.StrCveAccion);
            sqlCommand.Parameters.AddWithValue("@p_DescAccion", this.StrDescAccion);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoFuncion", this.StrObjetoFuncion);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoNombre", this.StrObjetoNombre);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoId", this.StrObjetoId);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoClase", this.StrObjetoClase);
            sqlCommand.Parameters.AddWithValue("@p_ObjetoIcono", this.StrObjetoIcono);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Accion.cs", "Accion.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo : Actualizar()");

            if (dataSetActualizar != null && dataSetActualizar.Tables.Count > 0 && dataSetActualizar.Tables[0].Rows.Count > 0)
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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        int IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        int IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionEliminar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdAccion", this.IntIdAccion);

            DataSet dataSetEliminar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo : Eliminar()");

            if (dataSetEliminar != null && dataSetEliminar.Tables.Count > 0 && dataSetEliminar.Tables[0].Rows.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetEliminar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetEliminar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetEliminar.Tables[0].Rows[0]["TipoError"].ToString()
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

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Accion.cs metodo: ObtenerLista()");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception ex)
        {
        }
        return objLista;
    }

    public static List<Accion> ObtenerListaAnidada(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        switch (ParamTipoBusqueda)
        {
            case 1:
                return Accion.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro).Select(s => new Accion()
                {
                    IntIdAccion = int.Parse(s.idAccion.ToString()),
                    StrDescAccion = s.descAccion.ToString(),
                    IntBMovimientoEspecial = int.Parse(s.bMovimientoEspecial.ToString())
                }).ToList();
            default:
                return Accion.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro).Select(s => new Accion() { }).ToList();
        }
    }

    #endregion
}

public class FiltrosDTAccion : DataTableAjaxPostModel
{
    public int IntActivo { get; set; }
}