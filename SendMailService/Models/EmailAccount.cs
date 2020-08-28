using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMailService.Models
{
    /// <summary>
    /// Clase Cuenta de Correo Electronico.
    /// Creada por: Juan Cubillos
    /// 27/08/2019
    /// </summary>
    public class EmailAccount
    {

        #region Constructor's
        public EmailAccount()
        {
            this.Account = string.Empty;
            this.Password = string.Empty;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Cuenta de correo.
        /// Creada por: Juan Cubillos
        /// 27/08/2019
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Password opcional, para definir la cuenta de correo de envio.
        /// Creada por: Juan Cubillos
        /// 27/08/2019
        /// </summary>
        ///        
        public string Password { get; set; }

        #endregion

    }
}