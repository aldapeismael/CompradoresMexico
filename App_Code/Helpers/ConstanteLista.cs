using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConstanteLista
/// </summary>
public class ConstanteLista
{
    public enum TipoLista
    {
        General = 0,
        DataMultiple = 1,
        AutoComplete = 2
    }

    public enum TipoTexto
    {
        Descripcion = 0,
        Clave = 1,
        ClaveDescripcion = 2
    }

    public const int TokenTiempoExpira = 180;
}