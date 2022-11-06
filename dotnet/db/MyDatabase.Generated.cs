using Microsoft.EntityFrameworkCore;
using modkaz.DBs.Entities;

namespace modkaz.DBs
{
    public partial class MyDatabase : DbContext
    {
        public MyDatabase() { }

        private MyDatabase(DbContextOptions<MyDatabase> options) : base(options) { }

        public virtual DbSet<DemoEntities> DemoEntities { get; set; }
        
        public virtual DbSet<Posts> Posts { get; set; }
        
        public virtual DbSet<Users> Users { get; set; }
        
        public virtual DbSet<Reviews> Reviews { get; set; }
        
        public virtual DbSet<Tickets> Tickets { get; set; }

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
