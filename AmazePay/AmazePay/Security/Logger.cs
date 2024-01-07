using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace AmazePay.Security
{
    public class Logger
    {

        string log_Path_Error;
        string log_Path_Audit;
        string log_Path_Activity;


        public Logger()
        {
            //log_Path_Error = Application.StartupPath;
            log_Path_Error = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Log_Path_Error")["key"];
            log_Path_Audit = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Log_Path_Audit")["key"];
            log_Path_Activity = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Log_Path_Activity")["key"];
        }

        #region Write Error Log

        public bool WriteErrorToFile(string sText)
        {
            try
            {
                string sPath = log_Path_Error + "Error" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".log";
                string sFolder = log_Path_Error;


                if (Directory.Exists(sFolder) == false)
                {
                    Directory.CreateDirectory(sFolder);
                }

                TextWriter tw = new StreamWriter(sPath, true);
                tw.WriteLine(DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + sText);
                tw.Flush();
                tw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Write Audit Log

        public bool WriteAuditToFile(string sText)
        {
            try
            {
                string sPath = log_Path_Audit + "Audit" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".log";
                string sFolder = log_Path_Audit;


                if (Directory.Exists(sFolder) == false)
                {
                    Directory.CreateDirectory(sFolder);
                }

                TextWriter tw = new StreamWriter(sPath, true);
                tw.WriteLine(DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + sText);
                tw.Flush();
                tw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Write Activity Log

        public bool WriteActivityToFile(string sText)
        {
            try
            {
                string sPath = log_Path_Activity + "Activity" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".log";
                string sFolder = log_Path_Activity;


                if (Directory.Exists(sFolder) == false)
                {
                    Directory.CreateDirectory(sFolder);
                }

                TextWriter tw = new StreamWriter(sPath, true);
                tw.WriteLine(DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + sText);
                tw.Flush();
                tw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

}
