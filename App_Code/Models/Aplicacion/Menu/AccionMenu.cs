using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccionesMenu
/// </summary>
public class AccionMenu
{
    int _IntBActivo;
    int _IntIdPagina;
    int _IntIdMenu;
    int _IntIdPaginaAccion; 
    int _IntIdAccion;
    string _StrPagina;
    string _StrAccion;
    string _StrCveAccionReferencia;
    string _StrDescAccion;
    int _IntBMovimientoEspecial;
    int _IntBClaveExiste;

    List<Accion> _LstObjAccion;
    List<PaginaMenu> _LstObjPagina;

    public AccionMenu()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string StrAccion
    {
        get { return _StrAccion; }
        set { _StrAccion = value; }
    }

    public string StrPagina
    {
        get { return _StrPagina; }
        set { _StrPagina = value; }
    }

    public int IntBActivo
    {
        get { return _IntBActivo; }
        set { _IntBActivo = value; }
    }

    public int IntIdMenu
    {
        get { return _IntIdMenu; }
        set { _IntIdMenu = value; }
    }

    public int IntIdPagina
    {
        get { return _IntIdPagina; }
        set { _IntIdPagina = value; }
    }

    public int IntIdAccion
    {
        get { return _IntIdAccion; }
        set { _IntIdAccion = value; }
    }

    public int IntIdPaginaAccion
    {
        get { return _IntIdPaginaAccion; }
        set { _IntIdPaginaAccion = value; }
    }

    public List<Accion> LstObjAccion
    {
        get { return _LstObjAccion; }
        set { _LstObjAccion = value; }
    }

    public List<PaginaMenu> LstObjPagina
    {
        get { return _LstObjPagina; }
        set { _LstObjPagina = value; }
    }

    public string StrCveAccionReferencia
    {
        get
        {
            return _StrCveAccionReferencia;
        }

        set
        {
            _StrCveAccionReferencia = value;
        }
    }

    public string StrDescAccion
    {
        get
        {
            return _StrDescAccion;
        }

        set
        {
            _StrDescAccion = value;
        }
    }

    public int IntBMovimientoEspecial
    {
        get
        {
            return _IntBMovimientoEspecial;
        }

        set
        {
            _IntBMovimientoEspecial = value;
        }
    }

    public int IntBClaveExiste
    {
        get
        {
            return _IntBClaveExiste;
        }

        set
        {
            _IntBClaveExiste = value;
        }
    }
}