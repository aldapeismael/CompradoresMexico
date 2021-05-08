using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioEmpresaTerminal
/// </summary>
public class UsuarioTerminal
{
    int _IntIdUsuarioTerminal;
    int _IntIdUsuario;
    int _IntIdTerminal;
    int _IntIdEmpresa;
    int _IntBActivo;

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

    public int IntIdTerminal
    {
        get
        {
            return _IntIdTerminal;
        }

        set
        {
            _IntIdTerminal = value;
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

    public int IntIdUsuarioTerminal
    {
        get
        {
            return _IntIdUsuarioTerminal;
        }

        set
        {
            _IntIdUsuarioTerminal = value;
        }
    }

    public UsuarioTerminal()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}