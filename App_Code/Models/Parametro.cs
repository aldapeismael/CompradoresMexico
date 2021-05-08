using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Parametro
{
	#region parametros
	// Definición de parámetros de BD
	int			_IntIdParametro;
	int			_IntTipoBusqueda;
	string		_StrCveParametro;
	string		_StrDescParametro;
	string		_StrValor1char;
    int      _IntValor1num;
    string _StrValor2char;

    #region GetAndSet
    public int		IntIdParametro
	{
		get
		{
			return _IntIdParametro;
		}

		set
		{
			_IntIdParametro = value;
		}
	}
	public string	StrCveParametro
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
	public string	StrDescParametro
	{
		get
		{
			return _StrDescParametro;
		}

		set
		{
			_StrDescParametro = value;
		}
	}
	public int		IntTipoBusqueda
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
	public string	StrValor1char
	{
		get
		{
			return _StrValor1char;
		}

		set
		{
			_StrValor1char = value;
		}
	}
    public int IntValor1num
    {
        get
        {
            return _IntValor1num;
        }

        set
        {
            _IntValor1num = value;
        }
    }
    public string StrValor2char
    {
        get
        {
            return _StrValor2char;
        }

        set
        {
            _StrValor2char = value;
        }
    }
    #endregion GetAndSet

    #endregion parametros

    public bool ObtenerPorClaveParametro()
    {
        //instanciamos los objeto y atributos a utilizar
        bool bool_Valido = false;

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spParametroObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
			sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", this._IntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_CveParametro", this._StrCveParametro);

            DataSet dataSetObtener = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "Archivo : Dolly.cs, Metdod: ObtenerPorId()");

            if (dataSetObtener.Tables.Count > 0 && dataSetObtener != null && dataSetObtener.Tables[0].Rows.Count > 0) 
            {
				this._IntIdParametro =		int.Parse(dataSetObtener.Tables[0].Rows[0]["idParametro"].ToString());
				this._StrCveParametro =		dataSetObtener.Tables[0].Rows[0]["cveParametro"].ToString();
				this._StrDescParametro =	dataSetObtener.Tables[0].Rows[0]["descParametro"].ToString();
				this._StrValor1char =		dataSetObtener.Tables[0].Rows[0]["valor1char"].ToString();
                this._IntValor1num =        int.Parse(dataSetObtener.Tables[0].Rows[0]["valor1num"].ToString());
                this._StrValor2char =       dataSetObtener.Tables[0].Rows[0]["valor2char"].ToString();
                bool_Valido = true;
            }
        }
        catch
        {
            bool_Valido = false;
        }

        return bool_Valido;
    }

}