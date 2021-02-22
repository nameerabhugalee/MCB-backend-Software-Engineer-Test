using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Model
{
    public class CustomerDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public virtual DbSet<ErrorLogger> logErrors { get; set; }
        public DbSet<DocumentData> DocumentDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER02;Database=CustomerDB;Trusted_Connection=True;");
        }
    }
}

