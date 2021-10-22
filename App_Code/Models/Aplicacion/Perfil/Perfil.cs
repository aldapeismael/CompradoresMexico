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
/// Summary description for Class1
/// </summary>
public class Perfil : IMetodosModelos<Perfil>
{

    #region Propiedades

    private int _IntIdPerfil;
    private string _StrDescPerfil;
    private int _IntBActivo;
    private int _IntIdEmpresa;
    private string _StrCveEmpresa;
    private string _StrDescEmpresa;
    private int _IntLimite;
    RespuestaBD _ObjRespuestaBD;
    string _StrNombreArchivo;
    string _StrImagenProveedor;
    string _StrImagenComprador;

    #region Getter y Setter

    public RespuestaBD ObjRespuestaBD
    {
        get { return _ObjRespuestaBD; }
        set { _ObjRespuestaBD = value; }
    }

    public int IntIdPerfil
    {
        get { return _IntIdPerfil; }
        set { _IntIdPerfil = value; }
    }

    public string StrDescPerfil
    {
        get { return _StrDescPerfil; }
        set { _StrDescPerfil = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdEmpresa
    {
        get { return _IntIdEmpresa; }
        set { _IntIdEmpresa = value; }
    }

    public string StrCveEmpresa
    {
        get { return _StrCveEmpresa; }
        set { _StrCveEmpresa = value; }
    }

    public string StrDescEmpresa
    {
        get { return _StrDescEmpresa; }
        set { _StrDescEmpresa = value; }
    }

    public int IntLimite
    {
        get { return _IntLimite; }
        set { _IntLimite = value; }
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

    public string StrImagenProveedor
    {
        get
        {
            return _StrImagenProveedor;
        }

        set
        {
            _StrImagenProveedor = value;
        }
    }

    public string StrImagenComprador
    {
        get
        {
            return _StrImagenComprador;
        }

        set
        {
            _StrImagenComprador = value;
        }
    }

    #endregion
    #endregion

    #region Constructores

    public Perfil()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Perfil(int IntIdPerfil)
    {
        _IntIdPerfil = IntIdPerfil;
    }

    public Perfil(int intIdPerfil, string strDescPerfil, int intIdEmpresa, int intBActivo)
    {
        _IntIdPerfil = intIdPerfil;
        _StrDescPerfil = strDescPerfil;
        _IntIdEmpresa = intIdEmpresa;
        _IntBActivo = intBActivo;
    }

    #endregion

    #region Metodos

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPerfilObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            //    sqlCommand.Parameters.AddWithValue("@p_Limite", 0);

            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo: Perfil");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();

            }
        }
        catch (Exception e)
        {
        }
        return objLista;
    }

