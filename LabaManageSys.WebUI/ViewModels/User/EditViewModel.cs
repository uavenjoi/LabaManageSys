using System.Collections.Generic;
using LabaManageSys.WebUI.Models;
using System.Web.Mvc;

namespace LabaManageSys.WebUI.ViewModels.User
{
    public class EditViewModel
    {
        public UserModel User { get; set; }

        public IEnumerable<RoleModel> Roles { get; set; }

    }

}