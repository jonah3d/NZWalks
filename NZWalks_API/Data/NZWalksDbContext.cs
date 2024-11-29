using Microsoft.EntityFrameworkCore;
using NZWalks_API.Models.Domain;



namespace NZWalks_API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("13481ca6-5adf-4d76-aa9d-3182bded63e8"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f0c54f92-fd53-4df1-a329-063aaf089b9d"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id =Guid.Parse("5aab4896-c41a-4b6b-9ae9-9303effa8a82"),
                    Name = "Hard"
                }
            };
            //seed data to the database

            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //Seed data for regions
            var regions = new List<Region>() {

                new Region()
                {
                    Id = Guid.Parse("c154e40d-6aac-41c6-94e7-6d59ddea950f"),
                    Code = "AUK",
                    Name = "Auckland",
                    RegionImageurl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland/auckland-landscape-1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("b19f23e1-4d5a-4e68-aaf2-a64e1262dc7b"),
                    Code = "WKO",
                    Name = "Waikato",
                    RegionImageurl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato/waikato/waikato-landscape-1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("433d0c6f-d3d4-4d9f-b3e2-b017d5bc0e34"),
                    Code = "BOP",
                    Name = "Bay of Plenty",
                    RegionImageurl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/bay-of-plenty/bay-of-plenty/bay-of-plenty-landscape-1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("3a2c466d-4da2-41b4-944b-6b6e8d6d7c6d"),
                    Code = "GIS",
                    Name = "Gisborne",
                    RegionImageurl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/gisborne/gisborne/gisborne-landscape-1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("172beacb-f46c-48b0-ac07-492e2adf7559"),
                    Code = "HKB",
                    Name = "Hawke's Bay",
                    RegionImageurl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/hawkes-bay/hawkes-bay/hawkes-bay-landscape-1.jpg"
                }

            };

            modelBuilder.Entity<Region>().HasData(regions);


        }
    }

   
}
