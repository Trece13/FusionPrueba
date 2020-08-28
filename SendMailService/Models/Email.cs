using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SendMailService.Models
{
    public class Email
    {
        #region Constructor's
        public Email()
        {
            this.From = new EmailAccount();
            this.To = new List<EmailAccount>();
            this.CC = new List<EmailAccount>();
            this.Body = string.Empty;
            this.Singature = string.Empty;
        }
        #endregion

        #region Properties
        public EmailAccount From { get; set; }
        public List<EmailAccount> To { get; set; }
        public List<EmailAccount> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string Singature { get; set; }
        #endregion

    }
}