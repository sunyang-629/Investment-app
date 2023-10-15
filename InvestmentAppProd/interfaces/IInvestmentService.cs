using System;
using System.Threading.Tasks;
using InvestmentAppProd.Models;
using InvestmentAppProd.Models.DTO;

namespace InvestmentAppProd.interfaces
{
	public interface IInvestmentService
	{
		public Investment AddNewInvestment(IInvestmentDTO investment);
	}
}