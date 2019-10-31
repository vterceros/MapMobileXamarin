using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Models.Negocio
{
    public partial class del_UsuarioViajeDocumentos
    {
        public int iUsuarioViajeDocumentos_id { get; set; }

        public int? iUsuarioViaje_id { get; set; }

        public string sNombre { get; set; }

        public byte[] iDocumento { get; set; }

        public string sExtension { get; set; }
    }
}
