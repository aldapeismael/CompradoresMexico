using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for ExcelBook
/// </summary>
public class ExcelBook
{
    // Objeto de ExcelBook, representa el archivo
    internal readonly IWorkbook objIWorkbook;
    internal readonly IDataFormat objIDataFormat;

    // Lista de sheets leídas del workbook
    private readonly List<ExcelSheet> lstISheets;


    /// <summary>
    /// Todos los constructores deben llamar a este
    /// </summary>
    private ExcelBook(IWorkbook workbook)
    {
        this.objIWorkbook = workbook;
        this.objIDataFormat = objIWorkbook.CreateDataFormat();

        this.lstISheets = new List<ExcelSheet>();

        for (int i = 0; i < objIWorkbook.NumberOfSheets; i++)
        {
            ISheet objSheet = objIWorkbook.GetSheetAt(i);

            lstISheets.Add(new ExcelSheet(objSheet));
        }
    }

    /// <summary>
    /// Construye un Workbook de Excel con un nombre de archivo y un stream.
    /// </summary>
    public ExcelBook(String nombreArchivo, Stream stream) : this(_crear_workbook(nombreArchivo, stream))
    {
        //No hacer nada
    }

    private static IWorkbook _crear_workbook(String nombreArchivo, Stream stream)
    {
        if (ExtensionMatch(nombreArchivo, "xls"))
        {
            return new HSSFWorkbook(stream);
        }
        else if (ExtensionMatch(nombreArchivo, "xlsx") || ExtensionMatch(nombreArchivo, "xlsm"))
        {
            return new XSSFWorkbook(stream);
        }

        throw new Exception("Archivo de Excel no valido: " + nombreArchivo);
    }

    /// <summary>
    /// Comprueba si el nombre del archivo termina con cualquier extensión.
    /// </summary>
    public static bool ExtensionMatch(string nombreArchivo, params string[] extensiones)
    {
        if (nombreArchivo != null && extensiones != null)
        {
            string ArchivoExt = Path.GetExtension(nombreArchivo);

            if (ArchivoExt != null)
            {
                foreach (String item in extensiones)
                {
                    String ext = item;

                    if (ext != null && ext.Length > 0)
                    {
                        if (ext[0] != '.')
                        {
                            ext = "." + ext;
                        }

                        if (ArchivoExt.Equals(ext, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public DataSet GenerarDataSet(int IntTieneHeader, int IntPersonalizado)
    {
        /** IntTieneHeader -> Variable para saber si el archivo cuenta con encabezado
         * VALORES
         * 0 -> El archivo Excel NO TIENE encabezado
         * 1 ->  El archivo Excel TIENE encabezado (DEFAULT)
         * */
        /** IntPersonalizado -> Variable para archivos con formatos que necesiten funciones especiales o se necesitan acceder a filas aleatorias
         * VALORES
         * 0 -> Default
         * 1 -> Para formato de Estado de Cuenta BANAMEX. Se utiliza en ConciliacionBancoImportacion.cs
         * 2 -> Para formato de Estado de Cuenta BANORTE, BBVABANCOMER, BANREGIO, HSBC, IBC. Se utiliza en ConciliacionBancoImportacion.cs
         * */
        DataSet dataSet = new DataSet();

        foreach (ExcelSheet sheet in Sheets)
        {
            DataTable tabla = dataSet.Tables.Add(sheet.Nombre);

            int colI = 0;
            int colF = -1;
            int filaI = 1;
            if (IntTieneHeader == 1)
            {
                for (int col = colI; !string.IsNullOrEmpty(sheet.ObtenerString(0, col)) || !string.IsNullOrEmpty(sheet.ObtenerString(0, col + 1)); col++)
                {
                    tabla.Columns.Add(sheet.ObtenerString(0, col));
                    colF = col;
                }
            } else
            {
                for (int col = colI; !string.IsNullOrEmpty(sheet.ObtenerString(0, col)) || !string.IsNullOrEmpty(sheet.ObtenerString(0, col + 1)); col++)
                {
                    tabla.Columns.Add();
                    colF = col;
                }
            }

            if (colF != -1)
            {
                int Columnas = tabla.Columns.Count;
                if (IntPersonalizado == 0) { 
                    for (int fila = filaI; !sheet.esFilaVacia(fila, colI, colF); fila++)
                    {
                        object[] valores = new object[Columnas];

                        for (int i = 0; i < valores.Length; i++)
                        {
                            valores[i] = sheet.ObtenerValor(fila, colI + i);
                        }

                        tabla.Rows.Add(valores);
                    }
                }
                else if(IntPersonalizado == 1)
                {
                    filaI = 2;
                    for (int fila = filaI; !sheet.esFilaVacia(fila, colI, colF); fila+=3)
                    {
                        object[] valores = new object[Columnas];

                        for (int i = 0; i < valores.Length; i++)
                        {
                            valores[i] = sheet.ObtenerValor(fila, colI + i);
                        }

                        tabla.Rows.Add(valores);
                    }
                }
                else if (IntPersonalizado == 2)
                {
                    filaI = 0;
                    for (int fila = filaI; !sheet.esFilaVacia(fila, colI, colF); fila ++)
                    {
                        object[] valores = new object[Columnas];

                        for (int i = 0; i < valores.Length; i++)
                        {
                            valores[i] = sheet.ObtenerValor(fila, colI + i);
                        }

                        tabla.Rows.Add(valores);
                    }
                }
            }
        }

        return dataSet;
    }


    /// <summary>
    /// Obtiene las Worksheets de este Workbook.
    /// </summary>
    public IList<ExcelSheet> Sheets
    {
        get
        {
            return lstISheets.ToArray();
        }
    }
}