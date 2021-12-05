using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{ 
    public class ApplicationUser : IdentityUser
    {

        public DateTime Created_at { get; set; }

        //public List<Employee> Empolyees { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DBsets
        public DbSet<Employee> Empolyee { get; set; }

        public ApplicationDbContext() : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=API_EmployeeEntity;Integrated Security=True")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");

            modelBuilder.Entity<ApplicationUser>();
                //.HasIndex(u => u.Email)
                //.IsUnique();

        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>
    {

        public ApplicationUserStore() : base(new ApplicationDbContext())
        {

        }

        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager() : base(new ApplicationUserStore())
        {
        }

        public ApplicationUserManager(DbContext db) : base(new ApplicationUserStore(db))
        {
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager() : base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {
        }

        public ApplicationRoleManager(DbContext db) : base(new RoleStore<IdentityRole>(db))
        {
        }
    }
}
