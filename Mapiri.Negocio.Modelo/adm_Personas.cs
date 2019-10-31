namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class adm_Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public adm_Personas()
        {
            seg_Usuario = new HashSet<seg_Usuario>();
        }

        [Key]
        public int iPersona_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sNombres { get; set; }

        [Required]
        [StringLength(500)]
        public string sApellidos { get; set; }

        public DateTime? dtFechaNacimiento { get; set; }

        [Column(TypeName = "image")]
        public byte[] sFotoDigital { get; set; }

        [Required]
        [StringLength(500)]
        public string sEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string sTelefono { get; set; }

        [StringLength(50)]
        public string sCedulaIdentidad { get; set; }

        [StringLength(50)]
        public string sNit { get; set; }

        [Required]
        [StringLength(500)]
        public string sDireccionDomicilio { get; set; }

        public DateTime dtFechaRegistro { get; set; }

        public int iEstadoRegistrado_fl { get; set; }

        public int iPais_id { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seg_Usuario> seg_Usuario { get; set; }
    }
}
