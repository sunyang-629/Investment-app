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
        private readonly InvestmentDBContext _context;
        private readonly IInvestmentService _service;

        public InvestmentsController(InvestmentDBContext context, IInvestmentService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("{investmentName}")]
        public ActionResult<Investment> CalculateInvestment([FromRoute] string investmentName)
        {
            try
            {
                var investment = _context.Investments.Find(investmentName);
                if (investment == null)
                    return NotFound();

                return Ok(investment);
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
        public ActionResult UpdateInvestment([FromRoute] string investmentName, [FromBody] Investment investment)
        {
            try
            {
                if (investmentName != investment.Name)
                    return BadRequest("Name does not match the Investment you are trying to update.");

                if (investment.StartDate > DateTime.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                investment.CalculateValue();
                _context.ChangeTracker.Clear();
                _context.Entry(investment).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
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
