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
        private readonly DBContext _context;
        private readonly IDBAccessService _dbAccess;

        public InvestmentService(DBContext context, IDBAccessService dbAccess)
        {
            _context = context;
            _dbAccess = dbAccess;
        }

        public Investment AddNewInvestment(IInvestmentDTO investment)
        {

            if (investment.StartDate > DateTime.Now)
                throw new ArgumentException("Investment Start Date cannot be in the future.");

            var existedInvestment = _dbAccess.FindInvestmentByName(investment.Name);
            if (existedInvestment != null)
                throw new ArgumentException($"Investment with name {investment.Name} is unavaliable.");

            var newInvestment = new Investment(investment);
            _dbAccess.AddInvestment(newInvestment);
            _dbAccess.SaveChanges();
            return newInvestment;
        }

        public void RemoveInvestment(string investmentName)
        {
            _dbAccess.DeleteInvestmentByName(investmentName);
            _dbAccess.SaveChanges();
        }

        public Investment UpdateInvestment(IInvestmentDTO investment)
        {
            if (investment.StartDate > DateTime.Now)
                throw new ArgumentException("Investment Start Date cannot be in the future.");
            var existedInvestment = _dbAccess.FindInvestmentByName(investment.Name);
            if (existedInvestment != null)
                throw new ArgumentException($"Investment with name {investment.Name} not found.");

            var updatedInvestment = new Investment(investment);
            _dbAccess.UpdateInvestment(updatedInvestment);
            _dbAccess.SaveChanges();
            return updatedInvestment;
        }

        public Investment GetInvestment(string investmentName)
        {
            var investment = _dbAccess.FindInvestmentByName(investmentName);
            if (investment == null)
                throw new ArgumentException($"Investment with name {investment.Name} not found.");
            investment.CalculateValue();
            return investment;
        }
    }
}

