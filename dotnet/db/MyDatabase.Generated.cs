using Microsoft.EntityFrameworkCore;
using modkaz.DBs.Entities;

namespace modkaz.DBs
{
    public partial class MyDatabase : DbContext
    {
        public MyDatabase() { }

        private MyDatabase(DbContextOptions<MyDatabase> options) : base(options) { }

        public virtual DbSet<DemoEntities> DemoEntities { get; set; }
        
        public virtual DbSet<PostsEntity> Posts { get; set; }
        
        public virtual DbSet<UsersEntity> Users { get; set; }
        
        public virtual DbSet<ReviewsEntity> Reviews { get; set; }
        
        public virtual DbSet<TicketsEntity> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
