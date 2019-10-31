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
    public class EmpresaController : ApiController
    {

        #region private vars
        private string Customer;
        private static EmpresaController _Instance;
        #endregion

        #region ctor

        public EmpresaController()
        {}

        static EmpresaController()
        { }
        public static EmpresaController Instance()
        {
            if (_Instance == null)
            {
                _Instance = new EmpresaController();
            }
            return _Instance;
        }

        #endregion

        #region actions

        // GET api/Empresa
        [HttpGet]
        [Route("Api/Empresa/GetAll")]
        public dynamic GetAll()
        {
            return Empresa.Instance().ObtenerTodas(); ;
        }

        // GET api/Empresa/5
        public string Get(int id)
        {
            return "value";
        }

        public void Post(seg_Empresa obj)
        {
            Empresa.Instance().Add(obj);
        }

        // PUT api/Empresa/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Empresa/5
        public void Delete(int id)
        {
        }

        #endregion

        #region non actions

        #endregion
    }
}
