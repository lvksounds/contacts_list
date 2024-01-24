using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Back.data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        
        public DbSet<Models.User> Users { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=app.db");
        //}
    }
}
