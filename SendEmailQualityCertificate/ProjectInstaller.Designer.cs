namespace SendEmailQualityCertificate
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SendEmailQualityCertificate = new System.ServiceProcess.ServiceProcessInstaller();
            this.SendEmailQuality = new System.ServiceProcess.ServiceInstaller();
            // 
            // SendEmailQualityCertificate
            // 
            this.SendEmailQualityCertificate.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SendEmailQualityCertificate.Password = null;
            this.SendEmailQualityCertificate.Username = null;
            // 
            // SendEmailQuality
            // 
            this.SendEmailQuality.Description = "Envio de correos certificado de calidad";
            this.SendEmailQuality.ServiceName = "SendEmailQuality";
            this.SendEmailQuality.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SendEmailQualityCertificate,
            this.SendEmailQuality});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SendEmailQualityCertificate;
        private System.ServiceProcess.ServiceInstaller SendEmailQuality;
    }
}