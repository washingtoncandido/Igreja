using FICR.Cooperation.Humanism.Data.Mapping;
using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Entities.Base;
using Microsoft.EntityFrameworkCore;


namespace FICR.Cooperation.Humanism.Data.Context
{
    public class CooperationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public CooperationDbContext(DbContextOptions<CooperationDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EventEntityMapping());

        }
        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity &&
                            (x.State == EntityState.Added ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Deleted));
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        ((BaseEntity)entry.Entity).IsDeleted = true;
                        ((BaseEntity)entry.Entity).LastModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        ((BaseEntity)entry.Entity).LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}