using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatatablesPopup
/// </summary>
public class DatatablesPopup
{
    #region Propiedades
    public string StrDescEjecuta { get; set; }

    public int Id { get; set; }
    public int IdGenerico1 { get; set; }
    public int IdGenerico2 { get; set; }
    public int IdGenerico3 { get; set; }
    public int IdGenerico4 { get; set; }
    public int IdGenerico5 { get; set; }
    public string ParamIntNombre1 { get; set; }
    public string ParamIntNombre2 { get; set; }
    public string ParamIntNombre3 { get; set; }
    public string ParamIntNombre4 { get; set; }
    public string ParamIntNombre5 { get; set; }


    public string StringGenerico1 { get; set; }
    public string StringGenerico2 { get; set; }
    public string StringGenerico3 { get; set; }
    public string StringGenerico4 { get; set; }
    public string ParamStringNombre1 { get; set; }
    public string ParamStringNombre2 { get; set; }
    public string ParamStringNombre3 { get; set; }
    public string ParamStringNombre4 { get; set; }

    public string ParamDtFechaNombre1 { get; set; }
    public DateTime DateTimeGenerico1 { get; set; }

    /// <summary>
    /// Query que se va a ejecutar
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    /// Tipo de query que se va a ejecutar
    /// </summary>
    public TipoQuery TipoQuery { get; set; }

    /// <summary>
    /// Mensaje de error que se envia cuando algo no sale bien
    /// </summary>
    public string Error { get; set; }

    //Tipo de tabla (Normal, CheckBox, RadioButton)
    public Selectores TipoSelector { get; set; }

    // Arreglo para saber los campos que se van a ocultar
    public List<string> CamposOcultos { get; set; }

    // Arreglo para saber los campos que serán link a popup
    public List<string> CamposPopup { get; set; }

    public string Controlador { get; set; }

    #endregion

    #region Constructores

    public DatatablesPopup()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    // Declaración de métodos

    #region Metodos

    /// <summary>
    /// Método para obtener los datos de un query genérico basado en los parámetros del objeto
    /// </summary>
    /// <returns></returns>
    public List<List<string>> fn_obtenerDatos()
    {
        List<List<string>> listGlobal = new List<List<string>>();
        List<string> list = new List<string>();

        //se manda ejecutar el query desde la clase DatabaseConection ubicada en Helpers
        var conexion = new ConexionBD();
        var dataSet = conexion.EjecutarTexto(1, 1, Query, Controlador, true);

        if (dataSet != null && dataSet.Tables.Count > 0)
        {
            // For para obtener los encabezados de la tabla
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                bool pintar = true;
                for (int x = 0; x < CamposOcultos.Count(); x++)
                {
					//bool bPintar = (CamposOcultos[x].Split(':')[2] != null ? 
					//	(int.Parse(CamposOcultos[x].Split(':')[2]) == 0 ? false : true) : 
					//	false);
                    bool bPintar = (CamposOcultos[x].Split(':')[0] != null ?
                        (int.Parse(CamposOcultos[x].Split(':')[0]) == 0 ? false : true) :
                        false);

                    int columna = Int32.Parse(CamposOcultos[x].Split(':')[0]);

                    if (columna == i)
                    {
                        pintar = false;
                        break;
                    }
                }

                if (pintar)
                {
                    list.Add(dataSet.Tables[0].Columns[i].ColumnName.ToString());                    
                }
            }

            listGlobal.Add(list);

            // For para obtener los datos de la tabla
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list = new List<string>();

                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {

                        // Si hay campos ocultos, armamos el string para agregarlo al input
                        string parametros = "";
                        for (int x = 0; x < CamposOcultos.Count(); x++)
                        {
                            int columna = Int32.Parse(CamposOcultos[x].Split(':')[0]);
                            parametros += " data-" + CamposOcultos[x].Split(':')[1] + "=\"" +
                                          dr[dataSet.Tables[0].Columns[columna].ColumnName].ToString() + " \"";
                        }

                        // validamos el tipo para pintar el check o el radio
                        switch (TipoSelector)
                        {
                            case Selectores.CheckBox:
                                list.Add("<input class=\"chk\" id=\"id['" +
                                         dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() +
                                         "']\" type=\"checkbox\" value=\"" +
                                         dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() + "\" " + parametros +
                                         ">");
                                break;
                            case Selectores.RadioButton:
                                list.Add("<input name=\"checkPopup\" class=\"chk\" id=\"id['" +
                                         dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() +
										 "']\" data-id=\"" +
                                         dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() +
                                         "\" type=\"radio\" value=\"" +
                                         dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() + "\" " + parametros +
                                         ">");
                                break;
                            default:
                                list.Add(dr[dataSet.Tables[0].Columns[i].ColumnName].ToString());
                                break;
                        }
                    }
                    //else if ((i + 1) == dataSet.Tables[0].Columns.Count && !dr[dataSet.Tables[0].Columns[i].ColumnName].ToString().Equals("")) {
                    //    list.Add("<a href=\"javascript:void(0)\" class=\"fn_popupDatosExtra\">" + dr[dataSet.Tables[0].Columns[i].ColumnName].ToString() + "</a>");
                    //} 
                    else
                    {
                        bool pintar = true;
						
                        for (int x = 0; x < CamposOcultos.Count(); x++)
                        {
                            //bool bPintar = (CamposOcultos[x].Split(':')[2] != null ? 
                            //	(int.Parse(CamposOcultos[x].Split(':')[2]) == 0 ? false : true) : 
                            //	false);
                            int columna = Int32.Parse(CamposOcultos[x].Split(':')[0]);

                            if (columna == i)
                            {
                                pintar = false;
                                break;
                            }
                        }

                        if (pintar)
                        {
                            string text = dr[dataSet.Tables[0].Columns[i].ColumnName].ToString();
                            for (int x = 0; x < CamposPopup.Count(); x++)
                            {
                                if (Int32.Parse(CamposPopup[x].Split(':')[0]) == i)
                                {
                                    string strDescMostrar = CamposPopup[x].Split(':')[2] != null ? CamposPopup[x].Split(':')[2]:dr[dataSet.Tables[0].Columns[i].ColumnName].ToString();
									text = "<a href=\"javascript:void(0)\" class=\"" + CamposPopup[x].Split(':')[1] +
                                           "\">" + strDescMostrar + "</a>";
                                }
                            }

                            list.Add(text);
                        }
                    }
                }

