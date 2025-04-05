using Microsoft.EntityFrameworkCore;

namespace PharmacyApp.Models;

public partial class PharmacyDbContext : DbContext
{
    public PharmacyDbContext()
    {
    }

    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<MedicationCategory> MedicationCategories { get; set; }

    public virtual DbSet<MedicationComponent> MedicationComponents { get; set; }

    public virtual DbSet<MedicationType> MedicationTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PendingOrder> PendingOrders { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierComponent> SupplierComponents { get; set; }

    public virtual DbSet<SupplyRequest> SupplyRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=pharmacydb;user=SeregaAdm;password=60Seri090420", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PRIMARY");

            entity.ToTable("components");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.CriticalNorm)
                .HasPrecision(10, 3)
                .HasColumnName("critical_norm");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ShelfLife).HasColumnName("shelf_life");
            entity.Property(e => e.UnitOfMeasure)
                .HasMaxLength(20)
                .HasColumnName("unit_of_measure");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.FullName)
            .HasMaxLength(100)
            .HasColumnName("full_name")
            .IsRequired();
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PRIMARY");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.LicenseNumber, "license_number").IsUnique();

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.ContactInfo)
                .HasColumnType("text")
                .HasColumnName("contact_info");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("license_number");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PRIMARY");

            entity.ToTable("medications");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.TypeId, "type_id");

            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.IsReadyMade)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_ready_made");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Medications)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medications_ibfk_2");

            entity.HasOne(d => d.Type).WithMany(p => p.Medications)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medications_ibfk_1");
        });

        modelBuilder.Entity<MedicationCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("medication_categories");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MedicationComponent>(entity =>
        {
            entity.HasKey(e => new { e.MedicationId, e.ComponentId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("medication_components");

            entity.HasOne(d => d.Medication)
                .WithMany(p => p.MedicationComponents)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("medication_components_ibfk_1");

            entity.HasOne(d => d.Component)
                .WithMany(p => p.MedicationComponents)
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("medication_components_ibfk_2");
        });


        modelBuilder.Entity<MedicationType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("medication_types");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.ApplicationMethod)
                .HasColumnType("enum('Внутрь','Наружное','Смешанное')")
                .HasColumnName("application_method");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.MedicationId, "medication_id");

            entity.HasIndex(e => e.PrescriptionId, "prescription_id");

            entity.HasIndex(e => e.RegistrarId, "registrar_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 3)
                .HasColumnName("amount");
            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("order_date");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.RegistrarId).HasColumnName("registrar_id");
            entity.Property(e => e.Status)
                .HasColumnType("enum('Ожидает обработки','В процессе изготовления','Ожидает компонентов','Готов к выдаче','Выдан пациенту')")
                .HasColumnName("status");

            entity.HasOne(d => d.Medication).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_2");

            entity.HasOne(d => d.Prescription).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_1");

            entity.HasOne(d => d.Registrar).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_3");
        });

        modelBuilder.Entity<PendingOrder>(entity =>
        {
            entity.HasKey(e => e.PendingId).HasName("PRIMARY");

            entity.ToTable("pending_orders");

            entity.HasIndex(e => e.ComponentId, "component_id");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.Property(e => e.PendingId).HasColumnName("pending_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.RequiredAmount)
                .HasPrecision(10, 3)
                .HasColumnName("required_amount");

            entity.HasOne(d => d.Component).WithMany(p => p.PendingOrders)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pending_orders_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.PendingOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pending_orders_ibfk_1");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PRIMARY");

            entity.ToTable("prescriptions");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.DoctorId, "doctor_id");

            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Diagnosis)
                .HasColumnType("text")
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Dosage)
                .HasColumnType("text")
                .HasColumnName("dosage");
            entity.Property(e => e.Duration)
                .HasColumnType("text")
                .HasColumnName("duration");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.Prescriptions)
        .HasForeignKey(d => d.CustomerId)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("prescriptions_ibfk_1");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_ibfk_2");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PRIMARY");

            entity.ToTable("stock");

            entity.HasIndex(e => e.ComponentId, "component_id");

            entity.Property(e => e.StockId).HasColumnName("stock_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 3)
                .HasColumnName("amount");
            entity.Property(e => e.ArrivalDate).HasColumnName("arrival_date");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");


            entity.HasOne(d => d.Component)
                .WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stock_ibfk_1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .HasColumnName("contact_person");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("'5'")
                .HasColumnName("rating");
        });

        modelBuilder.Entity<SupplierComponent>(entity =>
        {
            entity.HasKey(e => new { e.SupplierId, e.ComponentId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("supplier_components");

            entity.HasIndex(e => e.ComponentId, "component_id");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.DeliveryTime).HasColumnName("delivery_time");

            entity.HasOne(d => d.Supplier)
                   .WithMany(p => p.SupplierComponents)
                   .HasForeignKey(d => d.SupplierId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("supplier_components_ibfk_1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierComponents)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_components_ibfk_1");
        });

        modelBuilder.Entity<SupplyRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PRIMARY");

            entity.ToTable("supply_requests");

            entity.HasIndex(e => e.ComponentId, "component_id");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("request_date");
            entity.Property(e => e.RequestedAmount)
                .HasPrecision(10, 3)
                .HasColumnName("requested_amount");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Ожидает обработки'")
                .HasColumnType("enum('Ожидает обработки','Заказана у поставщика','Доставлена')")
                .HasColumnName("status");

            entity.HasOne(d => d.Component).WithMany(p => p.SupplyRequests)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supply_requests_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasColumnType("enum('admin','pharmacist','customer')")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
