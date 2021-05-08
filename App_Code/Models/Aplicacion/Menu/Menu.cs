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
/// Summary description for Menu
/// </summary>
public class Menu : IMetodosModelos<Menu>
{
    #region Atributos
    private int _IntIdMenu;
    private int _IntBActivo;
    private int _IntIdMenuPadre;
    private string _StrDescMenu;
    private int _IntOrden;
    private int _IntOrdenMenu;
    private string _StrDescMenuPadre;
    private int _IntPaginasMenu;
    private string _StrJsonAcciones;
    private int _IntIdPaginaInicial;
    private string _StrDescPaginaInicial;
    private string _StrRuta;
    private string _StrIcono;
    private string _StrPaginasMenu;
    private string _StrDescRuta;

    //atributos agregados
    List<PaginaMenu> _LstObjPagina;
    List<Accion> _LstObjAccion;
    List<AccionMenu> _LstObjPaginaAccion;

    public string StrPaginasMenu
    {
        get { return _StrPaginasMenu; }
        set { _StrPaginasMenu = value; }
    }

    public string StrIcono
    {
        get { return _StrIcono; }
        set { _StrIcono = value; }
    }

    public string StrRuta
    {
        get { return _StrRuta; }
        set { _StrRuta = value; }
    }

    public string StrDescPaginaInicial
    {
        get { return _StrDescPaginaInicial; }
        set { _StrDescPaginaInicial = value; }
    }

    public int IntIdPaginaInicial
    {
        get { return _IntIdPaginaInicial; }
        set { _IntIdPaginaInicial = value; }
    }

    public string StrJsonAcciones
    {
        get { return _StrJsonAcciones; }
        set { _StrJsonAcciones = value; }
    }

