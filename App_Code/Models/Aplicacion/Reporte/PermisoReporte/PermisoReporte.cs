using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Descripción breve de PermisoReporte
/// </summary>
public class PermisoReporte
{
    #region Propiedades

    private string _StrDescRutaMenu;
    private string _StrDesPermisosEspeciales;

    public string StrDescRutaMenu
    {
        get { return _StrDescRutaMenu; }
        set { _StrDescRutaMenu = value; }
    }
    private string _StrRutaVista;

    public string StrRutaVista
    {
        get { return _StrRutaVista; }
        set { _StrRutaVista = value; }
    }
    private string _StrDescAccion;

    public string StrDescAccion
    {
        get { return _StrDescAccion; }
        set { _StrDescAccion = value; }
    }
    private string _StrDescAcceso;

    public string StrDescAcceso
    {
        get { return _StrDescAcceso; }
        set { _StrDescAcceso = value; }
    }
    private string _StrDescAccesoPor;

    public string StrDescAccesoPor
    {
        get { return _StrDescAccesoPor; }
        set { _StrDescAccesoPor = value; }
    }

    public string StrDesPermisosEspeciales
    {
        get
        {
            return _StrDesPermisosEspeciales;
        }

        set
        {
            _StrDesPermisosEspeciales = value;
        }
    }

    #endregion

    #region Contructores

    public PermisoReporte()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #endregion

    #region Metodos

    public List<PermisoReporte> ObtenerPermisosUsuarios(string filtros)
    {
        FiltrosDTPermisoReporte objFiltro = JsonConvert.DeserializeObject<FiltrosDTPermisoReporte>(filtros);
        List<PermisoReporte> objPermisos = new List<PermisoReporte>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spPermisoReporteObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", objFiltro.IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_idPerfil", objFiltro.IntIdPerfil);
            sqlCommand.Parameters.AddWithValue("@p_idMenu", objFiltro.IntIdMenu);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            //Empieza registro Acceso
            string StrOrigen = "";
            var ObjUrl = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer : HttpContext.Current.Request.Url; 
 string StrUrlQuery = ObjUrl.Query;
            string[] StrValorQuery = StrUrlQuery.Split(new string[] { "Origen=" }, StringSplitOptions.RemoveEmptyEntries);
            if (StrValorQuery.Length > 1)
            {
                string[] StrValorQuery2 = StrValorQuery[1].Split('&');
                StrOrigen = "?Origen=" + StrValorQuery2[0];
            }

            ConexionBD conexion = new ConexionBD();
            conexion.SqlCommandComando = sqlCommand;
            conexion.SqlParametro = sqlCommand.Parameters;
            conexion.StrDescRuta = ObjUrl.AbsolutePath + StrOrigen;
            conexion.StrDescModelo = this.GetType().Name + ".cs";
            conexion.StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name;
            conexion.IntBRegistrarAcceso = 1; //En caso de que no se quiera registrar el proceso, solo ejecutarlo, mandar en 0

            DataSet documentoInsertar = conexion.EjecutarComandoRegistroAcceso();
            //Termina registro Acceso

            //DataSet dataSetObtenerAnticipoReporte = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: PermisoReporte.cs, metodo: ObtenerPermisosUsuarios()");

            if (documentoInsertar != null && documentoInsertar.Tables.Count > 0)
            {
                foreach (DataRow fila in documentoInsertar.Tables[0].Rows)
                {
                    
                    objPermisos.Add(new PermisoReporte()
                    {
                        StrDescRutaMenu = fila["menu"].ToString(),
                        StrRutaVista = fila["rutapagina"].ToString(),
                        StrDescAccesoPor = fila["tipo"].ToString(),
                        StrDescAcceso = fila["nomtipo"].ToString(),
                        StrDescAccion = fila["accion"].ToString(),
                        StrDesPermisosEspeciales = fila["permisosEspeciales"].ToString()
                    });
                }
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objPermisos;

    }

    #endregion
}

public class FiltrosDTPermisoReporte : DataTableAjaxPostModel
{
    public int IntIdMenu { get; set; }
    public int IntIdPerfil { get; set; }
    public int IntIdUsuario { get; set; }
}