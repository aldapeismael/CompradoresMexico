using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioEmpresa
/// </summary>
public class UsuarioEmpresa : IMetodosModelos<UsuarioEmpresa>
{
    #region atributos de la tabla
    int _IntIdUsuarioEmpresa;
    int _IntIdUsuario;
    int _IntIdEmpresa;
    int _IntIdPerfil;
    int _IntBAccesoRemoto;
    int _IntIdTerminal_Predeterminada;
    int _intIdTipoUsuario;
    int _intIdPersonaRH;
    int _intIdPuestoRH;
    string _DescPersona;
    string _DescPuesto;
    int _IntBActivo;

    //parametros adicionales
    List<UsuarioTerminal> _LstObjUsuarioTerminal;
    List<UsuarioAplicacionComplementaria> _LstObjUsuarioAplicacionComplementaria;

    //parametros adicionales para el datatable
    string _StrNomEmpresa;
    string _StrTerminales;
    string _StrAplicaciones;
    string _StrDescPerfil;

    public int IntIdUsuarioEmpresa
    {
        get
        {
            return _IntIdUsuarioEmpresa;
        }

        set
        {
            _IntIdUsuarioEmpresa = value;
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

    public int IntIdEmpresa
    {
        get
        {
            return _IntIdEmpresa;
        }

        set
        {
            _IntIdEmpresa = value;
        }
    }

    public int IntIdPerfil
    {
        get
        {
            return _IntIdPerfil;
        }

        set
        {
            _IntIdPerfil = value;
        }
    }

    public int IntBAccesoRemoto
    {
        get
        {
            return _IntBAccesoRemoto;
        }

        set
        {
            _IntBAccesoRemoto = value;
        }
    }

    public int IntIdTerminal_Predeterminada
    {
        get
        {
            return _IntIdTerminal_Predeterminada;
        }

        set
        {
            _IntIdTerminal_Predeterminada = value;
        }
    }
    public int intIdTipoUsuario
    {
        get
        {
            return _intIdTipoUsuario;
        }

        set
        {
            _intIdTipoUsuario = value;
        }
    }
    public int intIdPersonaRH
    {
        get
        {
            return _intIdPersonaRH;
        }

        set
        {
            _intIdPersonaRH = value;
        }
    }
    public int intIdPuestoRH
    {
        get
        {
            return _intIdPuestoRH;
        }

        set
        {
            _intIdPuestoRH = value;
        }
    }
    public string DescPersona
    {
        get
        {
            return _DescPersona;
        }

        set
        {
            _DescPersona = value;
        }
    }
    public string DescPuesto
    {
        get
        {
            return _DescPuesto;
        }

        set
        {
            _DescPuesto = value;
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

    public List<UsuarioTerminal> LstObjUsuarioTerminal
    {
        get
        {
            return _LstObjUsuarioTerminal;
        }

        set
        {
            _LstObjUsuarioTerminal = value;
        }
    }

    public List<UsuarioAplicacionComplementaria> LstObjUsuarioAplicacionComplementaria
    {
        get
        {
            return _LstObjUsuarioAplicacionComplementaria;
        }

        set
        {
            _LstObjUsuarioAplicacionComplementaria = value;
        }
    }

    public string StrNomEmpresa
    {
        get
        {
            return _StrNomEmpresa;
        }

        set
        {
            _StrNomEmpresa = value;
        }
    }

    public string StrTerminales
    {
        get
        {
            return _StrTerminales;
        }

        set
        {
            _StrTerminales = value;
        }
    }

    public string StrAplicaciones
    {
        get
        {
            return _StrAplicaciones;
        }

        set
        {
            _StrAplicaciones = value;
        }
    }

    public string StrDescPerfil
    {
        get
        {
            return _StrDescPerfil;
        }

        set
        {
            _StrDescPerfil = value;
        }
    }


    #endregion

    #region constructores

    public UsuarioEmpresa()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public UsuarioEmpresa(int ParamIntidUsuarioEmpresa)
    {
        this.IntIdUsuarioEmpresa = ParamIntidUsuarioEmpresa;
    }


    #endregion

    #region Metodos
    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var SesionIntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var SesionIntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioEmpresaActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            //parametros de la tabla
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", this.IntIdUsuario);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idEmpresa", this.IntIdEmpresa);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPerfil", this.IntIdPerfil);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_bAccesoRemoto", this.IntBAccesoRemoto);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idTerminalPredeterminada", this.IntIdTerminal_Predeterminada);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idTipoUsuario", this.intIdTipoUsuario);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPersonaRH", this.intIdPersonaRH);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPuestoRH", this.intIdPuestoRH);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_bActivo", this.IntBActivo);//PARAMETRO PUBLICO

