namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_Rol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public seg_Rol()
        {
            seg_AccionMenuRol = new HashSet<seg_AccionMenuRol>();
            seg_MenuRoles = new HashSet<seg_MenuRoles>();
            seg_UsuarioRol = new HashSet<seg_UsuarioRol>();
        }

        [Key]
        public int iRol_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sNombre { get; set; }

        [StringLength(500)]
        public string sObservacion { get; set; }

        public int iEstado_fl { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public DateTime dtFechaModificacion { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_AccionMenuRol> seg_AccionMenuRol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_MenuRoles> seg_MenuRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_UsuarioRol> seg_UsuarioRol { get; set; }
    }
}
