using System;
namespace InvestmentAppProd.Interfaces
{
	public interface IThirdPartyService
	{
        decimal CalculateCurrentValue(DateTime startDate, decimal principle, decimal interestRate);
    }
}

