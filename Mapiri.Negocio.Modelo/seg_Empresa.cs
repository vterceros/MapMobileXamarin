namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_Empresa
    {
        [Key]
        [StringLength(10)]
        public string iEmpresa_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sNombre { get; set; }

        public int iEstado_fl { get; set; }

        [Required]
        [StringLength(500)]
        public string sCreadoPor { get; set; }

        [Required]
        [StringLength(500)]
        public string sModificadoPor { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public DateTime dtFechaModificacion { get; set; }

        public int iControlConcurrencia { get; set; }
    }
}
