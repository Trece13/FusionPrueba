using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using whusa.Interfases;
using System.Configuration;
using whusa.Entidades;
using System.Net.Mail;

namespace SendEmailQualityCertificate
{
    public partial class Service1 : ServiceBase
    {
        private InterfazDAL_tticol180 _idalticol180 = new InterfazDAL_tticol180();
        private static string strError;
        System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
        private static string _documentoAnterior = String.Empty;

        private System.Timers.Timer tmProcess;

        public Service1()
        {
            eventLog.Source = "Service1";
            tmProcess = new System.Timers.Timer();
            tmProcess.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timeSendEmail"].ToString());
            tmProcess.Enabled = true;
            tmProcess.Elapsed += new System.Timers.ElapsedEventHandler(tmProcess_Elapsed);
        }

        void tmProcess_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmProcess.Enabled = false;
            SendEmails();
            tmProcess.Enabled = true;
        }

        private void SendEmails()
        {
            try
            {
                var registrosEnvio = _idalticol180.listRecordSendEmail(ref strError);
                
                if (registrosEnvio.Count > 0)
                {
                    eventLog.WriteEntry("SendEmailCertificate -  Inicio envio de Mails, " + registrosEnvio.Count + " registros encontrados", EventLogEntryType.Information);

                    foreach (Ent_tticol180 item in registrosEnvio.OrderBy(x => x.docn))
                    {
                        var register = item;
                        var documentoRegistro = item.docn;
                        var usuarioRegistro = item.user;

                        if (documentoRegistro.Substring(0, 9) != (String.IsNullOrEmpty(_documentoAnterior) ? _documentoAnterior : _documentoAnterior.Substring(0, 9)))
                        {
                            _documentoAnterior = documentoRegistro;
                            if (!System.IO.File.Exists(register.path.Trim().Replace('\\', '/')))
                            {
                                register.send = 2;
                                register.mssh = "La ruta del archivo no existe.";
                                var update = _idalticol180.updateRecord(ref register, ref strError);

                            }
                            else if (register.tgbrg835_mail.Trim() == "")
                            {
                                register.send = 2;
                                register.mssh = "El usuario no tiene correo.";
                                var update = _idalticol180.updateRecord(ref register, ref strError);
                            }
                            else
                            {

                                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                                var mail = new MailMessage();
                                mail.From = new MailAddress(ConfigurationManager.AppSettings["email"].ToString());
                                mail.To.Add(register.tgbrg835_mail.Trim());

                                var buscarRegistroDocumento = registrosEnvio.Where(x => x.docn.Substring(0, 9) == documentoRegistro.Substring(0, 9) && x.user == usuarioRegistro).ToList();

                                foreach (var registroDocumento in buscarRegistroDocumento)
                                {
                                    Attachment doc = new Attachment(registroDocumento.path.Trim().Replace('\\', '/'));
                                    doc.Name = registroDocumento.path.Trim().Replace('\\', '/').Split('/').Last().Trim();
                                    mail.Attachments.Add(doc);
                                }

                                //registrosEnvio.RemoveAll(x => x.docn == documentoRegistro && x.user == usuarioRegistro);

                                mail.Subject = ConfigurationManager.AppSettings["asuntoEmail"].ToString() + " - " + documentoRegistro.Substring(0, 9);
                                mail.IsBodyHtml = true;
                                string htmlBody;
                                var firmaHtml = ConfigurationManager.AppSettings["firmaEmail"].ToString();
                                var splitFirma = firmaHtml.Split('%');
                                htmlBody = String.Concat(ConfigurationManager.AppSettings["cuerpoEmail"].ToString(), "<br /><br /><b>", splitFirma[0], "<br />", splitFirma[1], "<br />", splitFirma[2], "</b>");
                                mail.Body = htmlBody;

                                SmtpServer.Port = 587;
                                SmtpServer.UseDefaultCredentials = false;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"].ToString(), ConfigurationManager.AppSettings["password"].ToString());
                                SmtpServer.EnableSsl = true;
                                SmtpServer.Send(mail);


                                foreach (var registroActualiza in buscarRegistroDocumento)
                                {
                                    registroActualiza.send = 1;
                                    registroActualiza.mssh = "Archivo enviado exitosamente.";
                                    registroActualiza.path = registroActualiza.path.Trim().Replace('\\', '/').Split('/').Last().Trim();
                                    var registroActualizar = registroActualiza;
                                    var update = _idalticol180.updateRecord(ref registroActualizar, ref strError);
                                }
                            }
                        }
                    }
                }
                else
                {
                    eventLog.WriteEntry("SendEmailCertificate - No se encontraron registros para envío de Mail", EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("SendEmailCertificate - Ha ocurrido un error en el servicio: " + ex.Message, EventLogEntryType.Error);
                throw;
            }
        }
    }
}
