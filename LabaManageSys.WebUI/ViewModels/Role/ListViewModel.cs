using LabaManageSys.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.ViewModels.Role
{
    public class ListViewModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }

    }

}