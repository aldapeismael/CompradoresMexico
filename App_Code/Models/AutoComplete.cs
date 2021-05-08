using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AutoComplete
/// </summary>
public class ResultadoBusqueda
{
    public int totalResponse { get; set; }
    public string inputPhrase { get; set; }
    public List<ListaBusqueda> listResults { get; set; }
}
public class ListaBusqueda
{
    public int Error { get; set; }
    public string MensajeError { get; set; }
    public string TipoError { get; set; }

    public long Value { get; set; }
    public string Text { get; set; }
    public int IdRef1 { get; set; }
    public int IdRef2 { get; set; }
    public int IdRef3 { get; set; }
    public int IdRef4 { get; set; }
    public int IdRef5 { get; set; }
    public int IdRef6 { get; set; }
    public int IdRef7 { get; set; }
    public int IdRef8 { get; set; }
    public int IdRef9 { get; set; }
    public string Des1 { get; set; }
	public string Des2 { get; set; }
	public string Des3 { get; set; }
	public string Des4 { get; set; }
    public string Des5 { get; set; }
    public string Des6 { get; set; }
    public string Des7 { get; set; }
    public string Des8 { get; set; }
    public string Des9 { get; set; }
    public string Des10 { get; set; }
    public string Des11 { get; set; }
    public string Des12 { get; set; }
    public DateTime Fec1 { get; set; }
    public decimal Dec1 { get; set; }
    public decimal Dec2 { get; set; }
    public decimal Dec3 { get; set; }
    public decimal Dec4 { get; set; }
    public string Json1 { get; set; }
    //public string Auxiliar1 { get; set; } //Se pueden Agregar mas datos para mostrar

    //Descripciones Generales (para no hacer if else en autocomplete)
    public string Des50 { get; set; }
    public string Des51 { get; set; }
    public string Des52 { get; set; }
    public string Des53 { get; set; }

    public decimal Dec50 { get; set; }
    public decimal Dec51 { get; set; }

    public string Json50 { get; set; }

}

public class ListaParametrosBusqueda
{
    public int IntLimite { get; set; }
	public int IntTipoBusqueda  { get; set; } 
	public string StrBuscar  { get; set; }
    public int IdRef1 { get; set; }
    public int IdRef2 { get; set; }
    public int IdRef3 { get; set; }
    public int IdRef4 { get; set; }
    public int IdRef5 { get; set; }
    public int IdRef6 { get; set; }
    public int IdRef7 { get; set; }
    public int IdRef8 { get; set; }
    public string Des1 { get; set; }
	public string Des2 { get; set; }
	public string Des3 { get; set; }
    public DateTime Fecha1 { get; set; }
}

/// <summary>
/// 
/// </summary>
public class ObjetoBusqueda
{
    // int
    public int IntEjecuta       { get; set; }
	public int IntLimite		{ get; set; }
	public int IntTipoBusqueda  { get; set; } 
	
	public int IntId1			{ get; set; }
    public int IntId2			{ get; set; }
    public int IntId3			{ get; set; }
	public int IntId4			{ get; set; }
    public int IntId5			{ get; set; }
    public int IntId6			{ get; set; }
    public int IntId7			{ get; set; }
    public int IntId8			{ get; set; }
    public int IntId9			{ get; set; }
    public int IntId10			{ get; set; }
    public int IntId11			{ get; set; }
    public int IntId12          { get; set; }
	
	// string
	public string StrBuscar		{ get; set; }
    
	public string StrDes1		{ get; set; }
	public string StrDes2		{ get; set; }
	public string StrDes3		{ get; set; }
	public string StrDes4		{ get; set; }

	public string StrJson1		{ get; set; }

	// DateTime
	public DateTime DtFec1		{ get; set; }
	public DateTime DtFec2		{ get; set; }
	public DateTime DtFec3		{ get; set; }

	// Decimal
	public decimal DecCantidad1			{ get; set; }
    public decimal DecCantidad2			{ get; set; }
}

