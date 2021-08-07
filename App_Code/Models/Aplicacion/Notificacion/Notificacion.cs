using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for Notificacion
/// </summary>
public class Notificacion : IMetodosModelos<Notificacion>
{
    #region Atributos
    int _IntIdNotificacion;
    int _IntIdUsuario_origen_db_erp;
    int _IntIdUsuario_destino_db_erp;
    int _IntIdUsuarioGrupo_destino;
    string _StrTituloNotificacion;
    string _StrDescNotificacion;
    int _IntIdMultilista_estatusNotificacion_db_erp;
    int _IntIdMultilista_prioridadNotificacion_db_erp;
    int _IntIdMultilista_tipoRegistroProceso_db_erp;
    int _IntIdMultilista_tipoRegistroCategoria_db_erp;
    DateTime _DtFechaNotificacion;
    DateTime _DtFechaLeido;
    DateTime _DtFechaPreLeido;
    DateTime _DtFechaEdita;
    DateTime _DtFechaElimina;
    int _IntBActivo;
    string _StrJsonNotificacionListado;
    string _StrDescMultilista_Estatus;
    string _StrDescMultilista_Proceso;
    string _StrDescMultilista_Categoria;
    string _StrIcono;
    string _StrTipo;
    int _IntCantidadNotificaciones;
    string _StrJsonChat;
    #endregion

