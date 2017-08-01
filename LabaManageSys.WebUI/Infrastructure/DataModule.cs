﻿using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Concrete;
using Ninject.Modules;

namespace LabaManageSys.WebUI
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<EFRepository>();
        }
    }
}
