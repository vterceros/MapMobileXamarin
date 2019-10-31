using Mapiri.Library.Negocio.Seguridad;
using Mapiri.Negocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mapiri.Negocio.Api.Controllers
{
    //[Authorize]
    public class CiudadesController : ApiController
    {

        #region private vars
        private string Customer;
        private static CiudadesController _Instance;
        #endregion

        #region ctor

        public CiudadesController()
        {}

        static CiudadesController()
        { }
        public static CiudadesController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new CiudadesController();
            }
            return _Instance;
        }

        #endregion

        #region actions
        [HttpGet]
        [Route("Api/Ciudades/GetByNombre")]
        public dynamic GetByNombre(string ciudad)
        {
            if (string.IsNullOrEmpty(ciudad) || ciudad.ToUpper() == "NULL")
            {
                return this.GetAllSelectedIntern();
            }
            else
            {
                return this.GetIntern(ciudad);
            }
        }
        [HttpGet]
        [Route("Api/Ciudades/GetByKey")]
        public dynamic GetByKey(int key)
        {
            return this.GetByKeyIntern(key);
        }
        [HttpGet]
        [Route("Api/Ciudades/GetAll")]
        public dynamic GetAll()
        {
            return this.GetAllIntern();
        }

        [HttpPost]
        [Route("Api/Ciudades/PostDetalle")]
        public void Post(int viajeId,decimal lat, decimal lon)
        {
        }

        // PUT api/Ciudades/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Ciudades/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions
        public dynamic GetIntern(string ciudad)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var res = db.del_Ciudades.Where(x => x.sCiudad.Contains(ciudad)).Take(15).Select(x=> new { x.iCiudades_id, Ciudad = x.sPais + " " + x.sCiudad, x.fLat, x.fLon }).ToList();
                return res;
            }
        }

        public dynamic GetAllIntern()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var res = db.del_Ciudades.Where(x=>x.bHabilitado== true).Select(x => new { x.iCiudades_id, Ciudad = x.sPais + " " + x.sCiudad }).ToList();
                return res;
            }
        }

        public dynamic GetAllSelectedIntern()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var ciudadesO = db.del_UsuarioViaje.Select(x => x.iCiudades_idOrigen).ToList();
                var ciudadesD = db.del_UsuarioViaje.Select(x => x.iCiudades_idDestino).ToList();
                ciudadesO.AddRange(ciudadesD);

                var res = db.del_Ciudades.Where(x => ciudadesO.Contains(x.iCiudades_id)).Select(x => new { x.iCiudades_id, Ciudad = x.sPais + " " + x.sCiudad }).ToList();
                return res;
            }
        }

        public dynamic GetByKeyIntern(int key)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var res = db.del_Ciudades.Where(x=>x.iCiudades_id == key).Select(x => new { x.iCiudades_id, Ciudad = x.sPais + " " + x.sCiudad }).FirstOrDefault();
                return res;
            }
        }


        #endregion
    }
}
