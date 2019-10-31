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
    public class UsuarioViajeDetalleController : ApiController
    {

        #region private vars
        private string Customer;
        private static UsuarioViajeDetalleController _Instance;
        #endregion

        #region ctor

        public UsuarioViajeDetalleController()
        {}

        static UsuarioViajeDetalleController()
        { }
        public static UsuarioViajeDetalleController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new UsuarioViajeDetalleController();
            }
            return _Instance;
        }

        #endregion

        #region actions
        [HttpGet]
        [Route("Api/UsuarioViajeDetalle/GetByTelfId")]
        public dynamic GetByTelfId(string telfId)
        {
            return this.GetIntern2(telfId);
        }

        [HttpGet]
        [Route("Api/UsuarioViajeDetalle/GetUltimoPuntoByTelfId")]
        public dynamic GetUltimoPuntoByTelfId(string telfId)
        {
            return this.GetUltimoPuntoIntern(telfId);
        }

        [HttpPost]
        [Route("Api/UsuarioViajeDetalle/PostDetalle")]
        public void Post(int viajeId,decimal lat, decimal lon)
        {
            this.AddIntern(viajeId,lat,lon);
        }

        // PUT api/UsuarioViajeDetalle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/UsuarioViajeDetalle/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions
        public List<del_UsuarioViajeDetalle> GetIntern(string sTelefono_Id)
        {
            List<del_UsuarioViajeDetalle> result = new List<del_UsuarioViajeDetalle>();
            using (MapiriModel db = new MapiriModel())
            {
                var telf = db.del_UsuarioTelefono.Where(x => x.sTelefono_Id == sTelefono_Id).FirstOrDefault();
                if (telf ==null)
                {
                    return result;
                }
                var viaje = db.del_UsuarioViaje.Where(x => x.iUsuarioTelefono_id == telf.iUsuarioTelefono_id && x.dtFechaFin == null && x.iCiudades_idOrigen != null && x.iCiudades_idDestino != null).FirstOrDefault();
                if (viaje == null)
                {
                    return result;
                }
                result = db.del_UsuarioViajeDetalle.Where(x => x.iUsuarioViaje_id == viaje.iUsuarioViaje_id).ToList();

                if (result == null || result.Count <= 0)
                {
                    var det = new del_UsuarioViajeDetalle()
                    {
                        iUsuarioViajeDetalle_id = 1,
                        iUsuarioViaje_id = viaje.iUsuarioViaje_id,
                        dtFecha = DateTime.Now,
                        fLat = db.del_Ciudades.Where(x => x.iCiudades_id == viaje.iCiudades_idOrigen).Select(x => x.fLat).FirstOrDefault(),
                        fLon = db.del_Ciudades.Where(x => x.iCiudades_id == viaje.iCiudades_idOrigen).Select(x => x.fLon).FirstOrDefault(),
                    };
                    result.Add(det);
                }

                return result;
            }
        }

        public dynamic GetIntern2(string sTelefono_Id)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var telf = db.del_UsuarioTelefono.Where(x => x.sTelefono_Id == sTelefono_Id).FirstOrDefault();
                if (telf == null)
                {
                    return null;
                }
                var viaje = db.del_UsuarioViaje.Where(x => x.iUsuarioTelefono_id == telf.iUsuarioTelefono_id && x.dtFechaFin == null
                && x.iCiudades_idDestino != null && x.iCiudades_idDestino != null).FirstOrDefault();
                if (viaje == null)
                {
                    return null;
                }
                var result = db.del_UsuarioViajeDetalle.Where(x => x.iUsuarioViaje_id == viaje.iUsuarioViaje_id)
                    .Select(r => new {
                        r.iUsuarioViajeDetalle_id,
                        r.iUsuarioViaje_id,
                        r.dtFecha,
                        r.fLat,
                        r.fLon,
                    }).OrderBy(x=>x.iUsuarioViajeDetalle_id).ToList();

                return result;
            }
        }

        public dynamic GetUltimoPuntoIntern(string sTelefono_Id)
        {
            var lista = this.GetIntern(sTelefono_Id);
            var result = lista.OrderByDescending(x => x.iUsuarioViajeDetalle_id).FirstOrDefault();
            return new { result.iUsuarioViajeDetalle_id,
                result.iUsuarioViaje_id,
                result.dtFecha,
                result.fLat,
                result.fLon,
            };
        }
        public void AddIntern(int viajeId, decimal lat, decimal lon)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var obj = new del_UsuarioViajeDetalle()
                {
                    iUsuarioViaje_id = viajeId,
                    fLat = lat,
                    fLon = lon,
                    dtFecha= DateTime.Now,
                };
                db.del_UsuarioViajeDetalle.Add(obj);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
