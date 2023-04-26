﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions{ get; set; }
        public DbSet<Walk> Walks { get; set; }
    }

}

 