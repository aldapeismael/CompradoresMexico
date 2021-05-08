using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages.Html;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Empresa
/// </summary>
public class Empresa : IMetodosModelos<Empresa>
{
    #region Propiedades

    private int _IntIdEmpresa;
    private string _StrDescEmpresa;
    private string _StrCveEmpresa;
    private string _StrRfc;
    private string _StrTelefono;
    private string _StrCalle;
    private string _StrColonia;
    private string _StrNumeroExterior;
    private string _StrNumeroInterior;
    private string _StrCodigoPostal;
    private int _IntIdMunicipio;
    private string _StrRegistroCanacar;
    private int _IntIdSatCatalogoCfdi_RegimenFiscal;
    private int _IntIdPeriodo;
    private int _IntBActivo;
    private int _IntIdCuentaContable_utilidad;

    private int _IntIdCuentaContable_FacturaPorRecibir;
    private int _IntIdCuentaContable_FacturaPorRecibirDolares;
    private int _IntIdCuentaContable_FacturaPorRecibirDolaresCompl;
    private int _IntIdCuentaContable_Consumible;
    private int _IntIdCuentaContable_ManoDeObra;
    private int _IntIdCuentaContable_ManoDeObraComponente;

    private string _StrCuentaContable_formato;
    private string _StrCuentaContableFormato_FacturaPorRecibir;
    private string _StrCuentaContableFormato_FacturaPorRecibirDolares;
    private string _StrCuentaContableFormato_FacturaPorRecibirDolaresCompl;
    private string _StrCuentaContableFormato_Consumible;
    private string _StrCuentaContableFormato_ManoDeObra;
    private string _StrCuentaContableFormato_ManoDeObraCompon;


    private string _StrCveEmpresaReferencia;

    //Propiedad extra
    private RespuestaBD _objRespuestaDB;
    private int _IntLimite;
    private string _StrDescMunicipio;
    private string _StrDescEstado;
    private string _StrDescCdfiRegimenFiscal;
    private string _StrJsonPeriodo;

    #region Getter y Setter

    public string StrDescEmpresa
    {
        get { return _StrDescEmpresa; }
        set { _StrDescEmpresa = value; }
    }

