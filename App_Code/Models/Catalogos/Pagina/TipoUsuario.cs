using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TipoUsuario
/// </summary>
public class TipoUsuario
{
    #region
    int _IntVerificado;
    int _IntReenvio;
    int _IntIdTipoUsuario;
    string _StrTipoUsuario;
    string _StrImagen;

    public int IntVerificado
    {
        get
        {
            return _IntVerificado;
        }

        set
        {
            _IntVerificado = value;
        }
    }
    public int IntReenvio
    {
        get
        {
            return _IntReenvio;
        }

        set
        {
            _IntReenvio = value;
        }
    }
    public string StrTipoUsuario
    {
        get { return _StrTipoUsuario; }
        set { _StrTipoUsuario = value; }
    }

    public int IntIdTipoUsuario
    {
        get
        {
            return _IntIdTipoUsuario;
        }

        set
        {
            _IntIdTipoUsuario = value;
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

    #region
    public TipoUsuario()
    {
    }
    #endregion

    #region Metodos
    public bool VerificaTipoPerfil()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        bool bool_Valido = false;
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var intIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var intTipoUsuario = VariableGlobal.SessionIntTipoUsuario;
        this.IntIdTipoUsuario = int.Parse(Encriptado.DesencriptarDatos(this.StrTipoUsuario));
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spTipoUsuario";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0); //verifica 
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", intIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_IdTipoUsuario", this.IntIdTipoUsuario);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: Publicacion.cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntVerificado = int.Parse(dataSetObtener.Tables[0].Rows[0]["bVerificacion"].ToString());
                this.IntReenvio = int.Parse(dataSetObtener.Tables[0].Rows[0]["reenvio"].ToString());
                this.StrImagen = dataSetObtener.Tables[0].Rows[0]["imagen"].ToString();

                HttpContext.Current.Session["StrImagenPerfil"] = this.StrImagen;
            }
        }
        catch (Exception ex)
        {
            objRespuestaBD = new RespuestaBD(1, ex.ToString(), "error");
            return false;
        }
        return false;
    }
    #endregion
}