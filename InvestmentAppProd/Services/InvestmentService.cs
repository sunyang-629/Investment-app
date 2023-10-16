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

        public Investment AddNewInvestment(InvestmentDTO investment)
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

        public Investment UpdateInvestment(InvestmentDTO investment)
        {
            if (investment.StartDate > DateTime.Now)
                throw new ArgumentException("Investment Start Date cannot be in the future.");
            var existedInvestment = _dbAccess.FindInvestmentByName(investment.Name);
            if (existedInvestment == null)
                throw new ArgumentException($"Investment with name {investment.Name} not found.");

            var updatedInvestment = new Investment(investment);
            _dbAccess.UpdateInvestment(updatedInvestment);
            _dbAccess.SaveChanges();
            return updatedInvestment;
        }

        public InvestmentResponseDTO GetInvestment(string investmentName)
        {
            var result = _dbAccess.FindInvestmentByName(investmentName);
            if (result == null)
                throw new ArgumentException($"Investment with name {investmentName} not found.");
            var investment = new InvestmentResponseDTO(result);
            investment.CalculateValue();
            return investment;
        }
    }
}

