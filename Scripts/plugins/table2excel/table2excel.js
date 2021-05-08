/*! @source http://purl.eligrey.com/github/FileSaver.js/blob/master/FileSaver.js */
var saveAs = saveAs || function (e) { "use strict"; if (typeof navigator !== "undefined" && /MSIE [1-9]\./.test(navigator.userAgent)) { return } var t = e.document, n = function () { return e.URL || e.webkitURL || e }, r = t.createElementNS("http://www.w3.org/1999/xhtml", "a"), i = "download" in r, o = function (e) { var t = new MouseEvent("click"); e.dispatchEvent(t) }, a = /Version\/[\d\.]+.*Safari/.test(navigator.userAgent), f = e.webkitRequestFileSystem, u = e.requestFileSystem || f || e.mozRequestFileSystem, s = function (t) { (e.setImmediate || e.setTimeout)(function () { throw t }, 0) }, c = "application/octet-stream", d = 0, l = 500, w = function (t) { var r = function () { if (typeof t === "string") { n().revokeObjectURL(t) } else { t.remove() } }; if (e.chrome) { r() } else { setTimeout(r, l) } }, p = function (e, t, n) { t = [].concat(t); var r = t.length; while (r--) { var i = e["on" + t[r]]; if (typeof i === "function") { try { i.call(e, n || e) } catch (o) { s(o) } } } }, v = function (e) { if (/^\s*(?:text\/\S*|application\/xml|\S*\/\S*\+xml)\s*;.*charset\s*=\s*utf-8/i.test(e.type)) { return new Blob(["\ufeff", e], { type: e.type }) } return e }, y = function (t, s, l) { if (!l) { t = v(t) } var y = this, m = t.type, S = false, h, R, O = function () { p(y, "writestart progress write writeend".split(" ")) }, g = function () { if (R && a && typeof FileReader !== "undefined") { var r = new FileReader; r.onloadend = function () { var e = r.result; R.location.href = "data:attachment/file" + e.slice(e.search(/[,;]/)); y.readyState = y.DONE; O() }; r.readAsDataURL(t); y.readyState = y.INIT; return } if (S || !h) { h = n().createObjectURL(t) } if (R) { R.location.href = h } else { var i = e.open(h, "_blank"); if (i == undefined && a) { e.location.href = h } } y.readyState = y.DONE; O(); w(h) }, b = function (e) { return function () { if (y.readyState !== y.DONE) { return e.apply(this, arguments) } } }, E = { create: true, exclusive: false }, N; y.readyState = y.INIT; if (!s) { s = "download" } if (i) { h = n().createObjectURL(t); r.href = h; r.download = s; setTimeout(function () { o(r); O(); w(h); y.readyState = y.DONE }); return } if (e.chrome && m && m !== c) { N = t.slice || t.webkitSlice; t = N.call(t, 0, t.size, c); S = true } if (f && s !== "download") { s += ".download" } if (m === c || f) { R = e } if (!u) { g(); return } d += t.size; u(e.TEMPORARY, d, b(function (e) { e.root.getDirectory("saved", E, b(function (e) { var n = function () { e.getFile(s, E, b(function (e) { e.createWriter(b(function (n) { n.onwriteend = function (t) { R.location.href = e.toURL(); y.readyState = y.DONE; p(y, "writeend", t); w(e) }; n.onerror = function () { var e = n.error; if (e.code !== e.ABORT_ERR) { g() } }; "writestart progress write abort".split(" ").forEach(function (e) { n["on" + e] = y["on" + e] }); n.write(t); y.abort = function () { n.abort(); y.readyState = y.DONE }; y.readyState = y.WRITING }), g) }), g) }; e.getFile(s, { create: false }, b(function (e) { e.remove(); n() }), b(function (e) { if (e.code === e.NOT_FOUND_ERR) { n() } else { g() } })) }), g) }), g) }, m = y.prototype, S = function (e, t, n) { return new y(e, t, n) }; if (typeof navigator !== "undefined" && navigator.msSaveOrOpenBlob) { return function (e, t, n) { if (!n) { e = v(e) } return navigator.msSaveOrOpenBlob(e, t || "download") } } m.abort = function () { var e = this; e.readyState = e.DONE; p(e, "abort") }; m.readyState = m.INIT = 0; m.WRITING = 1; m.DONE = 2; m.error = m.onwritestart = m.onprogress = m.onwrite = m.onabort = m.onerror = m.onwriteend = null; return S }(typeof self !== "undefined" && self || typeof window !== "undefined" && window || this.content); if (typeof module !== "undefined" && module.exports) { module.exports.saveAs = saveAs } else if (typeof define !== "undefined" && define !== null && define.amd != null) { define([], function () { return saveAs }) }


