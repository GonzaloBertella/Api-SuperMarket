using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;
using System.Diagnostics.Tracing;
using System.Data.Common;
using System.ComponentModel.Design;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.ShippingCompanyCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;


namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class ShippingCompanyController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<ShippingCompanyController> _logger;

        public ShippingCompanyController(ILogger<ShippingCompanyController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("ShippingCompany/GetShippingCompanyById")]
        public ActionResult<ResultAPI> GetById([FromBody] CommandFindShippingCompany shippingCompany)
        {
            var result = new ResultAPI();
            try
            {

                var p = db.ShippingCompanies.ToList().Where(c => c.IdShippingCompany == shippingCompany.IdShippingCompany).FirstOrDefault();
                if (p != null)
                {
                    result.Ok = true;
                    result.Return = p;
                    result.AdditionalInfo = "Se muestra la empresa correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
                else
                {
                    result.Ok = false;
                    result.Error = "Empresa no encontrada";
                    result.ErrorCode = 400;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar las empresas" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpPost]
        [Route("ShippingCompany/RegisterShippingCompany")]
        public ActionResult<ResultAPI> RegisterShippingCompany([FromBody] CommandRegisterShippingCompany command)
        {
            ResultAPI result = new ResultAPI();
            ShippingCompany s = new ShippingCompany();

            s.BusinessName = command.BusinessName;
            s.Address = command.Address;
            s.Location = command.Location;
            s.Phone = command.Phone;
            s.Email = command.Email;
            s.Cuit = command.Cuit;
            s.ShiftStartTime = command.ShiftStartTime;
            s.ShiftEndTime = command.ShiftEndTime;
            s.IdShippingType = command.IdShippingType;
            s.IsActive = true;
            s.Salary = command.Salary;
            s.ContactName =command.ContactName;
            s.MaxShippingsPerDay = command.MaxShippingsPerDay;


            try
            {
                if (s.BusinessName == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el nombre de la empresa, por favor.";
                    return result;
                }
                if (s.Address == "")
                {
                    result.Ok = false;
                    result.Error = "Complete la dirección de la empresa, por favor.";
                    return result;
                }
                if (s.Location == "")
                {
                    result.Ok = false;
                    result.Error = "Complete la Localidad de la empresa, por favor.";
                    return result;
                }
                if (s.Phone == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el teléfono de la empresa, por favor.";
                    return result;
                }
                if (s.Email == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el email de la empresa, por favor.";
                    return result;
                }
                if (s.Cuit == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el CUIT de la empresa, por favor.";
                    return result;
                }
                if (s.ShiftStartTime == "")
                {
                    result.Ok = false;
                    result.Error = "Complete la hora de inicio del turno de la empresa, por favor.";
                    return result;
                }
                if (s.ShiftEndTime == "")
                {
                    result.Ok = false;
                    result.Error = "Complete la hora de finalización del turno de la empresa, por favor.";
                    return result;
                }


                if (s.IdShippingType <= 0)
                {
                    result.Ok = false;
                    result.Error = "Ese tipo de envío no existe";
                    return result;
                }

                 if (s.Salary <=0)
                {
                    result.Ok = false;
                    result.Error = " Ingrese el salario";
                    return result;
                }
                 if (s.ContactName == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el nombre del contacto, por favor.";
                    return result;
                }
                 if (s.MaxShippingsPerDay <=0)
                {
                    result.Ok = false;
                    result.Error = "Complete la hora de finalización del turno de la empresa, por favor.";
                    return result;
                }


                db.ShippingCompanies.Add(s);
                db.SaveChanges();
                result.Ok = true;
                var shippingCompany = db.ShippingCompanies.ToList();

                result.Return = shippingCompany;
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al cargar la empresa de envío. Error: " + ex.ToString();
            }
            return result;
        }

        [HttpPut]
        [Route("ShippingCompany/UpdateShippingCompany")]
        public ActionResult<ResultAPI> UpdateShippingCompany([FromBody] CommandUpdateShippingCompany command)
        {
            ResultAPI result = new ResultAPI();



            if (command.IdShippingCompany <= 0)
            {
                result.Ok = false;
                result.Error = "Esa empresa de envío no existe";
                return result;
            }
            if (command.BusinessName == "")
            {
                result.Ok = false;
                result.Error = "Complete el nombre de la empresa, por favor.";
                return result;
            }
            if (command.Address == "")
            {
                result.Ok = false;
                result.Error = "Complete la dirección de la empresa, por favor.";
                return result;
            }
            if (command.Location == "")
            {
                result.Ok = false;
                result.Error = "Complete la Localidad de la empresa, por favor.";
                return result;
            }
            if (command.Phone == "")
            {
                result.Ok = false;
                result.Error = "Complete el teléfono de la empresa, por favor.";
                return result;
            }
            if (command.Email == "")
            {
                result.Ok = false;
                result.Error = "Complete el email de la empresa, por favor.";
                return result;
            }
            if (command.Cuit == "")
            {
                result.Ok = false;
                result.Error = "Complete el CUIT de la empresa, por favor.";
                return result;
            }            
            if (command.IdShippingType <= 0)
            {
                result.Ok = false;
                result.Error = "Ese tipo de envío no existe";
                return result;
            }
            if (command.Salary <=0)
                {
                    result.Ok = false;
                    result.Error = " Ingrese el salario";
                    return result;
                }
            if (command.ContactName == "")
                {
                    result.Ok = false;
                    result.Error = "Complete el nombre del contacto, por favor.";
                    return result;
                }
                 if (command.MaxShippingsPerDay <=0)
                {
                    result.Ok = false;
                    result.Error = "Complete la hora de finalización del turno de la empresa, por favor.";
                    return result;
                }

            var ship = db.ShippingCompanies.Where(c => c.IdShippingCompany == command.IdShippingCompany).FirstOrDefault();
            if (ship != null)
            {

                ship.BusinessName = command.BusinessName;
                ship.Address = command.Address;
                ship.Location = command.Location;
                ship.Phone = command.Phone;
                ship.Email = command.Email;
                ship.Cuit = command.Cuit;
                ship.ShiftStartTime = "07:00";
                ship.ShiftEndTime = "18:00";
                ship.IdShippingType = command.IdShippingType;
                ship.Salary = command.Salary;
                ship.ContactName =command.ContactName;
                ship.MaxShippingsPerDay = command.MaxShippingsPerDay;

                db.ShippingCompanies.Update(ship);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.ShippingCompanies.ToList();
                return result;
            }
            else
            {
                result.Ok = false;
                result.ErrorCode = 200;
                result.Error = "Empresa de envío no encontrada";
                return result;
            }
        }

        [HttpPut]
        [Route("ShippingCompany/DeleteShippingCompany")]
        public ActionResult<ResultAPI> DeleteCompany([FromBody] CommandFindShippingCompany command)
        {
            ResultAPI result = new ResultAPI();
            ShippingCompany s = new ShippingCompany();


            if (s.IdShippingCompany <= 0)
            {
                result.Ok = false;
                result.Error = "Esa empresa de envío no existe";
                return result;
            }


            var ship = db.ShippingCompanies.Where(c => c.IdShippingCompany == command.IdShippingCompany).FirstOrDefault();
            if (ship != null)
            {


                ship.IdShippingCompany = command.IdShippingCompany;
                ship.IsActive = false;


                db.ShippingCompanies.Update(ship);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.ShippingCompanies.ToList();
                return result;
            }
            else
            {
                result.Ok = false;
                result.ErrorCode = 200;
                result.Error = "Empresa no encontrada";
                return result;
            }
        }

        [HttpGet]
        [Route("ShippingCompany/GetAll")]
        public ActionResult<ResultAPI> GetAll()
        {
            var result = new ResultAPI();
            try
            {

                result.Ok = true;
                result.Return = db.ShippingCompanies.ToList();
                result.AdditionalInfo = "Se muestra la empresa de envío correctamente";
                result.ErrorCode = 200;
                return result;


            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar la empresa de envío" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        // [HttpGet]
        // [Route("ShippingCompany/GetPercentageShippingsByCompany")]
        // public ActionResult<ResultAPI> GetPercentageShippingsByCompany()
        // {
            

        //     var query = from s in db.Shippings
        //                 join sc in db.ShippingCompanies on s.IdShippingCompany equals sc.IdShippingCompany
        //                 group new { sc.BusinessName, s.IdShippingCompany } by sc.BusinessName into g
        //                 select new
        //                 {
        //                     nombre_empresa = g.Key,
        //                     porcentaje = Math.Round((Convert.ToDouble(g.Select(z => z.IdShippingCompany).Count()) * 100) /
        //                                                                                 (Math.Round(Convert.ToDouble((from s in db.Shippings
        //                                                                                                               select s.IdShippingCompany).Count()))), 2)
        //                 };

        //     // var query = from s in db.Shippings
        //     //             where s.IdShipping == id
        //     //             select s;

        //     var result = new ResultAPI();
        //     try
        //     {
        //         if (query != null)
        //         {
        //             result.Ok = true;
        //             result.Return = query;
        //             result.AdditionalInfo = "Se cargó la lista correctamente";
        //             result.ErrorCode = 200;
        //             return result;
        //         }
        //     }

        //     catch (Exception ex)
        //     {
        //         result.Ok = false;
        //         result.Error = "Algo salió mal al mostrar la cantidad. Error: " + ex.ToString();
        //         return result;
        //     }
        //     return result;
        // }
    }
}

