﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions <NZWalksDbContext>dbContextOptions) : base(dbContextOptions)
        {

        }
        
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions{ get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image>Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for defficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            { 
                new Difficulty()
                {
                    Id =Guid.Parse("76cc25cc-6b80-4f81-9926-6437aa0334e1") ,
                    Name="Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("98179c79-5a8f-4cdb-ace1-19cb5bf3ba82"),
                    Name="Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("c01d63d2-0882-4710-b0c9-882161c94596"),
                    Name="Hard"
                }
            };
            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            //Seed Data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id= Guid.Parse("de6c0596-656a-4c3a-9b0a-cc587170ff07"),
                    Name ="Auckland",
                    Code = "AKL",
                    RegionImageUrl = "Aukland-image.png"
                    },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }

}



 