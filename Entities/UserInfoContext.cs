using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Manager.Entities
{

    public static class ModelBuilderExtensions
{
    //public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    //{
    //    foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
    //    {
    //        entity.Relational().TableName = entity.DisplayName();
    //    }
    //}
}

    //public class UserInfoContext : IdentityDbContext<IdentityUser>

    public class UserInfoContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Credential> Credentials { get; set; }

        public DbSet<LearnerDetail> LearnerDetails { get; set; }

        public DbSet<EducatorDetail> EducatorDetails { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }   

        public DbSet<Classroom> Classroom { get; set; }

        public DbSet<StudentInvitation> StudentInvitation { get; set; }

        public DbSet<Assignment> Assignment { get; set; }


        public UserInfoContext(DbContextOptions<UserInfoContext> options)
            : base(options)
        {
            Database.Migrate();

        }

    }
}
