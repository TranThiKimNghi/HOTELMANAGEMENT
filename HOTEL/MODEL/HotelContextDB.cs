using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HOTEL.MODEL
{
    public partial class HotelContextDB : DbContext
    {
        public HotelContextDB()
            : base("name=HotelContextDB")
        {
        }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<ServiceDetail> ServiceDetails { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceUsage> ServiceUsages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetail>()
                .Property(e => e.BookingID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BookingDetail>()
                .Property(e => e.RoomID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BookingDetail>()
                .Property(e => e.RoomPrice)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Booking>()
                .Property(e => e.BookingID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.CustomerID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.BookingDetails)
                .WithRequired(e => e.Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.BookingID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.BasePrice)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.BookingDetails)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceDetail>()
                .Property(e => e.ServiceUsageID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServiceDetail>()
                .Property(e => e.ServiceID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.ServiceID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Price)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceDetails)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceUsage>()
                .Property(e => e.ServiceUsageID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServiceUsage>()
                .Property(e => e.TotalAmount)
                .HasPrecision(10, 3);

            modelBuilder.Entity<ServiceUsage>()
                .Property(e => e.TotalService)
                .HasPrecision(10, 3);

            modelBuilder.Entity<ServiceUsage>()
                .Property(e => e.PaymentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServiceUsage>()
                .Property(e => e.RoomID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServiceUsage>()
                .HasMany(e => e.ServiceDetails)
                .WithRequired(e => e.ServiceUsage)
                .WillCascadeOnDelete(false);
        }
    }
}
