using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioAplicacionComplementaria
/// </summary>
public class UsuarioAplicacionComplementaria
{
    int _IntIdUsuarioAplicacionComplementaria;
    int _IntIdUsuario;
    int _IntIdAplicacionComplementaria;
    int _IntIdEmpresa;
    int _IntBActivo;

    public UsuarioAplicacionComplementaria()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

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

    public int IntIdAplicacionComplementaria
    {
        get
        {
            return _IntIdAplicacionComplementaria;
        }

        set
        {
            _IntIdAplicacionComplementaria = value;
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

    public int IntIdUsuarioAplicacionComplementaria
    {
        get
        {
            return _IntIdUsuarioAplicacionComplementaria;
        }

        set
        {
            _IntIdUsuarioAplicacionComplementaria = value;
        }
    }
}