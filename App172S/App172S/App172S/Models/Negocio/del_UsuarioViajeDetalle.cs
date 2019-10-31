using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Models.Negocio
{
    public partial class del_UsuarioViajeDetalle
    {
        [PrimaryKey]
        [AutoIncrement]
        public int iUsuarioViajeDetalle_id { get; set; }

        public int? iUsuarioViaje_id { get; set; }

        public DateTime dtFecha { get; set; }

        public decimal fLat { get; set; }

        public decimal fLon { get; set; }
    }
}
