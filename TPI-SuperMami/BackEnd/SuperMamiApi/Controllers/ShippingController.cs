using System.Reflection;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.ShippingCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using LinqToDB;


namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class ShippingController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();

        private readonly ILogger<ShippingController> _logger;

        public ShippingController(ILogger<ShippingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Shipping/GetAllShippings")]
        public ActionResult<ResultAPI> GetAllShippings()
        {
            var resultado = new ResultAPI();
            var s = db.Shippings.ToList();
            if (s != null)
            {
                resultado.Ok = true;
                resultado.Return = s;
                resultado.AdditionalInfo = "Se cargó la lista correctamente";
                return resultado;
            }
            else
            {
                resultado.Ok = false;
                resultado.Error = "Error al cargar los envíos";
                resultado.ErrorCode = 400;
                return resultado;
            }
        }


        [HttpPost]
        [Route("Shipping/GetShippingById")]
        public ActionResult<ResultAPI> GetShippingById([FromBody] CommandFindShipping shipping)
        {
            var resultado = new ResultAPI();
            try
            {

                var s = db.Shippings.ToList().Where(c => c.IdShipping == shipping.IdShipping).FirstOrDefault();
                if (s != null)
                {
                    resultado.Ok = true;
                    resultado.Return = s;
                    resultado.AdditionalInfo = "Se muestra el envío correctamente";
                    return resultado;
                }
                else
                {
                    resultado.Ok = false;
                    resultado.Error = "Envío no encontrado";
                    resultado.ErrorCode = 400;
                    return resultado;
                }

            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al cargar Envíos" + ex.Message;
                resultado.ErrorCode = 400;
                return resultado;
            }
        }

        [HttpPost]
        [Route("Shipping/RegisterShipping")]
        public ActionResult<ResultAPI> RegisterShipping([FromBody] CommandRegisterShipping command)
        {
            ResultAPI result = new ResultAPI();

            try
            {
                if (command.IdShippingCompany <= 0)
                {
                    result.Ok = false;
                    result.Error = "Esa empresa de envíos no existe";
                    return result;
                }
                if (command.IdDeliveryOrder <= 0)
                {
                    result.Ok = false;
                    result.Error = "Esa orden de entrega no existe";
                    return result;
                }
                if (command.IdUser <= 0)
                {
                    result.Ok = false;
                    result.Error = "Ese usuario no existe";
                    return result;
                }
                // DETAIL
                if (command.Weight <= 0)
                {
                    result.Ok = false;
                    result.Error = "El peso no puede estar vacio";
                    return result;
                }

                Shipping s = new Shipping();
                s.IdShippingCompany = command.IdShippingCompany;
                s.IdDeliveryOrder = command.IdDeliveryOrder;
                s.IdUser = command.IdUser;
                s.IdState = 1;
                s.IsActive = true;

                db.Shippings.Add(s);
                db.SaveChanges();

                ShippingDetail sp = new ShippingDetail();
                sp.IdShipping = s.IdShipping;
                sp.Weight = command.Weight;
                sp.Comment = command.Comment;

                db.ShippingDetails.Add(sp);
                db.SaveChanges();

                result.Ok = true;
                result.Return = db.Shippings.ToList();
                result.AdditionalInfo = "Se cargó la lista correctamente";
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al cargar el Envío. Error: " + ex.ToString();
            }
            return result;
        }

        // [HttpPut]
        // [Route("Shipping/UpdateShipping")]
        // public ActionResult<ResultAPI> UpdateShipping([FromBody] CommandUpdateShipping command)
        // {
        //     ResultAPI result = new ResultAPI();
        //     try
        //     {
        //         if (command.IdShipping <= 0)
        //         {
        //             result.Ok = false;
        //             result.Error = "Esa empresa de envíos no existe";
        //             return result;
        //         }
        //         if (command.IdState <= 0)
        //         {
        //             result.Ok = false;
        //             result.Error = "Ese estado de envíos no existe";
        //             return result;
        //         }

        //         var shipp = db.Shippings.Where(c => c.IdDeliveryOrder == command.IdDeliveryOrder).FirstOrDefault();
        //         if (shipp != null)
        //         {
        //             shipp.IdShippingCompany = command.IdShippingCompany;
        //             shipp.IdState = command.IdState;

        //             db.Shippings.Update(shipp);
        //             db.SaveChanges();
        //             result.Ok = true;
        //             result.Return = db.Shippings.ToList();
        //             return result;
        //         }
        //         else
        //         {
        //             result.Ok = false;
        //             result.ErrorCode = 200;
        //             result.Error = "Envío no encontrado, revise el Nro de Orden";
        //             return result;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         result.Ok = false;
        //         result.Error = "Algo salió mal al actualizar el Envío. Error: " + ex.ToString();
        //         return result;
        //     }
        // }

        [HttpPost]
        [Route("Shipping/DeleteShipping")]
        public ActionResult<ResultAPI> DeleteShipping([FromBody] CommandDeleteShipping command)
        {
            ResultAPI result = new ResultAPI();
            Shipping s = new Shipping();

            try
            {
                var shipp = db.Shippings.Where(c => c.IdShipping == command.IdShipping).FirstOrDefault();
                if (shipp != null)
                {
                    if (shipp.IsActive == true)
                    {
                        shipp.IsActive = false;
                    }
                    else
                    {
                        shipp.IsActive = true;
                    }

                    db.Shippings.Update(shipp);
                    db.SaveChanges();
                    result.Ok = true;
                    result.Return = db.Shippings.ToList();
                    return result;
                }
                else
                {
                    result.Ok = false;
                    result.ErrorCode = 200;
                    result.Error = "Envío no encontrado, revise el Nro de Orden";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al eliminar el Envío. Error: " + ex.ToString();
                return result;
            }

        }


        [HttpPut]
        [Route("Shipping/UpdateShippingState")]
        public ActionResult<ResultAPI> UpdateShippingState([FromBody] CommandUpdateShipping command)
        {
            ResultAPI result = new ResultAPI();

            var shipp = db.Shippings.Where(c => c.IdShipping == command.IdShipping).FirstOrDefault();
            if (shipp != null)
            {
                if (shipp.IsActive == true)
                {
                    shipp.IsActive = false;
                }


                if (shipp.IdState == 1)
                {
                    shipp.IdState = 4;
                }
                else
                if (shipp.IdState == 4)
                {
                    shipp.IdState = 5;
                }

                db.Shippings.Update(shipp);
                db.SaveChanges();
                result.Ok = true;
                result.Return = shipp;
                return result;
            }
            else
            {
                result.Ok = false;
                result.ErrorCode = 200;
                result.Error = "Retiro no encontrado, revise el Documento";
                return result;
            }


        }

        //LISTADO
        [HttpGet]
        [Route("Shipping/GetListJoin")]
        public ActionResult<ResultAPI> GetListJoin()
        {

            var query = from s in db.Shippings
                        join sd in db.ShippingDetails on s.IdShipping equals sd.IdShipping
                        join sc in db.ShippingCompanies on s.IdShippingCompany equals sc.IdShippingCompany
                        join doo in db.DeliveryOrders on s.IdDeliveryOrder equals doo.IdDeliveryOrder 
                        join st in db.States on s.IdState equals st.IdState
                        where s.IsActive == true
                        group s by new { s.IdShipping, s.IdDeliveryOrder, sc.BusinessName, doo.DeliveryDate, sd.Comment, sd.Weight, st.State1 } into g
                        select new { IdShipping = g.Key, IdDeliveryOrder = g.Key, BusinessName = g.Key, DeliveryDate = g.Key, Comment = g.Key, Weight = g.Key, State = g.Key };

            var result = new ResultAPI();
            try
            {
                if (query != null)
                {
                    result.Ok = true;
                    result.Return = query;
                    result.AdditionalInfo = "Se cargó la lista correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al mostrar la cantidad. Error: " + ex.ToString();
                return result;
            }
            return result;
        }
        //CONTADOR
        //OBTENER CANTIDAD ENVIOS POR EMPRESA EN EL DIA
        [HttpPost]
        [Route("Shipping/GetShippingsByCompanyCountToday")]
        public ActionResult<ResultAPI> GetShippingsByCompanyCountToday([FromBody] int id)
        {
            var query = (from s in db.Shippings
                         join doo in db.DeliveryOrders on s.IdDeliveryOrder equals doo.IdDeliveryOrder
                         where s.IdShippingCompany == id && doo.DeliveryDate.Month == DateTime.Now.Month
                            && doo.DeliveryDate.Day == DateTime.Now.Day && doo.DeliveryDate.Year == DateTime.Now.Year
                         select s).Count();

            var result = new ResultAPI();
            try
            {
                if (query != null)
                {
                    result.Ok = true;
                    result.Return = query;
                    result.AdditionalInfo = "Se cargó la lista correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al mostrar la cantidad. Error: " + ex.ToString();
                return result;
            }
            return result;
        }




        // //REPORTES
        // [HttpGet]
        // [Route("Shipping/GetCountShippingType")]
        // public ActionResult<ResultAPI> GetCountShippingType()
        // {

        //     var query = from s in db.Shippings
        //                 join sc in db.ShippingCompanies on s.IdShippingCompany equals sc.IdShippingCompany
        //                 join st in db.ShippingTypes on sc.IdShippingType equals st.IdShippingType
        //                 group st by st.Description into g
        //                 select new { tipo_de_envio = g.Key, Total = g.Count() };

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

        // [HttpPost]
        // [Route("Shipping/GetCountShippingsByDate")]
        // public ActionResult<ResultAPI> GetCountShippingsByDate([FromBody] int month)
        // {
        //     ResultAPI result = new ResultAPI();

        //     var query = (from s in db.Shippings
        //                  join d in db.DeliveryOrders on s.IdDeliveryOrder equals d.IdDeliveryOrder
        //                  where d.DeliveryDate.Month == month
        //                  select s).Count();
        //     try
        //     {
        //         result.Ok = true;
        //         result.Return = query;
        //         result.AdditionalInfo = "Se muestra la cantidad de envios por fecha correctamente";
        //         result.ErrorCode = 200;
        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         result.Ok = false;
        //         result.Error = "Algo salió mal al mostrar la cantidad. Error: " + ex.ToString();
        //         return result;
        //     }
        // }

        // [HttpPost]
        // [Route("Shipping/GetPriceRangeByMonth")]
        // public ActionResult<ResultAPI> GetPriceRangeByMonth([FromBody] int year)
        // {

        //     var query = from doo in db.DeliveryOrders
        //                 where doo.DeliveryDate.Year == year && doo.ShippingPrice != null
        //                 group doo by doo.DeliveryDate into g
        //                 select new
        //                 {
        //                     Mes_de_facturación = g.Key.Month,
        //                     Facturación_máxima = g.Max(z => z.ShippingPrice),
        //                     Facturación_mínima = g.Min(z => z.ShippingPrice)
        //                 };

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