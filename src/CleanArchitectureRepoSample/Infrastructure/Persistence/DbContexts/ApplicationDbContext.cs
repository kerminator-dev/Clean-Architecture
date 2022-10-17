﻿using Application.Common.Interface;
using Domain.Common;
using Domain.Master;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly DateTime _currentDateTime;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _currentDateTime = DateTime.Now;
        }

        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Category> Categories { get; set; }

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Author = 1; // Затычка
                        entry.Entity.Created = _currentDateTime;
                        entry.Entity.Editor = 1; // Затычка
                        entry.Entity.Modified = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Editor = 1; // Затычка
                        entry.Entity.Modified = _currentDateTime;
                        break;

                }
            }

            return base.SaveChangesAsync();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
            (
                assembly: typeof(ApplicationDbContext).Assembly
            );
        }
    }
}
