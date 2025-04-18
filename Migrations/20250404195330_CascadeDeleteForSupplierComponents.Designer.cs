﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PharmacyApp.Models;

#nullable disable

namespace PharmacyApp.Migrations
{
    [DbContext(typeof(PharmacyDbContext))]
    [Migration("20250404195330_CascadeDeleteForSupplierComponents")]
    partial class CascadeDeleteForSupplierComponents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("PharmacyApp.Models.Component", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.Property<decimal>("CriticalNorm")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)")
                        .HasColumnName("critical_norm");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int?>("ShelfLife")
                        .HasColumnType("int")
                        .HasColumnName("shelf_life");

                    b.Property<string>("UnitOfMeasure")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("unit_of_measure");

                    b.HasKey("ComponentId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Name" }, "name")
                        .IsUnique();

                    b.ToTable("components", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("birth_date");

                    b.HasKey("CustomerId")
                        .HasName("PRIMARY");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("doctor_id");

                    b.Property<string>("ContactInfo")
                        .HasColumnType("text")
                        .HasColumnName("contact_info");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("license_number");

                    b.HasKey("DoctorId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "LicenseNumber" }, "license_number")
                        .IsUnique();

                    b.ToTable("doctors", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Medication", b =>
                {
                    b.Property<int>("MedicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("medication_id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<bool?>("IsReadyMade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_ready_made")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<decimal?>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("price");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.HasKey("MedicationId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CategoryId" }, "category_id");

                    b.HasIndex(new[] { "TypeId" }, "type_id");

                    b.ToTable("medications", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("CategoryId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Name" }, "name")
                        .IsUnique()
                        .HasDatabaseName("name1");

                    b.ToTable("medication_categories", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationComponent", b =>
                {
                    b.Property<int>("MedicationId")
                        .HasColumnType("int")
                        .HasColumnName("medication_id");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("MedicationId", "ComponentId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex("ComponentId");

                    b.ToTable("medication_components", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.Property<string>("ApplicationMethod")
                        .IsRequired()
                        .HasColumnType("enum('Внутрь','Наружное','Смешанное')")
                        .HasColumnName("application_method");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("TypeId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Name" }, "name")
                        .IsUnique()
                        .HasDatabaseName("name2");

                    b.ToTable("medication_types", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)")
                        .HasColumnName("amount");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int")
                        .HasColumnName("medication_id");

                    b.Property<DateTime?>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("order_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int")
                        .HasColumnName("prescription_id");

                    b.Property<decimal?>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("price");

                    b.Property<int>("RegistrarId")
                        .HasColumnType("int")
                        .HasColumnName("registrar_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("enum('Ожидает обработки','В процессе изготовления','Ожидает компонентов','Готов к выдаче','Выдан пациенту')")
                        .HasColumnName("status");

                    b.HasKey("OrderId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MedicationId" }, "medication_id");

                    b.HasIndex(new[] { "PrescriptionId" }, "prescription_id");

                    b.HasIndex(new[] { "RegistrarId" }, "registrar_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.PendingOrder", b =>
                {
                    b.Property<int>("PendingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pending_id");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<decimal>("RequiredAmount")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)")
                        .HasColumnName("required_amount");

                    b.HasKey("PendingId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ComponentId" }, "component_id");

                    b.HasIndex(new[] { "OrderId" }, "order_id");

                    b.ToTable("pending_orders", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("prescription_id");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text")
                        .HasColumnName("diagnosis");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("doctor_id");

                    b.Property<string>("Dosage")
                        .HasColumnType("text")
                        .HasColumnName("dosage");

                    b.Property<string>("Duration")
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.Property<DateOnly>("IssueDate")
                        .HasColumnType("date")
                        .HasColumnName("issue_date");

                    b.HasKey("PrescriptionId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CustomerId" }, "customer_id");

                    b.HasIndex(new[] { "DoctorId" }, "doctor_id");

                    b.ToTable("prescriptions", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("stock_id");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("arrival_date");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.HasKey("StockId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ComponentId" }, "component_id")
                        .HasDatabaseName("component_id1");

                    b.ToTable("stock", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("supplier_id");

                    b.Property<string>("ContactPerson")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("contact_person");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<sbyte?>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("rating")
                        .HasDefaultValueSql("'5'");

                    b.HasKey("SupplierId")
                        .HasName("PRIMARY");

                    b.ToTable("suppliers", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.SupplierComponent", b =>
                {
                    b.Property<int>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("supplier_id");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.Property<int?>("DeliveryTime")
                        .HasColumnType("int")
                        .HasColumnName("delivery_time");

                    b.Property<decimal?>("UnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("SupplierId", "ComponentId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "ComponentId" }, "component_id")
                        .HasDatabaseName("component_id2");

                    b.ToTable("supplier_components", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.SupplyRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("request_id");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("component_id");

                    b.Property<DateTime?>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("request_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<decimal>("RequestedAmount")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)")
                        .HasColumnName("requested_amount");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('Ожидает обработки','Заказана у поставщика','Доставлена')")
                        .HasColumnName("status")
                        .HasDefaultValueSql("'Ожидает обработки'");

                    b.HasKey("RequestId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ComponentId" }, "component_id")
                        .HasDatabaseName("component_id3");

                    b.ToTable("supply_requests", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("contact_phone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("enum('admin','pharmacist','customer')")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Username" }, "username")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("PharmacyApp.Models.Medication", b =>
                {
                    b.HasOne("PharmacyApp.Models.MedicationCategory", "Category")
                        .WithMany("Medications")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("medications_ibfk_2");

                    b.HasOne("PharmacyApp.Models.MedicationType", "Type")
                        .WithMany("Medications")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("medications_ibfk_1");

                    b.Navigation("Category");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationComponent", b =>
                {
                    b.HasOne("PharmacyApp.Models.Component", "Component")
                        .WithMany("MedicationComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("medication_components_ibfk_2");

                    b.HasOne("PharmacyApp.Models.Medication", "Medication")
                        .WithMany("MedicationComponents")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("medication_components_ibfk_1");

                    b.Navigation("Component");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("PharmacyApp.Models.Order", b =>
                {
                    b.HasOne("PharmacyApp.Models.Medication", "Medication")
                        .WithMany("Orders")
                        .HasForeignKey("MedicationId")
                        .IsRequired()
                        .HasConstraintName("orders_ibfk_2");

                    b.HasOne("PharmacyApp.Models.Prescription", "Prescription")
                        .WithMany("Orders")
                        .HasForeignKey("PrescriptionId")
                        .IsRequired()
                        .HasConstraintName("orders_ibfk_1");

                    b.HasOne("PharmacyApp.Models.User", "Registrar")
                        .WithMany("Orders")
                        .HasForeignKey("RegistrarId")
                        .IsRequired()
                        .HasConstraintName("orders_ibfk_3");

                    b.Navigation("Medication");

                    b.Navigation("Prescription");

                    b.Navigation("Registrar");
                });

            modelBuilder.Entity("PharmacyApp.Models.PendingOrder", b =>
                {
                    b.HasOne("PharmacyApp.Models.Component", "Component")
                        .WithMany("PendingOrders")
                        .HasForeignKey("ComponentId")
                        .IsRequired()
                        .HasConstraintName("pending_orders_ibfk_2");

                    b.HasOne("PharmacyApp.Models.Order", "Order")
                        .WithMany("PendingOrders")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("pending_orders_ibfk_1");

                    b.Navigation("Component");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PharmacyApp.Models.Prescription", b =>
                {
                    b.HasOne("PharmacyApp.Models.Customer", "Customer")
                        .WithMany("Prescriptions")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("prescriptions_ibfk_1");

                    b.HasOne("PharmacyApp.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DoctorId")
                        .IsRequired()
                        .HasConstraintName("prescriptions_ibfk_2");

                    b.Navigation("Customer");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("PharmacyApp.Models.Stock", b =>
                {
                    b.HasOne("PharmacyApp.Models.Component", "Component")
                        .WithMany("Stocks")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("stock_ibfk_1");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("PharmacyApp.Models.SupplierComponent", b =>
                {
                    b.HasOne("PharmacyApp.Models.Component", "Component")
                        .WithMany("SupplierComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PharmacyApp.Models.Supplier", "Supplier")
                        .WithMany("SupplierComponents")
                        .HasForeignKey("SupplierId")
                        .IsRequired()
                        .HasConstraintName("supplier_components_ibfk_1");

                    b.Navigation("Component");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("PharmacyApp.Models.SupplyRequest", b =>
                {
                    b.HasOne("PharmacyApp.Models.Component", "Component")
                        .WithMany("SupplyRequests")
                        .HasForeignKey("ComponentId")
                        .IsRequired()
                        .HasConstraintName("supply_requests_ibfk_1");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("PharmacyApp.Models.Component", b =>
                {
                    b.Navigation("MedicationComponents");

                    b.Navigation("PendingOrders");

                    b.Navigation("Stocks");

                    b.Navigation("SupplierComponents");

                    b.Navigation("SupplyRequests");
                });

            modelBuilder.Entity("PharmacyApp.Models.Customer", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("PharmacyApp.Models.Doctor", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("PharmacyApp.Models.Medication", b =>
                {
                    b.Navigation("MedicationComponents");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationCategory", b =>
                {
                    b.Navigation("Medications");
                });

            modelBuilder.Entity("PharmacyApp.Models.MedicationType", b =>
                {
                    b.Navigation("Medications");
                });

            modelBuilder.Entity("PharmacyApp.Models.Order", b =>
                {
                    b.Navigation("PendingOrders");
                });

            modelBuilder.Entity("PharmacyApp.Models.Prescription", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PharmacyApp.Models.Supplier", b =>
                {
                    b.Navigation("SupplierComponents");
                });

            modelBuilder.Entity("PharmacyApp.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
