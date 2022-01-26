using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands.StateCommands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;

namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class StateController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<StateController> _logger;

        public StateController(ILogger<StateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("State/GetAllStates")]
        public ActionResult<ResultAPI> GetAllStates()
        {
            var result = new ResultAPI();
            try
            {

                result.Ok = true;
                result.Return = db.States.ToList();
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

        [HttpPost]
        [Route("State/UpdateState")]
        public ActionResult<ResultAPI> UpdateState([FromBody] CommandUpdateState command)
        {
            var result = new ResultAPI();
            if (command.State1 == "")
            {
                result.Ok = false;
                result.Error = "Ingrese la descripción del estado";
                return result;
            }
            try
            {
                State s = db.States.FirstOrDefault(c => c.IdState == command.IdState);

                s.State1 = command.State1;
                db.States.Update(s);
                db.SaveChanges();
                result.Ok = true;
                result.Return = s;
                return result;

            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Error = "Error al actualizar el estado" + ex.Message;
                result.ErrorCode = 400;
                return result;
            }
        }

    }
}
