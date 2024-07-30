using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity;
namespace NZWALKS.Data
{
    public class DbContextAutoClass:IdentityDbContext
    {
        public DbContextAutoClass(DbContextOptions<DbContextAutoClass> options):base(options)
        {
           
        }
        //seeding data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "42a7e2fc-2c43-4875-ad85-1e4ee259d5fb";
            var writerRoleId = "c39bb7b8-3ea2-4a18-b1d8-a47dde2d0bf6";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp =writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
        }
    }
}
