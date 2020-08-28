using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SendMailService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class SendMailService : ISendMailService
    {
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}


        public void SendEmails(Models.Email eMail, Models.EmailAccount emailAccount = null)
        {
            try
            {
                string pass = "GpM@1l.17#";
                string correo = "mailgp@grupophoenix.com";
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new MailMessage();
                mail.From = new MailAddress(correo);
                eMail.To.ForEach(To => mail.To.Add(To.Account));//
                eMail.CC.ForEach(CC => mail.CC.Add(CC.Account));
                //eMail.Attachments.ForEach(itemAttachment => mail.Attachments.Add(itemAttachment));


                mail.Subject = eMail.Subject;
                mail.IsBodyHtml = true;
                var firmaHtml = eMail.Singature;
                mail.Body = eMail.Body;

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential((string.IsNullOrEmpty(eMail.From.Account) ? correo : eMail.From.Account), (string.IsNullOrEmpty(eMail.From.Password) ? pass : eMail.From.Password));
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine("SendEmailCertificate - Ha ocurrido un error en el servicio: " + ex.Message);
                throw;
            }
        }
    }
}
