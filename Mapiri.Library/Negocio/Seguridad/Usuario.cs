using Mapiri.Negocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapiri.Library.Negocio.Seguridad
{
    public class Usuario
    {
        #region private vars        
        private static Usuario _Instance;
        #endregion

        #region ctor

        public Usuario()
        {

        }

        static Usuario()
        { }
        public static Usuario Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Usuario();
            }
            return _Instance;
        }

        #endregion

        #region metodos

        public seg_Usuario Add(seg_Usuario obj, out string msg)
        {
            msg = "";
            {
                if (!obj.sEmail.Contains("@"))
                {
					msg = "Email Invalido";
					throw new Exception("Email Invalido");
                }
                //db.seg_Usuario.Add(obj);
                //db.SaveChanges();
                return obj;
            }
        }
        

        #endregion
    }
}
