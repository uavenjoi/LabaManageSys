using Ninject.Modules;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.Concrete;

namespace LabaManageSys.Domain
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEFDbContext>().To<EFDbContext>();
        }
    }
}
