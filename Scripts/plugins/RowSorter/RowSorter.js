!function (t, o) { "use strict"; "function" == typeof define && define.amd ? define("RowSorter", o) : "object" == typeof exports ? module.exports = o() : t.RowSorter = o() }(this, function () { "use strict"; var i = window.jQuery || !1, e = Array.prototype, r = !!("ontouchstart" in document), n = "data-rowsorter", s = { handler: null, tbody: !0, tableClass: "sorting-table", dragClass: "sorting-row", stickTopRows: 0, stickBottomRows: 0, onDragStart: null, onDragEnd: null, onDrop: null }; function h(t, o) { if (!(this instanceof h)) return new h(t, o); if ("string" == typeof t && (t = f(t)), !1 === g(t, "table")) throw new Error("Table not found."); if (t[n] instanceof h) return t[n]; this._options = function (t, o) { if (i) return i.extend({}, t, o); var n, s = {}; for (n in t) t.hasOwnProperty(n) && (s[n] = t[n]); if (o && "[object Object]" === Object.prototype.toString.call(o)) for (n in o) o.hasOwnProperty(n) && (s[n] = o[n]); return s }(s, o), this._table = t, this._tbody = t, this._rows = [], this._lastY = !1, this._draggingRow = null, this._firstTouch = !0, this._lastSort = null, this._ended = !0, this._mousedown = D(a, this), this._mousemove = D(l, this), this._mouseup = D(c, this), this._touchstart = D(u, this), this._touchmove = D(d, this), this._touchend = D(_, this), this._touchId = null, (this._table[n] = this).init() } function a(t) { return t = t || window.event, !this._start(t.target || t.srcElement, t.clientY) || (t.preventDefault ? t.preventDefault() : t.returnValue = !1, !1) } function u(t) { if (1 === t.touches.length) { var o = t.touches[0], n = document.elementFromPoint(o.clientX, o.clientY); if (this._touchId = o.identifier, this._start(n, o.clientY)) return t.preventDefault ? t.preventDefault() : t.returnValue = !1, !1 } return !0 } function l(t) { return t = t || window.event, this._move(t.target || t.srcElement, t.clientY), !0 } function d(t) { if (1 === t.touches.length) { var o = t.touches[0], n = document.elementFromPoint(o.clientX, o.clientY); this._touchId === o.identifier && this._move(n, o.clientY) } return !0 } function c() { this._end() } function _(t) { 0 < t.changedTouches.length && this._touchId === t.changedTouches[0].identifier && this._end() } function p(t) { return t instanceof h ? t : ("string" == typeof t && (t = f(t)), g(t, "table") && n in t && t[n] instanceof h ? t[n] : null) } function f(t) { var o = E(document, t); return 0 < o.length && g(o[0], "table") ? o[0] : null } function g(t, o) { return t && "object" == typeof t && "nodeName" in t && t.nodeName === o.toUpperCase() } function w(t, o) { for (var n = t.rows, s = n.length, i = 0; i < s; i++)if (o === n[i]) return i; return -1 } function m(t, o, n) { t.attachEvent ? t.attachEvent("on" + o, n) : t.addEventListener(o, n, !1) } function v(t, o, n) { t.detachEvent ? t.detachEvent("on" + o, n) : t.removeEventListener(o, n, !1) } function b(t) { return t.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, "") } function y(t, o) { if ("" === (o = b(o))) return !1; if (-1 === o.indexOf(" ")) return t.classList ? !!t.classList.contains(o) : !!t.className.match(new RegExp("(\\s|^)" + o + "(\\s|$)")); for (var n = o.replace(/\s+/g, " ").split(" "), s = 0, i = n.length; s < i; s++)if (!1 === y(t, n[s])) return !1; return !0 } function R(t, o) { if ("" !== (o = b(o))) if (-1 === o.indexOf(" ")) !1 === y(t, o) && (t.classList ? t.classList.add(o) : t.className += " " + o); else for (var n = o.replace(/\s+/g, " ").split(" "), s = 0, i = n.length; s < i; s++)R(t, n[s]) } function x(t, o) { if ("" !== (o = b(o))) if (-1 === o.indexOf(" ")) y(t, o) && (t.classList ? t.classList.remove(o) : t.className = t.className.replace(new RegExp("(\\s|^)" + o + "(\\s|$)"), " ")); else for (var n = o.replace(/\s+/g, " ").split(" "), s = 0, i = n.length; s < i; s++)x(t, n[s]) } function D(t, o) { return Function.prototype.bind ? t.bind(o) : function () { t.apply(o, e.slice.call(arguments)) } } function E(t, o) { return i ? i.makeArray(i(t).find(o)) : t.querySelectorAll(o) } function k(t, o) { var n = 1, s = t; for (o = o.toLowerCase(); s.tagName && s.tagName.toLowerCase() !== o;) { if (20 < n || !s.parentNode) return null; s = s.parentNode, n++ } return s } function S(t, o) { if (e.indexOf) return e.indexOf.call(t, o); for (var n = 0, s = t.length; n < s; n++)if (o === t[n]) return n; return -1 } return h.prototype.init = function () { if (this._options.tbody) { var t = this._table.getElementsByTagName("tbody"); 0 < t.length && (this._tbody = t[0]) } if ("function" != typeof this._options.onDragStart && (this._options.onDragStart = null), "function" != typeof this._options.onDrop && (this._options.onDrop = null), "function" != typeof this._options.onDragEnd && (this._options.onDragEnd = null), ("number" != typeof this._options.stickTopRows || this._options.stickTopRows < 0) && (this._options.stickTopRows = 0), ("number" != typeof this._options.stickBottomRows || this._options.stickBottomRows < 0) && (this._options.stickBottomRows = 0), m(this._table, "mousedown", this._mousedown), m(document, "mouseup", this._mouseup), r && (m(this._table, "touchstart", this._touchstart), m(this._table, "touchend", this._touchend)), "onselectstart" in document) { var n = this; m(document, "selectstart", function (t) { var o = t || window.event; if (null !== n._draggingRow) return o.preventDefault ? o.preventDefault() : o.returnValue = !1, !1 }) } }, h.prototype._start = function (t, o) { if (this._draggingRow && this._end(), this._rows = this._tbody.rows, this._rows.length < 2) return !1; if (this._options.handler) { var n = E(this._table, this._options.handler); if (!n || -1 === S(n, t)) return !1 } var s = k(t, "tr"), i = w(this._tbody, s); return !(-1 === i || 0 < this._options.stickTopRows && i < this._options.stickTopRows || 0 < this._options.stickBottomRows && i >= this._rows.length - this._options.stickBottomRows) && (this._draggingRow = s, this._options.tableClass && R(this._table, this._options.tableClass), this._options.dragClass && R(this._draggingRow, this._options.dragClass), this._oldIndex = i, this._options.onDragStart && this._options.onDragStart(this._tbody, this._draggingRow, this._oldIndex), this._lastY = o, this._ended = !1, m(this._table, "mousemove", this._mousemove), r && m(this._table, "touchmove", this._touchmove), !0) }, h.prototype._move = function (t, o) { if (this._draggingRow) { var n, s, i, e, r = o > this._lastY ? 1 : o < this._lastY ? -1 : 0; if (0 !== r) { var h = k(t, "tr"); if (h && h !== this._draggingRow && -1 !== S(this._rows, h)) { var a = !0; if (0 < this._options.stickTopRows || 0 < this._options.stickBottomRows) { var u = w(this._tbody, h); (0 < this._options.stickTopRows && u < this._options.stickTopRows || 0 < this._options.stickBottomRows && u >= this._rows.length - this._options.stickBottomRows) && (a = !1) } a && (n = this._draggingRow, s = h, i = r, e = n.parentNode, 1 === i ? s.nextSibling ? e.insertBefore(n, s.nextSibling) : e.appendChild(n) : -1 === i && e.insertBefore(n, s)), this._lastY = o } } } }, h.prototype._end = function () { if (!this._draggingRow) return !0; this._options.tableClass && x(this._table, this._options.tableClass), this._options.dragClass && x(this._draggingRow, this._options.dragClass); var t = w(this._tbody, this._draggingRow); if (t !== this._oldIndex) { var o = this._lastSort; this._lastSort = { previous: o, newIndex: t, oldIndex: this._oldIndex }, this._options.onDrop && this._options.onDrop(this._tbody, this._draggingRow, t, this._oldIndex) } else this._options.onDragEnd && this._options.onDragEnd(this._tbody, this._draggingRow, this._oldIndex); this._draggingRow = null, this._lastY = !1, this._touchId = null, this._ended = !0, v(this._table, "mousemove", this._mousemove), r && v(this._table, "touchmove", this._touchmove) }, h.prototype.undo = h.prototype.revert = function () { if (null !== this._lastSort) { var t = this._lastSort, o = t.oldIndex, n = t.newIndex, s = this._tbody.rows, i = s.length - 1; 1 < s.length && (o < i ? this._tbody.insertBefore(s[n], s[o + (o < n ? 0 : 1)]) : this._tbody.appendChild(s[n])), this._lastSort = t.previous } }, h.prototype.destroy = function () { this._table[n] = null, !1 === this._ended && this._end(), v(this._table, "mousedown", this._mousedown), v(document, "mouseup", this._mouseup), r && (v(this._table, "touchstart", this._touchstart), v(this._table, "touchend", this._touchend)) }, h.undo = h.revert = function (t, o) { var n = p(t); if (null === n && !1 === o) throw new Error("Table not found."); n && n.revert() }, h.destroy = function (t, o) { var n = p(t); if (null === n && !1 === o) throw new Error("Table not found."); n && n.destroy() }, i && (i.fn.extend({ rowSorter: function (n) { var s = []; return this.each(function (t, o) { s.push(new h(o, n)) }), 1 === s.length ? s[0] : s } }), i.rowSorter = { undo: h.undo, revert: h.revert, destroy: h.destroy }), h });