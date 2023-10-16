using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using InvestmentAppProd.Models.DTO;
using System.Runtime.Serialization;
using InvestmentAppProd.Helpers;

namespace InvestmentAppProd.Models
{
	public class Investment
	{
		[Required]
		[Key]
		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public InvestmentInterestTypeEnum InterestType { get; set; }

		public double InterestRate { get; set; }

		public double PrincipalAmount { get; set; }

		public Investment()
		{
		}

		public enum InvestmentInterestTypeEnum
		{
            Simple,
            Compound
		}

		public Investment(InvestmentDTO investment)
		{
			Name = investment.Name;
			StartDate = investment.StartDate;
			InterestType = investment.InterestType;
			InterestRate = investment.InterestRate;
			PrincipalAmount = investment.PrincipalAmount;
		}
	}
}
