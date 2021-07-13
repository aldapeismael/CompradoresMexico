using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Data.SqlClient;
using System.IO;

public class Correo
{
    #region parametros

    private int _IntIdEmail;
    private string _StrEmailDe;
    private string _StrEmailA;
    private string _StrEmailACc;
    private string _StrEmailACo;
    private string _StrAsunto;
    private string _StrMensaje;
    private RespuestaBD _ObjRespuestaBD;
    private SmtpClient _ObjSmtp;
    private string _StrLlaveSip;
    private int _IntBArchivo;
    private byte[] _ArrPdfBytes;
    private string _StrNombreArchivo;
    private string _StrUrlVista;
    private dynamic _StrParametrosUri;

    #region parametros publicos
    public int IntIdEmail
    {
        get { return _IntIdEmail; }
        set { _IntIdEmail = value; }
    }
    public string StrEmailDe
    {
        get
        {
            return _StrEmailDe;
        }

        set
        {
            _StrEmailDe = value;
        }
    }
    public string StrEmailA
    {
        get
        {
            return _StrEmailA;
        }

        set
        {
            _StrEmailA = value;
        }
    }
    public string StrAsunto
    {
        get
        {
            return _StrAsunto;
        }

        set
        {
            _StrAsunto = value;
        }
    }
    public string StrMensaje
    {
        get
        {
            return _StrMensaje;
        }

        set
        {
            _StrMensaje = value;
        }
    }
    public string StrEmailACc
    {
        get { return _StrEmailACc; }
        set { _StrEmailACc = value; }
    }
    public string StrEmailACo
    {
        get { return _StrEmailACo; }
        set { _StrEmailACo = value; }
    }
    public RespuestaBD ObjRespuestaBD
    {
        get
        {
            return _ObjRespuestaBD;
        }

        set
        {
            _ObjRespuestaBD = value;
        }
    }
    public SmtpClient ObjSmtp
    {
        get
        {
            return _ObjSmtp;
        }

        set
        {
            _ObjSmtp = value;
        }
    }
    public string StrLlaveSip
    {
        get { return _StrLlaveSip; }

        set { _StrLlaveSip = value; }
    }
    public int IntBArchivo
    {
        get
        {
            return _IntBArchivo;
        }

        set
        {
            _IntBArchivo = value;
        }
    }
    public byte[] ArrPdfBytes
    {
        get
        {
            return _ArrPdfBytes;
        }

        set
        {
            _ArrPdfBytes = value;
        }
    }
    public string StrNombreArchivo
    {
        get
        {
            return _StrNombreArchivo;
        }

        set
        {
            _StrNombreArchivo = value;
        }
    }
    public string StrUrlVista
    {
        get
        {
            return _StrUrlVista;
        }

        set
        {
            _StrUrlVista = value;
        }
    }
    public dynamic StrParametrosUri
    {
        get
        {
            return _StrParametrosUri;
        }

        set
        {
            _StrParametrosUri = value;
        }
    }
    #endregion parametros publicos

    #endregion parametros

    #region constructor
    public Correo()
    {

    }
    #endregion constructor

    public RespuestaBD ValidarDatosCorreo()
    {
        if (String.IsNullOrEmpty(this._StrEmailDe))
        {
            this._ObjRespuestaBD = new RespuestaBD(1, "No se definió un correo remitente", "error");
        }

        if (String.IsNullOrEmpty(this._StrEmailA))
        {
            this._ObjRespuestaBD = new RespuestaBD(1, "No se definió un correo destinatario.", "error");
        }

        if (String.IsNullOrEmpty(this._StrAsunto))
        {
            this._ObjRespuestaBD = new RespuestaBD(1, "No se definió un Asunto.", "error");
        }

        if (String.IsNullOrEmpty(this._StrMensaje))
        {
            this._ObjRespuestaBD = new RespuestaBD(1, "No se definió un Mensaje para el correo.", "error");
        }

        return this._ObjRespuestaBD;
    }

