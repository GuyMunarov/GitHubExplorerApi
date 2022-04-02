using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExplorerApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<List<string>>();
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<GitHubRepository> GitHubRepositories { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<License> Licenses { get; set; }


    }
}
