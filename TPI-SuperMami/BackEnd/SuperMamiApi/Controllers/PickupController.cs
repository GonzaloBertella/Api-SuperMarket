using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Threading.Tasks.Dataflow;
using System.Security.Cryptography;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Net.Mail;
using System.Data;
using System.Data.Common;
using System.ComponentModel.Design.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.PickupCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using SuperMamiApi.Commands.DeliveryOrderCommands;
using SuperMamiApi.Controllers;


namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class PickupController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<PickupController> _logger;

        public PickupController(ILogger<PickupController> logger)
        {
            _logger = logger;
        }
        

        [HttpPost]
        [Route("Pickup/GetPickupById")]
        public ActionResult<ResultAPI> GetById([FromBody] CommandFindPickup pickup)
        {
            var result = new ResultAPI();
            try
            {

                var p = db.Pickups.ToList().Where(c => c.IdPickup == pickup.IdPickup).FirstOrDefault();
                if (p != null)
                {
                    result.Ok = true;
                    result.Return = p;
                    result.AdditionalInfo = "Se muestra el retiro correctamente";
                    result.ErrorCode = 200;
                    return result;
                }
                else
                {
                    result.Ok = false;
                    result.Error = "Retiro no encontrado";
                    result.ErrorCode = 400;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar retiros" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpPost]
        [Route("Pickup/RegisterPickup")]
        public ActionResult<ResultAPI> RegisterPickup([FromBody] CommandRegisterPickup command)
        {
            ResultAPI result = new ResultAPI();


            try
            {
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


                Pickup r = new Pickup();

                r.IdDeliveryOrder = command.IdDeliveryOrder;
                r.IdState = 1;
                r.IdUser = command.IdUser;
                r.IsActive = true;
               
                db.Pickups.Add(r);
                db.SaveChanges();

                PickupDetail pd = new PickupDetail();

                pd.IdPickup = r.IdPickup;
                pd.Volume = command.Volume;

                db.PickupDetails.Add(pd);
                db.SaveChanges();

                CommandUpdateDeliveryOrder command2 = new CommandUpdateDeliveryOrder();
                command2.IdDeliveryOrder = command.IdDeliveryOrder;
                command2.IdBranch = command.IdBranch;
                

                this.UpdateDeliveryOrder(command2);

                result.Ok = true;
                var pickup = db.Pickups.ToList();
                result.Return = pickup;
            }

            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Algo salió mal al cargar el retiro. Error: " + ex.ToString();
            }

            return result;
        }

        [HttpPut]
        [Route("Pickup/UpdatePickup")]
        public ActionResult<ResultAPI> UpdatePickup([FromBody] CommandUpdatePickup command)
        {
            ResultAPI result = new ResultAPI();



            if (command.IdPickup <= 0)
            {
                result.Ok = false;
                result.Error = "Ese retiro no existe";
                return result;
            }
            if (command.IdDeliveryOrder <= 0)
            {
                result.Ok = false;
                result.Error = "Esa orden de entrega no existe";
                return result;
            }

            if (command.IdState <= 0)
            {
                result.Ok = false;
                result.Error = "Ese usuario no existe";
                return result;
            }

            var pick = db.Pickups.Where(c => c.IdDeliveryOrder == command.IdDeliveryOrder).FirstOrDefault();
            if (pick != null)
            {

                pick.IdPickup = command.IdPickup;
                pick.IdDeliveryOrder = command.IdDeliveryOrder;
                pick.IdState = command.IdState;

                db.Pickups.Update(pick);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.Pickups.ToList();
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

        [HttpPost]
        [Route("Pickup/DeletePickup")]
        public ActionResult<ResultAPI> DeletePickup([FromBody] CommandDeletePickup command)
        {
            ResultAPI result = new ResultAPI();
            Pickup r = new Pickup();


            if (r.IdDeliveryOrder <= 0)
            {
                result.Ok = false;
                result.Error = "Ese retiro no existe";
                return result;
            }


            var pick = db.Pickups.Where(c => c.IdPickup == command.IdPickup).FirstOrDefault();
            if (pick != null)
            {
               
                    if (pick.IsActive == true)
                    {
                        pick.IsActive = false;
                    }                   

                db.Pickups.Update(pick);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.Pickups.ToList();
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

        [HttpGet]
        [Route("Pickup/GetAll")]
        public ActionResult<ResultAPI> GetAll()
        {
            var result = new ResultAPI();
            try
            {
                var s = db.Pickups.ToList().Where(c => c.IsActive == true);
                result.Ok = true;
                result.Return = s;
                result.AdditionalInfo = "Se muestra el retiro correctamente";
                result.ErrorCode = 200;
                return result;

            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al cargar retiros" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

        [HttpPut]
        [Route("Pickup/UpdatePickupState")]
        public ActionResult<ResultAPI> UpdatePickupState([FromBody] CommandUpdatePickup command)
        {
            ResultAPI result = new ResultAPI();


            var pick = db.Pickups.Where(c => c.IdPickup == command.IdPickup).FirstOrDefault();
            if (pick != null)
            {

                if (pick.IdState == 1)
                {
                    pick.IdState = 4;
                }
                else
                if (pick.IdState == 4)
                {
                    pick.IdState = 5;
                }

                db.Pickups.Update(pick);
                db.SaveChanges();
                result.Ok = true;
                result.Return = pick;
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
        [Route("Pickup/GetListJoin")]
        public ActionResult<ResultAPI> GetListJoin()
        {

            var query = from p in db.Pickups
                        join pd in db.PickupDetails on p.IdPickup equals pd.IdPickup join st in db.States on  p.IdState equals st.IdState
                        join doo in db.DeliveryOrders on p.IdDeliveryOrder equals doo.IdDeliveryOrder
                        join br in db.Branches on doo.IdBranch equals br.IdBranch
                        where p.IsActive == true
                        group p by new {p.IdPickup, p.IdDeliveryOrder, st.State1, pd.Volume, br.Name, doo.DeliveryDate} into g
                        select new { IdPickup = g.Key, IdDeliveryOrder = g.Key, State = g.Key, Volume = g.Key, Branch = g.Key, fecha = g.Key};

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

        [HttpPut]
        [Route("DeliveryOrder/UpdateDeliveryOrder")]
        public ActionResult<ResultAPI> UpdateDeliveryOrder([FromBody] CommandUpdateDeliveryOrder command)
        {
            ResultAPI result = new ResultAPI();

            if (command.IdBranch <= 0)
            {
                result.Ok = false;
                result.Error = "Ese retiro no existe";
                return result;
            }
            if (command.IdDeliveryOrder <= 0)
            {
                result.Ok = false;
                result.Error = "Esa orden de entrega no existe";
                return result;
            }

            var delOr = db.DeliveryOrders.Where(c => c.IdDeliveryOrder == command.IdDeliveryOrder).FirstOrDefault();
            if (delOr != null)
            {                                
                delOr.IdBranch = command.IdBranch;                
                delOr.IsFree = true;
                db.DeliveryOrders.Update(delOr);
                db.SaveChanges();
                result.Ok = true;
                result.Return = db.DeliveryOrders.ToList();
                return result;
            }
            else
            {
                result.Ok = false;
                result.ErrorCode = 200;
                result.Error = "Orden de pedido no encontrado, revise el Documento";
                return result;
            }
        }



    }
}
