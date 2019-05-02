using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApp_OpenIDConnect_DotNet.Models;

namespace WebApp_OpenIDConnect_DotNet.Context
{
    public class dbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}