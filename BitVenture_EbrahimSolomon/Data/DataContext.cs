using BitVenture_EbrahimSolomon.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BitVenture_EbrahimSolomon.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<MasterRecord> MasterRecords { get; set; }
        public DbSet<DetailRecord> DetailRecords { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
