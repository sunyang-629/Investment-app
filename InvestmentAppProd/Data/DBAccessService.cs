using System;
using System.Linq;
using System.Threading.Tasks;
using InvestmentAppProd.Interfaces;
using InvestmentAppProd.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentAppProd.Data
{
	public class DBAccessService:IDBAccessService
	{
        private readonly DBContext _context;
        public DBAccessService(DBContext context)
		{
			_context = context;
		}

        #region generals
        public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}
        #endregion generals

        #region investment
        public void AddInvestment(Investment investment)
        {
            _context.ChangeTracker.Clear();
            _context.Investments.Add(investment);
        }

        public Investment FindInvestmentByName(string investmentName)
        {
            _context.ChangeTracker.Clear();
            return _context.Investments.Where(i => i.Name == investmentName).FirstOrDefault();
        }

        public void UpdateInvestment(Investment investment)
        {
            _context.ChangeTracker.Clear();
            var investmentToUpdate = FindInvestmentByName(investment.Name);
            if (investmentToUpdate != null)
            {
                //** assuming name as the unique identify which is not able to update here
                investmentToUpdate.InterestType = investment.InterestType;
                investmentToUpdate.InterestRate = investment.InterestRate;
                investmentToUpdate.StartDate = investment.StartDate;
                investmentToUpdate.PrincipalAmount = investment.PrincipalAmount;

                _context.Entry(investmentToUpdate).State = EntityState.Modified;
            }
        }

        public void DeleteInvestmentByName(string investmentName)
        {
            var investmentToDelete = FindInvestmentByName(investmentName);
            if (investmentToDelete != null)
            {
                _context.ChangeTracker.Clear();
                _context.Investments.Remove(investmentToDelete);
            }
        }
        #endregion investment
    }
}

