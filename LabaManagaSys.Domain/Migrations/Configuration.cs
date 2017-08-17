namespace LabaManagaSys.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LabaManageSys.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LabaManageSys.Domain.Concrete.EFDbContext context)
        {
            base.Seed(context);
        }
    }
}
