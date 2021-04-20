using MbanqPeopleCRUD.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MbanqPeopleCRUD.DAL
{
    public class MbanqContext : DbContext
    {
        public MbanqContext() : base("MbanqContext")
        {
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}