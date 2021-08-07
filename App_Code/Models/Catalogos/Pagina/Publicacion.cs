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
/// Summary description for Publicacion
/// </summary>
public class Publicacion: IMetodosModelos<Publicacion>
{
    public const string StrUrlArchivoPublicacion = @"CompradoresMexico\Views\Publicacion\PublicacionImagen\";
    #region variables

    int _IntIdPublicacion;
    int _IntIdCategoria;
    int _IntTipoVigencia;
    Guid _GuidGuidPublicacion;
    int _IntIdMegusta;
    string _StrDescripcion;
    string _StrNombreArchivo;
    string _StrCveComprador;
    string _StrTelefono;
    string _StrCorreo;
    string _StrCveCategoria;
    string _StrDescCategoria;
    string _StrDescEstado;
    string _StrCveUsuario;
    string _StrImagen;
    decimal _DecPresupuesto;
    DateTime _DtFechaAlta;
    int _IntBActivo;
    int _IntIdChat;
    int _IntIdComprador;
    int _IntIdUsuario;

    public int IntIdPublicacion
    {
        get { return _IntIdPublicacion; }
        set { _IntIdPublicacion = value; }
    }
    public Guid GuidGuidPublicacion
    {
        get { return _GuidGuidPublicacion; }
        set { _GuidGuidPublicacion = value; }
    }
    public string StrDescripcion
    {
        get { return _StrDescripcion; }
        set { _StrDescripcion = value; }
    }
    public string StrCveComprador
    {
        get { return _StrCveComprador; }
        set { _StrCveComprador = value; }
    }
    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }
    public string StrTelefono
    {
        get
        {
            return _StrTelefono;
        }

        set
        {
            _StrTelefono = value;
        }
    }
    public string StrCorreo
    {
        get
        {
            return _StrCorreo;
        }

        set
        {
            _StrCorreo = value;
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
    public string StrDescCategoria
    {
        get
        {
            return _StrDescCategoria;
        }

        set
        {
            _StrDescCategoria = value;
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

    public int IntIdCategoria
    {
        get
        {
            return _IntIdCategoria;
        }

        set
        {
            _IntIdCategoria = value;
        }
    }

    public string StrNombreArchivo
    {
        get
        {
            return _StrNombreArchivo;
        }

        set
        {
            _StrNombreArchivo = value;
        }
    }

    public int IntIdComprador
    {
        get
        {
            return _IntIdComprador;
        }

        set
        {
            _IntIdComprador = value;
        }
    }

    public int IntIdUsuario
    {
        get
        {
            return _IntIdUsuario;
        }

        set
        {
            _IntIdUsuario = value;
        }
    }

    public int IntIdChat
    {
        get
        {
            return _IntIdChat;
        }

        set
        {
            _IntIdChat = value;
        }
    }

    public string StrCveUsuario
    {
        get
        {
            return _StrCveUsuario;
        }

        set
        {
            _StrCveUsuario = value;
        }
    }

    public int IntIdMegusta
    {
        get
        {
            return _IntIdMegusta;
        }

        set
        {
            _IntIdMegusta = value;
        }
    }

    public int IntTipoVigencia
    {
        get
        {
            return _IntTipoVigencia;
        }

        set
        {
            _IntTipoVigencia = value;
        }
    }

    public string StrDescEstado
    {
        get
        {
            return _StrDescEstado;
        }

        set
        {
            _StrDescEstado = value;
        }
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

    #region Constructores

    public Publicacion()
    {
    }

    public Publicacion(int ParamIntIdPublicacion)
    {
        this.IntIdPublicacion = ParamIntIdPublicacion;
    }

    public Publicacion(int ParamIntIdPublicacion, string ParamStrDescripcion, string ParamStrCveComprador, int ParamIntBActivo)
    {
        this.IntIdPublicacion = ParamIntIdPublicacion;
        this.StrDescripcion = ParamStrDescripcion;
        this.StrCveComprador = ParamStrCveComprador;
        this.IntBActivo = ParamIntBActivo;
    }


    #endregion

    #region Metodos

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPublicacionObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Publicacion");
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
    /// Obtener la Lista generica de Publicacions
    /// </summary>
    /// <param name="ParamObjeto"></param>
    /// <returns></returns>
    public List<Publicacion> ObtenerListaGenerica(ObjetoBusqueda ParamObjeto)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var intTipoUsuario = VariableGlobal.SessionIntTipoUsuario;

        List<Publicacion> lstObjPublicacion = new List<Publicacion>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spPublicacion";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", ParamObjeto.IntEjecuta);   
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", ParamObjeto.IntId1);
            sqlCommand.Parameters.AddWithValue("@p_IdGenerico2", ParamObjeto.IntId2);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico1", ParamObjeto.StrDes1);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico2", ParamObjeto.StrDes2);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_TipoUsuario", intTipoUsuario);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaPublicacion in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjPublicacion.Add(new Publicacion
                    {
                        IntIdPublicacion = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idPublicacion") ? FilaPublicacion["idPublicacion"].ToString() : "0"),
                        IntIdComprador = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idComprador") ? FilaPublicacion["idComprador"].ToString() : "0"),
                        DecPresupuesto = decimal.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("presupuesto") ? FilaPublicacion["presupuesto"].ToString() : "0"),
                        StrDescripcion = dataSetObtenerDataTable.Tables[0].Columns.Contains("descripcion") ? FilaPublicacion["descripcion"].ToString() : "",
                        StrCveComprador = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveComprador") ? FilaPublicacion["cveComprador"].ToString() : "",
                        StrTelefono = dataSetObtenerDataTable.Tables[0].Columns.Contains("telefono") ? FilaPublicacion["telefono"].ToString() : "",
                        StrCorreo = dataSetObtenerDataTable.Tables[0].Columns.Contains("correo") ? FilaPublicacion["correo"].ToString() : "",
                        StrCveCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveCategoria") ? FilaPublicacion["cveCategoria"].ToString() : "",
                        StrDescCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("descCategoria") ? FilaPublicacion["descCategoria"].ToString() : "",
                        StrDescEstado = dataSetObtenerDataTable.Tables[0].Columns.Contains("descEstado") ? FilaPublicacion["descEstado"].ToString() : "",
                        StrNombreArchivo = dataSetObtenerDataTable.Tables[0].Columns.Contains("imagen1") ? FilaPublicacion["imagen1"].ToString() : "",
                        StrCveUsuario = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveUsuario") ? FilaPublicacion["cveUsuario"].ToString() : "",
                        StrImagen = dataSetObtenerDataTable.Tables[0].Columns.Contains("imagen") ? FilaPublicacion["imagen"].ToString() : "",
                        IntBActivo = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("bActivo") ? FilaPublicacion["bActivo"].ToString() : "0"),
                        IntIdUsuario = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idUsuario") ? FilaPublicacion["idUsuario"].ToString() : "0"),
                        IntIdChat = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idChat") ? FilaPublicacion["idChat"].ToString() : "0"),
                        IntIdMegusta = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idMeGusta") ? FilaPublicacion["idMeGusta"].ToString() : "0"),
                        DtFechaAlta = DateTime.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("fechaAlta") ? FilaPublicacion["fechaAlta"].ToString() : "01/01/0001")
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Publicacion, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return lstObjPublicacion;
    }

    public RespuestaDataTable<Publicacion> ResultadoDataTables(string filtros)
    {
        FiltrosDTPublicacion filtrodt = JsonConvert.DeserializeObject<FiltrosDTPublicacion>(filtros);
        int IntTotalFilasFitradas = 0;
        int IntTotalFilas = 0;
        List<Publicacion> Publicacion = new List<Publicacion>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPublicacionObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
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

                    Publicacion.Add(new Publicacion(
                        int.Parse(filaTipoCambio["idPublicacion"].ToString()),
                        filaTipoCambio["cvePublicacion"].ToString(),
                        filaTipoCambio["descPublicacion"].ToString(),
                        int.Parse(filaTipoCambio["bActivo"].ToString())
                        ));
                }

                IntTotalFilasFitradas = int.Parse(dataSetObtener.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtener.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {

        }

        return new RespuestaDataTable<Publicacion>
        {

            data = Publicacion,
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
            sqlCommand.CommandText = "dbo.spPublicacionInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_DescPublicacion", this.StrDescripcion);
            sqlCommand.Parameters.AddWithValue("@p_IdCategoria", this.IntIdCategoria);
            sqlCommand.Parameters.AddWithValue("@p_Presupuesto", this.DecPresupuesto);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", 1);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuarioAlta", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_TipoVigencia", this.IntTipoVigencia);
            sqlCommand.Parameters.AddWithValue("@p_NombreArchivo", this.StrNombreArchivo);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

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
        var intTipoUsuario = VariableGlobal.SessionIntTipoUsuario;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPublicacionObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_idPublicacion", this.IntIdPublicacion);
            //sqlCommand.Parameters.AddWithValue("@p_IdUsuario", intIdUsuario);
            //sqlCommand.Parameters.AddWithValue("@p_TipoUsuario", intTipoUsuario);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: Publicacion.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdPublicacion = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPublicacion"].ToString());
                this.StrDescripcion = dataSetObtener.Tables[0].Rows[0]["cvePublicacion"].ToString();
                this.StrCveComprador = dataSetObtener.Tables[0].Rows[0]["descPublicacion"].ToString();
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
            sqlCommand.CommandText = "corporativo.spPublicacionActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", this.IntIdPublicacion);
            sqlCommand.Parameters.AddWithValue("@p_CvePublicacion", this.StrDescripcion);
            sqlCommand.Parameters.AddWithValue("@p_DescPublicacion", this._StrCveComprador);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            DataSet datasetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs, metodo => Actualizar()");
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
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spPublicacion";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 2);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", this.IntIdPublicacion);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", "[]");

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

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
    #endregion
}

public class FiltrosDTPublicacion : DataTableAjaxPostModel
{
    public string SrtBActivoFiltro { get; set; }
    public string StrOrigen { get; set; }
}