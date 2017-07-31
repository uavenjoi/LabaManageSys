using System.Data.Entity;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.Domain.Abstract;

namespace LabaManageSys.Domain.Concrete
{
    public class EFDbContext : DbContext, IEFDbContext 
    {
       //static EFDbContext()
       // {
       //     Database.SetInitializer<EFDbContext>(new EFDbInit());
       // }

       public EFDbContext() : base("name=EFDbContext")
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        //public class EFDbInit : NullDatabaseInitializer<EFDbContext>
        //{
        //}
    }
}