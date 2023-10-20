using System;
using static InvestmentAppProd.Models.Investment;
using System.ComponentModel.DataAnnotations;
using InvestmentAppProd.Helpers;
using System.Collections.Generic;

namespace InvestmentAppProd.Models.DTO
{
	public class InvestmentResponseDTO
	{
		public InvestmentResponseDTO()
		{
		}

        [Required]
        [Key]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public InvestmentInterestTypeEnum InterestType { get; set; }

        public double InterestRate { get; set; }

        public double PrincipalAmount { get; set; }

        public double CurrentValue { get; set; } = 0;


        public InvestmentResponseDTO(Investment investment)
        {
            Name = investment.Name;
            StartDate = investment.StartDate;
            InterestType = ((InvestmentInterestTypeEnum)investment.InterestTypeId);
            InterestRate = investment.InterestRate;
            PrincipalAmount = investment.PrincipalAmount;
        }

        public void CalculateValue()
        {
            double r;
            double t;
            double n;
            double simpleInterestFinalAmount;
            double compoundInterestFinalAmount;
            double monthsDiff;

            // Interest rate is divided by 100.
            r = this.InterestRate / 100;

            // Time t is calculated to the nearest month.
            monthsDiff = DateTimeHelper.GetRoundedMonthDiff(this.StartDate);
            t = monthsDiff / 12;

            // Compounding period is set to monthly (i.e. n = 12).
            n = 12;

            List<double> InterestList = new List<double>();

            // SIMPLE INTEREST.
            simpleInterestFinalAmount = this.PrincipalAmount * (1 + (r * t));
            InterestList.Add(Math.Round(simpleInterestFinalAmount, 2));

            // COMPOUND INTEREST.
            compoundInterestFinalAmount = this.PrincipalAmount * Math.Pow((1 + (r / n)), (n * t));
            InterestList.Add(Math.Round(compoundInterestFinalAmount, 2));

            int InterestIndex = (int)this.InterestType;

            this.CurrentValue = InterestList[InterestIndex];
        }
    }
}

