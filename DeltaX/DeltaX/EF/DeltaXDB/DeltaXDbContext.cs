using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DeltaX.EF.DeltaXDB
{
    public class DeltaXDbContext : DbContext
    {
        public DeltaXDbContext()
            : base("name=DeltaXIMDBEntities2")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        #region old DbSet
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<Actor_Movie> Actor_Movie { get; set; }
        public DbSet<Movie> Movie { get; set; }
        #endregion

        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder">Db Model Builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}