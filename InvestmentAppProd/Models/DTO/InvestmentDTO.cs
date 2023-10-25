using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static InvestmentAppProd.Models.Investment;
using InvestmentAppProd.Helpers;
using InvestmentAppProd.Interfaces;
using InvestmentAppProd.Services;

namespace InvestmentAppProd.Models.DTO
{
    public class InvestmentDTO
    {
        public InvestmentDTO()
        {
        }

        [Required]
        [Key]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public InvestmentInterestTypeEnum InterestType { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public decimal PrincipalAmount { get; set; }
    }

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

        public decimal InterestRate { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal CurrentValue { get; set; } = 0;

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
            decimal r;
            decimal t;
            decimal n;
            decimal simpleInterestFinalAmount;
            decimal compoundInterestFinalAmount;
            decimal complexInterestFinalAmount;
            decimal monthsDiff;

            var thirdPartyService = new ThirdPartyService();

            // Interest rate is divided by 100.
            r = this.InterestRate / 100;

            // Time t is calculated to the nearest month.
            monthsDiff = DateTimeHelper.GetRoundedMonthDiff(this.StartDate);
            t = monthsDiff / 12;

            // Compounding period is set to monthly (i.e. n = 12).
            n = 12;

            List<decimal> InterestList = new List<decimal>();

            // SIMPLE INTEREST.
            simpleInterestFinalAmount = this.PrincipalAmount * (1 + (r * t));
            InterestList.Add(Math.Round(simpleInterestFinalAmount, 2));

            // COMPOUND INTEREST.
            compoundInterestFinalAmount = this.PrincipalAmount * (decimal)Math.Pow((double)(1 + (r / n)), (double)(n * t));
            InterestList.Add(Math.Round(compoundInterestFinalAmount, 2));

            // COMPLEX INTEREST
            //complexInterestFinalAmount = ThirdPartyInterestHelper.CalculateCurrentValue(this.StartDate, this.PrincipalAmount, this.InterestRate);
            complexInterestFinalAmount = thirdPartyService.CalculateCurrentValue(this.StartDate, this.PrincipalAmount, this.InterestRate);
            InterestList.Add(Math.Round(complexInterestFinalAmount, 2));


            int InterestIndex = (int)this.InterestType;

            this.CurrentValue = InterestList[InterestIndex];
        }

    }
}