            //objetos 
            sqlCommand.Parameters.AddWithValue("@p_ListaTerminales", JsonConvert.SerializeObject(this.LstObjUsuarioTerminal));
            sqlCommand.Parameters.AddWithValue("@p_ListaAplicacionesComplementarias", JsonConvert.SerializeObject(this.LstObjUsuarioAplicacionComplementaria));

            /********************************************************** Objeto Registro Error **************************************************************************************/

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            /***********************************************************************************************************************************************************************/

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo : Cliente.cs, metodo : Insertar()");

            if (dataSetActualizar != null && dataSetActualizar.Tables.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetActualizar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetActualizar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetActualizar.Tables[0].Rows[0]["TipoError"].ToString()
                );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }

        }
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de actualizar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error", 0);
            /********************************************************************************************************************************************************************************/
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var SesionIntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var SesionIntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioEmpresaInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            //parametros de la tabla
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", this.IntIdUsuario);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idEmpresa", this.IntIdEmpresa);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPerfil", this.IntIdPerfil);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_bAccesoRemoto", this.IntBAccesoRemoto);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idTerminalPredeterminada", this.IntIdTerminal_Predeterminada);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idTipoUsuario", this.intIdTipoUsuario);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPersonaRH", this.intIdPersonaRH);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_idPuestoRH", this.intIdPuestoRH);//PARAMETRO PUBLICO
            sqlCommand.Parameters.AddWithValue("@p_bActivo", this.IntBActivo);//PARAMETRO PUBLICO

            //objetos 
            sqlCommand.Parameters.AddWithValue("@p_ListaTerminales", JsonConvert.SerializeObject(this.LstObjUsuarioTerminal));
            sqlCommand.Parameters.AddWithValue("@p_ListaAplicacionesComplementarias", JsonConvert.SerializeObject(this.LstObjUsuarioAplicacionComplementaria));

            /********************************************************** Objeto Registro Error **************************************************************************************/

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            /***********************************************************************************************************************************************************************/

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo : UsuarioEmpresa.cs, metodo : Insertar()");

            if (dataSetInsertar != null && dataSetInsertar.Tables.Count > 0)
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
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error", 0);
            /********************************************************************************************************************************************************************************/
        }

        return objRespuestaBD;
    }

    public bool ObtenerPorId()
    {
        bool bool_valido = false;

        var Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var Session_IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spUsuarioEmpresaObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_IdUsuarioEmpresa", this.IntIdUsuarioEmpresa);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(Session_IntIdEmpresa, Session_IntIdUsuario, sqlcommand, "archivo : Usuario.cs => ObtenerPorId");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                
                IntIdUsuario = int.Parse(dataSetObtener.Tables[0].Rows[0]["idUsuario"].ToString());
                IntIdEmpresa = int.Parse(dataSetObtener.Tables[0].Rows[0]["idEmpresa"].ToString());
                IntIdPerfil = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPerfil"].ToString());
                IntIdTerminal_Predeterminada = int.Parse(dataSetObtener.Tables[0].Rows[0]["idTerminal_predeterminada"].ToString());
                intIdTipoUsuario = int.Parse(dataSetObtener.Tables[0].Rows[0]["TipoUsuario"].ToString());
                intIdPersonaRH = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPersonaRH"].ToString());
                intIdPuestoRH = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPuestoRH"].ToString());
                DescPersona = dataSetObtener.Tables[0].Rows[0]["Perona"].ToString();
                DescPuesto = dataSetObtener.Tables[0].Rows[0]["Puesto"].ToString();
                IntBAccesoRemoto = int.Parse(dataSetObtener.Tables[0].Rows[0]["bAccesoRemoto"].ToString());
                IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());
                StrNomEmpresa = dataSetObtener.Tables[0].Rows[0]["descEmpresa"].ToString();

                LstObjUsuarioTerminal = ObtenerListaUsuarioTerminal(IntIdUsuario, IntIdEmpresa);
                LstObjUsuarioAplicacionComplementaria = ObtenerListaUsuarioAplicaciones(IntIdUsuario, IntIdEmpresa);

                bool_valido = true;
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return bool_valido;
    }

    public RespuestaDataTable<UsuarioEmpresa> ResultadoDataTables(string filtros)
    {
        FiltrosDTUsuarioEmpresa objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTUsuarioEmpresa>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<UsuarioEmpresa> UsuarioListado = new List<UsuarioEmpresa>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioEmpresaObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);
            //filtros
            sqlCommand.Parameters.AddWithValue("@p_FiltroIntIdUsuario", objFiltroDt.FiltroIntIdUsuario);
            

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: UsuarioEmpresa.cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaDataset in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    UsuarioListado.Add(new UsuarioEmpresa
                    {
                        IntIdUsuarioEmpresa = int.Parse(filaDataset["idUsuarioEmpresa"].ToString()),
                        StrNomEmpresa = filaDataset["nomEmpresa"].ToString(),
                        StrTerminales = filaDataset["terminales"].ToString(),
                        StrAplicaciones = filaDataset["aplicaciones"].ToString(),
                        StrDescPerfil = filaDataset["descPerfil"].ToString()
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return new RespuestaDataTable<UsuarioEmpresa>
        {
            data = UsuarioListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    #endregion

    #region Metodos extra

    public static List<UsuarioTerminal> ObtenerListaUsuarioTerminal(int ParamIntIdUsuario, int ParamIntIdEmpresa)
    {
        List<UsuarioTerminal> objDatosContacto = new List<UsuarioTerminal>();
        UsuarioTerminal objContacto = new UsuarioTerminal();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioTerminalObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", ParamIntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_idEmpresa", ParamIntIdEmpresa);
            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: CompaniaTransportista");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow dataRowFila in dataSetObtener.Tables[0].Rows)
                {
                    objContacto = new UsuarioTerminal
                    {
                        IntIdUsuarioTerminal = int.Parse(dataRowFila["idUsuarioTerminal"].ToString()),
                        IntIdUsuario = int.Parse(dataRowFila["idUsuario"].ToString()),
                        IntIdTerminal = int.Parse(dataRowFila["idTerminal"].ToString()),
                        IntIdEmpresa = int.Parse(dataRowFila["idEmpresa"].ToString()),
                        IntBActivo = int.Parse(dataRowFila["bActivo"].ToString())
                    };
                    objDatosContacto.Add(objContacto);
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "UsuarioEmpresa.cs", "UsuarioEmpresaController.cs", "Error al obtener las terminales, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objDatosContacto;
    }
    public static List<UsuarioAplicacionComplementaria> ObtenerListaUsuarioAplicaciones(int ParamIntIdUsuario, int ParamIntIdEmpresa)
    {
        List<UsuarioAplicacionComplementaria> objDatosContacto = new List<UsuarioAplicacionComplementaria>();
        UsuarioAplicacionComplementaria objContacto = new UsuarioAplicacionComplementaria();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioAplicacionComplementariaObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", ParamIntIdUsuario);
            sqlCommand.Parameters.AddWithValue("@p_idEmpresa", ParamIntIdEmpresa);
            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: CompaniaTransportista");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                foreach (DataRow dataRowFila in dataSetObtener.Tables[0].Rows)
                {
                    objContacto = new UsuarioAplicacionComplementaria
                    {
                        IntIdUsuarioAplicacionComplementaria = int.Parse(dataRowFila["idUsuarioAplicacionComplementaria"].ToString()),
                        IntIdUsuario = int.Parse(dataRowFila["idUsuario"].ToString()),
                        IntIdAplicacionComplementaria = int.Parse(dataRowFila["idAplicacionComplementaria"].ToString()),
                        IntIdEmpresa = int.Parse(dataRowFila["idEmpresa"].ToString()),
                        IntBActivo = int.Parse(dataRowFila["bActivo"].ToString())
                    };
                    objDatosContacto.Add(objContacto);
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "UsuarioEmpresa.cs", "UsuarioEmpresaController.cs", "Error al obtener las aplicaciones complementarias, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objDatosContacto;
    }
    #endregion
}

public class FiltrosDTUsuarioEmpresa : DataTableAjaxPostModel
{
    public int FiltroIntIdUsuario
    {
        get; set;
    }
}