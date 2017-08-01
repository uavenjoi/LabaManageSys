using System.Data.Entity;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.Domain.Concrete
{
    public class EFDbContext : DbContext, IEFDbContext 
    {
       public EFDbContext() : base("name=EFDbContext")
        {
        }

        public IDbSet<AppUser> AppUsers { get; set; }

        public IDbSet<Role> Roles { get; set; }
    }
}