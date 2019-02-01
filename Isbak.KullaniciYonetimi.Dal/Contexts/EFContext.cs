using Isbak.KullaniciYonetimi.Entities.Roles;
using Isbak.KullaniciYonetimi.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbak.KullaniciYonetimi.Dal.Contexts
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public EFContext() : base ("EFContext")
        {
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Role)// user can have many roles
            //    .WithMany(r => r.User)// role contains multiple users in basic
            //    .Map(m =>
            //    {
            //        m.ToTable("UserRoles");
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("RoleId");
            //    });

            modelBuilder.Entity<User>()
           .HasRequired<Role>(s => s.Role)
           .WithMany(g => g.Users)
           .HasForeignKey<int>(s => s.RolId);
        }
    }
}