    public RespuestaBD EnviarCorreo()
    {
        // Validamos los campos necesarios para el envio de correo
        this._ObjRespuestaBD = ValidarDatosCorreo();

        // Los parámetros para el servidor de correo deben de ser obtenidos de la tabla de parámetros de la aplicación
        ObtenerParametros();

        // Si se obtiene un error en la validación, regresamos el error.
        if (this._ObjRespuestaBD != null && this._ObjRespuestaBD.IntError == 1)
        {
            return this._ObjRespuestaBD;
        }

        SmtpClient SmtpClient = new SmtpClient()
        {
            //UseDefaultCredentials = false,
            //Port = 587,
            //Host = "secure.emailsrvr.com",
            //Timeout = 10000,
            //EnableSsl = true,
            //Credentials = new System.Net.NetworkCredential("alertas@acompraodres.com", "notificaciones.AT18"),

            UseDefaultCredentials = false,
            Port = this._ObjSmtp.Port,
            Host = this._ObjSmtp.Host,
            Timeout = 10000,
            EnableSsl = true,
            Credentials = this._ObjSmtp.Credentials,
        };

        var empresaListaParametros = new MultiListaController().ObtenerListaParametros(0, "empresa", 0, 0, "", "", 1).First();
        string cveEmpresa = !string.IsNullOrEmpty(empresaListaParametros.valor1char) ? empresaListaParametros.valor1char + " - " : "";

        MailMessage mailMessage = new MailMessage();
        mailMessage.Subject = cveEmpresa + this._StrAsunto;
        mailMessage.Body = this._StrMensaje;
        mailMessage.IsBodyHtml = true;
        mailMessage.From = new MailAddress(this._StrEmailDe);


        if (IntBArchivo == 1) 
        {
            Attachment attachment;
            attachment = new Attachment(new MemoryStream(ArrPdfBytes), StrNombreArchivo);
            mailMessage.Attachments.Add(attachment);
        }

        //Se agregan todos los destinatarios
        String[] AMailto = this.StrEmailA.Split(';');
        foreach (String mail in AMailto)
        {
            mailMessage.To.Add(new MailAddress(mail));
        }

        //Se agrega la copia a los destinatarios indicados
        if (!String.IsNullOrEmpty(_StrEmailACc))
        {
            String[] AMailToCc = this._StrEmailACc.Split(';');
            foreach (String mailToCc in AMailToCc)
            {
                mailMessage.CC.Add(mailToCc);
            }
        }

        //Se agrega la copia oculta a los destinatarios indicados
        if (!String.IsNullOrEmpty(_StrEmailACo))
        {
            String[] AMailToCo = this._StrEmailACo.Split(';');
            foreach (String mailToCo in AMailToCo)
            {
                mailMessage.Bcc.Add(new MailAddress(mailToCo));
            }
        }
        AlternateView AlternateView = AlternateView.CreateAlternateViewFromString(this._StrMensaje, System.Text.UTF8Encoding.UTF8, "text/html");

        mailMessage.AlternateViews.Add(AlternateView);

        try
        {
            SmtpClient.Send(mailMessage);
            SmtpClient.Dispose();
            this._ObjRespuestaBD = new RespuestaBD(0, "Correo electrónico enviado satisfactoriamente.", "success");
        }
        catch (Exception ex)
        {
            this._ObjRespuestaBD = new RespuestaBD(1, "Error enviando correo electrónico: " + ex.Message, "error");
        }

        return this._ObjRespuestaBD;
    }

    public bool ObtenerParametros()
    {
        //instanciamos los objeto y atributos a utilizar
        bool bool_Valido = false;
        this._ObjSmtp = new SmtpClient();
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "aplicacion.spParametroObtenerLista";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_Ejecuta", 0);
            sqlCommand.Parameters.AddWithValue("@p_TipoBusqueda", 1);
            sqlCommand.Parameters.AddWithValue("@p_CveParametro", "correo_notificacion");

            DataSet dataSetObtener = ConexionBD.EjecutarComando(1, 1, sqlCommand, "archivo: Contrasena.cs");
            if (dataSetObtener != null && dataSetObtener.Tables.Count > 0)
            {
                string StrParametrosCorreo = dataSetObtener.Tables[0].Rows[0]["valor1char"].ToString();
                string[] StrArrayParametros = StrParametrosCorreo.Split(new string[] { "|" }, StringSplitOptions.None);        //separamos el string en pedazos, obteniendo individualmente la info de usr, correo, host y puerto
                //Buscamos el usuario
                var StrBuscaUsr = "usuario=";
                var StrUsrParametro = Array.FindAll(StrArrayParametros, s => s.Contains(StrBuscaUsr));
                var StrUsrCorreo = StrUsrParametro[0].Replace(StrBuscaUsr, string.Empty);
                //Buscamos la contraseña
                var StrBuscaPwd = "contrasena=";
                var StrPwdParametro = Array.FindAll(StrArrayParametros, s => s.Contains(StrBuscaPwd));
                var StrPwd = StrPwdParametro[0].Replace(StrBuscaPwd, string.Empty);
                //Buscamos el Host
                var StrBuscaHost = "host=";
                var StrHostParametro = Array.FindAll(StrArrayParametros, s => s.Contains(StrBuscaHost));
                var StrHost = StrHostParametro[0].Replace(StrBuscaHost, string.Empty);

                //Buscamos el Host
                var StrBuscaPuerto = "puerto=";
                var StrPuertoParametro = Array.FindAll(StrArrayParametros, s => s.Contains(StrBuscaPuerto));
                int IntPuerto = int.Parse(StrPuertoParametro[0].Replace(StrBuscaPuerto, string.Empty));

                this._ObjSmtp.Host = StrHost;
                this._ObjSmtp.Port = IntPuerto;
                this._ObjSmtp.EnableSsl = true;
                this._ObjSmtp.UseDefaultCredentials = false;
                this._ObjSmtp.Credentials = new NetworkCredential(StrUsrCorreo, StrPwd);
            }

        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, this.GetType().Name + ".cs", this.GetType().Name + "Controller.cs", "Error al tratar de obtener el registro, IDRegistroError: ", e.HResult, e.Message, (int)Severidad.BAJA);
        }

        return bool_Valido;
    }
}