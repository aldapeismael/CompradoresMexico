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
/// Descripción breve de RegistroAcceso
/// </summary>
public class RegistroAcceso: IMetodosModelos<RegistroAcceso>
{
    #region Propiedades
    string _StrJsonRegistroAcceso;

    public string StrJsonRegistroAcceso
    {
        get
        {
            return _StrJsonRegistroAcceso;
        }

        set
        {
            _StrJsonRegistroAcceso = value;
        }
    }

    #endregion

    #region Contructores
    public RegistroAcceso()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    #endregion

    #region Métodos

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
        throw new NotImplementedException();
    }

    public bool ObtenerPorId()
    {
        throw new NotImplementedException();
    }

    public RespuestaDataTable<RegistroAcceso> ResultadoDataTables(string filtros)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroAccesoObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);

            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: RegistroAcceso -> ObtenerLista()");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Area.cs", "AreaController.cs", "Error al tratar de obtener la lista de creditos, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }

    public List<RegistroAcceso> ObtenerRegistroAccesoListado(string filtros)
    {
        FiltrosRegitroAcceso objFiltro = JsonConvert.DeserializeObject<FiltrosRegitroAcceso>(filtros);
        List<RegistroAcceso> objRegistroAccesoListado = new List<RegistroAcceso>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroAccesoObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_FechaInicial", objFiltro.DtFechaInicial);
            sqlCommand.Parameters.AddWithValue("@p_FechaFinal", objFiltro.DtFechaFinal);
            sqlCommand.Parameters.AddWithValue("@p_IdPagina", objFiltro.IntIdPagina);
            sqlCommand.Parameters.AddWithValue("@p_IdTipoReporte", objFiltro.IntTipoReporte);
            sqlCommand.Parameters.AddWithValue("@p_BError", objFiltro.IntBError);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: RegistroAcceso.cs, metodo: ObtenerRegistroAccesoListado()");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtener.Tables[0].Rows)
                {
                    objRegistroAccesoListado.Add(new RegistroAcceso()
                    {
                        StrJsonRegistroAcceso = fila["jsonRegistroAcceso"].ToString(),
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objRegistroAccesoListado;

    }
    #endregion
}

public class FiltrosRegitroAcceso
{
    public DateTime DtFechaInicial { get; set; }
    public DateTime DtFechaFinal { get; set; }
    public int IntTipoReporte { get; set; }
    public int IntIdPagina { get; set; }
    public int IntBError { get; set; }
}