﻿// <auto-generated />
using System;
using Mainframe.BuyerSupplier.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20181130043731_DefaultInventoryItemColumn")]
    partial class DefaultInventoryItemColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.Delivery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNo");

                    b.Property<string>("ContactPerson");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<DateTime>("DeliveryTime");

                    b.Property<int>("OrderID");

                    b.Property<int>("Status");

                    b.Property<string>("VehicleNo");

                    b.HasKey("ID");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.DeliverySlots", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountdownTime");

                    b.Property<string>("CutoffTime");

                    b.Property<string>("DispatchesConfirmTime");

                    b.Property<string>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("OrderAcceptTime");

                    b.Property<string>("OrderCofirmTime");

                    b.Property<string>("SlotName");

                    b.Property<string>("StartTime");

                    b.HasKey("ID");

                    b.ToTable("DeliverySlots");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.InventoryItemCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("InventoryItemCategory");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.InventoryItemSubCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("InventoryItemCategoryID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("InventoryItemSubCategory");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpectedDiliveredDate");

                    b.Property<bool>("IsPreOrder");

                    b.Property<DateTime>("OrderedDate");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.OrderAssignment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BuyerAcknowledgement");

                    b.Property<int>("OrderDetailID");

                    b.Property<int>("OrderID");

                    b.Property<decimal>("Qty");

                    b.Property<bool>("SupplierAcknowledgement");

                    b.Property<int>("SupplierInventoryID");

                    b.Property<bool>("VehicleAcknowledgement");

                    b.HasKey("ID");

                    b.ToTable("OrderAssignment");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.OrderDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemID");

                    b.Property<int>("OrderID");

                    b.Property<decimal>("Qty");

                    b.HasKey("ID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.StandardInventory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Group");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ItemName");

                    b.Property<decimal>("MinimumInventory");

                    b.Property<string>("OrderQuantityBasis");

                    b.Property<string>("Seasonality");

                    b.Property<string>("SubGroup");

                    b.HasKey("ID");

                    b.ToTable("StandardInventory");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.SupplierBase", b =>
                {
                    b.Property<int>("SupplierBaseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliverySlot");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SupplierBaseName");

                    b.HasKey("SupplierBaseId");

                    b.ToTable("SupplierBase");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.SupplierInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AvailableQty");

                    b.Property<int>("ItemID");

                    b.Property<decimal>("ProcessingQty");

                    b.Property<decimal>("Qty");

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("UserID");

                    b.HasKey("Id");

                    b.ToTable("SupplierInventory");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.UnitOfMeasure", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("UnitOfMeasure");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Category");

                    b.Property<string>("Configuration");

                    b.Property<string>("ContactNo");

                    b.Property<int>("DefaultSupplierBaseId");

                    b.Property<string>("DeliverySlot");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("SupplierDefautInventoryItem");

                    b.Property<string>("UserType");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mainframe.BuyerSupplier.Data.Models.UserSupplierBase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Distance");

                    b.Property<int>("SupplierBaseID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("UserSupplierBase");
                });
#pragma warning restore 612, 618
        }
    }
}
