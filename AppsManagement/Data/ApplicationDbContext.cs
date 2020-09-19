using System;
using System.Collections.Generic;
using System.Text;
using AppsManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppsManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppModel> Apps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
