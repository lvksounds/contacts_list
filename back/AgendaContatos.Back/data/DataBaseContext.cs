using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Back.data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Contact> Contacts { get; set; }

    }
}
