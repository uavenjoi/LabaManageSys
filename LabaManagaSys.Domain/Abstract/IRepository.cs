using System.Collections.Generic;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.Domain.Abstract
{
    public interface IRepository
    {
        IEnumerable<AppUser> AppUsers { get; }

        IEnumerable<Role> Roles { get; }

        AppUser UserDelete(int id);

        void UserUpdate(AppUser user);

        Role RoleDelete(int id);

        void RoleUpdate(Role role);
    }
}