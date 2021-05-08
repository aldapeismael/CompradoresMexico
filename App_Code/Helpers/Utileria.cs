
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Xml.Schema;
using System.Data.OleDb;
using System.IO.Compression;
using System.Collections.Generic;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Descripción breve de Utileria
/// </summary>
public static class Utileria
{

    public static List<string[]> ObtenerCSV(string StrUrlArchivo, char StrSeparador, string StrNombreArchivo)
    {

        string Extension = Path.GetExtension(StrNombreArchivo);
        string Nombre = Path.GetFileNameWithoutExtension(StrNombreArchivo);

        if (Extension == ".xls" || Extension == ".xlsx")
        {

            using (OleDbConnection cn = new OleDbConnection())
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + StrUrlArchivo + StrNombreArchivo + ";Extended Properties=Excel 12.0;";
                    cmd.Connection = cn;
                    cmd.CommandText = "select * from [SIP2020$]";
                    string sep = "\t";


                    using (OleDbDataAdapter adp = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        using (StreamWriter wr = new StreamWriter(@StrUrlArchivo + Nombre + ".txt"))
                        {

                            foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
                            //// get column names
                            var columnNames = dt.Columns.OfType<DataColumn>().Select(x => x.ColumnName);

                            //// write column names row
                            wr.WriteLine(String.Join("\t", columnNames));

                            // get column count
                            int columnCount = dt.Columns.Count;

                            //// array to hold data values
                            string[] values = new string[columnCount];

                            //// write columns in each row
                            foreach (DataRow row in dt.Rows)
                            {
                                for (int i = 0; i < columnCount; i++)
                                {
                                    object value = row[i];

                                    //// check row value for null
                                    values[i] = (value != null) ? value.ToString() : "NULL";
                                }

                                //// write data line
                                wr.WriteLine(String.Join("\t", values));
                            }

                        }

                    }

                }
            }

