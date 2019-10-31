using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapiri.Library.Helper
{
    class Log
    {
        public Log()
        { }
        static Log()
        { }

        public static void Guardar(string Mensaje, string Usuario, string Origen, string TipoLog = "")
        {
            try
            {
                if (string.IsNullOrEmpty(Usuario))
                {
                    Usuario = "_";
                }
                if (string.IsNullOrEmpty(Origen))
                {
                    Origen = "_";
                }
                string log = string.Format("{0} || {1} || {2} || MENSAJE [{3}]", DateTime.Now, Usuario, Origen, Mensaje);
                string fileName = string.Format("{0}_{1}_{2}_Log.txt", DateTime.Now.Year, DateTime.Now.Month, TipoLog);
                string fileDirectory = System.Configuration.ConfigurationManager.AppSettings["PathErrores"];
                if (string.IsNullOrEmpty(fileDirectory))
                {
                    fileDirectory = @"C:\";
                }
                using (StreamWriter writer = new StreamWriter(string.Format("{0}{1}", fileDirectory, fileName), true))
                {
                    writer.WriteLine(log);
                }
            }
            catch
            { }
        }
        public static void Guardar(string Mensaje)
        {
            //Guardar(Mensaje, "_","_", Common.Constants.CRM);
        }
        public static void Guardar(string Mensaje, string Usuario, string Origen, string TipoLog, string CorreoTO)
        {
            Guardar(Mensaje, Usuario, Origen, TipoLog);
            string Respuesta = string.Empty;
            //
        }

    }
}
