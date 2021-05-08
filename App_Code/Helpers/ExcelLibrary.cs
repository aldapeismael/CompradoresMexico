using NPOI.SS.UserModel;

/// <summary>
/// Summary description for ExcelLibrary
/// </summary>
public class ExcelLibrary
{
    /// <summary>
    /// Obtiene el objeto ICell con coordenadas o devuelve null si no existe
    /// </summary>
    public static ICell obtener_ICell(ISheet sheet, int fila, int col)
    {
        IRow objRow = sheet.GetRow(fila);

        if (objRow != null)
        {
            ICell objCell = objRow.GetCell(col);

            if (objCell != null)
            {
                return objCell;
            }
        }

        return null;
    }
}