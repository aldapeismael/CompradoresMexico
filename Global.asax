<%@ Application Language="C#" %>

<script runat="server">
    string UrlPrefixRelative { get { return "~/api"; } }

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse la aplicación
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación
        System.Diagnostics.Debug.WriteLine("Application_ended"); //write console

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error sin procesar
        Exception ex = Server.GetLastError();

        HttpException httpException = (HttpException) ex;
        int httpCode = httpException.GetHttpCode();
        int IntPopUp = int.Parse(Request.QueryString["Popup"]?.ToString() ?? "0");
        string path = "N/A";
        string strCodigoError = "";

        if (sender is HttpApplication){
            path = ((HttpApplication) sender).Request.Url.PathAndQuery;
        }

        string Str_ErrorMensaje = string.Format("<b> Path:</b> {0}", path);
        string Str_Excepcion = ex.InnerException?.Message?.ToString() + ex.Message?.ToString();
        strCodigoError = httpCode.ToString();

        if (httpCode == 404)
        {
            Response.Redirect("~/Views/Aplicacion/Acceso/PaginaNoEncontrada.cshtml?Popup="+IntPopUp+"&Url="+path);
        } 
        else
        {
            Response.Redirect("~/Views/Error500.cshtml?Popup="+IntPopUp+"&Url="+path+"&CodigoError="+strCodigoError+"&Excepcion="+Str_Excepcion);
            //RegistroError objRegistroError = new RegistroError("", "Global.asax", "", "Error al tratar de insertar el registro. IdRegistroError: ", ex.HResult, ex.InnerException?.ToString(), (int)Severidad.BAJA);
        }
        // Custom code that generates an HTML-formatted exception dump
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse una nueva sesión
        System.Diagnostics.Debug.WriteLine("Session_Start"); //write console

    }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.
        System.Diagnostics.Debug.WriteLine("Session_End"); //write console

    }
    void Application_PostAuthorizeRequest()
    {
        if (IsWebApiRequest())
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
    bool IsWebApiRequest()
    {
        return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(UrlPrefixRelative);
    }

    //void Application_BeginRequest(object sender, EventArgs e) {

    //    string fullOrigionalpath = Request.Url.ToString();
    //    //Views/Trafico/Pedido/pedidoAbc.cshtml?Popup=0&IdPedido=118
    //    if (fullOrigionalpath.Contains("/Trafico/Pedido/pedidoAbc.cshtml")) {
    //        Context.RewritePath("Views/Trafico/Pedido/");
    //    }
    //    else if (fullOrigionalpath.Contains("/Products/DVDs.aspx")) {
    //        Context.RewritePath("/Products.aspx?Category=DVDs");
    //    }
    //} 

</script>
