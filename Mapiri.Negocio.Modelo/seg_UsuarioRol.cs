namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_UsuarioRol
    {
        public int iEstado_fl { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iRol_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iUsuario_id { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public DateTime drFechaModificacion { get; set; }

        [Required]
        [StringLength(500)]
        public string sCreadoPor { get; set; }

        [Required]
        [StringLength(500)]
        public string sModificadoPor { get; set; }

        public int iControlConcurrencia { get; set; }

        [Required]
        [StringLength(10)]
        public string iEmpresa_id { get; set; }

        public virtual seg_Rol seg_Rol { get; set; }

        public virtual seg_Usuario seg_Usuario { get; set; }
    }
}
