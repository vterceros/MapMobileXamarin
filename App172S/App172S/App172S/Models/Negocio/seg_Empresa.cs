using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Models.Negocio
{
    public partial class seg_Empresa
    {
        public string iEmpresa_id { get; set; }

        public string sNombre { get; set; }

        public int iEstado_fl { get; set; }

        public string sCreadoPor { get; set; }

        public string sModificadoPor { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public DateTime dtFechaModificacion { get; set; }

        public int iControlConcurrencia { get; set; }
    }
}
