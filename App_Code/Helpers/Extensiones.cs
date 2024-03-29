﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
/// <summary>
/// Descripción breve de Extensiones
/// </summary>
public static class Extensiones
{
    public static IEnumerable<dynamic> ObtenerComboDinamico(this DataTable table)
    {
        // Validate argument here..

        return table.AsEnumerable().Select(row => new DynamicRow(row));
    }

    private sealed class DynamicRow : DynamicObject
    {
        private readonly DataRow _row;

        internal DynamicRow(DataRow row) { _row = row; }

        // Interprets a member-access as an indexer-access on the 
        // contained DataRow.
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var retVal = _row.Table.Columns.Contains(binder.Name);
            result = retVal ? _row[binder.Name] : null;
            return retVal;
        }
    }
}