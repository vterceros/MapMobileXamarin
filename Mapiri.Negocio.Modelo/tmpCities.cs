namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tmpCities
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string pais { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(45)]
        public string ciudad { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal lat { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal lon { get; set; }
    }
}
