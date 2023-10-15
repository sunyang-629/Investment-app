using System;
namespace InvestmentAppProd.Helpers
{
	public class DateTimeHelper
	{
		public DateTimeHelper()
		{
		}

		public static double GetRoundedMonthDiff (DateTime date)
		{
            double monthsDiff = 12 * (DateTime.Now.Year - date.Year) + DateTime.Now.Month - date.Month;
			double averageDaysInMonth = 30.4167;
			double daysOffset = Math.Round((DateTime.Now.Day - date.Day) / averageDaysInMonth);
			monthsDiff += daysOffset;
			return monthsDiff;
        }
	}
}

