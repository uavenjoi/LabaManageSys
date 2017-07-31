using System.Collections.Generic;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        private IEFDbContext context;

        public EFRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<AppUser> AppUsers
        {
            get { return this.context.AppUsers; }
        }

        public IEnumerable<Role> Roles
        {
            get { return this.context.Roles; }
        }

        public AppUser UserDelete(int id)
        {
            AppUser entryDb = this.context.AppUsers.Find(id);
            if (entryDb != null)
            {
                this.context.AppUsers.Remove(entryDb);
                (this.context as EFDbContext).SaveChanges();
            }

            return entryDb;
        }

        public void UserUpdate(AppUser user)
        {
            if (user.UserId == 0)
            {
                this.context.AppUsers.Add(user);
            }
            else
            {
                AppUser entryDb = this.context.AppUsers.Find(user.UserId);
                if (entryDb != null)
                {
                    entryDb.Name = user.Name;
                    entryDb.Email = user.Email;
                    entryDb.RoleId = user.RoleId;
                    entryDb.Password = entryDb.Password;
                }
            }

            (this.context as EFDbContext).SaveChanges();
        }

        public Role RoleDelete(int id)
        {
            Role entryDb = this.context.Roles.Find(id);
            if (entryDb != null)
            {
                this.context.Roles.Remove(entryDb);
                (this.context as EFDbContext).SaveChanges();
            }

            return entryDb;
        }

        public void RoleUpdate(Role role)
        {
            if (role.RoleId == 0)
            {
                this.context.Roles.Add(role);
            }
            else
            {
                Role entryDb = this.context.Roles.Find(role.RoleId);
                if (entryDb != null)
                {
                    entryDb.Name = role.Name;
                }
            }

            (this.context as EFDbContext).SaveChanges();
        }
    }
}