using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

[AutorizacionRequerida]
public class MeGustaController : ApiController
{

    [HttpPost]
    public JsonResult<RespuestaBD> Post([FromBody]MeGusta value)// metodo para insertar
    {
        return Json(value.Insertar());
    }

    // GET api/<controller>/5
    public JsonResult<MeGusta> Get(int id)
    {
        MeGusta objMeGusta = new MeGusta(id);
        objMeGusta.ObtenerPorId();
        return Json(objMeGusta);
    }

    // PUT api/<controller>/5
    public JsonResult<RespuestaBD> Put([FromBody]MeGusta value)
    {
        return Json(value.Actualizar());
    }

    public IEnumerable<dynamic> ObtenerLista(int ParamIntTipoLista, int ParamIntTipoTexto)
    {
        return MeGusta.ObtenerLista(ParamIntTipoLista, ParamIntTipoTexto);
    }


    [HttpPost]
    public JsonResult<RespuestaDataTable<MeGusta>> ObtenerListado(FiltrosDTMeGusta objMeGusta)//metodo post para datatable
    {
        MeGusta obj_MeGusta = new MeGusta();
        return Json(obj_MeGusta.ResultadoDataTables(JsonConvert.SerializeObject(objMeGusta)));
    }


}