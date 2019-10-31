namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class seg_Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public seg_Usuario()
        {
            seg_UsuarioLog = new HashSet<seg_UsuarioLog>();
            seg_UsuarioRol = new HashSet<seg_UsuarioRol>();
        }

        [Key]
        public int iUsuario_id { get; set; }

        public int iPersona_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sLoginUsuario { get; set; }

        [Required]
        [StringLength(500)]
        public string sClave { get; set; }

        [Required]
        [StringLength(500)]
        public string sPreguntaSecreta { get; set; }

        [Required]
        [StringLength(500)]
        public string sRespuestaSecreta { get; set; }

        public DateTime? dtFechaVigencia { get; set; }

        public bool bEstadoUsuario { get; set; }

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

        [StringLength(500)]
        public string sEmail { get; set; }

        public virtual adm_Personas adm_Personas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_UsuarioLog> seg_UsuarioLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_UsuarioRol> seg_UsuarioRol { get; set; }
    }
}
