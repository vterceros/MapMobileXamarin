namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class del_UsuarioViaje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public del_UsuarioViaje()
        {
            del_UsuarioViajeDetalle = new HashSet<del_UsuarioViajeDetalle>();
            del_UsuarioViajeDocumentos = new HashSet<del_UsuarioViajeDocumentos>();
        }

        [Key]
        public int iUsuarioViaje_id { get; set; }

        public DateTime dtFechaInicio { get; set; }

        public DateTime? dtFechaFin { get; set; }

        public DateTime dtFechaRegistro { get; set; }

        public int? iUsuarioTelefono_id { get; set; }

        [StringLength(100)]
        public string sNombre { get; set; }

        public int? iCiudades_idOrigen { get; set; }

        public int? iCiudades_idDestino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<del_UsuarioViajeDetalle> del_UsuarioViajeDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<del_UsuarioViajeDocumentos> del_UsuarioViajeDocumentos { get; set; }
    }
}
