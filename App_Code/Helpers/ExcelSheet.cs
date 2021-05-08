using NPOI.SS.UserModel;
using System;

/// <summary>
/// Summary description for ExcelSheet
/// </summary>
public class ExcelSheet
{
    internal readonly ISheet objISheet;

    /// <summary>
    /// Construye el objeto con un ISheet
    /// </summary>
    internal ExcelSheet(ISheet sheet)
    {
        this.objISheet = sheet;
    }

    /// <summary>
    /// Devuelve el nombre de un sheet.
    /// </summary>
    public String Nombre
    {
        get { return objISheet.SheetName; }
    }

    /// <summary>
    /// Devuelve el valor en la celda especificada como String o null
    /// </summary>
    public String ObtenerString(int fila, int col)
    {
        ICell objCell = ExcelLibrary.obtener_ICell(objISheet, fila, col);

        if (objCell != null)
        {
            return objCell.ToString();
        }

        return null;
    }

    /// <summary>
    /// Determina si las celdas de un rango de la fila especificada están vacías.
    /// </summary>
    public bool esFilaVacia(int fila, int colInicio, int colFin)
    {
        IRow objIRow = objISheet.GetRow(fila);

        if (objIRow != null)
        {
            for (int colIndex = colInicio; colIndex <= colFin; colIndex++)
            {
                ICell objICell = objIRow.GetCell(colIndex);

                if (objICell != null && objICell.CellType != CellType.Blank)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Object ObtenerValor(int fila, int col)
    {
        ICell objCell = ExcelLibrary.obtener_ICell(objISheet, fila, col);

        if (objCell != null)
        {
            switch (objCell.CellType)
            {
                case CellType.Blank:
                    return String.Empty;
                case CellType.Numeric:
                    if (objCell.CellStyle != null)
                    {
                        if (objCell.CellStyle.DataFormat == 164 || objCell.CellStyle.DataFormat == 14)
                        {
                            return objCell.DateCellValue;
                        }
                    }

                    return objCell.NumericCellValue;
                case CellType.Boolean:
                    return objCell.BooleanCellValue;
                default:
                    return objCell.ToString();
            }
        }

        return null;
    }
}