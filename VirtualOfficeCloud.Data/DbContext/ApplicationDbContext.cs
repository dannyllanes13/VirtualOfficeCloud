using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualOfficeCloud.Data.Models;

namespace VirtualOfficeCloud.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<StoreUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Persons> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

//Contact -------------------------------------------------------
            modelBuilder.Entity<Contacts>(contact =>
            {
                contact.Property(prop => prop.FirstName).HasColumnType("varchar(50)");

                contact.Property(prop => prop.LastName).HasColumnType("varchar(50)");

                contact.Property(prop => prop.MiddleName).HasColumnType("varchar(50)");

                contact.Property(prop => prop.FamiliarName).HasColumnType("varchar(50)");

                contact.Property(prop => prop.FullName)
                    .HasColumnType("varchar(101)")
                    .HasComputedColumnSql("([FirstName]+ ' ')+[LastName]")
                    .ValueGeneratedOnAddOrUpdate();

                contact.Property(prop => prop.Email).HasColumnType("varchar(100)");

                contact.Property(prop => prop.CellPhone).HasColumnType("varchar(25)");

                contact.Property(prop => prop.Phone).HasColumnType("varchar(25)");

                contact.Property(prop => prop.SinSsn).HasColumnType("varchar(50)");

                contact.Property(prop => prop.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                contact.Property(prop => prop.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                contact.Property(prop => prop.IsActive)
                    .HasDefaultValueSql("1");
            });

 //Person ----------------------------------------------------
            modelBuilder.Entity<Persons>(person =>
            {
                person.Property(prop => prop.AccessLevel).HasColumnType("varchar(70)");

                person.Property(prop => prop.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                person.Property(prop => prop.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                person.Property(prop => prop.IsActive)
                    .HasDefaultValueSql("1");
            });
        }

    }
}
