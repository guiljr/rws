using Microsoft.EntityFrameworkCore;
using Models = Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Engine.Contracts.Entities;

namespace Engine.Data
{
    public class SCIContext : DbContext
    {
        public class Result
        {
            public int Id { get; set; }
        }
        public SCIContext(DbContextOptions<SCIContext> options) : base(options)
        {          
        }
        public virtual DbSet<tblExchangeRate> tblExchangeRates { get; set; }
      

        protected override void OnModelCreating(ModelBuilder builder)
        {          
            builder.Entity<tblExchangeRate>().ToTable("tblExchangeRate");

    }

      
    }
}