    #region Getter y Setter
    public string StrJsonNotificacionListado
    {
        get { return _StrJsonNotificacionListado; }
        set { _StrJsonNotificacionListado = value; }
    }
    public int IntIdNotificacion
    {
        get { return _IntIdNotificacion; }
        set { _IntIdNotificacion = value; }
    }
    public int IntIdUsuario_origen_db_erp
    {
        get { return _IntIdUsuario_origen_db_erp; }
        set { _IntIdUsuario_origen_db_erp = value; }
    }
    public int IntIdUsuario_destino_db_erp
    {
        get { return _IntIdUsuario_destino_db_erp; }
        set { _IntIdUsuario_destino_db_erp = value; }
    }
    public int IntIdUsuarioGrupo_destino
    {
        get { return _IntIdUsuarioGrupo_destino; }
        set { _IntIdUsuarioGrupo_destino = value; }
    }
    public string StrTituloNotificacion
    {
        get { return _StrTituloNotificacion; }
        set { _StrTituloNotificacion = value; }
    }
    public string StrDescNotificacion
    {
        get { return _StrDescNotificacion; }
        set { _StrDescNotificacion = value; }
    }
    public int IntIdMultilista_estatusNotificacion_db_erp
    {
        get { return _IntIdMultilista_estatusNotificacion_db_erp; }
        set { _IntIdMultilista_estatusNotificacion_db_erp = value; }
    }
    public int IntIdMultilista_prioridadNotificacion_db_erp
    {
        get { return _IntIdMultilista_prioridadNotificacion_db_erp; }
        set { _IntIdMultilista_prioridadNotificacion_db_erp = value; }
    }
    public int IntIdMultilista_tipoRegistroProceso_db_erp
    {
        get { return _IntIdMultilista_tipoRegistroProceso_db_erp; }
        set { _IntIdMultilista_tipoRegistroProceso_db_erp = value; }
    }
    public int IntIdMultilista_tipoRegistroCategoria_db_erp
    {
        get { return _IntIdMultilista_tipoRegistroCategoria_db_erp; }
        set { _IntIdMultilista_tipoRegistroCategoria_db_erp = value; }
    }
    public DateTime DtFechaNotificacion
    {
        get { return _DtFechaNotificacion; }
        set { _DtFechaNotificacion = value; }
    }
    public DateTime DtFechaLeido
    {
        get { return _DtFechaLeido; }
        set { _DtFechaLeido = value; }
    }
    public DateTime DtFechaPreLeido
    {
        get { return _DtFechaPreLeido; }
        set { _DtFechaPreLeido = value; }
    }
    public DateTime DtFechaEdita
    {
        get { return _DtFechaEdita; }
        set { _DtFechaEdita = value; }
    }
    public DateTime DtFechaElimina
    {
        get { return _DtFechaElimina; }
        set { _DtFechaElimina = value; }
    }
    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }
    public string StrDescMultilista_Estatus
    {
        get { return _StrDescMultilista_Estatus; }
        set { _StrDescMultilista_Estatus = value; }
    }
    public string StrDescMultilista_Proceso
    {
        get { return _StrDescMultilista_Proceso; }
        set { _StrDescMultilista_Proceso = value; }
    }
    public string StrDescMultilista_Categoria
    {
        get { return _StrDescMultilista_Categoria; }
        set { _StrDescMultilista_Categoria = value; }
    }

    public string StrIcono
    {
        get
        {
            return _StrIcono;
        }

        set
        {
            _StrIcono = value;
        }
    }

    public string StrTipo
    {
        get
        {
            return _StrTipo;
        }

        set
        {
            _StrTipo = value;
        }
    }

    public int IntCantidadNotificaciones
    {
        get
        {
            return _IntCantidadNotificaciones;
        }

        set
        {
            _IntCantidadNotificaciones = value;
        }
    }

    public string StrJsonChat
    {
        get
        {
            return _StrJsonChat;
        }

        set
        {
            _StrJsonChat = value;
        }
    }
    #endregion

    #region Constructor
    public Notificacion()
    {
    }
    public Notificacion(int ParamIntIdNotificacion)
    {
        this.IntIdNotificacion = ParamIntIdNotificacion;
    }
    #endregion

    #region Metodos
    public RespuestaBD Actualizar()
    {
        throw new NotImplementedException();
    }

    public RespuestaBD Eliminar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        int IntDebug = 0;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "notificacion.spNotificacionActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 3);
            sqlCommand.Parameters.AddWithValue("@p_Debug", IntDebug);
            sqlCommand.Parameters.AddWithValue("@p_IdNotificacion", this._IntIdNotificacion);

            /********************************************************** Objeto Registro Error **************************************************************************************/

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            /***********************************************************************************************************************************************************************/

            DataSet dataSetInsertar = ConexionBD.EjecutarComandoEstadistica(sqlCommand);
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
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/

            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de eliminar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");

            /********************************************************************************************************************************************************************************/
        }
        return objRespuestaBD;
    }

    public RespuestaBD Insertar()
    {
        throw new NotImplementedException();
    }

    public bool ObtenerPorId()
    {
        bool bool_Valido = false;
        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "notificacion.spNotificacionObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_IdNotificacion", this.IntIdNotificacion);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + ".cs");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));


            DataSet dataSetObtener = ConexionBD.EjecutarComandoEstadistica(sqlcommand);

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdNotificacion = int.Parse(dataSetObtener.Tables[0].Rows[0]["idNotificacion"].ToString());
                this.StrTituloNotificacion = dataSetObtener.Tables[0].Rows[0]["tituloNotificacion"].ToString();
                this.StrDescNotificacion = dataSetObtener.Tables[0].Rows[0]["descNotificacion"].ToString();
                this.DtFechaNotificacion = DateTime.Parse(dataSetObtener.Tables[0].Rows[0]["fechaNotificacion"].ToString());
                this.StrDescMultilista_Estatus = dataSetObtener.Tables[0].Rows[0]["descMultiLista_Estatus"].ToString();
                this.StrDescMultilista_Categoria = dataSetObtener.Tables[0].Rows[0]["descMultiLista_Proceso"].ToString();
                this.StrDescMultilista_Proceso = dataSetObtener.Tables[0].Rows[0]["descMultiLista_Categoria"].ToString();
            }
        }
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            /********************************************************************************************************************************************************************************/
        }
        return bool_Valido;
    }

    public bool ObtenerCantidadNotificacion(int ParamIdUsuario, int ParamIdUsuarioDestino, int ParamIdPublicacion)
    {
        bool bool_Valido = false;
        SqlCommand sqlcommand = new SqlCommand();
        try
        {
            sqlcommand.CommandText = "dbo.spChatNotificacion";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 10);
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", ParamIdUsuario);
            sqlcommand.Parameters.AddWithValue("@p_IdUsuariodestino", ParamIdUsuarioDestino);
            sqlcommand.Parameters.AddWithValue("@p_IdPublicacion", ParamIdPublicacion);

            //RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + ".cs");
            //RegistroError objRegistroError = new RegistroError()
            //{
            //    StrDescStoredProcedureParametro = new RegistroError().ObtenerParametros(sqlcommand.Parameters),
            //    StrIP = RegistroError.ObtenerIpLocal(),
            //    StrDescNavegador = HttpContext.Current.Request.Browser.Type,
            //    StrVista = HttpContext.Current.Request.Url.AbsoluteUri,
            //    DateFechaInicio = DateTime.Now,
            //    StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name,
            //    StrDescModelo = this.GetType().Name + ".cs",
            //    StrDescControlador = this.GetType().Name + "Controller",
            //    IntBActivo = 1,
            //};
            //sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));
            DataSet dataSetObtener = ConexionBD.EjecutarComando(1, 1, sqlcommand, "");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.StrJsonChat = dataSetObtener.Tables[0].Rows[0]["jsonChat"].ToString();
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError()
            {
                StrDescStoredProcedureParametro = new RegistroError().ObtenerParametros(sqlcommand.Parameters),
                StrIP = RegistroError.ObtenerIpLocal(),
                StrDescNavegador = HttpContext.Current.Request.Browser.Type,
                StrVista = HttpContext.Current.Request.Url.AbsoluteUri,
                DateFechaInicio = DateTime.Now,
                StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name,
                StrDescModelo = this.GetType().Name + ".cs",
                StrDescControlador = this.GetType().Name + "Controller",
                IntBActivo = 1,
                IntErrorCodigo = e.HResult,
                StrErrorMensaje = e.Message,
                DateFechaFin = DateTime.Now,
                IntIdMultilista_Severidad = (int)Severidad.BAJA,
            };
            objRegistroError.InsertarRegistroError();
        }
        return bool_Valido;
    }

    public RespuestaDataTable<Notificacion> ResultadoDataTables(string filtros)
    {
        throw new NotImplementedException();
    }

    public List<Notificacion> ObtenerNotificacionListado(FiltrosDTNotificacion objFiltro)
    {
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        List<Notificacion> objNotificacionListado = new List<Notificacion>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "notificacion.spNotificacionObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_IdMultilista_estatusNotificacion", objFiltro.IntIdMultilista_estatusNotificacion_db_erp);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComandoEstadistica(sqlCommand);
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtener.Tables[0].Rows)
                {
                    objNotificacionListado.Add(new Notificacion()
                    {
                        StrJsonNotificacionListado = fila["resultadoJson"].ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return objNotificacionListado;
    }

    public List<Notificacion> ObtenerNotificacionPopUpListado()
    {
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        List<Notificacion> objNotificacionListado = new List<Notificacion>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "notificacion.spNotificacionPopUpObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComandoEstadistica(sqlCommand);
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtener.Tables[0].Rows)
                {
                    objNotificacionListado.Add(new Notificacion()
                    {
                        IntIdNotificacion = int.Parse(fila["idNotificacion"].ToString()),
                        StrTituloNotificacion = fila["tituloNotificacion"].ToString(),
                        DtFechaNotificacion = DateTime.Parse(fila["fechaNotificacion"].ToString()),
                        StrIcono = fila["valor2char_Icono"].ToString(),
                        StrTipo = fila["tipo"].ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return objNotificacionListado;
    }

    #endregion
}

public class FiltrosDTNotificacion : DataTableAjaxPostModel
{
    public int IntBActivo { get; set; }
    public int IntIdMultilista_estatusNotificacion_db_erp { get; set; }
}