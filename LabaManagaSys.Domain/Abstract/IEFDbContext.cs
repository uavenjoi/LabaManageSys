using LabaManageSys.Domain.EntitiesModel;
using System.Data.Entity;

namespace LabaManageSys.Domain.Abstract
{
    public interface IEFDbContext
    {
         DbSet<AppUser> AppUsers { get; set; }

         DbSet<Role> Roles { get; set; }
    }
}
