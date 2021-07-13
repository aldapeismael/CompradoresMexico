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
public class RegistroProcesoExterno : IMetodosModelos<RegistroProcesoExterno>
{
    #region Propiedades
    int _IntIdReferencia1;
    string _StrCveReferencia1;
    string _StrDescAccion;
    string _StrMultiLista_TipoProceso;
    string _StrTipoAccion;
    string _StrCveUsuario;
    string _StrCveEmpresaReferencia;

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
    public string StrDescAccion
    {
        get { return _StrDescAccion; }
        set { _StrDescAccion = value; }
    }
    public string StrMultiLista_TipoProceso
    {
        get { return _StrMultiLista_TipoProceso; }
        set { _StrMultiLista_TipoProceso = value; }
    }
    public string StrTipoAccion
    {
        get { return _StrTipoAccion; }
        set { _StrTipoAccion = value; }
    }
    public string StrCveUsuario
    {
        get { return _StrCveUsuario; }
        set { _StrCveUsuario = value; }
    }
    public string StrCveEmpresaReferencia
    {
        get { return _StrCveEmpresaReferencia; }
        set { _StrCveEmpresaReferencia = value; }
    }
    #endregion

    #region Constructor
    public RegistroProcesoExterno()
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
        RespuestaBD objRespuestaBD = new RespuestaBD();
        Usuario objUsuario = new Usuario();
        Empresa objEmpresa = new Empresa();
        objUsuario.StrCveUsuario = StrCveUsuario;
        objEmpresa.StrCveEmpresaReferencia = StrCveEmpresaReferencia;
        try
        {
            objUsuario.ObtenerPorCve();
            objEmpresa.ObtenerPorCve();
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroProcesoInsertar";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Transaccion", 0);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_IdEmpresa", objEmpresa.IntIdEmpresa);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_MultiLista_TipoProceso", StrMultiLista_TipoProceso);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", objUsuario.IntIdUsuario);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_IdReferencia1",IntIdReferencia1);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_CveReferencia1", StrCveReferencia1);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_TipoAccion", StrTipoAccion);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_DescAccion", StrDescAccion);//PARAMETRO PUBLICO
            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));
            DataSet dataSetInsertar = ConexionBD.EjecutarComando(objEmpresa.IntIdEmpresa, objUsuario.IntIdUsuario, sqlcommand, "archivo : RegistroProcesoExterno.cs, metodo : Insertar()");
            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0)
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
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error", 0);
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error", 0);

        }

        return objRespuestaBD;
    }

    public bool ObtenerPorId()
    {
        throw new NotImplementedException();
    }

    public RespuestaDataTable<RegistroProcesoExterno> ResultadoDataTables(string filtros)
    {
        throw new NotImplementedException();
    }
    #endregion
}
