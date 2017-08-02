using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Abstract
{
    public interface IRepository
    {
        IEnumerable<UserModel> UserModels { get; }

        IEnumerable<RoleModel> RoleModels { get; }

        UserModel UserDelete(int id);

        void UserUpdate(UserModel user);

        bool UserPasswordValidate(UserModel user, string password);

        void UserPasswordSet(UserModel user, string password);

        RoleModel RoleDelete(int id);

        void RoleUpdate(RoleModel role);
    }
}