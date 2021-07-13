using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de DatabaseConection
/// </summary>
public class ConexionBD
{
    #region Propiedades
    public int IntIdEmpresa;
    public int IntIdAcceso;
    public int IntIdTeminal;
    public int IntIdMultilistaTipoRegistroProceso;
    //public int _IntIdMenu;
    //public int _IntIdPagina;
    public int IntIdUsuario;
    public SqlCommand SqlCommandComando;
    public SqlParameterCollection SqlParametro;
    public string StrDescRuta;
    public string StrDescModelo;
    public string StrDescModeloMetodo;
    public int IntBRegistrarAcceso;

    public ConexionBD()
    {
        this.IntIdEmpresa = VariableGlobal.IntIdEmpresaAcceso;
        this.IntIdAcceso = VariableGlobal.IntIdEmpresaAcceso;
        this.IntIdTeminal = VariableGlobal.SessionIntIdTerminal;
        this.IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
    }

    #endregion
    //funcion que ejecuta un comando de SQL
    public static DataSet EjecutarComando(int idEmpresa, int idUsuario, SqlCommand command, string Controller)
    {
        try
        {
            string conexionName = string.Empty;
            if (1 != 0)
            {
                switch (1)
                {
                    case 1:
                        conexionName = VariableGlobal.StrNombreConexionBD;
                        break;
                    //case 2:
                    //    conexionName = "erp2";
                    //    break;
                    //case 3:
                    //    conexionName = "erp3";
                    //    break;
                    default:
                        conexionName = VariableGlobal.StrNombreConexionBD;
                        break;
                }
            }
            else
            {
                conexionName = "bolsa";
            }
            string constr = ConfigurationManager.ConnectionStrings[conexionName].ConnectionString;
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand com = command;
                com.Connection = conn;
                com.CommandTimeout = 15000;
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = com };

                da.Fill(dataSet);
            }
            return dataSet;
        }
        catch (Exception errorException)
        {
            //try
            //{
            //    string parameters = string.Empty;
            //    foreach (SqlParameter param in command.Parameters)
            //    {
            //        parameters += param.ParameterName + "=" + param.Value + ",";
            //    }
            //    string sQuery = $"INSERT INTO prueba_excepcion VALUES ({idEmpresa},{idUsuario},GETDATE(),'{errorException.Message.Replace("'", "\"")}','{errorException.StackTrace.Replace("'", "\"")}','{command.CommandText.Replace("'", "\"")}','{parameters.Replace("'", "\"")}','{Controller}')";
            //    EjecutarTexto(idEmpresa, idUsuario, sQuery, Controller, true);

            //    //PONER RETURN
            //}
            //catch (Exception errorException2)
            //{

            //}
            throw errorException;
        }
        return null;
    }

    public static DataSet EjecutarComandoExterno(SqlCommand command, string Controller)
    {
        try
        {
            string conexionName = VariableGlobal.StrNombreConexionBD;
              
            string constr = ConfigurationManager.ConnectionStrings[conexionName].ConnectionString;
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand com = command;
                com.Connection = conn;
                com.CommandTimeout = 15000;
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = com };

                da.Fill(dataSet);
            }
            return dataSet;
        }
        catch (Exception errorException)
        {

            throw errorException;
        }
    }

    /// <summary>
    /// Funcion que ejecuta un STRING de un query
    /// </summary>
    /// <param name="idEmpresa">Id de la empresa</param>
    /// <param name="idUsuario">Id del usuario</param>
    /// <param name="textToExecute">Query a ejecutar</param>
    /// <param name="Controller">Controlador</param>
    /// <param name="guardaError">Bandera para indicar si guarda el error</param>
    /// <returns></returns>
    public DataSet EjecutarTexto(int idEmpresa, int idUsuario, string textToExecute, string Controller, bool guardaError = false)
    {
        var QuerysExcluidos = new[] { "drop", "delete", "eliminar", "truncate", "update", "actualizar", "insert", "insertar" };

        try
        {
            if (QuerysExcluidos.Any(x => textToExecute.ToLower().Contains(x)))
            {
                throw new Exception("El query: '" + textToExecute + "' no está permitido.");
            }
            string constr = ConfigurationManager.ConnectionStrings[VariableGlobal.StrNombreConexionBD].ConnectionString;
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand com = new SqlCommand(textToExecute, conn);
                com.CommandTimeout = 15000;
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = com };
                da.Fill(dataSet);
            }
            return dataSet;
        }
        catch (Exception errorException)
        {
            if (guardaError)
            {

                try
                {
                    //string sQuery = $"INSERT INTO erpEstadistica.aplicacion.registroError VALUES ({idEmpresa},{idUsuario},GETDATE(), GETDATE(),'{errorException.Message.Replace("'", "\"")}','{errorException.StackTrace.Replace("'", "\"")}','{textToExecute.Replace("'", "\"")}',NULL,'{Controller}')";
                    //EjecutarTexto(idEmpresa, idUsuario, sQuery, Controller, true);
                    RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar insertar el Registro Acceso, IDRegistroError: ", errorException.HResult, errorException.Message, (int)Severidad.BAJA);
                }
                catch (Exception errorException2)
                {

                }
            }
        }
        return null;
    }

    public static DataSet EjecutarComandoEstadistica(SqlCommand command)
    {
        try
        {
            string constr = ""; //ConfigurationManager.ConnectionStrings[VariableGlobal.StrNombreConexionBDEstadistica].ConnectionString;
            DataSet dataSet = new DataSet();
            //using (SqlConnection conn = new SqlConnection(constr))
            //{
            //    SqlCommand com = command;
            //    com.Connection = conn;
            //    com.CommandTimeout = 15000;
            //    SqlDataAdapter da = new SqlDataAdapter { SelectCommand = com };

            //    da.Fill(dataSet);
            //}
            return dataSet;
        }
        catch (Exception errorException)
        {
            throw errorException;
        }
        return null;
    }

    //funcion que ejecuta un comando de SQL
    public DataSet EjecutarComandoRegistroAcceso()
    {
        try
        {
            DateTime DtFechaInicio = DateTime.Now;
            string conexionName = string.Empty;
            
            if (this.IntIdEmpresa != 0)
            {
                switch (this.IntIdEmpresa)
                {
                    case 1:
                        conexionName = VariableGlobal.StrNombreConexionBD;
                        break;
                    //case 2:
                    //    conexionName = "erp2";
                    //    break;
                    //case 3:
                    //    conexionName = "erp3";
                    //    break;
                    default:
                        conexionName = VariableGlobal.StrNombreConexionBD;
                        break;
                }
            }
            else
            {
                conexionName = "bolsa";
            }
            string constr = ConfigurationManager.ConnectionStrings[conexionName].ConnectionString;
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand com = this.SqlCommandComando;
                com.Connection = conn;
                com.CommandTimeout = 15000;
                SqlDataAdapter da = new SqlDataAdapter { SelectCommand = com };

                da.Fill(dataSet);
            }

            //--------Registro Acceso
            if (this.IntBRegistrarAcceso == 1)
            {
                string StrDescStoreProcedure = this.SqlCommandComando.CommandText;
                DateTime DtFechaFin = DateTime.Now;
                double segundos = Math.Abs((DtFechaInicio - DtFechaFin).TotalSeconds);
                decimal DecSegundos = (decimal)segundos;
                RegistroError registroError = new RegistroError();
                string StrDescStoreProcedureParametro = registroError.ObtenerParametros(this.SqlParametro);
                int IntBError = 0;
                int IntErrorCodigo = 0;
                string StrErrorMensaje = "";
                bool bool_EsNumero = true;
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    for(int i = 0; i < dataSet.Tables.Count; i++)
                    {
                        if (dataSet.Tables[i].Rows.Count > 0)
                        {
                            IntBError = int.Parse(dataSet.Tables[i].Columns.Contains("Error") ? dataSet.Tables[i].Rows[0]["Error"].ToString() : "0");
                            StrErrorMensaje = dataSet.Tables[i].Columns.Contains("MensajeError") ? dataSet.Tables[i].Rows[0]["MensajeError"].ToString() : "";
                            if (StrErrorMensaje.Contains("IdRegistroError:"))
                            {
                                string[] codigoError = StrErrorMensaje.Split(new string[] { "IdRegistroError:" }, StringSplitOptions.RemoveEmptyEntries);
                                bool_EsNumero = int.TryParse(codigoError[1].Trim(), out IntErrorCodigo);
                            }
                        }
                    }
                    
                } else
                {
                    IntBError = 1;
                    StrErrorMensaje = "No se obtuvo respuesta de la base de datos";
                }

                try
                {
                    string constrRegistroAcceso = ConfigurationManager.ConnectionStrings[conexionName].ConnectionString;
                    DataSet dataSetRegistroAcceso = new DataSet();
                    using (SqlConnection connRegistroAcceso = new SqlConnection(constrRegistroAcceso))
                    {
                        SqlCommand sqlCommandRegistroAcceso = new SqlCommand();
                        sqlCommandRegistroAcceso.CommandText = "erpEstadisticaCompradores.aplicacion.spRegistroAccesoInsertar";
                        sqlCommandRegistroAcceso.CommandType = CommandType.StoredProcedure;
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_Ejecuta", 0);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_IdAcceso", this.IntIdAcceso);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_IdTerminal", this.IntIdTeminal);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_IdMultilistaTipoRegistroProceso", 0);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_FechaInicio", DtFechaInicio);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_FechaFin", DtFechaFin);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_Segundos", DecSegundos);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_IdUsuario", this.IntIdUsuario);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_DescStoreProcedure", StrDescStoreProcedure);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_DescStoreProcedureParametro", StrDescStoreProcedureParametro);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_Ruta", this.StrDescRuta);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_DescModelo", this.StrDescModelo);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_DescModeloMetodo", this.StrDescModeloMetodo);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_ErrorCodigo", IntErrorCodigo);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_ErrorMensaje", StrErrorMensaje);
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_BError", IntBError);

                        RegistroError objRegistroError = new RegistroError(sqlCommandRegistroAcceso.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
                        sqlCommandRegistroAcceso.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

                        sqlCommandRegistroAcceso.Connection = connRegistroAcceso;
                        sqlCommandRegistroAcceso.CommandTimeout = 15000;
                        SqlDataAdapter daRegistroAcceso = new SqlDataAdapter { SelectCommand = sqlCommandRegistroAcceso };

                        daRegistroAcceso.Fill(dataSetRegistroAcceso);
                    }

                }
                catch (Exception e)
                {
                    RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar insertar el Registro Acceso, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
                }
            }
            //-----Termina registro acceso

            return dataSet;
        }
        catch (Exception errorException)
        {
            throw errorException;
        }
        return null;
    }
}