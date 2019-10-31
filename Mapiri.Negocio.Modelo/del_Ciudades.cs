namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class del_Ciudades
    {
        [Key]
        public int iCiudades_id { get; set; }

        [Required]
        [StringLength(500)]
        public string sCiudad { get; set; }

        [Required]
        [StringLength(500)]
        public string sPais { get; set; }

        public decimal fLat { get; set; }

        public decimal fLon { get; set; }

        public bool bHabilitado { get; set; }
    }
}
