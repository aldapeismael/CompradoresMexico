using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Proveedor
/// </summary>
public class Proveedor
{
    #region Metodos
    int _IntIdProveedor;
    int _IntIdUsuario;
    string _StrCveProveedor;
    string _StrTelefono;
    string _StrCorreo;
    string _StrRfc;
    string _StrDescEmpresa;
    int _IntBActivo;
    int _IntIdEstado;
    string _StrContrasena;
    string _StrNombreUsuario;
    string _StrApellidoUsuario;

    public int IntIdProveedor
    {
        get
        {
            return _IntIdProveedor;
        }

        set
        {
            _IntIdProveedor = value;
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

    public string StrCveProveedor
    {
        get
        {
            return _StrCveProveedor;
        }

        set
        {
            _StrCveProveedor = value;
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

    public string StrRfc
    {
        get
        {
            return _StrRfc;
        }

        set
        {
            _StrRfc = value;
        }
    }

    public string StrDescEmpresa
    {
        get
        {
            return _StrDescEmpresa;
        }

        set
        {
            _StrDescEmpresa = value;
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

    #region constructor
    public Proveedor()
    {
        //
        // TODO: Add constructor logic here
        //
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
            sqlCommand.CommandText = "dbo.spProveedor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdProveedor", this.IntIdProveedor);
            sqlCommand.Parameters.AddWithValue("@p_CveProveedor", this.StrCveProveedor);
            sqlCommand.Parameters.AddWithValue("@p_Contrasena", this.StrContrasena);
            sqlCommand.Parameters.AddWithValue("@p_Telefono", this.StrTelefono);
            sqlCommand.Parameters.AddWithValue("@p_Correo", this.StrCorreo);
            sqlCommand.Parameters.AddWithValue("@p_NombreUsuario", this.StrNombreUsuario);
            sqlCommand.Parameters.AddWithValue("@p_ApellidoUsuario", this.StrApellidoUsuario);
            sqlCommand.Parameters.AddWithValue("@p_RFC", this.StrRfc);
            sqlCommand.Parameters.AddWithValue("@p_DescEmpresa", this.StrDescEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_IdEstado", this.IntIdEstado);
            sqlCommand.Parameters.AddWithValue("@p_bActivo", this.IntBActivo);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Proveedor.cs => Insertar()");

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