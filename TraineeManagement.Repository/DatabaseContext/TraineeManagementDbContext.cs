using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.Models.EntityModels;

namespace TraineeManagement.Repository.DatabaseContext
{
    public class TraineeManagementDbContext : DbContext
    {        
        public TraineeManagementDbContext() : base("ServerDB")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static TraineeManagementDbContext Create()
        {
            return new TraineeManagementDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TraineeManagementDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        // Tables

        public DbSet<Advance> Advances { get; set; }

        public DbSet<AdvanceDetails> AdvanceDetails { get; set; }

        public DbSet<Billed> Billed { get; set; }

        public DbSet<BilledDetails> BilledDetails { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<SubDistrict> SubDistricts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Adjust> Adjusts { get; set; }
    }
}
