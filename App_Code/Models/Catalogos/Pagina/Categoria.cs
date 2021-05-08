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
/// Summary description for Categoria
/// </summary>
public class Categoria
{
    #region Atributos
    int _IntIdCategoria;
    string _StrCveCategoria;
    string _StrDescCategoria;
    string _StrDescripcion;
    int _IntIdCategoria_padre;
    int _IntBActivo;

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

    public int IntIdCategoria_padre
    {
        get
        {
            return _IntIdCategoria_padre;
        }

        set
        {
            _IntIdCategoria_padre = value;
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
    #endregion

    #region Constructor
    public Categoria()
    {
    }
    #endregion

    #region Metodos
    /// <summary>
    /// Obtener la Lista generica de Categorias
    /// </summary>
    /// <param name="ParamObjeto"></param>
    /// <returns></returns>
    public List<Categoria> ObtenerListaGenerica(ObjetoBusqueda ParamObjeto)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var intTipoUsuario = VariableGlobal.SessionIntTipoUsuario;

        List<Categoria> lstObjCategoria = new List<Categoria>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spCategoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", ParamObjeto.IntEjecuta);
            sqlCommand.Parameters.AddWithValue("@p_IdCategoria", ParamObjeto.IntId1);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            //sqlCommand.Parameters.AddWithValue("@p_TipoUsuario", intTipoUsuario);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaCategoria in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjCategoria.Add(new Categoria
                    {
                        IntIdCategoria = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idCategoria") ? FilaCategoria["idCategoria"].ToString() : "0"),
                        StrCveCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveCategoria") ? FilaCategoria["cveCategoria"].ToString() : "",
                        StrDescCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("descCategoria") ? FilaCategoria["descCategoria"].ToString() : "",
                        StrDescripcion = dataSetObtenerDataTable.Tables[0].Columns.Contains("descripcion") ? FilaCategoria["descripcion"].ToString() : "",
                        IntIdCategoria_padre = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idCategoria") ? FilaCategoria["idCategoria"].ToString() : "0"),
                        IntBActivo = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idCategoria") ? FilaCategoria["idCategoria"].ToString() : "0"),
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Categoria, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return lstObjCategoria;
    }
    #endregion
}