    public string StrCveEmpresa
    {
        get { return _StrCveEmpresa; }
        set { _StrCveEmpresa = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdEmpresa
    {
        get { return _IntIdEmpresa; }
        set { _IntIdEmpresa = value; }
    }

    public string StrRfc
    {
        get { return _StrRfc; }
        set { _StrRfc = value; }
    }

    public string StrTelefono
    {
        get { return _StrTelefono; }
        set { _StrTelefono = value; }
    }

    public string StrCalle
    {
        get { return _StrCalle; }
        set { _StrCalle = value; }
    }

    public string StrColonia
    {
        get { return _StrColonia; }
        set { _StrColonia = value; }
    }

    public string StrNumeroExterior
    {
        get { return _StrNumeroExterior; }
        set { _StrNumeroExterior = value; }
    }

    public string StrNumeroInterior
    {
        get { return _StrNumeroInterior; }
        set { _StrNumeroInterior = value; }
    }

    public string StrCodigoPostal
    {
        get { return _StrCodigoPostal; }
        set { _StrCodigoPostal = value; }
    }

    public int IntIdMunicipio
    {
        get { return _IntIdMunicipio; }
        set { _IntIdMunicipio = value; }
    }

    public string StrRegistroCanacar
    {
        get { return _StrRegistroCanacar; }
        set { _StrRegistroCanacar = value; }
    }

    public int IntIdSatCatalogoCfdi_RegimenFiscal
    {
        get { return _IntIdSatCatalogoCfdi_RegimenFiscal; }
        set { _IntIdSatCatalogoCfdi_RegimenFiscal = value; }
    }
    public string StrCveEmpresaReferencia
    {
        get { return _StrCveEmpresaReferencia; }
        set { _StrCveEmpresaReferencia = value; }
    }
    //Propiedad extra
    public RespuestaBD objRespuestaDB
    {
        get { return _objRespuestaDB; }
        set { _objRespuestaDB = value; }
    }

    public int IntLimite
    {
        get { return _IntLimite; }
        set { _IntLimite = value; }
    }

    public string StrDescMunicipio
    {
        get
        {
            return _StrDescMunicipio;
        }

        set
        {
            _StrDescMunicipio = value;
        }
    }

    public string StrDescEstado
    {
        get
        {
            return _StrDescEstado;
        }

        set
        {
            _StrDescEstado = value;
        }
    }

    public string StrDescCdfiRegimenFiscal
    {
        get
        {
            return _StrDescCdfiRegimenFiscal;
        }

        set
        {
            _StrDescCdfiRegimenFiscal = value;
        }
    }

    public string StrJsonPeriodo
    {
        get
        {
            return _StrJsonPeriodo;
        }

        set
        {
            _StrJsonPeriodo = value;
        }
    }

    public int IntIdPeriodo
    {
        get { return _IntIdPeriodo; }
        set { _IntIdPeriodo = value; }
    }

    public int IntIdCuentaContableUtilidad
    {
        get { return _IntIdCuentaContable_utilidad; }
        set { _IntIdCuentaContable_utilidad = value; }
    }

    public int IntIdCuentaContable_FacturaPorRecibir
    {
        get { return _IntIdCuentaContable_FacturaPorRecibir; }
        set { _IntIdCuentaContable_FacturaPorRecibir = value; }
    }


    public int IntIdCuentaContable_FacturaPorRecibirDolares
    {
        get { return _IntIdCuentaContable_FacturaPorRecibirDolares; }
        set { _IntIdCuentaContable_FacturaPorRecibirDolares = value; }
    }

    public int IntIdCuentaContable_FacturaPorRecibirDolaresCompl
    {
        get { return _IntIdCuentaContable_FacturaPorRecibirDolaresCompl; }
        set { _IntIdCuentaContable_FacturaPorRecibirDolaresCompl = value; }
    }


    public int IntIdCuentaContable_Consumible
    {
        get { return _IntIdCuentaContable_Consumible; }
        set { _IntIdCuentaContable_Consumible = value; }
    }

    public int IntIdCuentaContable_ManoDeObra
    {
        get { return _IntIdCuentaContable_ManoDeObra; }
        set { _IntIdCuentaContable_ManoDeObra = value; }
    }

    public int IntIdCuentaContable_ManoDeObraComponente
    {
        get { return _IntIdCuentaContable_ManoDeObraComponente; }
        set { _IntIdCuentaContable_ManoDeObraComponente = value; }
    }


    public string StrCuentaContableFormato
    {
        get { return _StrCuentaContable_formato; }
        set { _StrCuentaContable_formato = value; }
    }

    public string StrCuentaContableFormato_FacturaPorRecibir
    {
        get { return _StrCuentaContableFormato_FacturaPorRecibir; }
        set { _StrCuentaContableFormato_FacturaPorRecibir = value; }
    }


    public string StrCuentaContableFormato_FacturaPorRecibirDolares
    {
        get { return _StrCuentaContableFormato_FacturaPorRecibirDolares; }
        set { _StrCuentaContableFormato_FacturaPorRecibirDolares = value; }
    }

    public string StrCuentaContableFormato_FacturaPorRecibirDolaresCompl
    {
        get { return _StrCuentaContableFormato_FacturaPorRecibirDolaresCompl; }
        set { _StrCuentaContableFormato_FacturaPorRecibirDolaresCompl = value; }
    }


    public string StrCuentaContableFormato_Consumible
    {
        get { return _StrCuentaContableFormato_Consumible; }
        set { _StrCuentaContableFormato_Consumible = value; }
    }

    public string StrCuentaContableFormato_ManoDeObra
    {
        get { return _StrCuentaContableFormato_ManoDeObra; }
        set { _StrCuentaContableFormato_ManoDeObra = value; }
    }

    public string StrCuentaContableFormato_ManoDeObraCompon
    {
        get { return _StrCuentaContableFormato_ManoDeObraCompon; }
        set { _StrCuentaContableFormato_ManoDeObraCompon = value; }
    }

    #endregion
    #endregion

    #region Constructores

    public Empresa()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Empresa(int intIdEmpresa)
    {
        _IntIdEmpresa = intIdEmpresa;
    }

    #endregion

    #region Metodos

    public RespuestaDataTable<Empresa> ResultadoDataTables(string filtros)
    {
        FiltrosDTEmpresa objFiltroDt = JsonConvert.DeserializeObject<FiltrosDTEmpresa>(filtros);
        int IntTotalFilasFiltradas = 0;
        int IntTotalFilas = 0;
        List<Empresa> aplicacionCompListado = new List<Empresa>();

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaObtenerDataTable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_OrdenColumna", objFiltroDt.order[0].column);
            sqlCommand.Parameters.AddWithValue("@p_OrdenDireccion", objFiltroDt.order[0].dir);
            sqlCommand.Parameters.AddWithValue("@p_FilaInicial", objFiltroDt.start);
            sqlCommand.Parameters.AddWithValue("@p_FilaFinal", objFiltroDt.length);

            if (objFiltroDt.IntBActivo >= -1)
                sqlCommand.Parameters.AddWithValue("@p_Activo", objFiltroDt.IntBActivo);


            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));


            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo: ResultadoDataTables()");

            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    aplicacionCompListado.Add(new Empresa()
                    {
                        IntIdEmpresa = int.Parse(fila["id"].ToString()),
                        StrDescEmpresa = fila["descEmpresa"].ToString(),
                        StrCveEmpresa = fila["cveEmpresa"].ToString(),
                        StrRfc = fila["rfc"].ToString(),
                        StrTelefono = fila["telefono"].ToString(),
                        StrCalle = fila["calle"].ToString(),
                        StrColonia = fila["colonia"].ToString(),
                        StrNumeroInterior = fila["numeroInterior"].ToString(),
                        StrNumeroExterior = fila["numeroExterior"].ToString(),
                        StrCodigoPostal = fila["codigoPostal"].ToString(),
                        StrDescMunicipio = fila["descMunicipio"].ToString(),
                        StrRegistroCanacar = fila["registroCanacar"].ToString(),
                        StrDescCdfiRegimenFiscal = fila["regimenFiscal"].ToString(),
                        IntBActivo = int.Parse(fila["bActivo"].ToString())
                    });
                }
                IntTotalFilasFiltradas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasFiltradas"].ToString());
                IntTotalFilas = int.Parse(dataSetObtenerDataTable.Tables[1].Rows[0]["totalFilasMostradas"].ToString());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return new RespuestaDataTable<Empresa>
        {
            data = aplicacionCompListado,
            recordsFiltered = IntTotalFilasFiltradas,
            recordsTotal = IntTotalFilas,
            draw = objFiltroDt.draw
        };
    }

    public RespuestaBD Insertar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaInsertar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_CveEmpresa", this.StrCveEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_DescEmpresa", this.StrDescEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_Rfc", this.StrRfc);
            sqlCommand.Parameters.AddWithValue("@p_Telefono", this.StrTelefono);
            sqlCommand.Parameters.AddWithValue("@p_Calle", this.StrCalle);
            sqlCommand.Parameters.AddWithValue("@p_Colonia", this.StrColonia);
            sqlCommand.Parameters.AddWithValue("@p_NumeroExterior", this.StrNumeroExterior);
            sqlCommand.Parameters.AddWithValue("@p_NumeroInterior", this.StrNumeroInterior);
            sqlCommand.Parameters.AddWithValue("@p_CodigoPostal", this.StrCodigoPostal);
            sqlCommand.Parameters.AddWithValue("@p_IdMunicipio", this.IntIdMunicipio);
            sqlCommand.Parameters.AddWithValue("@p_RegistroCanacar", this.StrRegistroCanacar);
            sqlCommand.Parameters.AddWithValue("@p_IdPeriodo", this.IntIdPeriodo);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_utilidad", this.IntIdCuentaContableUtilidad);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibir", this._IntIdCuentaContable_FacturaPorRecibir);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibirDolares", this._IntIdCuentaContable_FacturaPorRecibirDolares);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibirDolaresCompl", this._IntIdCuentaContable_FacturaPorRecibirDolaresCompl);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_Consumible", this._IntIdCuentaContable_Consumible);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_ManoDeObra", this._IntIdCuentaContable_ManoDeObra);
            sqlCommand.Parameters.AddWithValue("@p_IntIdCuentaContable_ManoDeObraComponente", this._IntIdCuentaContable_ManoDeObraComponente);

            sqlCommand.Parameters.AddWithValue("@p_IdSatCatalogoCfdi_regimenFiscal", this.IntIdSatCatalogoCfdi_RegimenFiscal);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", IntIdUsuario);

            /********************************************************** Objeto Registro Error **************************************************************************************/

            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            /***********************************************************************************************************************************************************************/

            //Empieza registro Acceso
            string StrOrigen = "";
            var ObjUrl = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer : HttpContext.Current.Request.Url;
            string StrUrlQuery = ObjUrl.Query;
            string[] StrValorQuery = StrUrlQuery.Split(new string[] { "Origen=" }, StringSplitOptions.RemoveEmptyEntries);
            if (StrValorQuery.Length > 1)
            {
                string[] StrValorQuery2 = StrValorQuery[1].Split('&');
                StrOrigen = "?Origen=" + StrValorQuery2[0];
            }

            ConexionBD conexion = new ConexionBD();
            conexion.SqlCommandComando = sqlCommand;
            conexion.SqlParametro = sqlCommand.Parameters;
            conexion.StrDescRuta = ObjUrl.AbsolutePath + StrOrigen;
            conexion.StrDescModelo = this.GetType().Name + ".cs";
            conexion.StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name;
            conexion.IntBRegistrarAcceso = 0; //En caso de que no se quiera registrar el proceso, solo ejecutarlo, mandar en 0

            DataSet dataSetInsertar = conexion.EjecutarComandoRegistroAcceso();
            //Termina registro Acceso

            //DataSet dataSetInsertar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs => Insertar()");

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
        catch (Exception e)
        {
            /*************************************************************************** Objeto Entra Catch CSHARP *************************************************************************/

            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de insertar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de insertar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");

            /********************************************************************************************************************************************************************************/
        }
        return objRespuestaBD;
    }

    public bool ObtenerPorId()
    {
        bool bool_Valido = false;
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var intIdUsuario = VariableGlobal.SessionIntIdUsuario;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaObtener";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, intIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs => ObtenerPorId");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                this.IntIdEmpresa = int.Parse(dataSetObtener.Tables[0].Rows[0]["idEmpresa"].ToString());
                this.IntIdMunicipio = int.Parse(dataSetObtener.Tables[0].Rows[0]["idMunicipio"].ToString());
                this.IntIdSatCatalogoCfdi_RegimenFiscal = int.Parse(dataSetObtener.Tables[0].Rows[0]["idSatCatalogoCfdi_regimenFiscal"].ToString());
                this.IntIdPeriodo = int.Parse(dataSetObtener.Tables[0].Rows[0]["idPeriodo"].ToString());
                this.IntIdCuentaContableUtilidad = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_utilidad"].ToString());

                this.IntIdCuentaContable_FacturaPorRecibir = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_FacturaPorRecibir"].ToString());
                this.StrCuentaContableFormato_FacturaPorRecibir = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoFactRec"].ToString();

                this._IntIdCuentaContable_FacturaPorRecibirDolares = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_FacturaPorRecibirDolares"].ToString());
                this._StrCuentaContableFormato_FacturaPorRecibirDolares = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoFactRecDolares"].ToString();

                this._IntIdCuentaContable_FacturaPorRecibirDolaresCompl = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_FacturaPorRecibirDolaresComplementaria"].ToString());
                this._StrCuentaContableFormato_FacturaPorRecibirDolaresCompl = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoFactRecDolaresCompl"].ToString();


                this._IntIdCuentaContable_ManoDeObra = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_ManoDeObra"].ToString());
                this._StrCuentaContableFormato_ManoDeObra = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoManoDeObra"].ToString();

                this._IntIdCuentaContable_Consumible = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_Consumible"].ToString());
                this._StrCuentaContableFormato_Consumible = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoConsumible"].ToString();

                this._IntIdCuentaContable_ManoDeObraComponente = int.Parse(dataSetObtener.Tables[0].Rows[0]["idCuentaContable_ManoDeObraComponente"].ToString());
                this._StrCuentaContableFormato_ManoDeObraCompon = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormatoManoDeObraComp"].ToString();

                this.IntBActivo = int.Parse(dataSetObtener.Tables[0].Rows[0]["bActivo"].ToString());

                this.StrDescEmpresa = dataSetObtener.Tables[0].Rows[0]["descEmpresa"].ToString();
                this.StrCveEmpresa = dataSetObtener.Tables[0].Rows[0]["cveEmpresa"].ToString();
                this.StrRfc = dataSetObtener.Tables[0].Rows[0]["rfc"].ToString();
                this.StrTelefono = dataSetObtener.Tables[0].Rows[0]["telefono"].ToString();
                this.StrCalle = dataSetObtener.Tables[0].Rows[0]["calle"].ToString();
                this.StrColonia = dataSetObtener.Tables[0].Rows[0]["colonia"].ToString();
                this.StrNumeroExterior = dataSetObtener.Tables[0].Rows[0]["numeroExterior"].ToString();
                this.StrNumeroInterior = dataSetObtener.Tables[0].Rows[0]["numeroInterior"].ToString();
                this.StrCodigoPostal = dataSetObtener.Tables[0].Rows[0]["codigoPostal"].ToString();
                this.StrRegistroCanacar = dataSetObtener.Tables[0].Rows[0]["registroCanacar"].ToString();
                this.StrCuentaContableFormato = dataSetObtener.Tables[0].Rows[0]["cuentaContableFormato"].ToString();

                bool_Valido = true;
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this._objRespuestaDB = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
        return bool_Valido;
    }

    public RespuestaBD Actualizar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaActualizar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_CveEmpresa", this.StrCveEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_DescEmpresa", this.StrDescEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_Rfc", this.StrRfc);
            sqlCommand.Parameters.AddWithValue("@p_Telefono", this.StrTelefono);
            sqlCommand.Parameters.AddWithValue("@p_Calle", this.StrCalle);
            sqlCommand.Parameters.AddWithValue("@p_Colonia", this.StrColonia);
            sqlCommand.Parameters.AddWithValue("@p_NumeroExterior", this.StrNumeroExterior);
            sqlCommand.Parameters.AddWithValue("@p_NumeroInterior", this.StrNumeroInterior);
            sqlCommand.Parameters.AddWithValue("@p_CodigoPostal", this.StrCodigoPostal);
            sqlCommand.Parameters.AddWithValue("@p_IdMunicipio", this.IntIdMunicipio);
            sqlCommand.Parameters.AddWithValue("@p_RegistroCanacar", this.StrRegistroCanacar);
            sqlCommand.Parameters.AddWithValue("@p_IdPeriodo", this.IntIdPeriodo);
            sqlCommand.Parameters.AddWithValue("@p_IdSatCatalogoCfdi_regimenFiscal", this.IntIdSatCatalogoCfdi_RegimenFiscal);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_utilidad", this.IntIdCuentaContableUtilidad);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibir", this._IntIdCuentaContable_FacturaPorRecibir);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibirDolares", this._IntIdCuentaContable_FacturaPorRecibirDolares);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_FacturaPorRecibirDolaresCompl", this._IntIdCuentaContable_FacturaPorRecibirDolaresCompl);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_Consumible", this._IntIdCuentaContable_Consumible);
            sqlCommand.Parameters.AddWithValue("@p_IdCuentaContable_ManoDeObra", this._IntIdCuentaContable_ManoDeObra);
            sqlCommand.Parameters.AddWithValue("@p_IntIdCuentaContable_ManoDeObraComponente", this._IntIdCuentaContable_ManoDeObraComponente);
            sqlCommand.Parameters.AddWithValue("@p_BActivo", this.IntBActivo);
            sqlCommand.Parameters.AddWithValue("@p_idUsuario", IntIdUsuario);

            //Registro de error
            RegistroError objRegistroError = new RegistroError(sqlCommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs");
            sqlCommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            //Empieza registro Acceso
            string StrOrigen = "";
            var ObjUrl = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer : HttpContext.Current.Request.Url;
            string StrUrlQuery = ObjUrl.Query;
            string[] StrValorQuery = StrUrlQuery.Split(new string[] { "Origen=" }, StringSplitOptions.RemoveEmptyEntries);
            if (StrValorQuery.Length > 1)
            {
                string[] StrValorQuery2 = StrValorQuery[1].Split('&');
                StrOrigen = "?Origen=" + StrValorQuery2[0];
            }

            ConexionBD conexion = new ConexionBD();
            conexion.SqlCommandComando = sqlCommand;
            conexion.SqlParametro = sqlCommand.Parameters;
            conexion.StrDescRuta = ObjUrl.AbsolutePath + StrOrigen;
            conexion.StrDescModelo = this.GetType().Name + ".cs";
            conexion.StrDescModeloMetodo = MethodBase.GetCurrentMethod().Name;
            conexion.IntBRegistrarAcceso = 0; //En caso de que no se quiera registrar el proceso, solo ejecutarlo, mandar en 0

            DataSet dataSetActualizar = conexion.EjecutarComandoRegistroAcceso();
            //Termina registro Acceso

            //DataSet dataSetActualizar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo : Actualizar()");

            if (dataSetActualizar != null && dataSetActualizar.Tables.Count > 0 && dataSetActualizar.Tables[0].Rows.Count > 0)
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
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de actualizar el registro. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;
    }

    public RespuestaBD Eliminar()
    {
        RespuestaBD objRespuestaBD = new RespuestaBD();
        int IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        int IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaEliminar";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_Debug", 0);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", this.IntIdEmpresa);

            DataSet dataSetEliminar = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: " + this.GetType().Name + ".cs, metodo : Eliminar()");

            if (dataSetEliminar != null && dataSetEliminar.Tables.Count > 0 && dataSetEliminar.Tables[0].Rows.Count > 0)
            {
                objRespuestaBD = new RespuestaBD(
                    short.Parse(dataSetEliminar.Tables[0].Rows[0]["Error"].ToString()),
                    dataSetEliminar.Tables[0].Rows[0]["MensajeError"].ToString(),
                    dataSetEliminar.Tables[0].Rows[0]["TipoError"].ToString()
                );
            }
            else
            {
                objRespuestaBD = new RespuestaBD(1, "No se obtuvo respuesta de la base de datos", "error");
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error, contacte con el administrador, ID: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error, contacte con el administrador, ID: " + objRegistroError.IntIdRegistroError, "error");
        }

        return objRespuestaBD;

    }
    public List<Empresa> ObtenerListaGenerica(int ParamObjeto)
    {
        List<Empresa> lstObj_Empresa = new List<Empresa>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamObjeto);

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : " + this.GetType().Name + ".cs, metodo : ObtenerListaGenerica()");

            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow fila in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObj_Empresa.Add(new Empresa
                    {
                        IntIdEmpresa = int.Parse(fila["idEmpresa"].ToString()),
                        StrDescEmpresa = fila["descEmpresa"].ToString(),
                        StrCveEmpresa = fila["cveEmpresa"].ToString(),
                    });
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros de Obtener Lista de Anticipo Banco Archivo, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return lstObj_Empresa;
    }
    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
            var IntBRobot = VariableGlobal.SessionIntBRobot;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBRobot);
            //    sqlCommand.Parameters.AddWithValue("@p_Limite", 0);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Empresa");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();

            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Empresa.cs", "EmpresaController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }

    public static IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro, string ParamCveEmpresas)
    {
        List<SelectListItem> objLista = new List<SelectListItem>();
        try
        {
            var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
            var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
            var IntBRobot = VariableGlobal.SessionIntBRobot;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spEmpresaObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);
            sqlCommand.Parameters.AddWithValue("@p_TipoFiltro", ParamTipoFiltro);
            sqlCommand.Parameters.AddWithValue("@p_BRobot", IntBRobot);
            sqlCommand.Parameters.AddWithValue("@p_cveEmpresas", ParamCveEmpresas);
            //    sqlCommand.Parameters.AddWithValue("@p_Limite", 0);
            DataSet dataSetObtenerLista = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlCommand, "archivo: Empresa");
            if (dataSetObtenerLista != null && dataSetObtenerLista.Tables.Count > 0)
            {
                return dataSetObtenerLista.Tables[0].ObtenerComboDinamico();

            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Empresa.cs", "EmpresaController.cs", "Error al tratar de obtener la lista, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return objLista;
    }


    #endregion

    #region Metodos Extra

    public Empresa PeriodoObtener()
    {

        RespuestaBD objRespuestaBD = new RespuestaBD();
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        Empresa ObjPoliza = new Empresa();

        try
        {

            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spEmpresaPeriodoObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlcommand.Parameters.AddWithValue("@p_IdEmpresa", IntIdEmpresa);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtenerDetalle = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlcommand, "archivo: " + this.GetType().Name + ".cs => Insertar()");
            if (dataSetObtenerDetalle != null && dataSetObtenerDetalle.Tables.Count > 0 && dataSetObtenerDetalle.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow objFilaCuentaContable in dataSetObtenerDetalle.Tables[0].Rows)
                {
                    ObjPoliza.StrJsonPeriodo = dataSetObtenerDetalle.Tables[0].Columns.Contains("jsonPeriodo") ? objFilaCuentaContable["jsonPeriodo"].ToString() : "[]";
                }
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            objRespuestaBD = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return ObjPoliza;
    }

    public bool ObtenerPorCve()
    {
        bool bool_valido = false;

        try
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "corporativo.spEmpresaObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_Ejecuta", 1);
            sqlcommand.Parameters.AddWithValue("@p_cveEmpresaReferencia", this.StrCveEmpresaReferencia);

            DataSet dataSetObtener = ConexionBD.EjecutarComandoExterno(sqlcommand, "archivo : Usuario.cs => ObtenerPorId");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                IntIdEmpresa = int.Parse(dataSetObtener.Tables[0].Rows[0]["idEmpresa"].ToString());
                bool_valido = true;
            }
        }
        catch (Exception e) { }

        return bool_valido;
    }

    #endregion
}

public class FiltrosDTEmpresa : DataTableAjaxPostModel
{
    public string StrDescEmpresaFiltro { get; set; }
    public int IntBActivo { get; set; }
}