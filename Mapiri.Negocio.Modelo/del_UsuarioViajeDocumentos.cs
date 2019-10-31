namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class del_UsuarioViajeDocumentos
    {
        [Key]
        public int iUsuarioViajeDocumentos_id { get; set; }

        public int? iUsuarioViaje_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sNombre { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] iDocumento { get; set; }

        [Required]
        [StringLength(10)]
        public string sExtension { get; set; }

        public virtual del_UsuarioViaje del_UsuarioViaje { get; set; }
    }
}
