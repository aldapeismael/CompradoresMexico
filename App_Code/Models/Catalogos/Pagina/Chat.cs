using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
    int _IntIdUsuario1;
    int _IntIdUsuario2;
    int _IntIdUsuarioDestino;
    string _StrMensaje;
    string _StrJsonMensajes;
    string _StrDescripcion;
    string _StrCveCategoria;
    string _StrImagen1;
    string _StrCveUsuario;
    string _StrCveUsuario1;
    string _StrCveUsuario2;
    DateTime _DtFechaMensaje;


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

    public DateTime DtFechaMensaje
    {
        get
        {
            return _DtFechaMensaje;
        }

        set
        {
            _DtFechaMensaje = value;
        }
    }

    public string StrCveUsuario
    {
        get
        {
            return _StrCveUsuario;
        }

        set
        {
            _StrCveUsuario = value;
        }
    }

    public string StrJsonMensajes
    {
        get
        {
            return _StrJsonMensajes;
        }

        set
        {
            _StrJsonMensajes = value;
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

    public string StrImagen1
    {
        get
        {
            return _StrImagen1;
        }

        set
        {
            _StrImagen1 = value;
        }
    }

    public string StrCveUsuario1
    {
        get
        {
            return _StrCveUsuario1;
        }

        set
        {
            _StrCveUsuario1 = value;
        }
    }

    public string StrCveUsuario2
    {
        get
        {
            return _StrCveUsuario2;
        }

        set
        {
            _StrCveUsuario2 = value;
        }
    }

    public int IntIdUsuario1
    {
        get
        {
            return _IntIdUsuario1;
        }

        set
        {
            _IntIdUsuario1 = value;
        }
    }

    public int IntIdUsuario2
    {
        get
        {
            return _IntIdUsuario2;
        }

        set
        {
            _IntIdUsuario2 = value;
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
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", this.IntIdPublicacion);
            sqlCommand.Parameters.AddWithValue("@p_Mensaje", this.StrMensaje);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Publicacion.cs => Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0 && dataSetInsertar.Tables[0].Rows.Count > 0)
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
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }
        }
        catch (Exception ex)
        {
            objRespuestaBD = new RespuestaBD(1, ex.ToString(), "error");
        }
        return objRespuestaBD;
    }

    public List<Chat> ObtenerListaGenerica(ObjetoBusqueda ParamObjeto)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var intTipoUsuario = VariableGlobal.SessionIntTipoUsuario;

        List<Chat> lstObjChat = new List<Chat>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.spChat";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", ParamObjeto.IntEjecuta);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_IdUsuariodestino", IntIdUsuarioDestino);
            sqlCommand.Parameters.AddWithValue("@p_IdChat", IntIdChat);
            sqlCommand.Parameters.AddWithValue("@p_IdPublicacion", IntIdPublicacion);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaChat in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjChat.Add(new Chat
                    {
                        IntIdChat = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idChat") ? FilaChat["idChat"].ToString() : "0"),
                        IntIdPublicacion = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idPublicacion") ? FilaChat["idPublicacion"].ToString() : "0"),
                        IntIdUsuario = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idUsuario1") ? FilaChat["idUsuario1"].ToString() : "0"),
                        IntIdUsuarioDestino = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idUsuario2") ? FilaChat["idUsuario2"].ToString() : "0"),
                        DtFechaMensaje = DateTime.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("fechaMensaje") ? FilaChat["fechaMensaje"].ToString() : "01-01-0001"),
                        StrCveUsuario = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveUsuario") ? FilaChat["cveUsuario"].ToString() : "",
                        StrMensaje = dataSetObtenerDataTable.Tables[0].Columns.Contains("mensaje") ? FilaChat["mensaje"].ToString() : "",
                        StrJsonMensajes = dataSetObtenerDataTable.Tables[0].Columns.Contains("jsonMensajes") ? FilaChat["jsonMensajes"].ToString() : "",
                        StrDescripcion = dataSetObtenerDataTable.Tables[0].Columns.Contains("descripcion") ? FilaChat["descripcion"].ToString() : "",
                        StrCveCategoria = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveCategoria") ? FilaChat["cveCategoria"].ToString() : "",
                        StrImagen1 = dataSetObtenerDataTable.Tables[0].Columns.Contains("imagen1") ? FilaChat["imagen1"].ToString() : "",

                        IntIdUsuario1 = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idUsuario1") ? FilaChat["idUsuario1"].ToString() : "0"),
                        IntIdUsuario2 = int.Parse(dataSetObtenerDataTable.Tables[0].Columns.Contains("idUsuario2") ? FilaChat["idUsuario2"].ToString() : "0"),
                        StrCveUsuario1 = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveUsuario1") ? FilaChat["cveUsuario1"].ToString() : "",
                        StrCveUsuario2 = dataSetObtenerDataTable.Tables[0].Columns.Contains("cveUsuario2") ? FilaChat["cveUsuario2"].ToString() : "",
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Chat, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return lstObjChat;
    }
    #endregion
}