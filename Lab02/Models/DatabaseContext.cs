using Microsoft.EntityFrameworkCore;

namespace Lab02.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :base(){ }
        public DbSet<Employees> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string str = "server=LAPTOP-PH1AFEK8\\SQLEXPRESS;database=TestDB;uid=sa;pwd=123;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(str);
        }
    }
}