    public int IntIdMenu
    {
        get { return _IntIdMenu; }
        set { _IntIdMenu = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdMenuPadre
    {
        get { return _IntIdMenuPadre; }
        set { _IntIdMenuPadre = value; }
    }

    public string StrDescMenu
    {
        get { return _StrDescMenu; }
        set { _StrDescMenu = value; }
    }

    public int IntOrden
    {
        get { return _IntOrden; }
        set { _IntOrden = value; }
    }

    public int IntOrdenMenu
    {
        get { return _IntOrdenMenu; }
        set { _IntOrdenMenu = value; }
    }

    public string StrDescMenuPadre
    {
        get { return _StrDescMenuPadre; }
        set { _StrDescMenuPadre = value; }
    }
    
    public int IntPaginasMenu
    {
        get { return _IntPaginasMenu; }
        set { _IntPaginasMenu = value; }
    }
    public List<PaginaMenu> LstObjPagina
    { 
        get { return _LstObjPagina; }
        set { _LstObjPagina = value; }
    }
    public List<Accion> LstObjAccion
    {
        get { return _LstObjAccion; }
        set { _LstObjAccion = value; }
    }

    public List<AccionMenu> LstObjPaginaAccion
    {
        get { return _LstObjPaginaAccion; }
        set { _LstObjPaginaAccion = value; }
    }

    public string StrDescRuta
    {
        get
        {
            return _StrDescRuta;
        }

        set
        {
            _StrDescRuta = value;
        }
    }
    #endregion

    #region Constructores

    public Menu()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Menu(int IntIdMenu)
    {
        _IntIdMenu = IntIdMenu;
    }

    #endregion

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMenuActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", this.IntIdMenu);
            sqlCommand.Parameters.AddWithValue("@p_DescMenu", this.StrDescMenu);
            sqlCommand.Parameters.AddWithValue("@p_IdMenuPadre", this.IntIdMenuPadre);
            sqlCommand.Parameters.AddWithValue("@p_IdPaginaInicial", this.IntIdPaginaInicial);
            sqlCommand.Parameters.AddWithValue("@p_Orden", this.IntOrden);
            sqlCommand.Parameters.AddWithValue("@p_Icono", this.StrIcono);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            //objetos grid
            sqlCommand.Parameters.AddWithValue("@p_ListaPaginas", JsonConvert.SerializeObject(this.LstObjPagina));

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Menu.cs, metodo : Actualizar()");
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

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMenuObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Menu -> ObtenerLista()");
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

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    public List<Menu> ObtenerGrid(string ParamStrBuscar, int ParamIntEjecuta)
    {
        List<Menu> obj_MenuListado = new List<Menu>();
        var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;
        var IntIdRobot = VariableGlobal.SessionIntBRobot;
        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spMenuObtenerDataTable";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", ParamIntEjecuta);
            sqlcommand.Parameters.AddWithValue("@p_StrBuscar", ParamStrBuscar);
            sqlcommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuarioSesion);
            sqlcommand.Parameters.AddWithValue("@p_EsRobot", IntIdRobot);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, "Menu.cs", "Menu.cs");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlcommand,"archivo : Menu.cs => ObtenerDataTable");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow filaCatalogo in dataSetObtener.Tables[0].Rows)
                {
                    obj_MenuListado.Add(new Menu()
                    {
                        IntIdMenu = int.Parse(filaCatalogo["idMenu"].ToString()),
                        StrDescMenu = filaCatalogo["descMenu"].ToString(),
                        StrDescRuta = filaCatalogo["descRuta"].ToString(),
                        IntIdMenuPadre = int.Parse(filaCatalogo["idMenuPadre"].ToString()),
                        IntOrden = int.Parse(filaCatalogo["Orden"].ToString()),
                        IntBActivo = int.Parse(filaCatalogo["bActivo"].ToString()),
                        IntPaginasMenu = dataSetObtener.Tables[0].Columns.Contains("cnt_paginasMenu") ? int.Parse(filaCatalogo["cnt_paginasMenu"].ToString()) : 0,
                        StrPaginasMenu = dataSetObtener.Tables[0].Columns.Contains("paginasMenu") ? filaCatalogo["paginasMenu"].ToString() : "[]",
                        StrJsonAcciones = dataSetObtener.Tables[0].Columns.Contains("json_acciones") ? filaCatalogo["json_acciones"].ToString() : "[]",
                        IntIdPaginaInicial = dataSetObtener.Tables[0].Columns.Contains("idPaginaInicial") ?  int.Parse(filaCatalogo["idPaginaInicial"].ToString()) : 0,
                        StrDescPaginaInicial = dataSetObtener.Tables[0].Columns.Contains("descPaginaInicial") ? filaCatalogo["descPaginaInicial"].ToString() : "",
                        StrRuta = dataSetObtener.Tables[0].Columns.Contains("ruta") ? filaCatalogo["ruta"].ToString() : "",
                        StrIcono = dataSetObtener.Tables[0].Columns.Contains("icono") ? filaCatalogo["icono"].ToString() : "",
                        IntOrdenMenu = dataSetObtener.Tables[0].Columns.Contains("ordenMenu") ? int.Parse(filaCatalogo["ordenMenu"].ToString()) : 0,
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Menu.cs", "Menu.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return obj_MenuListado;
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresaSesion = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuarioSesion = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMenuInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_DescMenu", this.StrDescMenu);
            sqlCommand.Parameters.AddWithValue("@p_IdMenuPadre", this.IntIdMenuPadre);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, "Menu.cs", "Menu.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Menu.cs, metodo : Insertar()");
            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetInsertar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetInsertar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetInsertar.Tables[0].Rows[0]["TipoError"].ToString(),
                    dataSetInsertar.Tables[0].Columns.Contains("IntIdRespuesta") ? int.Parse(dataSetInsertar.Tables[0].Rows[0]["IntIdRespuesta"].ToString()) : 0

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
            sqlCommand.CommandText = "corporativo.spMenuObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresaSesion);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", this.IntIdMenu);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresaSesion, IntIdUsuarioSesion, sqlCommand, "archivo : Menu.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdMenu          = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMenu"].ToString());
                this.StrDescMenu        = dataSetObtener.Tables[0].Rows[0]["descMenu"].ToString();
                this.IntIdMenuPadre     = int.Parse(dataSetObtener.Tables[0].Rows[0]["IdMenu_Padre"].ToString());
                this.StrDescMenuPadre   = dataSetObtener.Tables[0].Rows[0]["descMenuPadre"].ToString();
                this.IntIdPaginaInicial = int.Parse(dataSetObtener.Tables[0].Rows[0]["PaginaInicial"].ToString());
                this.IntOrden           = int.Parse(dataSetObtener.Tables[0].Rows[0]["Orden"].ToString());
                this.StrIcono           = dataSetObtener.Tables[0].Rows[0]["Icono"].ToString();
                this.IntBActivo         = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());

                LstObjPagina            = ObtenerListaPagina(IntIdMenu);
                LstObjAccion            = ObtenerListaAccion();
                LstObjPaginaAccion      = ObtenerListaPaginaAccion(1,IntIdMenu);

                bool_Valido = true;
            }
        }
        catch (Exception e)
        {
        }
        return bool_Valido;
    }

    public RespuestaDataTable<Menu> ResultadoDataTables(string ParamStrFiltros)
    {
        throw new NotImplementedException();
    }

    public static List<PaginaMenu> ObtenerListaPagina(int ParamIntIdMenu)
    {
        List<PaginaMenu> objDatosPagina = new List<PaginaMenu>();
        PaginaMenu objPagina = new PaginaMenu();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMenuPaginaObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", ParamIntIdMenu);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Menu.cs");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow dataRowFila in dataSetObtener.Tables[0].Rows)
                {
                    objPagina = new PaginaMenu
                    {
                        IntIdMenu       = int.Parse(dataRowFila["IdMenu_Padre"].ToString()),
                        IntIdPagina     = int.Parse(dataRowFila["idPagina"].ToString()),
                        StrDescPagina   = dataRowFila["descPagina"].ToString(),
                        StrUrl          = dataRowFila["ruta"].ToString(),
                        IntBActivo      = int.Parse(dataRowFila["bActivo"].ToString())
                    };
                    objDatosPagina.Add(objPagina);
                }
            }
        }
        catch (Exception ex)
        {

        }
        return objDatosPagina;
    }

    public static List<Accion> ObtenerListaAccion()
    {
        int Int_Cero    = 0;
        int Int_Dos     = 2;

        List<Accion> objDatosAccion = new List<Accion>();
        Accion objAccion = new Accion();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spAccionObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", Int_Dos);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", Int_Dos);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", Int_Cero);
            sqlCommand.Parameters.AddWithValue("@p_Limite", Int_Cero);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Menu.cs");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow dataRowFila in dataSetObtener.Tables[0].Rows)
                {
                    objAccion = new Accion
                    {
                        IntIdAccion         = int.Parse(dataRowFila["idAccion"].ToString()),
                        StrDescAccion       = dataRowFila["descAccion"].ToString(),
                        StrObjetoFuncion    = dataRowFila["ObjetoFuncion"].ToString(),
                        StrObjetoNombre     = dataRowFila["ObjetoNombre"].ToString(),
                        StrCveAccion        = dataRowFila["cveAccionReferencia"].ToString(),
                        IntBActivo          = int.Parse(dataRowFila["bActivo"].ToString())
                    };
                    objDatosAccion.Add(objAccion);
                }
            }
        }
        catch (Exception ex)
        {

        }
        return objDatosAccion;
    }

    public static List<AccionMenu> ObtenerListaPaginaAccion(int ParamIntEjecuta, int ParamIntIdMenu)
    {
        int Int_Cero = 0;

        List<AccionMenu> objDatosAccion = new List<AccionMenu>();
        AccionMenu objAccion = new AccionMenu();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMenuAccionObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", ParamIntEjecuta);
            sqlCommand.Parameters.AddWithValue("@p_Debug", Int_Cero);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", VariableGlobal.SessionIntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", ParamIntIdMenu);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Menu.cs");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow dataRowFila in dataSetObtener.Tables[0].Rows)
                {
                    objAccion = new AccionMenu
                    {
                        IntIdMenu = int.Parse(dataRowFila["IdMenu"].ToString()),
                        IntIdPagina = int.Parse(dataRowFila["idPagina"].ToString()),
                        IntIdPaginaAccion = int.Parse(dataRowFila["idPaginaAccion"].ToString()),
                        IntIdAccion = int.Parse(dataRowFila["idAccion"].ToString()),
                        IntBActivo = int.Parse(dataRowFila["bActivo"].ToString()),
                        StrPagina = dataRowFila["pagina"].ToString(),
                        StrAccion = dataRowFila["acciones"].ToString(),
                        StrCveAccionReferencia = dataRowFila["cveAccionReferencia"].ToString(),
                        StrDescAccion = dataRowFila["descAccion"].ToString(),
                        IntBMovimientoEspecial = int.Parse(dataRowFila["bMovimientoEspecial"].ToString()),
                        IntBClaveExiste = int.Parse(dataRowFila["bClaveExiste"].ToString())
                    };
                    objDatosAccion.Add(objAccion);
                }
            }
        }
        catch (Exception ex)
        {

        }
        return objDatosAccion;
    }

}