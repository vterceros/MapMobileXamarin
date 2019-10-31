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
    public class UsuarioController : ApiController
    {

        #region private vars
        private string Customer;
        private static UsuarioController _Instance;
        #endregion

        #region ctor

        public UsuarioController()
        {}

        static UsuarioController()
        { }
        public static UsuarioController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new UsuarioController();
            }
            return _Instance;
        }

        #endregion

        #region actions

        // GET api/Usuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        public void Post(seg_Usuario obj)
        {
            //Usuario.Instance().Add(obj);
        }

        // PUT api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Usuario/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions

        #endregion
    }
}
