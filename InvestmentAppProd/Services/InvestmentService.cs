using System;
using System.Threading.Tasks;
using InvestmentAppProd.Data;
using InvestmentAppProd.Interfaces;
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

            if (investment.StartDate > DateTime.Now)
                throw new Exception("Investment Start Date cannot be in the future.");

            var existedInvestment = _context.FindInvestmentByName(investment.Name);
            if (existedInvestment != null)
                throw new Exception($"Investment with name {investment.Name} is unavaliable.");

            try
            {
                var newInvestment = new Investment(investment);
                _context.ChangeTracker.Clear();
                _context.AddInvestment(newInvestment);
                _context.SaveChanges();
                return newInvestment;
            } catch
            { 
                throw new NotImplementedException();
            }
        }
    }
}

