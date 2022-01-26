using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMamiApi.Models;
using SuperMamiApi.Results;

namespace SuperMamiApi.Controllers
{
    [ApiController]
    [EnableCors("speMsi")]
    public class ReportsController : ControllerBase
    {
        private readonly super_mami_entregasContext db = new super_mami_entregasContext();
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }

        //REPORTE 1
        [HttpGet]
        [Route("Reports/GetCountShippingType")]
        public ActionResult<ResultAPI> GetCountShippingType()
        {

            var query = from s in db.Shippings
                        join sc in db.ShippingCompanies on s.IdShippingCompany equals sc.IdShippingCompany
                        join st in db.ShippingTypes on sc.IdShippingType equals st.IdShippingType
                        group st by st.Description into g
                        select new { tiposDeEnvio = g.Key, Total = g.Count() };

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
        //REPORTE 2
        [HttpGet]
        [Route("Reports/GetPercentageShippingsByCompany")]
        public ActionResult<ResultAPI> GetPercentageShippingsByCompany()
        {


            var query = from s in db.Shippings
                        join sc in db.ShippingCompanies on s.IdShippingCompany equals sc.IdShippingCompany
                        group new { sc.BusinessName, s.IdShippingCompany } by sc.BusinessName into g
                        select new
                        {
                            nombreEmpresa = g.Key,
                            porcentaje = Math.Round(((Convert.ToDouble(g.Select(z => z.IdShippingCompany).Count())) /
                                                                                        (Math.Round(Convert.ToDouble((from s in db.Shippings
                                                                                                                      select s.IdShippingCompany).Count()))) * 100), 2)
                        };

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
        //REPORTE 3
        [HttpPost]
        [Route("Reports/GetPriceRangeByMonth")]
        public ActionResult<ResultAPI> GetPriceRangeByMonth([FromBody] CommandGetPriceRangeByMonth command)
        {

            var query = from doo in db.DeliveryOrders
                        where doo.DeliveryDate.Year == command.Year && doo.ShippingPrice != null
                        group doo by doo.DeliveryDate.Month into g
                        select new
                        {
                            MesFacturacion = g.Key,
                            FacturacionMaxima = g.Max(z => z.ShippingPrice),
                            FacturacionMinima = g.Min(z => z.ShippingPrice)
                        };

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

        //REPORTE 4
        [HttpPost]
        [Route("Reports/GetTotalYearlyBillingByMonth")]
        public ActionResult<ResultAPI> GetTotalYearlyBillingByMonth([FromBody] CommandGetPriceRangeByMonth command)
        {

            var query = from doo in db.DeliveryOrders
                        where doo.DeliveryDate.Year == command.Year && doo.ShippingPrice != null
                        group doo by doo.DeliveryDate.Month into g
                        select new
                        {
                            MesFacturacion = g.Key,
                            FacturacionTotal = g.Sum(z => z.ShippingPrice),
                        };

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

        //REPORTE 5
        [HttpGet]
        [Route("Reports/GetTotalPricePerYear")]
        public ActionResult<ResultAPI> GetTotalPricePerYear()
        {

            var query = from b in db.ShippingCompanies
                        select new { Empresa = b.BusinessName, SalarioAnual = (b.Salary*12) };
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
    }
}