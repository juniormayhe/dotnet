using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public class OrnithologyDbContext : DbContext
    {
        public OrnithologyDbContext()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ave>();
            modelBuilder.Entity<Pais>();
            modelBuilder.Entity<Zona>();
            modelBuilder.Entity<AvePais>();
        }
    }
}
