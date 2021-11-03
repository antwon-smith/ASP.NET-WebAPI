using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChoresWebAPI.Models;

namespace ChoresWebAPI.Models
{
    public class ChoresContext : DbContext
    {
        public ChoresContext(DbContextOptions<ChoresContext> options)
            : base(options)
        {
        }

        public DbSet<TodoChore> TodoChores { get; set; }

        public DbSet<ChoresWebAPI.Models.TodoAssignment> TodoAssignment { get; set; }
    }
}
