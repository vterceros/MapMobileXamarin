namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class del_UsuarioTelefono
    {
        [Key]
        public int iUsuarioTelefono_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sTelefono_Id { get; set; }

        [Required]
        [StringLength(500)]
        public string dtTelefono_Numero { get; set; }

        [Required]
        [StringLength(500)]
        public string sChoferNombre { get; set; }

        [Required]
        [StringLength(500)]
        public string sChoferDoc { get; set; }

        public DateTime dtFechaRegistro { get; set; }
    }
}
