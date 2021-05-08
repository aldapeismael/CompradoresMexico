using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

[AutorizacionRequerida]
public class MenuController : ApiController
{
    public IEnumerable<dynamic> ObtenerLista(int ParamTipoBusqueda, int ParamTipoFiltro)
    {
        return Menu.ObtenerLista(ParamTipoBusqueda, ParamTipoFiltro);
    }

    // GET api/<controller>/5
    public JsonResult<List<Menu>> Get(string ParamStrBuscar, int ParamIntEjecuta)//metodo para obtener por Busqueda en filtro
    {
        Menu ObjMenu = new Menu();
        return Json(ObjMenu.ObtenerGrid(ParamStrBuscar, ParamIntEjecuta));
    }

    [HttpPost] //Obtener Menu para dibujar por razor
    public List<Menu> ObtenerMenu(string ParamStrBuscar, int ParamIntEjecuta)
    {
        Menu ObjMenu = new Menu();
        return ObjMenu.ObtenerGrid(ParamStrBuscar, ParamIntEjecuta);
    }

    // GET api/<controller>/5
    public JsonResult<Menu> Get(int id)//metodo para obtener por ID
    {
        Menu ObjMenu = new Menu(id);
        ObjMenu.ObtenerPorId();
        return Json(ObjMenu);
    }

    public JsonResult<RespuestaBD> Post([FromBody] Menu value)// metodo para insertar
    {
        //Menu ObjMenu = new Menu();
        return Json(value.Insertar());
    }

    public JsonResult<RespuestaBD> Put([FromBody] Menu value)//metodo para actualizar
    {
        //Menu ObjMenu = new Menu();
        return Json(value.Actualizar());
    }

    // DELETE api/<controller>/5
    public JsonResult<RespuestaBD> Delete(int id)//metodo para eliminar
    {
        Menu ObjMenu = new Menu(id);
        return Json(ObjMenu.Eliminar());
    }
}
