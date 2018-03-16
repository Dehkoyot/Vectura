using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VecturaServer.Models;

namespace VecturaServer.Enteties
{
    public class VisitsContext : DbContext
    {
        public DbSet<VisitModel> Visits { get; set; }

        public VisitsContext() : base("VisitConnection") { }

        public VisitsContext(string connectionString) : base(connectionString) { }

    }
}