using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.Domain.Abstract;

namespace LabaManageSys.Domain.Concrete
{
    public class EFDbContext : DbContext, IEFDbContext 
    {
       public EFDbContext() : base("name=EFDbContext")
        {
        }

        public IDbSet<AppUser> AppUsers { get; set; }

        public IDbSet<Role> Roles { get; set; }

        

        //public static EFDbContext Create()
        //{
        //    return new EFDbContext();
        //}

    }
}