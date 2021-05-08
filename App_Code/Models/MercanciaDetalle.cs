using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de PedidoMercancia
/// </summary>
public class MercanciaDetalle
{
    int _IntIdPedidoMercancia;
    int _IntIdPedido;
    int _IntIdMercancia;
    decimal _DecCantidad;
    decimal _DecPeso;
	decimal _DecPesoEstimado;
    int _IntIdMultilista_embalaje;
    int _IntIdMultilista_medida;
    int _IntBActivo;
	string _StrDescripcion1;
	string _StrDescripcion2;
	
	// Parámetros extras
	string _StrMercancia;
	string _StrEmbalaje;
	string _StrMedida;
	bool _BDetalle;

    public MercanciaDetalle()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public int IntIdPedidoMercancia
    {
        get
        {
            return _IntIdPedidoMercancia;
        }

        set
        {
            _IntIdPedidoMercancia = value;
        }
    }

    public int IntIdPedido
    {
        get
        {
            return _IntIdPedido;
        }

        set
        {
            _IntIdPedido = value;
        }
    }

    public int IntIdMercancia
    {
        get
        {
            return _IntIdMercancia;
        }

        set
        {
            _IntIdMercancia = value;
        }
    }

    public decimal DecCantidad
    {
        get
        {
            return _DecCantidad;
        }

        set
        {
            _DecCantidad = value;
        }
    }

    public decimal DecPeso
    {
        get
        {
            return _DecPeso;
        }

        set
        {
            _DecPeso = value;
        }
    }

    public int IntIdMultilista_embalaje
    {
        get
        {
            return _IntIdMultilista_embalaje;
        }

        set
        {
            _IntIdMultilista_embalaje = value;
        }
    }

    public int IntIdMultilista_medida
    {
        get
        {
            return _IntIdMultilista_medida;
        }

        set
        {
            _IntIdMultilista_medida = value;
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

	public decimal DecPesoEstimado
	{
		get
		{
			return _DecPesoEstimado;
		}

		set
		{
			_DecPesoEstimado = value;
		}
	}

	public string StrDescripcion1
	{
		get
		{
			return _StrDescripcion1;
		}

		set
		{
			_StrDescripcion1 = value;
		}
	}

	public string StrDescripcion2
	{
		get
		{
			return _StrDescripcion2;
		}

		set
		{
			_StrDescripcion2 = value;
		}
	}

	public string StrEmbalaje
	{
		get
		{
			return _StrEmbalaje;
		}

		set
		{
			_StrEmbalaje = value;
		}
	}

	public string StrMedida
	{
		get
		{
			return _StrMedida;
		}

		set
		{
			_StrMedida = value;
		}
	}

	public bool BDetalle
	{
		get
		{
			return _BDetalle;
		}

		set
		{
			_BDetalle = value;
		}
	}

	public string StrMercancia
	{
		get
		{
			return _StrMercancia;
		}

		set
		{
			_StrMercancia = value;
		}
	}

	public List<MercanciaDetalle> ObtenerListaGenerica(int ParamIntTipoBusqueda, int ParamIntIdGenerico1, int ParamIntIdGenerico2, string ParamStringGenerico1, string ParamStringGenerico2)
    {
        var IntIdEmpresa = VariableGlobal.SessionIntIdEmpresa;
        var IntIdUsuario = VariableGlobal.SessionIntIdUsuario;
        List<MercanciaDetalle> lstObjMercanciaDetalleListado = new List<MercanciaDetalle>();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "corporativo.spMercanciaDetalleObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", ParamIntTipoBusqueda);
            sqlCommand.Parameters.AddWithValue("@p_IntIdGenerico1", ParamIntIdGenerico1);
            sqlCommand.Parameters.AddWithValue("@p_IntIdGenerico2", ParamIntIdGenerico2);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico1", ParamStringGenerico1);
            sqlCommand.Parameters.AddWithValue("@p_StrGenerico2", ParamStringGenerico2);

            DataSet dataSetObtenerDataTable = ConexionBD.EjecutarComando(VariableGlobal.SessionIntIdEmpresa, VariableGlobal.SessionIntIdUsuario, sqlCommand, "archivo : RemitenteDestinatario.cs, metodo : ObtenerListaGenerica()");

            if (dataSetObtenerDataTable != null && dataSetObtenerDataTable.Tables.Count > 0)
            {
                foreach (DataRow FilaRemitenteDestino in dataSetObtenerDataTable.Tables[0].Rows)
                {
                    lstObjMercanciaDetalleListado.Add(new MercanciaDetalle
                    {
                        IntIdPedidoMercancia = int.Parse(FilaRemitenteDestino["IntIdPedidoMercancia"].ToString()),
						IntIdMercancia = int.Parse(FilaRemitenteDestino["IdMercancia"].ToString()),
						StrMercancia = FilaRemitenteDestino["mercancia"].ToString(),
						DecCantidad = decimal.Parse(FilaRemitenteDestino["Cantidad"].ToString()),
						DecPeso = decimal.Parse(FilaRemitenteDestino["Peso"].ToString()),
						DecPesoEstimado = decimal.Parse(FilaRemitenteDestino["PesoEstimado"].ToString()),
						IntIdMultilista_embalaje = int.Parse(FilaRemitenteDestino["IdMultilista_embalaje"].ToString()),
						IntIdMultilista_medida = int.Parse(FilaRemitenteDestino["Idmultilista_medida"].ToString()),
						StrDescripcion1 = FilaRemitenteDestino["Descripcion1"].ToString(),
						StrDescripcion2 = FilaRemitenteDestino["Descripcion2"].ToString(),
						StrEmbalaje = FilaRemitenteDestino["embalaje"].ToString(),
						StrMedida	= FilaRemitenteDestino["medida"].ToString(),
						BDetalle	= int.Parse(FilaRemitenteDestino["Idmultilista_medida"].ToString()) > 0 ? true : false
                    });
                }
				
            }
        }
        catch (Exception e)
        {
			RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener los registros, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
			lstObjMercanciaDetalleListado = null;
        }

        return lstObjMercanciaDetalleListado;
    }
}