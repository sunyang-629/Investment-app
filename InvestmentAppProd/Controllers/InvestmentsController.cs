using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;
using InvestmentAppProd.Data;
using InvestmentAppProd.Models.DTO;
using InvestmentAppProd.Interfaces;

namespace InvestmentAppProd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestmentsController : Controller
    {
        private readonly IInvestmentService _service;

        public InvestmentsController(IInvestmentService service)
        {
            _service = service;
        }

        [HttpGet("{investmentName}")]
        public ActionResult<Investment> CalculateInvestment([FromRoute] string investmentName)
        {
            try
            {
                var result = _service.GetInvestment(investmentName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }


        [HttpPost]
        public ActionResult<Investment> AddInvestment([FromBody] IInvestmentDTO investment)
        {
            try
            {
                var result = _service.AddNewInvestment(investment);
                return CreatedAtAction("AddInvestment", investment.Name, investment);
            }
            catch (DbUpdateException dbE)
            {
                return Conflict(dbE.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut("{investmentName}")]
        public ActionResult UpdateInvestment([FromRoute] string investmentName, [FromBody] IInvestmentDTO investment)
        {
            try
            {
                if (investmentName != investment.Name)
                    return BadRequest("Name does not match the Investment you are trying to update.");

                var result = _service.UpdateInvestment(investment);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete("{investmentName}")]
        public ActionResult DeleteInvestment([FromRoute] string investmentName)
        {
            try
            {
                _service.RemoveInvestment(investmentName);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }
    }
}
