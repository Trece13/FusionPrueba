using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;

namespace whusap
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            //base.InitializeCulture();
        }

        void Application_End(object sender, EventArgs e)
        {
            

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            Session.Clear();
        }

    }
}
