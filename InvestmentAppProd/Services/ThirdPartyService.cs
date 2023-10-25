using System;
using InvestmentAppProd.Interfaces;

namespace InvestmentAppProd.Services
{
	public class ThirdPartyService:IThirdPartyService
	{
		public ThirdPartyService()
		{
		}

        public decimal CalculateCurrentValue(DateTime startDate, decimal principle, decimal interestRate)

        {

            return 10m;

        }
    }
}

