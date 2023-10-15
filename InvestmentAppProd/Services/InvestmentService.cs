using System;
using System.Threading.Tasks;
using InvestmentAppProd.Data;
using InvestmentAppProd.interfaces;
using InvestmentAppProd.Models;
using InvestmentAppProd.Models.DTO;

namespace InvestmentAppProd.Services
{
    public class InvestmentService : IInvestmentService
	{
        private readonly InvestmentDBContext _context;

        public InvestmentService(InvestmentDBContext context)
		{
            _context = context;
		}

        public Investment AddNewInvestment(IInvestmentDTO investment)
        {
            try
            {
                //if (investment.StartDate > DateTime.Now)
                //    return new BadRequest("Investment Start Date cannot be in the future.");

                var newInvestment = new Investment(investment);
                newInvestment.CalculateValue();
                _context.ChangeTracker.Clear();
                _context.Investments.Add(newInvestment);
                _context.SaveChanges();
                return newInvestment;
            } catch
            { 
                throw new NotImplementedException();
            }
        }
    }
}

