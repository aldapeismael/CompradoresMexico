using Newtonsoft.Json;
using OpenHtmlToPdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class HtmlString
{
    public static string RenderViewToString(ControllerContext context, string viewPath, dynamic model = null, bool partial = false)
    {
        // first find the ViewEngine for this view
        ViewEngineResult viewEngineResult = null;
        if (partial)
            viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
        else
            viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

        if (viewEngineResult == null)
            throw new FileNotFoundException("View cannot be found.");

        // get the view and attach the model to view data
        var view = viewEngineResult.View;
        context.Controller.ViewData.Model = model;

        string result = null;

        using (var sw = new StringWriter())
        {
            var ctx = new ViewContext(context, view,
                                        context.Controller.ViewData,
                                        context.Controller.TempData,
                                        sw);
            view.Render(ctx, sw);
            viewEngineResult.ViewEngine.ReleaseView(ctx, viewEngineResult.View);
            result = sw.ToString();
        }
        //Response.BufferOutput = true;
        return result;
    }

    public static T CreateController<T>(RouteData routeData = null)
    where T : Controller, new()
    {
        // create a disconnected controller instance
        T controller = new T();

        // get context wrapper from HttpContext if available
        HttpContextBase wrapper;
        if (System.Web.HttpContext.Current != null)
            wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
        else
            throw new InvalidOperationException(
                "Can't create Controller Context if no " +
                "active HttpContext instance is available.");

        if (routeData == null)
            routeData = new RouteData();

        // add the controller routing if not existing
        if (!routeData.Values.ContainsKey("controller") &&
            !routeData.Values.ContainsKey("Controller"))
            routeData.Values.Add("controller",
                                 controller.GetType()
                                           .Name.ToLower().Replace("controller", ""));

        controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
        return controller;
    }

    public async Task<HttpResponseMessage> ExecutePostAsync<M>(M ObjModel, string StrPath, string Strtoken = null)
            where M : class
    {
        try
        {
            if (string.IsNullOrEmpty(StrPath))
                throw new ArgumentNullException(nameof(StrPath));

            if (ObjModel == null)
                throw new ArgumentNullException(nameof(ObjModel));

            // Crea al cliente http
            using (HttpClient objHttpClient = new HttpClient())
            {
                // Transforma el objecto a un json y genera el contenido para el request
                HttpContent objHttpContent = new StringContent(content: JsonConvert.SerializeObject(ObjModel),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

                // Al cliente http le pasas la url base del api y le pones los headers necesarios
                objHttpClient.BaseAddress = new Uri(VariableGlobal.StrUrlApi);
                objHttpClient.DefaultRequestHeaders.Accept.Clear();
                objHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Si el token no viene nulo le agrega el header para mandarlo 
                if (!string.IsNullOrEmpty(Strtoken))
                    objHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + Strtoken);

                // Manda la petición del cliente con el path esperando un http response
                HttpResponseMessage objHttpResponseMessage = await objHttpClient.PostAsync(StrPath, objHttpContent);
                objHttpResponseMessage.EnsureSuccessStatusCode();

                // Si la peticion fue exitasa regresa el http response
                if (objHttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return objHttpResponseMessage;
                }
                else
                {
                    throw new HttpRequestException("Bad Request");
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
public class GenericController : Controller
{ }