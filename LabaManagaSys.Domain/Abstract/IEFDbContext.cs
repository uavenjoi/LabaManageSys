using LabaManageSys.Domain.EntitiesModel;
using System.Data.Entity;

namespace LabaManageSys.Domain.Abstract
{
    public interface IEFDbContext
    {
         IDbSet<AppUser> AppUsers { get; set; }

         IDbSet<Role> Roles { get; set; }
    }
}
