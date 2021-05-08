using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages.Html;

/// <summary>
/// Summary description for MapaEquipoGps
/// </summary>
public class MapaEquipoGps
{
    #region Propiedades

    private int _IntTipoEquipo;
    private int _IntIdEquipo;
    private int _IntTipoBusqueda;
    private int _IntSpeedVehicle;
    private int _IntIdEventoGeocerca;
    private DateTime _DtGPSDateTime;
    private decimal _DcLatitude;
    private decimal _DcLongitude;
    private string _StrPlace;
    private string _StrCveParametro;
    private string _StrUrlMapaGoogle;

    /// <summary>
    /// Propiedades extra
    /// </summary>
    private RespuestaBD _ObjRespuestaDB;

    #endregion

    #region Getter y setter

    public int IntTipoEquipo
    {
        get
        {
            return _IntTipoEquipo;
        }

        set
        {
            _IntTipoEquipo = value;
        }
    }

    public int IntIdEquipo
    {
        get
        {
            return _IntIdEquipo;
        }

        set
        {
            _IntIdEquipo = value;
        }
    }

    public int IntTipoBusqueda
    {
        get
        {
            return _IntTipoBusqueda;
        }

        set
        {
            _IntTipoBusqueda = value;
        }
    }

    public int IntSpeedVehicle
    {
        get
        {
            return _IntSpeedVehicle;
        }

        set
        {
            _IntSpeedVehicle = value;
        }
    }
    public int IntIdEventoGeocerca
    {
        get
        {
            return _IntIdEventoGeocerca;
        }

        set
        {
            _IntIdEventoGeocerca = value;
        }
    }

    public DateTime DtGPSDateTime
    {
        get
        {
            return _DtGPSDateTime;
        }

        set
        {
            _DtGPSDateTime = value;
        }
    }

    public decimal DcLatitude
    {
        get
        {
            return _DcLatitude;
        }

        set
        {
            _DcLatitude = value;
        }
    }

    public decimal DcLongitude
    {
        get
        {
            return _DcLongitude;
        }

        set
        {
            _DcLongitude = value;
        }
    }

    public string StrPlace
    {
        get
        {
            return _StrPlace;
        }

        set
        {
            _StrPlace = value;
        }
    }

    public string StrCveParametro
    {
        get
        {
            return _StrCveParametro;
        }

        set
        {
            _StrCveParametro = value;
        }
    }

    public string StrUrlMapaGoogle
    {
        get
        {
            return _StrUrlMapaGoogle;
        }

        set
        {
            _StrUrlMapaGoogle = value;
        }
    }

    public RespuestaBD ObjRespuestaDB
    {
        get
        {
            return _ObjRespuestaDB;
        }

        set
        {
            _ObjRespuestaDB = value;
        }
    }
    #endregion

    #region Constructores

    public MapaEquipoGps()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #endregion

    #region Metodos

    public MapaEquipoGps MapaPosionObtener()
    {

        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;

        this.StrCveParametro = "llaveMapaEmbedApi";

        var ObjMapaEquipoGps = new MapaEquipoGps();

        try
        {

            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "trafico.spMapaEquipoGpsPosicionObtener";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@p_TipoEquipo", this.IntTipoEquipo);
            sqlcommand.Parameters.AddWithValue("@p_IdEquipo", this.IntIdEquipo);
            sqlcommand.Parameters.AddWithValue("@p_TipoBusqueda", this.IntTipoBusqueda);
            sqlcommand.Parameters.AddWithValue("@p_IdEventoGeocerca", this.IntIdEventoGeocerca);

            RegistroError objRegistroError = new RegistroError(sqlcommand.Parameters, MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller");
            sqlcommand.Parameters.AddWithValue("@p_RegistroError", JsonConvert.SerializeObject(objRegistroError));

            DataSet dataSetObtener = ConexionBD.EjecutarComando(IntIdEmpresa, IntIdUsuario, sqlcommand, "archivo: " + this.GetType().Name + ".cs => MapaPosionObtener");

            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                ObjMapaEquipoGps.StrPlace = (dataSetObtener.Tables[0].Rows[0]["Place"].ToString());
                ObjMapaEquipoGps.DcLatitude = decimal.Parse(dataSetObtener.Tables[0].Rows[0]["Latitude"].ToString());
                ObjMapaEquipoGps.DcLongitude = decimal.Parse(dataSetObtener.Tables[0].Rows[0]["Longitude"].ToString());
                ObjMapaEquipoGps.IntSpeedVehicle = int.Parse(dataSetObtener.Tables[0].Rows[0]["SpeedVehicle"].ToString());
                ObjMapaEquipoGps.DtGPSDateTime = DateTime.Parse(dataSetObtener.Tables[0].Rows[0]["GPSDateTime"].ToString());

            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            this._ObjRespuestaDB = new RespuestaBD(1, "Error al tratar de obtener el registro, IDRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }

        return ObjMapaEquipoGps;
    }

    public string LlaveMapaObtener()
    {
        Parametro obj_parametro = new Parametro
        {
            IntTipoBusqueda = 1,
            StrCveParametro = this.StrCveParametro
        };

        obj_parametro.ObtenerPorClaveParametro();

        return obj_parametro.StrDescParametro;
    }

    #endregion
}