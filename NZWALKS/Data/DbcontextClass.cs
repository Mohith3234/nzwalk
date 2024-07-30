using Microsoft.EntityFrameworkCore;
using NZWALKS.Model.Domine;

namespace NZWALKS.Data
{
    public class DbcontextClass : DbContext
    {
        public DbcontextClass(DbContextOptions<DbcontextClass> options) : base(options)
        {

        }

        public DbSet<Deficulty> deficulties { get; set; }

        public DbSet<Region> regions { get; set; }

        public DbSet<Walk> walks { get; set; }

        //seeding data to difficultys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var Defficulty = new List<Deficulty>()
            {
                new Deficulty()
                {
                    Id = Guid.Parse("5999cf31-ad5b-4ab9-aa9a-5a6b5d38fea4"),
                    Name = "Easy"
                },
                new Deficulty()
                {
                    Id=Guid.Parse("00d4569d-a7f6-4aa2-913d-2ecdfb0de937"),
                    Name="Medium"
                },
                new Deficulty()
                {
                    Id=Guid.Parse("07a0fd26-16c3-417c-9fc1-6686b7d32955"),
                    Name="Hard"
                }
            };
            //seed difficultys to data base
            modelBuilder.Entity<Deficulty>().HasData(Defficulty);

            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
