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
/// Summary description for MeGusta
/// </summary>
public class MeGusta : IMetodosModelos<MeGusta>
{
    #region variables

    int _IntIdMeGusta;
    Guid _GuidGuidMeGusta;
    int _IntIdPublicacion;
    DateTime _DtFechaAlta;
    string _StrCvePublicacion;
    string _StrFechaPublicacion;
    string _StrCveCompradorPublicacion;
    string _StrDescripcion;
    string _StrImagen1;
    string _StrCveCategoria;
    int _IntBActivo;
    decimal _DecPresupuesto;

    public int IntIdMeGusta
    {
        get { return _IntIdMeGusta; }
        set { _IntIdMeGusta = value; }
    }
    public Guid GuidGuidMeGusta
    {
        get { return _GuidGuidMeGusta; }
        set { _GuidGuidMeGusta = value; }
    }

    public DateTime DtFechaAlta
    {
        get
        {
            return _DtFechaAlta;
        }

        set
        {
            _DtFechaAlta = value;
        }
    }

    public int IntIdPublicacion
    {
        get
        {
            return _IntIdPublicacion;
        }

        set
        {
            _IntIdPublicacion = value;
        }
    }

    public int IntBActivo
    {
        get
        {
            return _IntBActivo;
        }

        set
        {
            _IntBActivo = value;
        }
    }

    public string StrCvePublicacion
    {
        get
        {
            return _StrCvePublicacion;
        }

        set
        {
            _StrCvePublicacion = value;
        }
    }

    public string StrFechaPublicacion
    {
        get
        {
            return _StrFechaPublicacion;
        }

        set
        {
            _StrFechaPublicacion = value;
        }
    }

    public string StrCveCompradorPublicacion
    {
        get
        {
            return _StrCveCompradorPublicacion;
        }

        set
        {
            _StrCveCompradorPublicacion = value;
        }
    }

    public string StrDescripcion
    {
        get
        {
            return _StrDescripcion;
        }

        set
        {
            _StrDescripcion = value;
        }
    }

    public decimal DecPresupuesto
    {
        get
        {
            return _DecPresupuesto;
        }

        set
        {
            _DecPresupuesto = value;
        }
    }

    public string StrImagen1
    {
        get
        {
            return _StrImagen1;
        }

        set
        {
            _StrImagen1 = value;
        }
    }

    public string StrCveCategoria
    {
        get
        {
            return _StrCveCategoria;
        }

        set
        {
            _StrCveCategoria = value;
        }
    }

    #endregion

    #region Constructores

    public MeGusta()
    {
    }

    public MeGusta(int ParamIntIdMeGusta)
    {
        this.IntIdMeGusta = ParamIntIdMeGusta;
    }

    #endregion

    #region Metodos

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMeGustaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: MeGusta");
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

