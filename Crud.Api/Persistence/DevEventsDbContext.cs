using Crud.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Api.Persistence
{
    public class DevEventsDbContext : DbContext
    {
        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {
            
        }

        // setar tabelas do banco de dados
        public DbSet<DevEvents> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeaker { get; set; }

        // configurar chaves primárias
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DevEvents>(e =>
            {
                e.HasKey(de => de.Id);
                e.Property(de => de.Title).IsRequired();
                e.Property(de => de.Description).HasMaxLength(200).HasColumnType("varchar()").IsRequired(false);
                e.HasMany(de => de.Speakers).WithOne().HasForeignKey(de => de.DevEventId);
            });
            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }
    }
}
