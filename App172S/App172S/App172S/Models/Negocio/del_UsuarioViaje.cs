using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Models.Negocio
{
    public partial class del_UsuarioViaje
    {
        public int iUsuarioViaje_id { get; set; }

        public string sTelefono_Numero { get; set; }

        public DateTime dtFechaInicio { get; set; }

        public DateTime? dtFechaFin { get; set; }

        public decimal fLatOrigen { get; set; }

        public decimal fLonOrigen { get; set; }

        public decimal fLatDestino { get; set; }

        public decimal fLonDestino { get; set; }

        public DateTime dtFechaRegistro { get; set; }

        public string CiudadOrigen { get; set; }
        public string PaisOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public string PaisDestino { get; set; }
    }
}
