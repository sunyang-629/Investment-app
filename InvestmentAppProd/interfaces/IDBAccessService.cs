using System;
using System.Threading.Tasks;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Interfaces
{
	public interface IDBAccessService
	{
		public Task<int> SaveChanges();

        #region investment
        public void AddInvestment(Investment investment);
        public Investment FindInvestmentByName(string investmentName);
        public void UpdateInvestment(Investment investment);
        public void DeleteInvestmentByName(string investmentName);
        #endregion investment
    }
}

