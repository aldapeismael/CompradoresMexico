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
/// Summary description for Opcion
/// </summary>
public class Opcion : IMetodosModelos<Opcion>
{
    #region Propiedades

    private int _IntIdOpcion;
    private int _IntIdTerminal;
    private int _IntIdMenu;
    private int _IntIdPagina;
    private int _IntIdMultilista_AplicacionOpcion;
    private int _IntBActivo;
    private string _StrValor1Json;
    


    public int IntIdOpcion
    {
        get { return _IntIdOpcion; }
        set { _IntIdOpcion = value; }
    }

    public int IntIdTerminal
    {
        get { return _IntIdTerminal; }
        set { _IntIdTerminal = value; }
    }

    public int IntIdMenu
    {
        get { return _IntIdMenu; }
        set { _IntIdMenu = value; }
    }

    public int IntIdPagina
    {
        get { return _IntIdPagina; }
        set { _IntIdPagina = value; }
    }

    public int IntIdMultilista_AplicacionOpcion
    {
        get { return _IntIdMultilista_AplicacionOpcion; }
        set { _IntIdMultilista_AplicacionOpcion = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public string StrValor1Json
    {
        get { return _StrValor1Json; }
        set { _StrValor1Json = value; }
    }

    #endregion

    #region Propiedades adicionales

    private string _StrDescTerminal;

    public string StrDescTerminal
    {
        get { return _StrDescTerminal; }
        set { _StrDescTerminal = value; }
    }

    private string _StrDescMenu;

    public string StrDescMenu
    {
        get { return _StrDescMenu; }
        set { _StrDescMenu = value; }
    }

    private string _StrDescMultiLista;

    public string StrDescMultiLista
    {
        get { return _StrDescMultiLista; }
        set { _StrDescMultiLista = value; }
    }
    private RespuestaBD _objRespuestaDB;

    public RespuestaBD objRespuestaDB
    {
        get { return _objRespuestaDB; }
        set { _objRespuestaDB = value; }
    }


    #endregion

    #region Constructores

    public Opcion()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Opcion(int intIdOpcion)
    {
        _IntIdOpcion = intIdOpcion;
    }

    #endregion

    #region Metodos

    public RespuestaDataTable<Opcion> ResultadoDataTables(string filtros)
    {
        FiltrosDTOpcion objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTOpcion>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<Opcion> aplicacionCompListado = new List<Opcion>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spOpcionObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);

            if (objFiltroDt.IntBActivo >= 0)
                sqlCommand.Parameters.AddWithValue("@p_Activo", objFiltroDt.IntBActivo);
            if (objFiltroDt.IntIdMenu >= 0)
                sqlCommand.Parameters.AddWithValue("@p_Menu", objFiltroDt.IntIdMenu);
            if (objFiltroDt.IntIdTerminal >= 0)
                sqlCommand.Parameters.AddWithValue("@p_Terminal", objFiltroDt.IntIdTerminal);

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    aplicacionCompListado.Add(new Opcion()
                    {
                        IntIdOpcion = int.Parse(fila["id"].ToString()),
                        StrDescTerminal = fila["descTerminal"].ToString(),
                        StrDescMenu = fila["descMenu"].ToString(),
                        StrDescMultiLista = fila["descAplicacionOpcion"].ToString(),
                        IntBActivo = int.Parse(fila["bActivo"].ToString())
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

        return new RespuestaDataTable<Opcion>
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
            sqlCommand.CommandText = "aplicacion.spOpcionInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdTerminal", this.IntIdTerminal);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", this.IntIdMenu);
            sqlCommand.Parameters.AddWithValue("@p_IdPagina", this.IntIdPagina);
            sqlCommand.Parameters.AddWithValue("@p_IdMultilista_aplicacionOpcion", this.IntIdMultilista_AplicacionOpcion);
            sqlCommand.Parameters.AddWithValue("@p_Valor1json", this.StrValor1Json);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            /********************************************************** Objeto Registro Error **************************************************************************************/

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            /***********************************************************************************************************************************************************************/

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
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/

            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");

            /********************************************************************************************************************************************************************************/
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
            sqlCommand.CommandText = "aplicacion.spOpcionObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_IdOpcion", this.IntIdOpcion);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdOpcion = int.Parse(dataSetObtener.Tables[0].Rows[0]["idOpcion"].ToString());
                this.IntIdTerminal = int.Parse(dataSetObtener.Tables[0].Rows[0]["idTerminal"].ToString());
                this.IntIdMenu = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMenu"].ToString());
                this.IntIdPagina = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPagina"].ToString());
                this.IntIdMultilista_AplicacionOpcion = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMultilista_aplicacionOpcion"].ToString());
                this.IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());

                this.StrValor1Json = dataSetObtener.Tables[0].Rows[0]["valor1json"].ToString();
                
                bool_Valido = true;
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
            sqlCommand.CommandText = "aplicacion.spOpcionActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdOpcion", this.IntIdOpcion);
            sqlCommand.Parameters.AddWithValue("@p_IdTerminal", this.IntIdTerminal);
            sqlCommand.Parameters.AddWithValue("@p_IdMenu", this.IntIdMenu);
            sqlCommand.Parameters.AddWithValue("@p_IdPagina", this.IntIdPagina);
            sqlCommand.Parameters.AddWithValue("@p_IdMultilista_aplicacionOpcion", this.IntIdMultilista_AplicacionOpcion);
            sqlCommand.Parameters.AddWithValue("@p_Valor1json", this.StrValor1Json);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);

            //Registro de error
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de actualizar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
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
            sqlCommand.CommandText = "aplicacion.spOpcionEliminar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdOpcion", this.IntIdOpcion);

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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error, contacte con el administrador, ID: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error, contacte con el administrador, ID: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    //public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    //{
    //    List<SelectListItem> objLista = new List<SelectListItem>();
    //    try
    //    {
    //        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
    //        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
    //        var IntBRobot = VariableGlobal.SessionIntBRobot;

    //        SqlCommand sqlCommand = new SqlCommand();
    //        sqlCommand.CommandText = "aplicacion.spOpcionObtenerLista";
    //        sqlCommand.CommandType = CommandType.StoredProcedure;
    //        sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
    //        sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
    //        sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
    //        sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBRobot);
    //        //    sqlCommand.Parameters.AddWithValue("@p_Limite", 0);
    //        DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Opcion");
    //        if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
    //        {
    //            return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();

    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Opcion.cs", "OpcionController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
    //    }
    //    return objLista;
    //}


    public List<Opcion> ObtenerListaGenerica(ObjetoBusqueda ParamObjeto)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        List<Opcion> lstObjOpcion = new List<Opcion>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spOpcionObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamObjeto.IntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IdGenerico1", ParamObjeto.IntId1);
            sqlCommand.Parameters.AddWithValue("@p_IdGenerico2", ParamObjeto.IntId2);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaAlmacen in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjOpcion.Add(new Opcion
                    {
                        IntIdOpcion = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idOpcion") ? FilaAlmacen["idOpcion"].ToString() : "0"),
                        IntIdTerminal = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idTerminal") ? FilaAlmacen["idTerminal"].ToString() : "0"),
                        IntIdMenu = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idMenu") ? FilaAlmacen["idMenu"].ToString() : "0"),
                        IntIdPagina = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idPagina") ? FilaAlmacen["idPagina"].ToString() : "0"),
                        StrValor1Json = dataSetObtenerDataTable.Tables[0].Columns.Contains("valor1json") ? FilaAlmacen["valor1json"].ToString() : ""
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Almacen, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return lstObjOpcion;
    }
    #endregion
}
public class FiltrosDTOpcion : DataTableAjaxPostModel
{
    public int IntBActivo { get; set; }
    public int IntIdMenu { get; set; }
    public int IntIdTerminal { get; set; }
}