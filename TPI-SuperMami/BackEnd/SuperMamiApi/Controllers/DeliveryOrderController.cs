using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.DeliveryOrderCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;


namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<DeliveryOrderController> _logger;

        public DeliveryOrderController(ILogger<DeliveryOrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("DeliveryOrder/GetAllDeliveryOrder")]
        public ActionResult<ResultAPI> GetTipoDocumento()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.DeliveryOrders.ToList();
                resultado.AdditionalInfo = "Se cargó la lista correctamente";
                resultado.ErrorCode = 200;
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al cargar ordenes de entrega" + ex.Message;
                resultado.ErrorCode = 400;
                return resultado;
            }
        }

        [HttpPost]
        [Route("DeliveryOrder/GetDeliveryOrderById")]
        public ActionResult<ResultAPI> GetDeliveryOrderById([FromBody] CommandFindDeliveryOrder command)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.DeliveryOrders.ToList().FirstOrDefault(c=>c.IdDeliveryOrder == command.IdDeliveryOrder);
                resultado.AdditionalInfo = "Se cargó la lista correctamente";
                resultado.ErrorCode = 200;
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al cargar la orden de entrega" + ex.Message;
                resultado.ErrorCode = 400;
                return resultado;
            }
        }

        [HttpPost]
        [Route("DeliveryOrder/UpdateDeliveryOrder")]
        public ActionResult<ResultAPI> UpdateDeliveryOrder([FromBody] CommandUpdateDeliveryOrder command)
        {
            ResultAPI result = new ResultAPI();          

            // if (s.IdShippingCompany <= 0)
            // {
            //     result.Ok = false;
            //     result.Error = "Esa empresa de envío no existe";
            //     return result;
            // }

            var delOrd = db.DeliveryOrders.Where(c => c.IdDeliveryOrder == command.IdDeliveryOrder && c.IsShipping == false).FirstOrDefault();
            if (delOrd != null)
            {

                delOrd.IdBranch = command.IdBranch;                


                db.DeliveryOrders.Update(delOrd);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.DeliveryOrders.ToList();
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
        
        // [HttpPost]
        // [Route("DeliveryOrder/GetTotalShippingsAndPickups")]
        // public ActionResult<ResultAPI> GetTotalShippingsAndPickups([FromBody] int p_anio)
        // {
        //     ResultAPI result = new ResultAPI();

        //     var resultado = new ResultAPI();
        //     var query = from doo in db.DeliveryOrders
        //                 where doo.DeliveryDate.Year == p_anio
        //                 group doo.DeliveryDate.Year by doo.DeliveryDate.Year into g
        //                 select new
        //                 {
        //                     Año = g.Key,
        //                     Cantidad_envíos = (from s1 in db.Shippings
        //                                        join d1 in db.DeliveryOrders on
        //                                        s1.IdDeliveryOrder equals d1.IdDeliveryOrder
        //                                        where s1.IsActive == true && d1.DeliveryDate.Year == p_anio
        //                                        select s1.IdShipping).Count(),
        //                     Cantidad_retiros =
        //                                         (from p1 in db.Pickups
        //                                          join d2 in db.DeliveryOrders on
        //                                          p1.IdDeliveryOrder equals d2.IdDeliveryOrder
        //                                          where p1.IsActive == true && d2.DeliveryDate.Year == p_anio
        //                                          select p1.IdPickup).Count()
        //                 };

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

    }
}