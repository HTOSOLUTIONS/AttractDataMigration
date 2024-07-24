using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataMigration.Models;

namespace DataMigration.Data
{
    public class DataMigrationDbContext : DbContext
    {
        public DataMigrationDbContext (DbContextOptions<DataMigrationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataMigration.Models.MigrationHistory> MigrationHistory { get; set; } = default!;
    }
}
