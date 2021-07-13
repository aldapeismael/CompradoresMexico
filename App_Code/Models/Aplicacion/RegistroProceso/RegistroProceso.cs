using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de RegistroProceso
/// </summary>
public class RegistroProceso : IMetodosModelos<RegistroProceso>
{
    #region Propiedades
    int _IntIdRegistroProceso;
    int _IntIdEmpresa;
    int _IntMultilista_TipoRegistroProceso;
    int _IntIdUsuario;
    DateTime _DateFecha;
    int _IntIdReferencia1;
    string _StrCveReferencia1;
    int _IntMultilista_TipoAccion;
    string _StrDescAccion;
    MultiLista _objMultiLista_TipoAccion;
    Usuario _objUsuario;
    string _StrDescTipoRegistroProceso;


    public int IntIdRegistroProceso
    {
        get { return _IntIdRegistroProceso; }
        set { _IntIdRegistroProceso = value; }
    }
    public int IntIdEmpresa
    {
        get { return _IntIdEmpresa; }
        set { _IntIdEmpresa = value; }
    }
    public int IntMultilista_TipoRegistroProceso
    {
        get { return _IntMultilista_TipoRegistroProceso; }
        set { _IntMultilista_TipoRegistroProceso = value; }
    }
    public int IntIdUsuario
    {
        get { return _IntIdUsuario; }
        set { _IntIdUsuario = value; }
    }
    public DateTime DateFecha
    {
        get { return _DateFecha; }
        set { _DateFecha = value; }
    }
    public int IntIdReferencia1
    {
        get { return _IntIdReferencia1; }
        set { _IntIdReferencia1 = value; }
    }
    public string StrCveReferencia1
    {
        get { return _StrCveReferencia1; }
        set { _StrCveReferencia1 = value; }
    }
    public int IntMultilista_TipoAccion
    {
        get { return _IntMultilista_TipoAccion; }
        set { _IntMultilista_TipoAccion = value; }
    }
    public string StrDescAccion
    {
        get { return _StrDescAccion; }
        set { _StrDescAccion = value; }
    }
    public MultiLista ObjMultiLista_TipoAccion
    {
        get { return _objMultiLista_TipoAccion; }
        set { _objMultiLista_TipoAccion = value; }
    }
    public Usuario ObjUsuario
    {
        get { return _objUsuario; }
        set { _objUsuario = value; }
    }

    public string StrDescTipoRegistroProceso
    {
        get
        {
            return _StrDescTipoRegistroProceso;
        }

        set
        {
            _StrDescTipoRegistroProceso = value;
        }
    }

    #endregion

    #region Constructor
    public RegistroProceso()
    {
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


    public RespuestaDataTable<RegistroProceso> ResultadoDataTables(string filtros)
    {
        FiltrosDTRegistroProceso objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTRegistroProceso>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<RegistroProceso> objRegistroProcesoListado = new List<RegistroProceso>();
        DateTime dtInicio = DateTime.Parse(objFiltroDt.DateFechaInicio);
        DateTime dtFin = DateTime.Parse(objFiltroDt.DateFechaFin);
        dtInicio = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, 0, 0, 0);
        dtFin = new DateTime(dtFin.Year, dtFin.Month, dtFin.Day, 23, 59, 59);

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroProcesoObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", VariableGlobal.SessionIntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_IdReferencia", objFiltroDt.IntIdRegistroProceso);
            sqlCommand.Parameters.AddWithValue("@p_MultiLista_CveTipoRegistroProceso", objFiltroDt.StrCveTipoRegistroProceso);
			sqlCommand.Parameters.AddWithValue("@p_MultiLista_ValorChar1", objFiltroDt.StrValor1Char);
            sqlCommand.Parameters.AddWithValue("@p_DescAccion", objFiltroDt.StrDescAccion);
            sqlCommand.Parameters.AddWithValue("@p_TipoAccion", objFiltroDt.IntTipoAccion);
            sqlCommand.Parameters.AddWithValue("@p_FechaInicio", dtInicio);
            sqlCommand.Parameters.AddWithValue("@p_FechaFin", dtFin.AddDays(1));
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: RegistroProceso.cs, metodo: ResultadoDataTables()");

            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaRegistroProceso in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    objRegistroProcesoListado.Add(new RegistroProceso()
                    {
                        _IntIdRegistroProceso = int.Parse(filaRegistroProceso["idRegistroProceso"].ToString()),
                        _DateFecha = DateTime.Parse(filaRegistroProceso["fecha"].ToString()),
                        _objUsuario = new Usuario()
                        {
                            StrDescUsuario = filaRegistroProceso["usuario"].ToString()
                        },
                        _objMultiLista_TipoAccion = new MultiLista()
                        {
                            StrDescMultiLista = filaRegistroProceso["descTipoAccion"].ToString()
                        },
                        _StrDescAccion = filaRegistroProceso["descAccion"].ToString(),
                        _StrDescTipoRegistroProceso = filaRegistroProceso["descTipoProceso"].ToString()
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return new RespuestaDataTable<RegistroProceso>
        {
            data = objRegistroProcesoListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }
    #endregion
}

public class FiltrosDTRegistroProceso : DataTableAjaxPostModel
{
    public int IntIdRegistroProceso { get; set; }
    public string StrCveTipoRegistroProceso { get; set; }
    public string StrDescAccion { get; set; }
	public string StrValor1Char { get; set; }
    public string IntTipoAccion { get; set; }
    public string DateFechaInicio { get; set; }
    public string DateFechaFin { get; set; }
}