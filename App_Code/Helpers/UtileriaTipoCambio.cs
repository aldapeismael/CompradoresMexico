using System;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

public class UtileriaTipoCambio
{
    public static Response ObtenerBanxicoTipoCambio(int paramIntIdTipoCambio)
    {
        string strDia = string.Empty;
        string strMes = string.Empty;
        string strFecha = string.Empty;
        int intDia = 0;
        int intMes = 0;
        int intIdTipocambioDia = 0;
        int intIdTipocambioFix = 0;
        string strAnio = string.Empty;
        string strBanxicoTipoCambioToken = string.Empty;
        string strBanxicoTipoCambioUrl = string.Empty;
        string StrBanxivoTipoCambioSerie = string.Empty;
        DateTime dtfecha;
        Parametro objParametro = new Parametro();
        var objTipocambioDia = MultiLista.ObtenerListaChar(8, 0, "TIPOCAMBIOORIGEN", "TIPOCAMBIODIA");
        var objTipocambioFix = MultiLista.ObtenerListaChar(8, 0, "TIPOCAMBIOORIGEN", "TIPOCAMBIOFIX");

        foreach (var item in objTipocambioDia)
        {
            if (item.valor1char == "TIPOCAMBIODIA")
            {
                intIdTipocambioDia = int.Parse(item.idMultiLista.ToString());
            }
        }

        foreach (var item in objTipocambioFix)
        {
            if (item.valor1char == "TIPOCAMBIOFIX")
            {
                intIdTipocambioFix = int.Parse(item.idMultiLista.ToString());
            }
        }

        try
        { 

            dtfecha = DateTime.Now;
            intDia = dtfecha.Day;
            intMes = dtfecha.Month;
            strAnio = dtfecha.Year.ToString();

            if (intDia <= 9) strDia = "0" + intDia.ToString();
            else strDia = intDia.ToString();

            if (intMes <= 9) strMes = "0" + intMes.ToString();
            else strMes = intMes.ToString();

            strFecha = strAnio + "-" + strMes + "-" + strDia;

            objParametro.IntTipoBusqueda = 1;
            objParametro.StrCveParametro = "BANXICOTIPOCAMBIOTOKEN";
            objParametro.ObtenerPorClaveParametro();

            if (objParametro.ObtenerPorClaveParametro())
                strBanxicoTipoCambioToken = objParametro.StrValor1char;
            else
                throw new Exception(String.Format("No se encontro el token de banxico"));

            objParametro.IntTipoBusqueda = 1;
            objParametro.StrCveParametro = "BANXICOTIPOCAMBIOURL";
            objParametro.ObtenerPorClaveParametro();

            if (objParametro.ObtenerPorClaveParametro())
                strBanxicoTipoCambioUrl = objParametro.StrValor1char;
            else
                throw new Exception(String.Format("No se encontro la url de banxico"));

            objParametro.IntTipoBusqueda = 1;

            if (paramIntIdTipoCambio == intIdTipocambioFix) objParametro.StrCveParametro = "BANXICOTIPOCAMBIOSERIEFIX";
            else if (paramIntIdTipoCambio == intIdTipocambioDia) objParametro.StrCveParametro = "BANXICOTIPOCAMBIOSERIEHOY";

            objParametro.ObtenerPorClaveParametro();

            if (objParametro.ObtenerPorClaveParametro())
                StrBanxivoTipoCambioSerie = objParametro.StrValor1char;
            else
                throw new Exception(String.Format("No se encontro la serie de banxico"));

            if (paramIntIdTipoCambio == intIdTipocambioFix) strBanxicoTipoCambioUrl += StrBanxivoTipoCambioSerie + strFecha + "/" + strFecha;
            else if (paramIntIdTipoCambio == intIdTipocambioDia) strBanxicoTipoCambioUrl += StrBanxivoTipoCambioSerie;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest objHttpWebRequest = WebRequest.Create(strBanxicoTipoCambioUrl) as HttpWebRequest;
            objHttpWebRequest.Accept = "application/json";
            objHttpWebRequest.Headers["Bmx-Token"] = strBanxicoTipoCambioToken;
            HttpWebResponse objHttpWebResponse = objHttpWebRequest.GetResponse() as HttpWebResponse;

            if (objHttpWebResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(String.Format(
                "Server error (HTTP {0}: {1}).",
                objHttpWebResponse.StatusCode,
                objHttpWebResponse.StatusDescription));

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
            object objResponse = jsonSerializer.ReadObject(objHttpWebResponse.GetResponseStream());
            Response jsonResponse = objResponse as Response;
            return jsonResponse;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}

[DataContract]
public class Serie
{
    [DataMember(Name = "titulo")]
    public string Title { get; set; }

    [DataMember(Name = "idSerie")]
    public string IdSerie { get; set; }

    [DataMember(Name = "datos")]
    public DataSerie[] Data { get; set; }

}

[DataContract]
public class DataSerie
{
    [DataMember(Name = "fecha")]
    public string Date { get; set; }

    [DataMember(Name = "dato")]
    public string Data { get; set; }
}

[DataContract]
public class SeriesResponse
{
    [DataMember(Name = "series")]
    public Serie[] series { get; set; }
}

[DataContract]
public class Response
{
    [DataMember(Name = "bmx")]
    public SeriesResponse seriesResponse { get; set; }
}