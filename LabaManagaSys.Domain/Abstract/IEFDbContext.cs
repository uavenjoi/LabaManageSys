using System.Data.Entity;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.Domain.Abstract
{
    public interface IEFDbContext
    {
         IDbSet<AppUser> AppUsers { get; set; }

         IDbSet<Role> Roles { get; set; }
    }
}
