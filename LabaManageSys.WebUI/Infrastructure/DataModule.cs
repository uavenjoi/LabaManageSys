using Ninject.Modules;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Concrete;

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

