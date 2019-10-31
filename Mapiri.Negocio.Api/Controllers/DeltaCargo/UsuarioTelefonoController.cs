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
    public class UsuarioTelefonoController : ApiController
    {

        #region private vars
        private string Customer;
        private static UsuarioTelefonoController _Instance;
        #endregion

        #region ctor

        public UsuarioTelefonoController()
        {}

        static UsuarioTelefonoController()
        { }
        public static UsuarioTelefonoController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new UsuarioTelefonoController();
            }
            return _Instance;
        }

        #endregion

        #region actions
        [HttpGet]
        [Route("Api/UsuarioTelefono/GetByTelfId")]
        public dynamic GetByTelfId(string telfId)
        {
            return this.GetIntern(telfId);
        }

        [HttpGet]
        [Route("Api/UsuarioTelefono/GetByNro")]
        public dynamic GetByNro(string nro)
        {
            return this.GetByNroIntern(nro);
        }

        [HttpGet]
        [Route("Api/UsuarioTelefono/GetByKey")]
        public dynamic GetByKey(int key)
        {
            return this.GetByKeyIntern(key);
        }

        [HttpGet]
        [Route("Api/UsuarioTelefono/GetAll")]
        public dynamic GetAll()
        {
            return this.GetAllIntern();
        }

        [HttpGet]
        [Route("Api/UsuarioTelefono/PostTelefono")]
        public void Post(string telfId, string telfNro)
        {
            this.AddIntern(telfId, telfNro);
        }

        // PUT api/UsuarioTelefono/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/UsuarioTelefono/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions
        public dynamic GetIntern(string sTelefono_Id)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var obj = db.del_UsuarioTelefono.Where(x => x.sTelefono_Id == sTelefono_Id).Select(x=> new { x.iUsuarioTelefono_id, x.sTelefono_Id, x.sChoferNombre, x.sChoferDoc, x.dtTelefono_Numero, x.dtFechaRegistro }).FirstOrDefault();
                return obj;
            }
        }
        public dynamic GetByNroIntern(string nro)
        {
            using (MapiriModel db = new MapiriModel())
            {
                nro = nro.Trim();
                var obj = db.del_UsuarioTelefono.Where(x => x.dtTelefono_Numero == nro).Select(x => new { x.iUsuarioTelefono_id, x.sTelefono_Id, x.sChoferNombre, x.sChoferDoc, x.dtTelefono_Numero, x.dtFechaRegistro }).FirstOrDefault();
                return obj;
            }
        }
        public dynamic GetByKeyIntern(int key)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var obj = db.del_UsuarioTelefono.Where(x=>x.iUsuarioTelefono_id == key).Select(x => new { x.iUsuarioTelefono_id, x.sChoferNombre, x.dtTelefono_Numero }).FirstOrDefault();
                return obj;
            }
        }
        public dynamic GetAllIntern()
        {
            using (MapiriModel db = new MapiriModel())
            {
                var obj = db.del_UsuarioTelefono.Select(x => new { x.iUsuarioTelefono_id, x.sChoferNombre, x.dtTelefono_Numero }).ToList();
                return obj;
            }
        }
        public void AddIntern(string sTelefono_Id, string dtTelefono_Numero)
        {
            dtTelefono_Numero = (dtTelefono_Numero ?? "").Trim();
            using (MapiriModel db = new MapiriModel())
            {
                var telf = db.del_UsuarioTelefono.Where(x => x.dtTelefono_Numero == dtTelefono_Numero).FirstOrDefault();
                if (telf == null)
                {
                    var obj = new del_UsuarioTelefono()
                    {
                        sTelefono_Id = sTelefono_Id,
                        dtTelefono_Numero = dtTelefono_Numero,
                        sChoferNombre = "NA",
                        sChoferDoc = "NA",
                        dtFechaRegistro = DateTime.Now,
                    };
                    db.del_UsuarioTelefono.Add(obj);
                }
                else
                {
                    telf.sTelefono_Id = sTelefono_Id;
                }
                db.SaveChanges();
            }
        }
        #endregion
    }
}