            // Seteamos el valor del nuevo archivo .txt generado, que pasara al streamReader
            StrNombreArchivo = Nombre + ".txt";

        }

        List<string[]> LstArrayStrDetalle = new List<string[]>();
        try
        {
            using (var ObjArchivo = new FileStream(StrUrlArchivo + StrNombreArchivo, FileMode.Open))
            {
                StreamReader CVSStreamReader = new StreamReader(ObjArchivo);
                while (!CVSStreamReader.EndOfStream)
                {
                    var StrFila = CVSStreamReader.ReadLine();
                    LstArrayStrDetalle.Add(StrFila.Split(StrSeparador));
                }
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Utileria.cs", "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
        return LstArrayStrDetalle;
    }

    public static List<DataRow> ObtenerArchivo(string StrUrlArchivo, string StrNombreArchivo, int IntTieneHeader = 1, int IntTablaObtener = -1, int IntPersonalizado = 0)
    {
        /** IntTieneHeader -> Variable para saber si el archivo cuenta con encabezado
        * VALORES
        * 0 -> El archivo Excel NO TIENE encabezado
        * 1 ->  El archivo Excel TIENE encabezado (DEFAULT)
        * */
        /** IntPersonalizado -> Variable para archivos con formatos especiales o fuera de lo normal
         * VALORES
         * 0 -> Default
         * 1 -> Para formato de Estado de Cuenta BANAMEX. Se utiliza en ConciliacionBancoImportacion.cs
         * 2 -> Para formato de Estado de Cuenta BANORTE, BBVABANCOMER, BANREGIO, HSBC, IBC. Se utiliza en ConciliacionBancoImportacion.cs
         * */
        /** IntTablaObtener -> Variable para saber que hoja (sheet) del excel vamos a obtener
        * VALORES
        * -1 -> Obtiene la ultima hoja (el último dataset[tabla] obtenido) DEFAULT
        *  0 ->  Obtiene la primer hoja
        *  1 -> Obtiene la segunda hoja
        *  n -> ...
        * */
        string Extension = Path.GetExtension(StrNombreArchivo);
        string Nombre = Path.GetFileNameWithoutExtension(StrNombreArchivo);
        char Separador = ',';

        if (Extension == ".csv")
        {
            Separador = ',';
        }
        if (Extension == ".txt")
        {
            Separador = '\t';
        }

        List<DataRow> LstArrayStrDetalle = new List<DataRow>();
        DataTable table = new DataTable();

        if (Extension == ".xls" || Extension == ".xlsx" || Extension == ".xlsm")
        {
            Stream fsExcel = File.OpenRead(StrUrlArchivo + StrNombreArchivo);
            var ObjExcel = new ExcelBook(StrUrlArchivo + StrNombreArchivo, fsExcel);
            DataSet dsexcel = ObjExcel.GenerarDataSet(IntTieneHeader, IntPersonalizado);

            if (IntTablaObtener == -1)
            {
                for (int i = 0; i < dsexcel.Tables.Count; i++)
                {
                    LstArrayStrDetalle = dsexcel.Tables[i].AsEnumerable().ToList();
                }
            }
            else
            {
                LstArrayStrDetalle = dsexcel.Tables[IntTablaObtener].AsEnumerable().ToList();
            }
        }
        else
        {

            try
            {

                using (StreamReader sr = new StreamReader(StrUrlArchivo + StrNombreArchivo))
                {
                    DataTable dtCSV = new DataTable();
                    string str_line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(str_line))
                    {
                        string[] headers = str_line.Split(Separador);
                        DataRow dr;
                        for (int i = 0; i < headers.Count(); i++)
                        {
                            dtCSV.Columns.Add();
                        }

                        if (IntTieneHeader == 0)
                        {
                            dr = dtCSV.NewRow();
                            for (int i = 0; i < headers.Count(); i++)
                            {
                                dr[i] = headers[i];
                            }
                            LstArrayStrDetalle.Add(dr);
                        }

                        while (!sr.EndOfStream)
                        {
                            str_line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(str_line))
                            {
                                string[] rows = str_line.Split(Separador);
                                dr = dtCSV.NewRow();
                                for (int i = 0; i < rows.Count(); i++)
                                {
                                    dr[i] = rows[i];
                                }
                                LstArrayStrDetalle.Add(dr);
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Utileria.cs", "Controller.cs", "Error al tratar de obtener los registros de documentos, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
            }
        }
        return LstArrayStrDetalle;
    }

    public static void MoverArchivoError(string StrUrlArchivo, string StrNombreArchivo)
    {
        try
        {
            if (File.Exists(StrUrlArchivo + StrNombreArchivo))
            {
                File.Move(StrUrlArchivo + StrNombreArchivo, StrUrlArchivo + "Error\\" + StrNombreArchivo + GenerarNombre());
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Utileria.cs", "Controller.cs", "Error al tratar de mover el/los archivo(s), IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }
    }

    public static string GenerarNombre()
    {
        var date_Fecha = DateTime.Now;
        string str_nombreArchivo = date_Fecha.Year.ToString();
        str_nombreArchivo += (int.Parse(date_Fecha.Month.ToString()) < 10 ? "0" : "") + date_Fecha.Month.ToString();
        str_nombreArchivo += (int.Parse(date_Fecha.Day.ToString()) < 10 ? "0" : "") + date_Fecha.Day.ToString();
        str_nombreArchivo += date_Fecha.Hour.ToString() + date_Fecha.Minute.ToString() + date_Fecha.Second.ToString() + date_Fecha.Millisecond.ToString();

        return str_nombreArchivo;
    }

    /// <summary>
    /// Funcion para obtener el numero del string del mes
    /// </summary>
    /// <param name="StrMes">String del mes a transformar</param>
    /// <returns></returns>
    public static string ObtenerNumeroMes(this string StrMes)
    {
        string StrMesNumero = string.Empty;

        var ArrMes = StrMes.Split('.');
        StrMes = ArrMes[0].ToUpper();

        if (StrMes.Equals("TODOS")) { StrMesNumero = "00"; }
        else if (StrMes.Equals("ENERO")) { StrMesNumero = "01"; }
        else if (StrMes.Equals("FEBRERO")) { StrMesNumero = "02"; }
        else if (StrMes.Equals("MARZO")) { StrMesNumero = "03"; }
        else if (StrMes.Equals("ABRIL")) { StrMesNumero = "04"; }
        else if (StrMes.Equals("MAYO")) { StrMesNumero = "05"; }
        else if (StrMes.Equals("JUNIO")) { StrMesNumero = "06"; }
        else if (StrMes.Equals("JULIO")) { StrMesNumero = "07"; }
        else if (StrMes.Equals("AGOSTO")) { StrMesNumero = "08"; }
        else if (StrMes.Equals("SEPTIEMBRE")) { StrMesNumero = "09"; }
        else if (StrMes.Equals("OCTUBRE")) { StrMesNumero = "10"; }
        else if (StrMes.Equals("NOVIEMBRE")) { StrMesNumero = "11"; }
        else if (StrMes.Equals("DICIEMBRE")) { StrMesNumero = "12"; }

        return StrMesNumero;

    }

    /// <summary>
    /// Funcion para validar un xml contra el esquema
    /// </summary>
    /// <param name="StrXml">La ruta del archivo xml o la string del xml</param>
    /// <param name="StrTargetNameSpaceSchema">El namespace esperado en el archivo xml</param>
    /// <param name="StrUrlSchema">Lugar donde se espera encontrar el archivo esquema del xml a validar</param>
    /// <param name="IntBTipoLectura">Bandera para saber la tipo de lectura valor 1: por string valor 2: por archivo guardado en disco 1|2</param>
    /// <returns></returns>
    public static string ValidarEsquemaXml(this string StrXml, string StrTargetNameSpaceSchema, string StrUrlSchema, int IntBTipoLectura)
    {
        /*
        * IntBTipoLectura 1 de un string
        * IntBTipoLectura 2 de un archivo xml guardado
        */

        string StrError = string.Empty;
        XmlReaderSettings objXmlReaderSettings = new XmlReaderSettings();
        XmlDocument objXmlDocument = new XmlDocument();
        RespuestaBD objRespuestaBD = new RespuestaBD();

        try
        {
            if (IntBTipoLectura == 1)
            {
                using (StringReader objStringReader = new StringReader(StrXml))
                {
                    objXmlReaderSettings.Schemas.Add(StrTargetNameSpaceSchema, StrUrlSchema);
                    objXmlReaderSettings.ValidationType = System.Xml.ValidationType.Schema;
                    XmlReader objXmlReader = XmlReader.Create(objStringReader, objXmlReaderSettings);
                    objXmlDocument.Load(objXmlReader);
                    ValidationEventHandler objValidationEventHandler = new ValidationEventHandler(ValidationEventHandler);
                    objXmlDocument.Validate(objValidationEventHandler);
                }
            }
            else if (IntBTipoLectura == 2)
            {
                objXmlReaderSettings.Schemas.Add(StrTargetNameSpaceSchema, StrUrlSchema);
                objXmlReaderSettings.ValidationType = System.Xml.ValidationType.Schema;
                XmlReader objXmlReader = XmlReader.Create(StrXml, objXmlReaderSettings);
                objXmlDocument.Load(objXmlReader);
                ValidationEventHandler objValidationEventHandler = new ValidationEventHandler(ValidationEventHandler);
                objXmlDocument.Validate(objValidationEventHandler);
            }

        }
        catch (Exception e)
        {
            StrError = "Error al validar el XML contra el esquema: " + e.Message;
        }

        return StrError;

    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        switch (e.Severity)
        {
            case XmlSeverityType.Error:
                throw new Exception(e.Message);
            case XmlSeverityType.Warning:
                throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// Funcion para pasar un json a un datatable
    /// </summary>
    /// <param name="StrJson">Json a transformar a un datatable</param>
    /// <returns>DataTable</returns>
    public static DataTable StringJsonADataTable(this string StrJson)
    {
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(StrJson);
        return dt;
    }

    /// <summary>
    ///  Funcion para transformar un json a un archivo excel
    /// </summary>
    /// <param name="StrJson">Json que se va transformar</param>
    /// <param name="StrRutaArchivo">Ruta donde se va almacenar el archivo creado</param>
    /// <param name="StrNombreArchivo">Nombre del archivo</param>
    /// <param name="StrNombreHojaExcel">Nombre de la hoja de trabajo de excel</param>
    /// <param name="StrExtension">Tipo de extension del archivo xls|xlsx</param>
    /// <param name="IntBBorra">Bandera para saber si se borra el archivo 0|1</param>
    /// <returns>string</returns>
    public static string StringJsonAArchivoExcel(this string StrJson, string StrRutaArchivo, string StrNombreArchivo, string StrNombreHojaExcel, string StrExtension, int IntBBorra)
    {
        string StrError = string.Empty;

        try
        {

            IWorkbook objWorkbook;

            if (StrExtension == "xlsx")
            {
                objWorkbook = new XSSFWorkbook();
            }
            else if (StrExtension == "xls")
            {
                objWorkbook = new HSSFWorkbook();
            }
            else
            {
                StrError = "el formato no es compatible";
                return StrError;
            }

            var objSheet = objWorkbook.CreateSheet(StrNombreHojaExcel);
            var objHeaderRow = objSheet.CreateRow(0);

            if (!Directory.Exists(StrRutaArchivo))
            {
                Directory.CreateDirectory(StrRutaArchivo);
            }

            if (IntBBorra == 1)
            {
                if (File.Exists(StrRutaArchivo + StrNombreArchivo))
                {
                    File.Delete(StrRutaArchivo + StrNombreArchivo);
                }
            }

            using (var dt = StrJson.StringJsonADataTable())
            {


                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var objCell = objHeaderRow.CreateCell(i);
                    string columnName = dt.Columns[i].ToString();
                    objCell.SetCellValue(columnName);

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var objRow = objSheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var objCell = objRow.CreateCell(j);
                        string columnName = dt.Columns[j].ToString();
                        objCell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    objSheet.AutoSizeColumn(i);
                }
            }


            using (var objFileStream = new FileStream(StrRutaArchivo + StrNombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                objWorkbook.Write(objFileStream);
            }
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Utileria.cs", "Controller.cs", "Error al generar el archivo Excel", e.HResult, e.Message, (int)Severidad.ALTA);
            StrError = e.Message;
        }

        return StrError;
    }

    /// <summary>
    /// Funcion para comprimir archivos o carpetas
    /// </summary>
    /// <param name="StrRutaArchivo">Ruta en donde se va gurardar el archivo zip</param>
    /// <param name="StrNombreArchivoZip">Nombre del archivo zip</param>
    /// <param name="LstArchivo">Lista de archivos o carpetas para comprimir en el archivo zip</param>
    /// <param name="IntBTipoGuardado">Bandera para saber el tipo de guardardo valor 1 archivos, valor 2 carpetas 1|2</param>
    /// <param name="IntBBorra">Bandera para saber si se borra el archivo 0|1</param>
    /// <returns>string</returns>
    public static string GuardarArchivoZip(this string StrRutaArchivo, string StrNombreArchivoZip, List<string> LstArchivo, int IntBTipoGuardado, int IntBBorra)
    {
        /*
        * IntBTipoGuardado 1 Solo archivos
        * IntBTipoGuardado 2 Solo carpetas
        */

        string StrError = string.Empty;
        try
        {
            if (!Directory.Exists(StrRutaArchivo))
            {
                Directory.CreateDirectory(StrRutaArchivo);
            }

            if (IntBBorra == 1)
            {
                if (File.Exists(StrRutaArchivo + StrNombreArchivoZip))
                {
                    File.Delete(StrRutaArchivo + StrNombreArchivoZip);
                }
            }

            if (IntBTipoGuardado == 1)
            {
                using (ZipArchive objZipArchive = ZipFile.Open(StrRutaArchivo + StrNombreArchivoZip, ZipArchiveMode.Create))
                {
                    foreach (string item in LstArchivo)
                    {
                        objZipArchive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }
                }
            }

            else if (IntBTipoGuardado == 2)
            {
                using (Ionic.Zip.ZipFile objZipFile = new Ionic.Zip.ZipFile())
                {

                    foreach (string item in LstArchivo)
                    {
                        string strNombreEntrada = item;
                        strNombreEntrada = ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(strNombreEntrada);
                        var arrCarpetas = strNombreEntrada.Split('/');

                        objZipFile.AddDirectory(item, arrCarpetas[arrCarpetas.Length - 1]);
                    }

                    objZipFile.Save(StrRutaArchivo + StrNombreArchivoZip);
                }
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Utileria.cs", "Controller.cs", "Error al generar el archivo Zip", e.HResult, e.Message, (int)Severidad.ALTA);
            StrError = e.Message;
        }
        return StrError;
    }

    /// <summary>
    /// Funcion para generar un hash en md5
    /// </summary>
    /// <param name="ObjMd5">Objeto md5 para generar el hash</param>
    /// <param name="StrLlave">String a convertir en hash md5</param>
    /// <returns>string</returns>
    public static string GenerarMd5Hash(this MD5 ObjMd5, string StrLlave)
    {

        byte[] data = ObjMd5.ComputeHash(Encoding.UTF8.GetBytes(StrLlave));
        StringBuilder objStringBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            objStringBuilder.Append(data[i].ToString("x2"));
        }

        return objStringBuilder.ToString();
    }

    /// <summary>
    /// Funcion para verificar un hash md5 contra una llave
    /// </summary>
    /// <param name="ObjMd5">Objeto md5 para generar el hash</param>
    /// <param name="StrLlave">String a verificar contra el hash</param>
    /// <param name="StrHash">Hash a verificar</param>
    /// <returns>bool</returns>
    public static bool ValidarMd5Hash(this MD5 ObjMd5, string StrLlave, string StrHash)
    {
        string hashOfInput = ObjMd5.GenerarMd5Hash(StrLlave);
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        bool BoolValido;
         if (0 == comparer.Compare(hashOfInput, StrHash))
        {
            BoolValido = true;
        }
        else
        {
            BoolValido = false;
        }

        return BoolValido;
    }

    /// <summary>
    /// Funcion para validar la peticion que viene de sql server
    /// </summary>
    /// <param name="StrMd5Hash">Hash a validar para la peticion externa.</param>
    /// <returns></returns>
    public static bool ValidarPeticionExterna(this string StrMd5Hash)
    {
        //Parametro objParametro;
        //Empresa objEmpresa;
        bool BoolValido = true;

        //try
        //{
        //    objParametro = new Parametro
        //    {
        //        IntTipoBusqueda = 1,
        //        StrCveParametro = "empresa"
        //    };
        //    objParametro.ObtenerPorClaveParametro();
        //    int int_IdEmpresa = objParametro.IntValor1num;

        //    objEmpresa = new Empresa(int_IdEmpresa);
        //    objEmpresa.ObtenerPorId();

        //    objParametro = new Parametro
        //    {
        //        IntTipoBusqueda = 1,
        //        StrCveParametro = "acceso_contrasena_llave"
        //    };
        //    objParametro.ObtenerPorClaveParametro();
        //    string str_Acceso = objParametro.StrValor1char;

        //    objParametro = new Parametro
        //    {
        //        IntTipoBusqueda = 1,
        //        StrCveParametro = "cfdi_llaveConnector"
        //    };
        //    objParametro.ObtenerPorClaveParametro();
        //    string str_Ambiente = objParametro.StrValor1char;

        //    string str_Llave = int_IdEmpresa + "_" + objEmpresa.StrCveEmpresa + "_" + str_Acceso + "_" + str_Ambiente;
        //    using (MD5 md5Hash = MD5.Create())
        //    {
        //        if (md5Hash.ValidarMd5Hash(str_Llave, StrMd5Hash))
        //        {
        //            BoolValido = true;
        //        }
        //        else
        //        {
        //            BoolValido = false;
        //        }
        //    }
        //}
        //catch 
        //{
        //    BoolValido = false;
        //}

        return BoolValido;
    }
}