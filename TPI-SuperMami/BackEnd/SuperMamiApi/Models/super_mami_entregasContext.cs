using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class super_mami_entregasContext : DbContext
    {
        public super_mami_entregasContext()
        {
        }

        public super_mami_entregasContext(DbContextOptions<super_mami_entregasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<PgBuffercache> PgBuffercaches { get; set; }
        public virtual DbSet<PgStatStatement> PgStatStatements { get; set; }
        public virtual DbSet<Pickup> Pickups { get; set; }
        public virtual DbSet<PickupDetail> PickupDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<ShippingCompany> ShippingCompanies { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
        public virtual DbSet<ShippingPayment> ShippingPayments { get; set; }
        public virtual DbSet<ShippingType> ShippingTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=administrador@dbtpimsi2; Password=Contra123*; SslMode=Prefer;Server=dbtpimsi2.postgres.database.azure.com; Database=super_mami_entregas;Integrated Security=true;Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.IdBranch)
                    .HasName("branches_pkey");

                entity.ToTable("branches");

                entity.Property(e => e.IdBranch)
                    .HasColumnName("id_branch")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(20)
                    .HasColumnName("zip_code");

                entity.HasOne(d => d.IdZoneNavigation)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.IdZone)
                    .HasConstraintName("fk_branches_id_zone");
            });

            modelBuilder.Entity<DeliveryOrder>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryOrder)
                    .HasName("delivery_order_pkey");

                entity.ToTable("delivery_order");

                entity.Property(e => e.IdDeliveryOrder)
                    .HasColumnName("id_delivery_order")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");

                entity.Property(e => e.IdBranch).HasColumnName("id_branch");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.IsFree).HasColumnName("is_free");

                entity.Property(e => e.IsOwner).HasColumnName("is_owner");

                entity.Property(e => e.IsShipping).HasColumnName("is_shipping");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.ShippingPrice).HasColumnName("shipping_price");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.HasOne(d => d.IdBranchNavigation)
                    .WithMany(p => p.DeliveryOrders)
                    .HasForeignKey(d => d.IdBranch)
                    .HasConstraintName("fk_delivery_order_id_branch");

                entity.HasOne(d => d.IdZoneNavigation)
                    .WithMany(p => p.DeliveryOrders)
                    .HasForeignKey(d => d.IdZone)
                    .HasConstraintName("fk_delivery_order_id_zone");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.IdDocumentType)
                    .HasName("document_type_pkey");

                entity.ToTable("document_type");

                entity.Property(e => e.IdDocumentType)
                    .HasColumnName("id_document_type")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DocumentType1)
                    .HasMaxLength(50)
                    .HasColumnName("document_type");
            });

            modelBuilder.Entity<PgBuffercache>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_buffercache");

                entity.Property(e => e.Bufferid).HasColumnName("bufferid");

                entity.Property(e => e.Isdirty).HasColumnName("isdirty");

                entity.Property(e => e.PinningBackends).HasColumnName("pinning_backends");

                entity.Property(e => e.Relblocknumber).HasColumnName("relblocknumber");

                entity.Property(e => e.Reldatabase)
                    .HasColumnType("oid")
                    .HasColumnName("reldatabase");

                entity.Property(e => e.Relfilenode)
                    .HasColumnType("oid")
                    .HasColumnName("relfilenode");

                entity.Property(e => e.Relforknumber).HasColumnName("relforknumber");

                entity.Property(e => e.Reltablespace)
                    .HasColumnType("oid")
                    .HasColumnName("reltablespace");

                entity.Property(e => e.Usagecount).HasColumnName("usagecount");
            });

            modelBuilder.Entity<PgStatStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnType("oid")
                    .HasColumnName("dbid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnType("oid")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Pickup>(entity =>
            {
                entity.HasKey(e => e.IdPickup)
                    .HasName("pickups_pkey");

                entity.ToTable("pickups");

                entity.Property(e => e.IdPickup)
                    .HasColumnName("id_pickup")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdDeliveryOrder).HasColumnName("id_delivery_order");

                entity.Property(e => e.IdState).HasColumnName("id_state");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.HasOne(d => d.IdDeliveryOrderNavigation)
                    .WithMany(p => p.Pickups)
                    .HasForeignKey(d => d.IdDeliveryOrder)
                    .HasConstraintName("fk_pickups_id_delivery_order");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Pickups)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("fk_pickups_id_state");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Pickups)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_pickups_id_user");
            });

            modelBuilder.Entity<PickupDetail>(entity =>
            {
                entity.HasKey(e => e.IdPickupDetail)
                    .HasName("pickup_detail_pkey");

                entity.ToTable("pickup_detail");

                entity.Property(e => e.IdPickupDetail)
                    .HasColumnName("id_pickup_detail")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BagsQuantity).HasColumnName("bags_quantity");

                entity.Property(e => e.IdPickup).HasColumnName("id_pickup");

                entity.Property(e => e.Volume)
                    .HasMaxLength(30)
                    .HasColumnName("volume");

                entity.Property(e => e.Weight)
                    .HasMaxLength(30)
                    .HasColumnName("weight");

                entity.HasOne(d => d.IdPickupNavigation)
                    .WithMany(p => p.PickupDetails)
                    .HasForeignKey(d => d.IdPickup)
                    .HasConstraintName("fk_pickup_detail_id_pickup");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => e.IdShipping)
                    .HasName("shippings_pkey");

                entity.ToTable("shippings");

                entity.Property(e => e.IdShipping)
                    .HasColumnName("id_shipping")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdDeliveryOrder).HasColumnName("id_delivery_order");

                entity.Property(e => e.IdShippingCompany).HasColumnName("id_shipping_company");

                entity.Property(e => e.IdState).HasColumnName("id_state");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.HasOne(d => d.IdDeliveryOrderNavigation)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.IdDeliveryOrder)
                    .HasConstraintName("fk_shippings_id_delivery_order");

                entity.HasOne(d => d.IdShippingCompanyNavigation)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.IdShippingCompany)
                    .HasConstraintName("fk_shippings_id_shipping_company");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("fk_shippings_id_state");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_shippings_id_user");
            });

            modelBuilder.Entity<ShippingCompany>(entity =>
            {
                entity.HasKey(e => e.IdShippingCompany)
                    .HasName("shipping_company_pkey");

                entity.ToTable("shipping_company");

                entity.Property(e => e.IdShippingCompany)
                    .HasColumnName("id_shipping_company")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(50)
                    .HasColumnName("business_name");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .HasColumnName("contact_name");

                entity.Property(e => e.Cuit)
                    .HasMaxLength(50)
                    .HasColumnName("cuit");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.IdShippingType).HasColumnName("id_shipping_type");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.MaxShippingsPerDay).HasColumnName("max_shippings_per_day");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.ShiftEndTime)
                    .HasMaxLength(20)
                    .HasColumnName("shift_end_time");

                entity.Property(e => e.ShiftStartTime)
                    .HasMaxLength(20)
                    .HasColumnName("shift_start_time");

                entity.HasOne(d => d.IdShippingTypeNavigation)
                    .WithMany(p => p.ShippingCompanies)
                    .HasForeignKey(d => d.IdShippingType)
                    .HasConstraintName("fk_shipping_company_id_shipping_type");
            });

            modelBuilder.Entity<ShippingDetail>(entity =>
            {
                entity.HasKey(e => e.IdShippingDetail)
                    .HasName("shipping_detail_pkey");

                entity.ToTable("shipping_detail");

                entity.Property(e => e.IdShippingDetail)
                    .HasColumnName("id_shipping_detail")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Comment)
                    .HasMaxLength(30)
                    .HasColumnName("comment");

                entity.Property(e => e.IdShipping).HasColumnName("id_shipping");

                entity.Property(e => e.Old)
                    .HasMaxLength(30)
                    .HasColumnName("old");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.IdShippingNavigation)
                    .WithMany(p => p.ShippingDetails)
                    .HasForeignKey(d => d.IdShipping)
                    .HasConstraintName("fk_shipping_detail_id_shipping");
            });

            modelBuilder.Entity<ShippingPayment>(entity =>
            {
                entity.HasKey(e => e.IdShippingPayment)
                    .HasName("shipping_payment_pkey");

                entity.ToTable("shipping_payment");

                entity.Property(e => e.IdShippingPayment)
                    .HasColumnName("id_shipping_payment")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.IdShipping).HasColumnName("id_shipping");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.HasOne(d => d.IdShippingNavigation)
                    .WithMany(p => p.ShippingPayments)
                    .HasForeignKey(d => d.IdShipping)
                    .HasConstraintName("fk_shipping_payment_id_shipping");
            });

            modelBuilder.Entity<ShippingType>(entity =>
            {
                entity.HasKey(e => e.IdShippingType)
                    .HasName("shipping_type_pkey");

                entity.ToTable("shipping_type");

                entity.Property(e => e.IdShippingType)
                    .HasColumnName("id_shipping_type")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.MaxBagsCapacity).HasColumnName("max_bags_capacity");

                entity.Property(e => e.MaxVolumeCapacity)
                    .HasMaxLength(30)
                    .HasColumnName("max_volume_capacity");

                entity.Property(e => e.MaxWeightCapacity)
                    .HasMaxLength(30)
                    .HasColumnName("max_weight_capacity");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("states_pkey");

                entity.ToTable("states");

                entity.Property(e => e.IdState)
                    .HasColumnName("id_state")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.State1)
                    .HasMaxLength(30)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(50)
                    .HasColumnName("document_number");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.IdDocumentType).HasColumnName("id_document_type");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.HasOne(d => d.IdDocumentTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdDocumentType)
                    .HasConstraintName("fk_users_id_document_type");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("fk_users_id_rol");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(e => e.IdZone)
                    .HasName("zones_pkey");

                entity.ToTable("zones");

                entity.Property(e => e.IdZone)
                    .HasColumnName("id_zone")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Zone1)
                    .HasMaxLength(50)
                    .HasColumnName("zone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
