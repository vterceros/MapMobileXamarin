namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_MenuRoles
    {
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

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iMenu_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iRol_id { get; set; }

        [Required]
        [StringLength(10)]
        public string iEmpresa_id { get; set; }

        public virtual seg_Menu seg_Menu { get; set; }

        public virtual seg_Rol seg_Rol { get; set; }
    }
}
