namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_Accion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public seg_Accion()
        {
            seg_AccionMenuRol = new HashSet<seg_AccionMenuRol>();
        }

        [Key]
        public int iAccion_id { get; set; }

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
        [StringLength(500)]
        public string sDescripcion { get; set; }

        [Required]
        [StringLength(10)]
        public string iEmpresa_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_AccionMenuRol> seg_AccionMenuRol { get; set; }
    }
}
