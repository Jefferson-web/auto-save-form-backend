using AutoSaveForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSaveForm.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
