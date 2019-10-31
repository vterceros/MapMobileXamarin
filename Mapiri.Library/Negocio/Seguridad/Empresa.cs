using Mapiri.Negocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapiri.Library.Negocio.Seguridad
{
    public class Empresa
    {
        #region private vars        
        private static Empresa _Instance;
        #endregion

        #region ctor

        public Empresa()
        {

        }

        static Empresa()
        { }
        public static Empresa Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Empresa();
            }
            return _Instance;
        }

        #endregion

        #region metodos

        public seg_Empresa Add(seg_Empresa obj)
        {
            using (MapiriModel db = new MapiriModel())
            {
                db.seg_Empresa.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public List<seg_Empresa> ObtenerTodas()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var result = db.seg_Empresa.ToList();
                return result;
            }
        }


        #endregion
    }
}
