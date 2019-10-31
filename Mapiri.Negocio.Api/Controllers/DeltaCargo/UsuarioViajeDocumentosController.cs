using Mapiri.Library.Negocio.Seguridad;
using Mapiri.Negocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mapiri.Negocio.Api.Controllers
{
    //[Authorize]
    public class UsuarioViajeDocumentosController : ApiController
    {

        #region private vars
        private string Customer;
        private static UsuarioViajeDocumentosController _Instance;
        #endregion

        #region ctor

        public UsuarioViajeDocumentosController()
        {}

        static UsuarioViajeDocumentosController()
        { }
        public static UsuarioViajeDocumentosController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new UsuarioViajeDocumentosController();
            }
            return _Instance;
        }

        #endregion

        #region actions
        [HttpGet]
        [Route("Api/UsuarioViajeDocumentos/GetByTelfId")]
        public dynamic GetByTelfId(string telfId)
        {
            return this.GetIntern(telfId);
        }

        [HttpGet]
        [Route("Api/UsuarioViajeDocumentos/GetByViajeId")]
        public dynamic GetByViajeId(int viajeId)
        {
            return this.GetIntern(viajeId);
        }

        [HttpGet]
        [Route("Api/UsuarioViajeDocumentos/GetById")]
        public dynamic GetBybId(int Id)
        {
            return this.GetByIdIntern(Id);
        }

        [HttpGet]
        [Route("Api/UsuarioViajeDocumentos/Get/{Id}")]
        public HttpResponseMessage Get(int Id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            using (MapiriModel db = new MapiriModel())
            {
                var img = db.del_UsuarioViajeDocumentos.Where(x => x.iUsuarioViajeDocumentos_id == Id).FirstOrDefault();
                if (img == null) return null;
                result.Content = new ByteArrayContent(img.iDocumento);

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("Delta{0}.{1}",Id, img.sExtension)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            return result;
        }

        [HttpPost]
        [Route("Api/UsuarioViajeDocumentos/PostFile")]
        public async Task<IHttpActionResult> PostFile(int viajeId, string nombre)
        {
            //this.AddIntern(2, obj.sNombre, "", obj.iDocumento);
            long fileID = 0;
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.Contents)
                {
                    //string filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();
                    this.AddIntern(viajeId,nombre,"png", buffer);
                }
            }
            catch (Exception err)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                message.Content = new StringContent(err.Message);
                throw new HttpResponseException(message);
            }
            return Ok<long>(0);

        }

        // PUT api/UsuarioViajeDocumentos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/UsuarioViajeDocumentos/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions

        public dynamic GetIntern(int viajeId)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var result = db.del_UsuarioViajeDocumentos.Where(x => x.iUsuarioViaje_id == viajeId).Select(x=> new { x.iUsuarioViajeDocumentos_id,x.sNombre }).ToList();

                return result;
            }
        }

        public dynamic GetByIdIntern(int Id)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var result = db.del_UsuarioViajeDocumentos.Where(x => x.iUsuarioViajeDocumentos_id == Id).Select(x => new { x.iUsuarioViajeDocumentos_id, x.iDocumento }).FirstOrDefault();

                return result;
            }
        }

        public List<del_UsuarioViajeDocumentos> GetIntern(string sTelefono_Id)
        {
            List<del_UsuarioViajeDocumentos> result = new List<del_UsuarioViajeDocumentos>();
            using (MapiriModel db = new MapiriModel())
            {
                var telf = db.del_UsuarioTelefono.Where(x => x.sTelefono_Id == sTelefono_Id).FirstOrDefault();
                if (telf ==null)
                {
                    return result;
                }
                var viaje = db.del_UsuarioViaje.Where(x => x.iUsuarioTelefono_id == telf.iUsuarioTelefono_id && x.dtFechaFin == null &&
                x.iCiudades_idOrigen != null && x.iCiudades_idDestino != null).FirstOrDefault();
                if (viaje == null)
                {
                    return result;
                }
                result = db.del_UsuarioViajeDocumentos.Where(x => x.iUsuarioViaje_id == viaje.iUsuarioViaje_id).ToList();
                
                return result;
            }
        }

        public void AddIntern(int viajeId, string nombre, string extension, byte[] doc)
        {
            using (MapiriModel db = new MapiriModel())
            {
                var obj = new del_UsuarioViajeDocumentos()
                {
                    iUsuarioViaje_id = viajeId,
                    sNombre = nombre,
                    sExtension = extension,
                    iDocumento = doc
                };
                db.del_UsuarioViajeDocumentos.Add(obj);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
