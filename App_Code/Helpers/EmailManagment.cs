using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Linq;

/// <summary>
/// Summary description for EmailManagment
/// </summary>
public class EmailManagment
{
    public static EmailManagment Email => new EmailManagment();
    const string smtpServer = "";
    const string emailFrom = "";
    const string emailPwd = "";
    private EmailManagment()
    {

    }
    public void SendRecordatorio(string Candidato, string Entrevistador, string fecha, string hora, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"";
            mail.Body = "";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }
    public void SendConfirmar(string Candidato, string evaluacion, string fecha, string hora, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"RH - RECORDATORIO";
            mail.Body = "¡Hola! " + Candidato + Environment.NewLine +
                "Recuerda que tienes una Entrevista que sera un:" + evaluacion + Environment.NewLine +
                "Que esta programada con la fecha: " + fecha + " a las:" + hora;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }
    public void SendListaNegraCandidato(string Candidato, string Motivo, string Puesto, string Entrevistador, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"RH - AVISO IMPORTANTE, ENVIADO A LISTA NEGRA";
            mail.Body = "El Candidato " + Candidato + Environment.NewLine +
                "Al postularse en el Puesto: " + Puesto + Environment.NewLine +
                "Fue enviado a la Lista Negra por el Usuario " + Entrevistador + Environment.NewLine +
                "Por el siguien Motivo: " + Motivo;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }
    public void SendAgradecimientoCandidato(string Nombre, string Puesto, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"RH - AVISO IMPORTANTE, AGRADECIMIENTO";
            mail.Body = "Candidato al Puesto: " + Puesto +  Environment.NewLine +
                "HOLA! : " + Nombre + Environment.NewLine +
                "Te agradecemos el que hayas sido candidato de este Puesto. Ya contratamos al personal que necesitabamos." + Environment.NewLine +  "Muchas gracias por tu tiempo.";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }


    //HTML para Encuesta
    private string HTMLNuevaEncuesta(string nombre, string anio, string sitio)
    {
        string html = String.Empty;
        try
        {
            string path = Path.Combine(VariableGlobal.StrUrlArchivosHTML ?? null, @"SIP2020\TemplateHTML\NuevaEncuesta.html");
            html = File.ReadAllText(path);
            html = html.Replace("@@NombreEvaluador@@", nombre);
            html = html.Replace("@@Anio@@", anio);
            html = html.Replace("@@Link@@", $"{sitio}/Acceso.cshtml?red=1");
            html = html.Replace("@@sitio@@", $"{sitio}");
            return html;
        }
        catch (Exception ex)
        {

            return html;
        }
        
    }
    private string HTMLContestarEncuesta(string nombre, string sitio, int year)
    {
        string html = String.Empty;
        try
        {
            string path = Path.Combine(VariableGlobal.StrUrlArchivosHTML ?? null, @"SIP2020\TemplateHTML\ContestarEncuesta.html");
            html = File.ReadAllText(path);
            html = html.Replace("@@NombreEvaluador@@", nombre);
            html = html.Replace("@@ano@@", year.ToString());
            html = html.Replace("@@Link@@", $"{sitio}/Acceso.cshtml?red=1");
            html = html.Replace("@@sitio@@", $"{sitio}");
            return html;
        }
        catch (Exception ex)
        {

            return html;
        }

    }
    private string HTMLTerminarEncuesta(string nombre, string fechaCierre, string horaCierre, string sitio)
    {
        string html = String.Empty;
        try
        {
            string path = Path.Combine(VariableGlobal.StrUrlArchivosHTML ?? null, @"SIP2020\TemplateHTML\ContestarEncuesta.html");
            html = File.ReadAllText(path);
            html = html.Replace("@@NombreEvaluador@@", nombre);
            html = html.Replace("@@Link@@", $"{sitio}/Acceso.cshtml?red=1");
            html = html.Replace("@@FechaCierre@@", fechaCierre);
            html = html.Replace("@@HoraCierre@@", horaCierre);
            html = html.Replace("@@sitio@@", $"{sitio}");
            return html;
        }
        catch (Exception ex)
        {

            return html;
        }

    }
    public void SendNuevaEncuesta(string Nombre, string Anio, string Asunto, string sitio, string[] Emails)
    {
        //try
        //{

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = Asunto;
            mail.Body = HTMLNuevaEncuesta(Nombre, Anio, sitio);
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            //}
            //catch (Exception exception)
            //{
            //    exception.ToString();
            //}
        }
    public void SendContestarEncuesta(string Nombre,  string Asunto, string sitio, string[] Emails)
    {
        //try
        //{ 
        System.DateTime moment = new System.DateTime();
        int year = moment.Year;

        MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = Asunto;
            mail.Body = HTMLContestarEncuesta(Nombre, sitio, year);
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        //}
        //catch (Exception exception)
        //{
        //    exception.ToString();
        //}
    }
    public void SendTerminarEncuesta(string Nombre, string fechaCierre,string horaCierre, string Asunto, string sitio, string[] Emails)
    {
        //try
        //{

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = Asunto;
            mail.Body = HTMLTerminarEncuesta(Nombre, fechaCierre,horaCierre, sitio);
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        //}
        //catch (Exception exception)
        //{
        //    exception.ToString();
        //}
    }


    public void SendCONTRATADO(string Candidato, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"RH - FELICIDADES";
            mail.Body = "Querido(a) " + Candidato + Environment.NewLine +
                "Nos complace informarte que fuiste Contratado!";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }

    public void SendNuevaVacante(string Candidato, string Vacante, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"";
            mail.Body = "" + Environment.NewLine;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }
    public void ss(string Candidato, string Vacante, string[] Emails)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(emailFrom);
            foreach (string mailTo in Emails)
                mail.To.Add(mailTo);
            mail.Subject = $"";
            mail.Body = "" + Environment.NewLine;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPwd);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception exception)
        {
            exception.ToString();
        }
    }
}