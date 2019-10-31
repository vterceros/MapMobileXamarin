namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_UsuarioLog
    {
        [Key]
        public int iUsuarioLog_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sDescripcion { get; set; }

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

        [Required]
        [StringLength(10)]
        public string iEmpresa_id { get; set; }

        public int iUsuario_id { get; set; }

        [StringLength(500)]
        public string sUrl { get; set; }

        public virtual seg_Usuario seg_Usuario { get; set; }
    }
}
