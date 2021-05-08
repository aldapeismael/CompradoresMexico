using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Web.WebPages.Html;

/// <summary>
/// Summary description for Usuario
/// </summary>
public class Usuario : IMetodosModelos<Usuario>
{
    #region Propiedades

    //parametros de la tabla
    int _IntIdUsuario;
    string _StrCveUsuario;
    string _StrDescUsuario;
    int _IntBActivo;
    string _StrContrasena;
    string _StrEmail;
    int _IntIdMultilista_TipoUsuario;
    int _IntIdBVigenciaContrasena;
    DateTime _DtFechaVigenciaContrasena;
    int _IntBMultiempresa;
    string _StrPerfilesUsuario;
    string _StrTerminalesUsuario;

    //parametros fuera de la tabla
    private int _IntCantidadCodigos;
    string _StrEmpresasUsuario;

    public int IntIdUsuario
    {
        get { return _IntIdUsuario; }
        set { _IntIdUsuario = value; }
    }

    public string StrCveUsuario
    {
        get { return _StrCveUsuario; }
        set { _StrCveUsuario = value; }
    }

    public string StrDescUsuario
    {
        get { return _StrDescUsuario; }
        set { _StrDescUsuario = value; }
    }

    public int IntCantidadCodigos
    {
        get { return _IntCantidadCodigos; }
        set { _IntCantidadCodigos = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
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
    

    public string StrEmail
    {
        get
        {
            return _StrEmail;
        }

        set
        {
            _StrEmail = value;
        }
    }

    public int IntIdMultilista_TipoUsuario
    {
        get
        {
            return _IntIdMultilista_TipoUsuario;
        }

        set
        {
            _IntIdMultilista_TipoUsuario = value;
        }
    }

    public int IntIdBVigenciaContrasena
    {
        get
        {
            return _IntIdBVigenciaContrasena;
        }

        set
        {
            _IntIdBVigenciaContrasena = value;
        }
    }

    public DateTime DtFechaVigenciaContrasena
    {
        get
        {
            return _DtFechaVigenciaContrasena;
        }

        set
        {
            _DtFechaVigenciaContrasena = value;
        }
    }

    public int IntBMultiempresa
    {
        get
        {
            return _IntBMultiempresa;
        }

        set
        {
            _IntBMultiempresa = value;
        }
    }

    public string StrEmpresasUsuario
    {
        get
        {
            return _StrEmpresasUsuario;
        }

        set
        {
            _StrEmpresasUsuario = value;
        }
    }

    public string StrPerfilesUsuario
    {
        get
        {
            return _StrPerfilesUsuario;
        }

        set
        {
            _StrPerfilesUsuario = value;
        }
    }

    public string StrTerminalesUsuario
    {
        get
        {
            return _StrTerminalesUsuario;
        }

        set
        {
            _StrTerminalesUsuario = value;
        }
    }


    #endregion
    #region Contructores

    public Usuario()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Usuario(int intIdUsuario)
    {
        _IntIdUsuario = intIdUsuario;
    }

    public Usuario(int intIdUsuario, string strCveUsuario, string strDescUsuario, int intCantidadCodigos, int intBActivo)
    {
        _IntIdUsuario = intIdUsuario;
        _StrCveUsuario = strCveUsuario;
        _StrDescUsuario = strDescUsuario;
        _IntCantidadCodigos = intCantidadCodigos;
        _IntBActivo = intBActivo;
    }

    #endregion




    public RespuestaDataTable<Usuario> ResultadoDataTablesAutorizacionCodigo(string filtros)
    {
        FiltrosDTUsuario objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTUsuario>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<Usuario> UsuarioListado = new List<Usuario>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "catalogos.spAutorizacionCodigoObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);

            if (objFiltroDt.IntActivo >= 0)
                sqlCommand.Parameters.AddWithValue("@p_Activo", objFiltroDt.IntActivo);
            if (!string.IsNullOrEmpty(objFiltroDt.StrUsuario))
                sqlCommand.Parameters.AddWithValue("@p_Usuario", objFiltroDt.StrUsuario.Trim());

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: AutorizacionCodigo.cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaAutorizacionCodigos in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    UsuarioListado.Add(new Usuario(
                        int.Parse(filaAutorizacionCodigos["IdUsuario"].ToString()),
                        filaAutorizacionCodigos["cveUsuario"].ToString(),
                        filaAutorizacionCodigos["descUsuario"].ToString(),
                        int.Parse(filaAutorizacionCodigos["codigos"].ToString()),
                        int.Parse(filaAutorizacionCodigos["bActivo"].ToString())
                    ));
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception ex)
        {
            // Crear tabla para agregar excepciones
        }

        return new RespuestaDataTable<Usuario>
        {
            data = UsuarioListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    public List<Usuario> ObtenerListaGenerica(ObjetoBusqueda filtrosGenericos)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        var IntBrobot = VariableGlobal.SessionIntBRobot;
        List<Usuario> lstObjUsuario = new List<Usuario>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", filtrosGenericos.IntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IdGenerico1", filtrosGenericos.IntId1);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico1", filtrosGenericos.StrDes1);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico2", filtrosGenericos.StrDes2);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBrobot);

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSet = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : usuario.cs, metodo : ObtenerListaGenerica()");

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                foreach (DataRow FilaUsuario in dataSet.Tables[0].Rows)
                {
                    lstObjUsuario.Add(new Usuario
                    {
                        IntIdUsuario = dataSet.Tables[0].Columns.Contains("idusuario") ? int.Parse(FilaUsuario["idusuario"].ToString()) : 0,
                        StrCveUsuario = dataSet.Tables[0].Columns.Contains("cveUsuario") ? FilaUsuario["cveUsuario"].ToString() : "",
                        StrDescUsuario = dataSet.Tables[0].Columns.Contains("descUsuario") ? FilaUsuario["descUsuario"].ToString() : ""
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Usuarios, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return lstObjUsuario;
    }


    public RespuestaDataTable<Usuario> ResultadoDataTables(string filtros)
    {
        FiltrosDTUsuario objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTUsuario>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<Usuario> UsuarioListado = new List<Usuario>();
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);

            sqlCommand.Parameters.AddWithValue("@p_FiltroUsuario", objFiltroDt.StrUsuario);
            sqlCommand.Parameters.AddWithValue("@p_FiltroActivo", objFiltroDt.IntActivo);
            sqlCommand.Parameters.AddWithValue("@p_FiltroIdEmpresa", objFiltroDt.IntEmresa);
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Usuario.cs, metodo: ResultadoDataTables()");
            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow filaDataset in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    var FechaVigencia = filaDataset["fechaVigenciaContrasena"].ToString();

                    var StrFechaVigencia_ = (String.IsNullOrEmpty(FechaVigencia)) ? DateTime.Parse("1900-01-01") : DateTime.Parse(FechaVigencia) ;
                        
                        

                    UsuarioListado.Add(new Usuario
                    {
                        IntIdUsuario = int.Parse(filaDataset["idusuario"].ToString()),
                        StrDescUsuario = filaDataset["descUsuario"].ToString(),
                        StrCveUsuario = filaDataset["cveUsuario"].ToString(),
                        StrContrasena = (VariableGlobal.SessionIntBRobot == 1) ? filaDataset["contrasena"].ToString()  : "***************",
                        DtFechaVigenciaContrasena = StrFechaVigencia_,
                        StrEmail = filaDataset["email"].ToString(),
                        StrEmpresasUsuario = filaDataset["empresas"].ToString(),
                        IntBActivo = int.Parse(filaDataset["bActivo"].ToString()),
                        StrTerminalesUsuario = filaDataset["terminales"].ToString(),
                        StrPerfilesUsuario = filaDataset["perfiles"].ToString()
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de actualizar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return new RespuestaDataTable<Usuario>
        {
            data = UsuarioListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        DateTime DtFechaDefault = new DateTime(1, 1, 1);
        try
        {
            var Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var Session_IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            //reglas
            //var DtFechaHoraCompra = Convert.ToDateTime(this.StrFechaCompra);

            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spUsuarioInsertar";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            //parametros por default
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlcommand.Parameters.AddWithValue("@p_Debug", 0);
            //parametros para insertar
            sqlcommand.Parameters.AddWithValue("@p_descUsuario", this.StrDescUsuario);//PARAMETRO PUBLICO
            sqlcommand.Parameters.AddWithValue("@p_cveUsuario", this.StrCveUsuario);
            sqlcommand.Parameters.AddWithValue("@p_contrasena", this.StrContrasena);
            sqlcommand.Parameters.AddWithValue("@p_email", this.StrEmail);
            sqlcommand.Parameters.AddWithValue("@p_bActivo", this.IntBActivo);

            sqlcommand.Parameters.AddWithValue("@p_idMultilista_TipoUsuario", this.IntIdMultilista_TipoUsuario);
            sqlcommand.Parameters.AddWithValue("@p_bFechaVigenciaContrasena", this.IntIdBVigenciaContrasena);
            //sqlcommand.Parameters.AddWithValue("@p_fechaVigenciaContrasena", this.DtFechaVigenciaContrasena);
            if (this.DtFechaVigenciaContrasena != DtFechaDefault)
                sqlcommand.Parameters.AddWithValue("@p_fechaVigenciaContrasena", this.DtFechaVigenciaContrasena);
            sqlcommand.Parameters.AddWithValue("@p_bMultiempresa", this.IntBMultiempresa);

            DataSet dataSetInsertar = ConexionBD.EjecutarComando(Session_IntIdEmpresa, Session_IntIdUsuario, sqlcommand, "archivo : Usuario.cs, metodo : Insertar()");

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
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos al insertar", "error");
            }

        }
        catch (Exception e)
        {
            objRespuestaBD = new RespuestaBD(1, e.ToString(), "error");
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
            sqlcommand.CommandText = "corporativo.spUsuarioObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", this.IntIdUsuario);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(Session_IntIdEmpresa, Session_IntIdUsuario, sqlcommand, "archivo : Usuario.cs => ObtenerPorId");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                //var StrFechaHoraCompra = string.Format(dataSetObtener.Tables[0].Rows[0]["fechaCompra"].ToString());
                //var DtFechaHoraCompra = (DateTime.Parse(dataSetObtener.Tables[0].Rows[0]["fechaCompra"].ToString())).ToString("dd/MM/yyyy");

                StrDescUsuario = dataSetObtener.Tables[0].Rows[0]["descUsuario"].ToString();
                StrCveUsuario = dataSetObtener.Tables[0].Rows[0]["cveUsuario"].ToString();
                StrContrasena = dataSetObtener.Tables[0].Rows[0]["contrasena"].ToString();
                StrEmail = dataSetObtener.Tables[0].Rows[0]["email"].ToString();
                IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());

                IntIdMultilista_TipoUsuario = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMultilista_tipoUsuario"].ToString());
                IntIdBVigenciaContrasena = int.Parse(dataSetObtener.Tables[0].Rows[0]["bVigenciaContrasena"].ToString());
                DtFechaVigenciaContrasena = DateTime.Parse(dataSetObtener.Tables[0].Rows[0]["FechaVigenciaContrasena"].ToString());
                IntBMultiempresa = int.Parse(dataSetObtener.Tables[0].Rows[0]["bMultiEmpresa"].ToString());

                bool_valido = true;
            }
        }
        catch (Exception e) { }

        return bool_valido;
    }