////table2excel.js
//;(function ( $, window, document, undefined ) {
//    var pluginName = "table2excel",

//    defaults = {
//        exclude: ".noExl",
//                name: "Table2Excel"
//    };

//    // The actual plugin constructor
//    function Plugin ( element, options ) {
//            this.element = element;
//            // jQuery has an extend method which merges the contents of two or
//            // more objects, storing the result in the first object. The first object
//            // is generally empty as we don't want to alter the default options for
//            // future instances of the plugin
//            //
//            this.settings = $.extend( {}, defaults, options );
//            this._defaults = defaults;
//            this._name = pluginName;
//            this.init();
//    }

//    Plugin.prototype = {
//        init: function () {
//            var e = this;

//            e.template = {
//                head: "<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>",
//                sheet: {
//                    head: "<x:ExcelWorksheet><x:Name>",
//                    tail: "</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>"
//                },
//                mid: "</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body>",
//                table: {
//                    head: "<table>",
//                    tail: "</table>"
//                },
//                foot: "</body></html>"
//            };

//            e.tableRows = [];

//            // get contents of table except for exclude
//            $(e.element).each( function(i,o) {
//                var tempRows = "";
//                $(o).find("tr").not(e.settings.exclude).each(function (i,o) {
//                    tempRows += "<tr>" + $(o).html() + "</tr>";
//                });
//                e.tableRows.push(tempRows);
//            });

//            e.tableToExcel(e.tableRows, e.settings.name);
//        },

//        tableToExcel: function (table, name) {
//            var e = this, fullTemplate="", i, link, a;

//            e.uri = "data:application/vnd.ms-excel;base64,";
//            e.base64 = function (s) {
//                return window.btoa(unescape(encodeURIComponent(s)));
//            };
//            e.format = function (s, c) {
//                return s.replace(/{(\w+)}/g, function (m, p) {
//                    return c[p];
//                });
//            };
//            e.ctx = {
//                worksheet: name || "Worksheet",
//                table: table
//            };

//            fullTemplate= e.template.head;

//            if ( $.isArray(table) ) {
//                for (i in table) {
//                    //fullTemplate += e.template.sheet.head + "{worksheet" + i + "}" + e.template.sheet.tail;
//                    fullTemplate += e.template.sheet.head + "Table" + i + "" + e.template.sheet.tail;
//                }
//            }

//            fullTemplate += e.template.mid;

//            if ( $.isArray(table) ) {
//                for (i in table) {
//                    fullTemplate += e.template.table.head + "{table" + i + "}" + e.template.table.tail;
//                }
//            }

//            fullTemplate += e.template.foot;

//            for (i in table) {
//                e.ctx["table" + i] = table[i];
//            }
//            delete e.ctx.table;

//            if (typeof msie !== "undefined" && msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
//            {
//                if (typeof Blob !== "undefined") {
//                    //use blobs if we can
//                    fullTemplate = [fullTemplate];
//                    //convert to array
//                    var blob1 = new Blob(fullTemplate, { type: "text/html" });
//                    window.navigator.msSaveBlob(blob1, getFileName(e.settings) );
//                } else {
//                    //otherwise use the iframe and save
//                    //requires a blank iframe on page called txtArea1
//                    txtArea1.document.open("text/html", "replace");
//                    txtArea1.document.write(fullTemplate);
//                    txtArea1.document.close();
//                    txtArea1.focus();
//                    sa = txtArea1.document.execCommand("SaveAs", true, getFileName(e.settings) );
//                }

//            } else {
//                link = e.uri + e.base64(e.format(fullTemplate, e.ctx));
//                a = document.createElement("a");
//                a.download = getFileName(e.settings);
//                a.href = link;

//                document.body.appendChild(a);

//                a.click();

//                document.body.removeChild(a);
//            }

//            return true;
//        }
//    };

//    function getFileName(settings) {
//        return ( settings.filename ? settings.filename : "table2excel") + ".xlsx";
//    }

//    $.fn[ pluginName ] = function ( options ) {
//        var e = this;
//            e.each(function() {
//                if ( !$.data( e, "plugin_" + pluginName ) ) {
//                    $.data( e, "plugin_" + pluginName, new Plugin( this, options ) );
//                }
//            });

//        // chain jQuery functions
//        return e;
//    };

//})( jQuery, window, document );