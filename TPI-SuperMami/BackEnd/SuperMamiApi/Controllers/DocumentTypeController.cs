using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Commands;
using SuperMamiApi.Models;
using SuperMamiApi.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;


namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<DocumentTypeController> _logger;

        public DocumentTypeController(ILogger<DocumentTypeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("DocumentType/GetAllDocumentTypes")]
        public ActionResult<ResultAPI> GetTipoDocumento()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.DocumentTypes.ToList();
                resultado.AdditionalInfo = "Se cargó la lista correctamente";            
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al cargar tipos de documento" + ex.Message;
                resultado.ErrorCode = 400;
                return resultado;
            }
        }
    }
}