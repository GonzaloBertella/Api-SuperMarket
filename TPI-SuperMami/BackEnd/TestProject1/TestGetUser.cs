using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMamiApi.Commands.UserCommands;
using SuperMamiApi.Controllers;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using System;

namespace TestProject1
{
    [TestClass]
    public class TestGetUser
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecuperarUsuario()
        {
            // UserController controller = new UserController();

            // CommandFindUser user = new CommandFindUser();

            // string a = "aa";

            // user.IdUser = 1;

            // controller.Get(user);
                       
        

        }
    }


    [TestClass]
    public class TestGetUser2
    {
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void RecuperarUsuario()
        {
            // UserController controller = new UserController();

            // CommandFindUser user = new CommandFindUser();

            // string a = "aa";

            // user.IdUser = a;

            // controller.Get(user);



        }
    }
}
