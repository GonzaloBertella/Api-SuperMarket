using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.UserCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class UserController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("User/GetUserById")]
        public ActionResult<ResultAPI> GetUserById([FromBody] CommandFindUser user)
        {
            var result = new ResultAPI();
            try
            {

                var u = db.Users.ToList().Where(c => c.IdUser == user.IdUser).FirstOrDefault();
                if (u != null)
                {
                    result.Ok = true;
                    result.Return = u;
                    result.AdditionalInfo = "Se muestra el usuario correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
                else
                {
                    result.Ok = false;
                    result.Error = "Usuario no encontrado";
                    result.ErrorCode = 400;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar Usuarios" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpGet]
        [Route("User/GetAllUsers")]
        public ActionResult<ResultAPI> GetAllUsers()
        {
            var result = new ResultAPI();
            try
            {
                result.Ok = true;
                result.Return = db.Users.ToList();
                result.AdditionalInfo = "Se cargó la lista correctamente";
                result.ErrorCode = 200;
                return result;
            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar los usuarios" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpPost]
        [Route("User/GetUserByEmailPass")]
        public ActionResult<ResultAPI> GetUserByEmailPass(CommandValidateUser user)
        {
            var result = new ResultAPI();            
            try
            {
                 var u = db.Users.ToList().Where(c => c.Email == user.Email && c.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    result.Ok = true;
                    result.Return = u;
                    result.AdditionalInfo = "Se muestra el usuario correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
                else
                {
                    result.Ok = false;
                    result.Error = "Usuario no encontrado";
                    result.ErrorCode = 400;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar los usuarios" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpPost]
        [Route("User/RegisterUser")]
        public ActionResult<ResultAPI> RegisterUser([FromBody] CommandRegisterUser command)
        {
            ResultAPI result = new ResultAPI();
            User u = new User();

            u.IdDocumentType = command.IdDocumentType;
            u.DocumentNumber = command.DocumentNumber;
            u.Name = command.Name;
            u.Surname = command.Surname;
            u.Email = command.Email;
            u.Phone = command.Phone;
            u.IdRol = command.IdRol;
            u.Password = command.Password;


            try
            {
                if (u.IdDocumentType <= 0)
                {
                    result.Ok = false;
                    result.Error = "Ese tipo de documento no existe";
                    return result;
                }
                if (u.DocumentNumber == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese el nro de documento del usuario porfavor";
                    return result;
                }
                if (u.Name == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese el nombre del usuario porfavor";
                    return result;
                }
                if (u.Surname == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese el apellido del usuario porfavor";
                    return result;
                }
                if (u.Email == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese el email del usuario porfavor";
                    return result;
                }
                if (u.Phone == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese un número válido para el usuario porfavor";
                    return result;
                }
                if (u.IdRol <= 0)
                {
                    result.Ok = false;
                    result.Error = "Ese rol no existe";
                    return result;
                }
                if (u.Password == "")
                {
                    result.Ok = false;
                    result.Error = "Ingrese una contraseña válida";
                    return result;
                }

                db.Users.Add(u);
                db.SaveChanges();
                result.Ok = true;
                var usuarios = db.Users.ToList();

                result.Return = usuarios;
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al cargar el Usuario. Error: " + ex.ToString();
            }
            return result;
        }

        [HttpPut]
        [Route("User/UpdateUser")]
        public ActionResult<ResultAPI> UpdateUser([FromBody] CommandUpdateUser command)
        {
            ResultAPI result = new ResultAPI();
            if (command.Name == "")
            {
                result.Ok = false;
                result.Error = "Ingrese el nombre del usuario porfavor";
                return result;
            }
            if (command.Surname == "")
            {
                result.Ok = false;
                result.Error = "Ingrese el apellido del usuario porfavor";
                return result;
            }
            if (command.Email == "")
            {
                result.Ok = false;
                result.Error = "Ingrese el email del usuario porfavor";
                return result;
            }
            if (command.Phone == "")
            {
                result.Ok = false;
                result.Error = "Ingrese un número válido para el usuario porfavor";
                return result;
            }
            if (command.Password == "")
            {
                result.Ok = false;
                result.Error = "Ingrese una contraseña válida";
                return result;
            }
            var usu = db.Users.Where(c => c.DocumentNumber == command.DocumentNumber).FirstOrDefault();
            if (usu != null)
            {
                usu.Name = command.Name;
                usu.Surname = command.Surname;
                usu.Email = command.Email;
                usu.Phone = command.Phone;
                usu.Password = command.Password;
                db.Users.Update(usu);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.Users.ToList();
                return result;
            }
            else
            {
                result.Ok = false;
                result.ErrorCode = 200;
                result.Error = "Usuario no encontrado, revise el Documento";
                return result;
            }
        }
    }
}

