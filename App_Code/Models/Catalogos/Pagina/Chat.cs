using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Chat
/// </summary>
public class Chat
{
    #region Atributos
    int _IntIdChat;
    int _IntIdPublicacion;
    int _IntIdUsuario;
    int _IntIdUsuarioDestino;
    string _StrMensaje;


    public int IntIdChat
    {
        get
        {
            return _IntIdChat;
        }

        set
        {
            _IntIdChat = value;
        }
    }

    public int IntIdPublicacion
    {
        get
        {
            return _IntIdPublicacion;
        }

        set
        {
            _IntIdPublicacion = value;
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

    public int IntIdUsuarioDestino
    {
        get
        {
            return _IntIdUsuarioDestino;
        }

        set
        {
            _IntIdUsuarioDestino = value;
        }
    }

    public string StrMensaje
    {
        get
        {
            return _StrMensaje;
        }

        set
        {
            _StrMensaje = value;
        }
    }

    #endregion

    #region Constructor
    public Chat()
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
            sqlCommand.CommandText = "dbo.spChat";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_IdChat", this.IntIdChat);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuariodestino", this.IntIdUsuarioDestino);
            sqlCommand.Parameters.AddWithValue("@p_Mensaje", this.StrMensaje);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

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