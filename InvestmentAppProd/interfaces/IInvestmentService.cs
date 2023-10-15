using System;
using System.Threading.Tasks;
using InvestmentAppProd.Models;
using InvestmentAppProd.Models.DTO;

namespace InvestmentAppProd.Interfaces
{
	public interface IInvestmentService
	{
		public Investment AddNewInvestment(IInvestmentDTO investment);
		public void RemoveInvestment(string investmentName);
		public Investment UpdateInvestment(IInvestmentDTO investment);
		public Investment GetInvestment(string investmentName);
	}
}