    public static List<Perfil> ObtenerListaAnidada(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        switch (ParamTipoBusqueda)
        {
            case 2:
                return Perfil.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro).Select(s => new Perfil()
                {
                    IntIdPerfil = int.Parse(s.idPerfil.ToString()),
                    StrDescPerfil = s.descPerfil.ToString()
                }).ToList();
            default:
                return Perfil.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro).Select(s => new Perfil() { }).ToList();
        }
    }

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPerfilActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdPerfil", this.IntIdPerfil);
            sqlCommand.Parameters.AddWithValue("@p_DescPerfil", this.StrDescPerfil);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Perfil.cs, metodo : Actualizar()");

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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    public RespuestaBD ActualizarImagen()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            if (VariableGlobal.SessionIntTipoUsuario == 1)
            {
                sqlCommand.CommandText = @"
                                            update dbo.comprador
                                            set imagen = @p_NombreArchivo
                                            OUTPUT INSERTED.IdComprador as IdActualiza
                                            where idUsuario = @p_IdUsuario
                                        ";
            }
            else
            {
                sqlCommand.CommandText = @"
                                            update dbo.proveedor
                                            set imagen = @p_NombreArchivo
                                            OUTPUT INSERTED.idProveedor as IdActualiza
                                            where idUsuario = @p_IdUsuario
                                        ";
            }
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_NombreArchivo", this.StrNombreArchivo);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0 && dataSetInsertar.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(dataSetInsertar.Tables[0].Rows[0]["IdActualiza"].ToString()) > 0)
                {
                    objRespuestaBD = new RespuestaBD(
                       0,
                       "Actualizado con Exito",
                       "success"
                   );
                    HttpContext.Current.Session["StrImagenPerfil"] = this.StrNombreArchivo;
                }
                else
                {
                    objRespuestaBD = new RespuestaBD(
                       1,
                       "Error al actualizar",
                       "error"
                   );
                }
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

    public void ObtenerImagenes()
    {
        try
        {
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"
                                           select
	                                            imagen = isnull(c.imagen, 'temporal.png')
                                            from dbo.comprador c(nolock)
                                            where idUsuario = @p_IdUsuario
                                            union all
                                            select
	                                            imagen = isnull(c.imagen, 'temporal.png')
                                            from dbo.proveedor c(nolock)
                                            where idUsuario = @p_IdUsuario
                                        ";
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0 && dataSetInsertar.Tables[0].Rows.Count > 0)
            {
                this.StrImagenComprador = dataSetInsertar.Tables[0].Rows[0]["imagen"].ToString();
                this.StrImagenProveedor = dataSetInsertar.Tables[0].Rows[1]["imagen"].ToString();
            }
            else
            {
                this.StrImagenComprador = "temporal.png";
                this.StrImagenProveedor = "temporal.png";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPerfilInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_DescPerfil", this.StrDescPerfil);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Perfil.cs, metodo : Insertar()");
            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0)
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
        var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPerfilObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_IdPerfil", this.IntIdPerfil);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Perfil.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdPerfil = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPerfil"].ToString());
                this.StrDescPerfil = dataSetObtener.Tables[0].Rows[0]["descPerfil"].ToString();
                this.IntIdEmpresa = int.Parse(dataSetObtener.Tables[0].Rows[0]["idEmpresa"].ToString());
                this.IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());
                bool_Valido = true;
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this.ObjRespuestaBD = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
        return bool_Valido;

    }

    public RespuestaDataTable<Perfil> ResultadoDataTables(string filtros)
    {
        FiltrosDTPerfil objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTPerfil>(filtros);
        int IntTotalFilasFitradas = 0;
        int IntTotalFilas = 0;
        List<Perfil> objPerfilListado = new List<Perfil>();
        int IntEjecuta = 1;
        var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;
        var IntBRobot = VariableGlobal.SessionIntBRobot;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spPerfilObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", IntEjecuta);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);
            sqlCommand.Parameters.AddWithValue("@p_StrBuscar", objFiltroDt.StrDescPerfilFiltro);
            if (objFiltroDt.IntIdEmpresaFiltro == null)
                sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);
            else
                sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", objFiltroDt.IntIdEmpresaFiltro);

            sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBRobot);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuarioSesion);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", objFiltroDt.IntBActivoPerfilFiltro);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Perfil.cs, metodo : ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaPerfil in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    objPerfilListado.Add(new Perfil()
                    {
                        IntIdPerfil = int.Parse(filaPerfil["idPerfil"].ToString()),
                        StrDescPerfil = filaPerfil["descPerfil"].ToString(),
                        IntIdEmpresa = int.Parse(filaPerfil["idEmpresa"].ToString()),
                        StrCveEmpresa = filaPerfil["cveEmpresa"].ToString(),
                        StrDescEmpresa = filaPerfil["descEmpresa"].ToString(),
                        IntBActivo = int.Parse(filaPerfil["bActivo"].ToString())
                    });
                }
                IntTotalFilasFitradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this.ObjRespuestaBD = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return new RespuestaDataTable<Perfil>
        {
            data = objPerfilListado,
            recordsFiltered = IntTotalFilasFitradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    #endregion

}
public class FiltrosDTPerfil : DataTableAjaxPostModel
{
    public string StrDescPerfilFiltro { get; set; }
    public string IntBActivoPerfilFiltro { get; set; }
    public string IntIdEmpresaFiltro { get; set; }
}