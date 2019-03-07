using Microsoft.EntityFrameworkCore;

namespace Messages.Api.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var messageEntity = modelBuilder.Entity<Message>();
            messageEntity.Property(p => p.Value).IsRequired().HasMaxLength(512);
            messageEntity.Property(p => p.IsPalindrome).IsRequired();
        }
    }
}