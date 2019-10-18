using CoreDemo.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class SqlServerDbcontext : IdentityDbContext
    {
        public SqlServerDbcontext(DbContextOptions<SqlServerDbcontext> options)
            : base(options)
        {

        }

        public DbSet<avc> Asdsa { get; set; }

    }
}