                listGlobal.Add(list);
            }
        }
        else
        {
            listGlobal = null;
        }

        return listGlobal;
    }

    /// <summary>
    /// Método usado para generar las validaciones del objeto instanciado
    /// </summary>
    /// <returns>Regresa la respuesta de las validaciones</returns>
    public bool fn_validaObjeto()
    {
        bool valido = true;

        if (String.IsNullOrEmpty(Query))
        {
            Error = "No se especificó una consulta. Favor de verificar.";
            valido = false;
        }

        if (TipoSelector != Selectores.CheckBox && TipoSelector != Selectores.RadioButton && TipoSelector != Selectores.Normal)
        {
            Error = "No se especificó el tipo de selector. Favor de verificar.";
            valido = false;
        }

        return valido;
    }

    public List<List<string>> ResultadoDataTables()
    {
        List<List<string>> listGlobal = new List<List<string>>();
        List<string> list = new List<string>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "corporativo.spEstadoObtenerDataTable";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
        sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", 0);
        sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", 1);
        sqlCommand.Parameters.AddWithValue("@p_FilaInicial", 1);
        sqlCommand.Parameters.AddWithValue("@p_FilaFinal", 10);

        DataSet dataSet = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Estado.cs, metodo: ResultadoDataTables()");

        //var dataSet = ConexionBD.EjecutarTexto(1, 1, sql, "DataTablePopUpController", true);//se manda ejecutar el query desde la clase DatabaseConection ubicada en Helpers

        for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
        {
            //value += dataSet.Tables[0].Columns[i].ColumnName.ToString() + ", ";
            list.Add(dataSet.Tables[0].Columns[i].ColumnName.ToString());
        }
        listGlobal.Add(list);

        foreach (DataRow dr in dataSet.Tables[0].Rows)
        {
            list = new List<string>();
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                //vals.Add(dt.Columns[i].ColumnName, dr[dt.Columns[i].ColumnName].ToString());
                list.Add(dr[dataSet.Tables[0].Columns[i].ColumnName].ToString());
            }
            listGlobal.Add(list);
        }

        listGlobal.Add(list);

        return listGlobal;
    }

    #endregion
}
public enum Selectores
{
    Normal = 0,
    CheckBox = 1,
    RadioButton = 2,
}