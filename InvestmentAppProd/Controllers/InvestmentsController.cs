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
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Get details of an investment by its name
        /// </summary>
        /// <param name="investmentName">The unique name of the investment</param>
        /// <returns>Returns an investment response containing the current value to the nearest month</returns>
        /// <returns code="404>If given investment name was not found</returns>
        /// <returns code="500>If there was an error while retrieving an investment</returns>
        [HttpGet("{investmentName}")]
        public ActionResult<InvestmentResponseDTO> CalculateInvestment([FromRoute, Required] string investmentName)
        {
            try
            {
                var result = _service.GetInvestment(investmentName);
                return Ok(result);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Add a new investment
        /// </summary>
        /// <param name="investment">A investment request with all the new values</param>
        /// <returns>Returns an investment response</returns>
        /// <returns code="400">If given start time is after today or investment name has been existed</returns>
        /// <returns code="500>If there was an error while adding an investment</returns>
        [HttpPost]
        public ActionResult<Investment> AddInvestment([FromBody, Required] InvestmentDTO investment)
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
            catch(ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Update an investment
        /// </summary>
        /// <param name="investmentName">The unique name of the investment</param>
        /// <param name="investment">A investment request containing the values to update</param>
        /// <returns>Returns an investment response</returns>
        /// <returns code="400">If given start time is after today or investment name has been existed, or investment name are not matched</returns>
        /// <returns code="500>If there was an error while updating an investment</returns>
        [HttpPut("{investmentName}")]
        public ActionResult<Investment> UpdateInvestment([FromRoute,Required] string investmentName, [FromBody,Required] InvestmentDTO investment)
        {
            try
            {
                if (investmentName != investment.Name)
                    return BadRequest("Name does not match the Investment you are trying to update.");

                var result = _service.UpdateInvestment(investment);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Delete an investment
        /// </summary>
        /// <param name="investmentName">The unique name of the investment</param>
        /// <returns>Returns no content</returns>
        /// <returns code="500>If there was an error while updating an investment</returns>
        [HttpDelete("{investmentName}")]
        public ActionResult DeleteInvestment([FromRoute, Required] string investmentName)
        {
            try
            {
                _service.RemoveInvestment(investmentName);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
