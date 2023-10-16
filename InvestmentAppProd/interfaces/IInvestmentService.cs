using System;
using System.Threading.Tasks;
using InvestmentAppProd.Models;
using InvestmentAppProd.Models.DTO;

namespace InvestmentAppProd.Interfaces
{
	public interface IInvestmentService
	{
		public Investment AddNewInvestment(InvestmentDTO investment);
		public void RemoveInvestment(string investmentName);
		public Investment UpdateInvestment(InvestmentDTO investment);
		public InvestmentResponseDTO GetInvestment(string investmentName);
	}
}