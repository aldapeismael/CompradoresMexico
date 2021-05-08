using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de IMetodosModelos
/// </summary>
public interface IMetodosModelos<T>
{
    RespuestaDataTable<T> ResultadoDataTables(string filtros);
    RespuestaBD Insertar();
    bool ObtenerPorId();
    RespuestaBD Actualizar();
    RespuestaBD Eliminar();
}