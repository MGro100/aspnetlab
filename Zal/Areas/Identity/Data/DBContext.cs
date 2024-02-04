using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zal.Areas.Identity.Data;
using Zal.Models.Entities;

namespace Zal.Areas.Identity.Data;

public class DBContext : IdentityDbContext<ZalUser>
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<Song> Songs { get; set; }
}

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ZalUser>
{
    public void Configure(EntityTypeBuilder<ZalUser> builder)
    {
        builder.Property(x => x.Imie).HasMaxLength(100);
        builder.Property(x => x.Nazwisko).HasMaxLength(100);
    }
}
