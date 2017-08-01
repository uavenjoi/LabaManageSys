using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Controllers;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.User;


namespace LabaManageSys.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.UserModels).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1"},
                new UserModel { UserId = 2, Name = "User2"},
                new UserModel { UserId = 3, Name = "User3"},
                new UserModel { UserId = 4, Name = "User4"},
                new UserModel { UserId = 5, Name = "User5"},
                new UserModel { UserId = 6, Name = "User6"},
                new UserModel { UserId = 7, Name = "User7"},
                new UserModel { UserId = 8, Name = "User8"},
                new UserModel { UserId = 9, Name = "User9"},
            });
           UserController controller = new UserController(mock.Object);

            // Действие (act)
            ListViewModel result = (ListViewModel)controller.List(page: 2).Model;

            // Утверждение (assert)
            List<UserModel> users = result.Users.ToList();
            Assert.IsTrue(users.Count == 4);
            Assert.AreEqual(users[0].Name, "User6");
            Assert.AreEqual(users[1].Name, "User7");
            Assert.AreEqual(users[2].Name, "User8");
            Assert.AreEqual(users[3].Name, "User9");
        }
    }
}
