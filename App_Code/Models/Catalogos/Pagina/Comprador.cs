using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comprador
/// </summary>
public class Comprador
{

    #region variables

    int _IntIdComprador;
    Guid _GuidGuidComprador;
    string _StrTelefono;
    string _StrCorreo;
    string _StrCveComprador;
    string _StrContrasena;
    string _StrNombreUsuario;
    string _StrApellidoUsuario;
    DateTime _DtFechaAlta;
    int _IntBActivo;
    int _IntIdEstado;

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

    public Guid GuidGuidComprador
    {
        get
        {
            return _GuidGuidComprador;
        }

        set
        {
            _GuidGuidComprador = value;
        }
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

    public string StrCveComprador
    {
        get
        {
            return _StrCveComprador;
        }

        set
        {
            _StrCveComprador = value;
        }
    }

    public string StrContrasena
    {
        get
        {
            return _StrContrasena;
        }

        set
        {
            _StrContrasena = value;
        }
    }

    public string StrNombreUsuario
    {
        get
        {
            return _StrNombreUsuario;
        }

        set
        {
            _StrNombreUsuario = value;
        }
    }

    public string StrApellidoUsuario
    {
        get
        {
            return _StrApellidoUsuario;
        }

        set
        {
            _StrApellidoUsuario = value;
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

    public int IntIdEstado
    {
        get
        {
            return _IntIdEstado;
        }

        set
        {
            _IntIdEstado = value;
        }
    }
    #endregion

    #region Constructor
    public Comprador()
    {
    }
    #endregion

    #region Metodos
    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spComprador";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdComprador", this.IntIdComprador);
            sqlCommand.Parameters.AddWithValue("@p_CveComprador", this.StrCveComprador);
            sqlCommand.Parameters.AddWithValue("@p_Contrasena", this.StrContrasena);
            sqlCommand.Parameters.AddWithValue("@p_Telefono", this.StrTelefono);
            sqlCommand.Parameters.AddWithValue("@p_Correo", this.StrCorreo);
            sqlCommand.Parameters.AddWithValue("@p_NombreUsuario", this.StrNombreUsuario);
            sqlCommand.Parameters.AddWithValue("@p_ApellidoUsuario", this.StrApellidoUsuario);
            sqlCommand.Parameters.AddWithValue("@p_bActivo", this.IntBActivo);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_IdEstado", this.IntIdEstado);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Comprador.cs => Insertar()");

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
    #endregion

}