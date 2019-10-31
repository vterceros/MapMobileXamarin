namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class del_UsuarioViajeDetalle
    {
        [Key]
        public int iUsuarioViajeDetalle_id { get; set; }

        public int? iUsuarioViaje_id { get; set; }

        public DateTime dtFecha { get; set; }

        public decimal fLat { get; set; }

        public decimal fLon { get; set; }

        public virtual del_UsuarioViaje del_UsuarioViaje { get; set; }
    }
}