    /// <summary>
    /// Obtener la Lista generica de MeGustas
    /// </summary>
    /// <param name="ParamObjeto"></param>
    /// <returns></returns>
    public List<MeGusta> ObtenerListaGenerica(ObjetoBusqueda ParamObjeto)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var IntTipoUsuario = VariableGlobal.SessionIntTipoUsuario;
        List<MeGusta> lstObjMeGusta = new List<MeGusta>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spMeGusta";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", ParamObjeto.IntEjecuta);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_TipoUsuario", IntTipoUsuario);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaMeGusta in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjMeGusta.Add(new MeGusta
                    {
                        IntIdMeGusta = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idMeGusta") ? FilaMeGusta["idMeGusta"].ToString() : "0"),
                        IntIdPublicacion = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idPublicacion") ? FilaMeGusta["idPublicacion"].ToString() : "0"),
                        StrDescripcion = dataSetObtenerDataTable.Tables[0].Columns.Contains("descripcion") ? FilaMeGusta["descripcion"].ToString() : "",
                        StrImagen1 = dataSetObtenerDataTable.Tables[0].Columns.Contains("imagen1") ? FilaMeGusta["imagen1"].ToString() : "",
                        StrCveCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveCategoria") ? FilaMeGusta["cveCategoria"].ToString() : "",
                        DecPresupuesto = decimal.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("presupuesto") ? FilaMeGusta["presupuesto"].ToString() : "0"),
                        DtFechaAlta = DateTime.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("fechaAlta") ? FilaMeGusta["fechaAlta"].ToString() : "01/01/0001")
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de MeGusta, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return lstObjMeGusta;
    }

    public RespuestaDataTable<MeGusta> ResultadoDataTables(string filtros)
    {
        FiltrosDTMeGusta filtrodt = JsonConvert.DeserializeObject<FiltrosDTMeGusta>(filtros);
        int IntTotalFilasFitradas = 0;
        int IntTotalFilas = 0;
        List<MeGusta> MeGusta = new List<MeGusta>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spMeGusta";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", filtrodt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", filtrodt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", filtrodt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", filtrodt.length);
            sqlCommand.Parameters.AddWithValue("@p_Origen", filtrodt.StrOrigen);

            if (!string.IsNullOrEmpty(filtrodt.SrtBActivoFiltro))
                sqlCommand.Parameters.AddWithValue("@p_Buscar", filtrodt.SrtBActivoFiltro);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : tipoCambio.cs, metodo : ResultadoDataTables()");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow filaTipoCambio in dataSetObtener.Tables[0].Rows)
                {

                    MeGusta.Add(new MeGusta() {
                        IntIdPublicacion = int.Parse(filaTipoCambio["idMeGusta"].ToString()),
                        StrCveCompradorPublicacion = filaTipoCambio["cveCompradorPublicacion"].ToString(),
                        StrFechaPublicacion = filaTipoCambio["fechaPublicacion"].ToString(),
                        StrCvePublicacion = filaTipoCambio["cvePublicacion"].ToString(),
                    });
                }

                IntTotalFilasFitradas = int.Parse(dataSetObtener.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtener.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {

        }

        return new RespuestaDataTable<MeGusta>
        {

            data = MeGusta,
            recordsFiltered = IntTotalFilasFitradas,
            recordsTotal = IntTotalFilas,
            draw = filtrodt.draw
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
            sqlCommand.CommandText = "dbo.spMeGusta";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", this.IntIdPublicacion);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: MeGusta.cs => Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0 && dataSetInsertar.Tables[0].Rows.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                   short.Parse(dataSetInsertar.Tables[0].Rows[0]["Error"].ToString()),
                   dataSetInsertar.Tables[0].Rows[0]["MensajeError"].ToString(),
                   dataSetInsertar.Tables[0].Rows[0]["TipoError"].ToString(),
                   int.Parse(dataSetInsertar.Tables[0].Rows[0]["IdRespuesta"].ToString())
               );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }
        }
        catch (Exception ex)
        {
            objRespuestaBD = new RespuestaBD(1, ex.ToString(), "error");
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
            sqlCommand.CommandText = "corporativo.spMeGustaObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_idMeGusta", this.IntIdMeGusta);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: MeGusta.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdMeGusta = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMeGusta"].ToString());
                this.IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());
            }
        }
        catch (Exception ex)
        {
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
            sqlCommand.CommandText = "corporativo.spMeGustaActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdMeGusta", this.IntIdMeGusta);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            DataSet datasetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: MeGusta.cs, metodo => Actualizar()");
            if (datasetActualizar != null && datasetActualizar.Tables.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                   short.Parse(datasetActualizar.Tables[0].Rows[0]["Error"].ToString()),
                   datasetActualizar.Tables[0].Rows[0]["MensajeError"].ToString(),
                   datasetActualizar.Tables[0].Rows[0]["TipoError"].ToString()
                );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }
        }
        catch (Exception ex)
        {
            objRespuestaBD = new RespuestaBD(1, ex.ToString(), "error");
        }
        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    #endregion
}

public class FiltrosDTMeGusta : DataTableAjaxPostModel
{
    public string SrtBActivoFiltro { get; set; }
    public string StrOrigen { get; set; }
}