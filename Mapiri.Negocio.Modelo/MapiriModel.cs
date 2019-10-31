namespace Mapiri.Negocio.Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MapiriModel : DbContext
    {
        public MapiriModel()
            : base("name=MapiriModel")
        {
        }

        public virtual DbSet<adm_Personas> adm_Personas { get; set; }
        public virtual DbSet<del_Ciudades> del_Ciudades { get; set; }
        public virtual DbSet<del_UsuarioTelefono> del_UsuarioTelefono { get; set; }
        public virtual DbSet<del_UsuarioViaje> del_UsuarioViaje { get; set; }
        public virtual DbSet<del_UsuarioViajeDetalle> del_UsuarioViajeDetalle { get; set; }
        public virtual DbSet<del_UsuarioViajeDocumentos> del_UsuarioViajeDocumentos { get; set; }
        public virtual DbSet<seg_Accion> seg_Accion { get; set; }
        public virtual DbSet<seg_AccionMenuRol> seg_AccionMenuRol { get; set; }
        public virtual DbSet<seg_Empresa> seg_Empresa { get; set; }
        public virtual DbSet<seg_Menu> seg_Menu { get; set; }
        public virtual DbSet<seg_MenuRoles> seg_MenuRoles { get; set; }
        public virtual DbSet<seg_Rol> seg_Rol { get; set; }
        public virtual DbSet<seg_Usuario> seg_Usuario { get; set; }
        public virtual DbSet<seg_UsuarioLog> seg_UsuarioLog { get; set; }
        public virtual DbSet<seg_UsuarioRol> seg_UsuarioRol { get; set; }
        public virtual DbSet<tmpCities> tmpCities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<adm_Personas>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<adm_Personas>()
                .HasMany(e => e.seg_Usuario)
                .WithRequired(e => e.adm_Personas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<del_Ciudades>()
                .Property(e => e.fLat)
                .HasPrecision(18, 7);

            modelBuilder.Entity<del_Ciudades>()
                .Property(e => e.fLon)
                .HasPrecision(18, 7);

            modelBuilder.Entity<del_UsuarioViaje>()
                .Property(e => e.sNombre)
                .IsUnicode(false);

            modelBuilder.Entity<del_UsuarioViajeDetalle>()
                .Property(e => e.fLat)
                .HasPrecision(18, 7);

            modelBuilder.Entity<del_UsuarioViajeDetalle>()
                .Property(e => e.fLon)
                .HasPrecision(18, 7);

            modelBuilder.Entity<del_UsuarioViajeDocumentos>()
                .Property(e => e.sExtension)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Accion>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Accion>()
                .HasMany(e => e.seg_AccionMenuRol)
                .WithRequired(e => e.seg_Accion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_AccionMenuRol>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Empresa>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Menu>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Menu>()
                .Property(e => e.sUrl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<seg_Menu>()
                .Property(e => e.iMenu_idPadre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<seg_Menu>()
                .HasMany(e => e.seg_AccionMenuRol)
                .WithRequired(e => e.seg_Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_Menu>()
                .HasMany(e => e.seg_MenuRoles)
                .WithRequired(e => e.seg_Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_MenuRoles>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Rol>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Rol>()
                .HasMany(e => e.seg_AccionMenuRol)
                .WithRequired(e => e.seg_Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_Rol>()
                .HasMany(e => e.seg_MenuRoles)
                .WithRequired(e => e.seg_Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_Rol>()
                .HasMany(e => e.seg_UsuarioRol)
                .WithRequired(e => e.seg_Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_Usuario>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_Usuario>()
                .HasMany(e => e.seg_UsuarioLog)
                .WithRequired(e => e.seg_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_Usuario>()
                .HasMany(e => e.seg_UsuarioRol)
                .WithRequired(e => e.seg_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<seg_UsuarioLog>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<seg_UsuarioRol>()
                .Property(e => e.iEmpresa_id)
                .IsUnicode(false);

            modelBuilder.Entity<tmpCities>()
                .Property(e => e.pais)
                .IsUnicode(false);

            modelBuilder.Entity<tmpCities>()
                .Property(e => e.ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<tmpCities>()
                .Property(e => e.lat)
                .HasPrecision(7, 5);

            modelBuilder.Entity<tmpCities>()
                .Property(e => e.lon)
                .HasPrecision(8, 5);
        }
    }
}
