using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusa.DAL
{
    public class twhcol025
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();

        String strSentencia = string.Empty;
        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".twhcol025" + env;

       

        public int insertRegistrItemAdjustment(ref List<Ent_twhcol025> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                foreach (Ent_twhcol025 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [twhcol025]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);

        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol025 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();

            try
            {
                 //T$PAID, T$ITEM, T$LOCA, T$CLOT, T$QTYA, T$UNIT, T$DATE, T$LOGN, T$EMNO, T$PROC, T$REFCNTD,T$REFCNTU

                ////obj.PAID = txtPalletId.Text.ToUpperInvariant();
                ////obj.ITEM = lblItemValue.Text.Trim().ToUpper();
                ////obj.LOCA = lblLocationValue.Text.Trim().ToUpper();
                ////obj.CLOT = lblLot.Text.Trim().ToUpper();
                ////obj.QTYA = Convert.ToInt32(txtAdjustmentQuantity.Text.Trim());
                ////obj.UNIT = lblUnitValue.Text.Trim().ToUpper();
                ////obj.DATE = lblUnitValue.Text.Trim().ToUpper();
                ////obj.CDIS = dropDownCostCenters.SelectedItem.Value;
                ////obj.EMNO = dropDownReasonCodes.SelectedItem.Value;

                ////obj.PROC = 0;
                ////obj.REFCNTD = 0;
                ////obj.REFCNTU = 0;
                ////T$PAID, T$ITEM, T$LOCA, T$CLOT, T$QTYA, T$UNIT, T$DATE, T$LOGN,T$CDIS, T$EMNO, T$PROC, T$REFCNTD,T$REFCNTU

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.PAID);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.ITEM.Trim());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.LOCA);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.CLOT);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYA", DbType.Int32, parametros.QTYA);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$UNIT", DbType.String, parametros.UNIT);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.DATE);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.DATE);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.LOGN);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDIS", DbType.String, parametros.CDIS);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$EMNO", DbType.String, parametros.EMNO);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.PROC);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.REFCNTD);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.REFCNTU);
              
            }
            catch (Exception ex)
            {
                strError = "Error when creating parameters [125]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

    }
}
