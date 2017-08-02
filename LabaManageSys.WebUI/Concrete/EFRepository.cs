using System;
using System.Collections.Generic;
using System.Linq;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.Concrete;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Concrete
{
    public class EFRepository : IRepository
    {
        private IEFDbContext context;

        public EFRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<UserModel> UserModels
        {
            get
            {
                return this.context.AppUsers.Select(_ => new UserModel { UserId = _.UserId, Name = _.Name, Email = _.Email, RoleId = _.RoleId });
            }
        }

        public IEnumerable<RoleModel> RoleModels
        {
            get
            {
                return this.context.Roles.Select(_ => new RoleModel { RoleId = _.RoleId, Name = _.Name });
            }
        }

        public UserModel UserDelete(int id)
        {
            AppUser entryDb = this.context.AppUsers.Find(id);
            if (entryDb != null)
            {
                this.context.AppUsers.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new UserModel(entryDb);
        }

        public void UserUpdate(UserModel user)
        {
            if (user.UserId == 0)
            {
                this.context.AppUsers.Add(new AppUser { Email = user.Email, Name = user.Name, RoleId = user.RoleId, Password = "1" });
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

            this.context.SaveChanges();
        }

        public bool UserPasswordValidate(UserModel user, string password)
        {
            AppUser entryDb = this.context.AppUsers.FirstOrDefault(_ => _.UserId == user.UserId && _.Password == password);
            return entryDb != null;
        }

        public void UserPasswordSet(UserModel user, string password)
        {
            AppUser entryDb = this.context.AppUsers.FirstOrDefault(_ => _.Name == user.Name);
            if (entryDb != null)
            {
                entryDb.Password = password;
            }

            this.context.SaveChanges();
        }

        public RoleModel RoleDelete(int id)
        {
            Role entryDb = this.context.Roles.Find(id);
            if (entryDb != null)
            {
                this.context.Roles.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new RoleModel(entryDb);
        }

        public void RoleUpdate(RoleModel role)
        {
            if (role.RoleId == 0)
            {
                this.context.Roles.Add(new Role { Name = role.Name });
            }
            else
            {
                Role entryDb = this.context.Roles.Find(role.RoleId);
                if (entryDb != null)
                {
                    entryDb.Name = role.Name;
                }
            }

            this.context.SaveChanges();
        }
    }
}