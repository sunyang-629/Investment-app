using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Investment> Investments { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

    }
}
