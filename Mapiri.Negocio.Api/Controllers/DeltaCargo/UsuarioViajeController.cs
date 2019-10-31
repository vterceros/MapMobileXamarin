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
    public class UsuarioViajeController : ApiController
    {

        #region private vars
        private string Customer;
        private static UsuarioViajeController _Instance;
        #endregion

        #region ctor

        public UsuarioViajeController()
        {}

        static UsuarioViajeController()
        { }
        public static UsuarioViajeController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new UsuarioViajeController();
            }
            return _Instance;
        }

        #endregion

        #region actions
        [HttpGet]
        [Route("Api/UsuarioViaje/GetByTelfId")]
        public dynamic GetByTelfId(string telfId)
        {
            return this.GetIntern(telfId); 
        }

        [HttpGet]
        [Route("Api/UsuarioViaje/GetAll")]
        public dynamic GetAll()
        {
            return this.GetAllIntern();
        }

        [HttpGet]
        [Route("Api/UsuarioViaje/GetMapaActivos")]
        public dynamic GetMapaActivos()
        {
            return this.GetMapaActivosIntern();
        }

        [HttpPost]
        [Route("Api/UsuarioViaje/Post")]
        public void Post(del_UsuarioViaje obj)
        {
            this.AddIntern(obj.iUsuarioTelefono_id, obj.sNombre, obj.dtFechaInicio, obj.iCiudades_idOrigen, obj.iCiudades_idDestino);
        }

        [HttpPut]
        [Route("Api/UsuarioViaje/PutCambios")]
        public void Put(int viajeId, int telId, string nombre, DateTime inicio, DateTime fin, int origen, int destino)
        {
            this.EditIntern(viajeId, telId, nombre, inicio, fin, origen, destino);
        }

        [HttpPut]
        [Route("Api/UsuarioViaje/Put")]
        public void Put(int viajeId, del_UsuarioViaje obj)
        {
            this.EditIntern(viajeId, obj.iUsuarioTelefono_id, obj.sNombre, obj.dtFechaInicio, obj.dtFechaFin, obj.iCiudades_idOrigen, obj.iCiudades_idDestino);
        }

        // PUT api/UsuarioViaje/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/UsuarioViaje/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions
        public void AddIntern(int? telId, string nombre, DateTime inicio, int? origen, int? destino)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var viaje = new del_UsuarioViaje();
                viaje.iUsuarioTelefono_id = telId;
                viaje.sNombre = nombre;
                viaje.dtFechaInicio = inicio;
                viaje.iCiudades_idOrigen = origen;
                viaje.iCiudades_idDestino = destino;
                viaje.dtFechaRegistro = DateTime.Now;

                db.del_UsuarioViaje.Add(viaje);
                db.SaveChanges();
            }
        }
        public void EditIntern(int viajeId, int? telId, string nombre, DateTime inicio, DateTime? fin, int? origen, int? destino)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var viaje = db.del_UsuarioViaje.Where(x => x.iUsuarioViaje_id == viajeId).FirstOrDefault();
                if (viaje == null)
                {
                    return;
                }
                viaje.iUsuarioTelefono_id = telId;
                viaje.sNombre = nombre;
                viaje.dtFechaInicio = inicio;
                viaje.dtFechaFin = null;
                if (fin.HasValue && fin.Value >= inicio)
                {
                    viaje.dtFechaFin = fin;
                }
                viaje.iCiudades_idOrigen = origen;
                viaje.iCiudades_idDestino = destino;

                db.SaveChanges();
            }
        }
        public dynamic GetIntern(string sTelefono_Id)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var telf = db.del_UsuarioTelefono.Where(x => x.sTelefono_Id == sTelefono_Id).FirstOrDefault();
                if (telf == null)
                {
                    return null;
                }
                var viaje = db.del_UsuarioViaje.Where(x => x.iUsuarioTelefono_id == telf.iUsuarioTelefono_id && x.dtFechaFin == null
                && x.iCiudades_idOrigen != null && x.iCiudades_idDestino != null).FirstOrDefault();
                if (viaje == null)
                {
                    return null;
                }
                var ciudadOrigen = db.del_Ciudades.Where(x => x.iCiudades_id == viaje.iCiudades_idOrigen).FirstOrDefault();
                var ciudadDestino = db.del_Ciudades.Where(x => x.iCiudades_id == viaje.iCiudades_idDestino).FirstOrDefault();
                return new
                {
                    viaje.iUsuarioViaje_id,
                    sTelefono_Numero = telf.dtTelefono_Numero,
                    viaje.dtFechaInicio,
                    viaje.dtFechaFin,
                    fLatOrigen = ciudadOrigen.fLat,
                    fLonOrigen = ciudadOrigen.fLon,
                    fLatDestino = ciudadDestino.fLat,
                    fLonDestino = ciudadDestino.fLon,
                    viaje.dtFechaRegistro,
                    CiudadOrigen = ciudadOrigen == null ? "No Reconocido" : ciudadOrigen.sCiudad,
                    PaisOrigen = ciudadOrigen == null ? "" : ciudadOrigen.sPais,
                    CiudadDestino = ciudadDestino == null ? "No Reconocido" : ciudadDestino.sCiudad,
                    PaisDestino = ciudadDestino == null ? "" : ciudadDestino.sPais,
                };
            }
        }
        public dynamic GetAllIntern()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var viaje = (from vi in db.del_UsuarioViaje
                             join te in db.del_UsuarioTelefono on vi.iUsuarioTelefono_id equals te.iUsuarioTelefono_id
                             join co in db.del_Ciudades on vi.iCiudades_idOrigen equals co.iCiudades_id
                             join cd in db.del_Ciudades on vi.iCiudades_idDestino equals cd.iCiudades_id
                             select new
                             {
                                 vi.iUsuarioViaje_id,
                                 vi.sNombre,
                                 te.iUsuarioTelefono_id,
                                 sTelefono_Numero = te.dtTelefono_Numero,
                                 vi.dtFechaInicio,
                                 vi.dtFechaFin,
                                 vi.iCiudades_idOrigen,
                                 vi.iCiudades_idDestino,
                                 PaisOrigen = co.sPais,
                                 CiudadOrigen = co.sCiudad,
                                 PaisDestino = cd.sPais,
                                 CiudadDestino = cd.sCiudad,
                                 Documentos = (db.del_UsuarioViajeDocumentos.Where(x=>x.iUsuarioViaje_id == vi.iUsuarioViaje_id).Select(x=> new { x.iUsuarioViajeDocumentos_id, x.sNombre}))
                             }
                             ).OrderByDescending(x => x.iUsuarioViaje_id).ToList();
                return viaje;
            }
        }
        public dynamic GetMapaActivosIntern()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var viajes = (from vi in db.del_UsuarioViaje
                              where vi.dtFechaFin == null
                              select new {
                                  vi.sNombre,
                                  lat = (
                                   db.del_UsuarioViajeDetalle.Where(x=>x.iUsuarioViaje_id == vi.iUsuarioViaje_id).FirstOrDefault() == null 
                                   ? (decimal ?)db.del_Ciudades.Where(x => x.iCiudades_id == vi.iCiudades_idOrigen).Select(x => x.fLat).FirstOrDefault()
                                   : (decimal?)db.del_UsuarioViajeDetalle.Where(x => x.iUsuarioViaje_id == vi.iUsuarioViaje_id).OrderByDescending(x => x.iUsuarioViajeDetalle_id).Select(x => x.fLat).FirstOrDefault()
                                  ),
                                  lon = (db.del_UsuarioViajeDetalle.Where(x => x.iUsuarioViaje_id == vi.iUsuarioViaje_id).FirstOrDefault() == null
                                  ? (decimal?)db.del_Ciudades.Where(x => x.iCiudades_id == vi.iCiudades_idOrigen).Select(x => x.fLon).FirstOrDefault()
                                  : (decimal?)db.del_UsuarioViajeDetalle.Where(x => x.iUsuarioViaje_id == vi.iUsuarioViaje_id).OrderByDescending(x => x.iUsuarioViajeDetalle_id).Select(x => x.fLon).FirstOrDefault()),
                              }
                              ).ToList();
                //(decimal?)db.del_Ciudades.Where(x => x.iCiudades_id == vi.iCiudades_idOrigen).Select(x => x.fLat).FirstOrDefault())
                //?? (decimal?)db.del_Ciudades.Where(x => x.iCiudades_id == vi.iCiudades_idOrigen).Select(x => x.fLon).FirstOrDefault())
                return viajes;
            }
        }
        #endregion
    }
}