    public bool ObtenerPorCve()
    {
        bool bool_valido = false;

        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spUsuarioObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 2);
            sqlcommand.Parameters.AddWithValue("@p_cveUsuario", this.StrCveUsuario);

            DataSet dataSetObtener = ConexionBD.EjecutarComandoExterno(sqlcommand, "archivo : Usuario.cs => ObtenerPorId");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                IntIdUsuario = int.Parse(dataSetObtener.Tables[0].Rows[0]["idUsuario"].ToString());
                bool_valido = true;
            }
        }
        catch (Exception e) { }

        return bool_valido;
    }

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            var Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var Session_IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spUsuarioActualizar";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlcommand.Parameters.AddWithValue("@p_Debug", 0);

            //parametros para actualizar
            sqlcommand.Parameters.AddWithValue("@p_IdUsuario", IntIdUsuario);
            sqlcommand.Parameters.AddWithValue("@p_descUsuario", StrDescUsuario);
            sqlcommand.Parameters.AddWithValue("@p_cveUsuario", StrCveUsuario);
            sqlcommand.Parameters.AddWithValue("@p_contrasena", StrContrasena);
            sqlcommand.Parameters.AddWithValue("@p_email", StrEmail);
            sqlcommand.Parameters.AddWithValue("@p_bActivo", IntBActivo);

            sqlcommand.Parameters.AddWithValue("@p_idMultilista_TipoUsuario", this.IntIdMultilista_TipoUsuario);
            sqlcommand.Parameters.AddWithValue("@p_bFechaVigenciaContrasena", this.IntIdBVigenciaContrasena);
            if (this.DtFechaVigenciaContrasena != DateTime.Parse("01/01/0001"))
            {
                sqlcommand.Parameters.AddWithValue("@p_fechaVigenciaContrasena", this.DtFechaVigenciaContrasena);
            }
            sqlcommand.Parameters.AddWithValue("@p_bMultiempresa", this.IntBMultiempresa);
            sqlcommand.Parameters.AddWithValue("@p_Roobot", VariableGlobal.SessionIntBRobot);

            DataSet dataSetActualizar = ConexionBD.EjecutarComando(Session_IntIdEmpresa, Session_IntIdUsuario, sqlcommand, "archivo : Usuario.cs, metodo : Actualizar()");

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
            objRespuestaBD = new RespuestaBD(1, e.ToString(), "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        throw new NotImplementedException();
    }

    public static ResultadoBusqueda ObtenerAutoComplete(int ParamIntLimite, int ParamIntTipoBusqueda, string ParamStrBuscar)
    {
        List<ListaBusqueda> objListaBusqueda = new List<ListaBusqueda>();
        try
        {
            var Session_IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var Session_IntIdTerminal = VariableGlobal.SessionIntIdTerminal;
            if (ParamStrBuscar == "empty") ParamStrBuscar = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtenerAutoComplete";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamIntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_Limite", ParamIntLimite);
            sqlCommand.Parameters.AddWithValue("@p_Buscar", ParamStrBuscar);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", Session_IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_IdTerminal", Session_IntIdTerminal);
            var dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: CuentaContable");
            foreach (DataRow objFilaCuentaContable in dataSetObtenerLista.Tables[0].Rows)
            {
                objListaBusqueda.Add(new ListaBusqueda
                {
                    Value = int.Parse(dataSetObtenerLista.Tables[0].Columns.Contains("idUsuario") ? objFilaCuentaContable["idUsuario"].ToString() : "0"),
                    Text = dataSetObtenerLista.Tables[0].Columns.Contains("descUsuario") ? objFilaCuentaContable["descUsuario"].ToString() : "",//.Substring(0, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(4, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(8, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(12, 4) : "",
                    Des1 = dataSetObtenerLista.Tables[0].Columns.Contains("cveUsuario") ? objFilaCuentaContable["cveUsuario"].ToString() : "",
                    //Des1 = dataSetObtenerLista.Tables[0].Columns.Contains("cuentaContable") ? objFilaCuentaContable["cuentaContable"].ToString().Substring(0, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(4, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(8, 4) + "-" + objFilaCuentaContable["cuentaContable"].ToString().Substring(12, 4) + "      " + objFilaCuentaContable["descCuentaContable"].ToString() : "",
                });
            }
        }
        catch (Exception ex)
        {
        }
        return new ResultadoBusqueda()
        {
            listResults = objListaBusqueda,
            totalResponse = objListaBusqueda.Count,
            inputPhrase = ParamStrBuscar
        };
    }
    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            //sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: UnidadNegocio");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();
            }
        }
        catch (Exception ex)
        {
        }
        return objLista;
    }


    public static ResultadoBusqueda ObtenerAutoComplete(ListaParametrosBusqueda objListaBusqueda)
    {
        List<ListaBusqueda> listObjListaBusqueda = new List<ListaBusqueda>();
        try
        {
            if (string.IsNullOrEmpty(objListaBusqueda.StrBuscar)) objListaBusqueda.StrBuscar = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spUsuarioObtenerAutoComplete";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", objListaBusqueda.IntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_Limite", objListaBusqueda.IntLimite);
            sqlCommand.Parameters.AddWithValue("@p_Buscar", objListaBusqueda.StrBuscar);

            var dataSetObtenerLista = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: Usuario");
            foreach (DataRow objFila in dataSetObtenerLista.Tables[0].Rows)
            {
                listObjListaBusqueda.Add(new ListaBusqueda
                {
                    Value = long.Parse(dataSetObtenerLista.Tables[0].Columns.Contains("idUsuario") ? objFila["idUsuario"].ToString() : "0"),
                    Text = dataSetObtenerLista.Tables[0].Columns.Contains("descUsuario") ? objFila["descUsuario"].ToString() : "",
                    Des1 = dataSetObtenerLista.Tables[0].Columns.Contains("cveUsuario") ? objFila["cveUsuario"].ToString() : "",
                    Des2 = dataSetObtenerLista.Tables[0].Columns.Contains("email") ? objFila["email"].ToString() : "",
                });
            }
        }
        catch (Exception ex)
        {
        }
        return new ResultadoBusqueda()
        {
            listResults = listObjListaBusqueda,
            totalResponse = listObjListaBusqueda.Count,
            inputPhrase = objListaBusqueda.StrBuscar
        };
    }
}
public class FiltrosDTUsuario : DataTableAjaxPostModel
{
    public string StrUsuario { get; set; }
    public int IntActivo { get; set; }
    public int IntEmresa { get; set; }
}