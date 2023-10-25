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

		public int InterestTypeId { get; set; }

		public decimal InterestRate { get; set; }

		public decimal PrincipalAmount { get; set; }

		public Investment()
		{
		}

		public enum InvestmentInterestTypeEnum
		{
            Simple,
            Compound,
			Complex
		}

		public Investment(InvestmentDTO investment)
		{
			Name = investment.Name;
			StartDate = investment.StartDate;
			InterestTypeId = (int)investment.InterestType;
			InterestRate = investment.InterestRate;
			PrincipalAmount = investment.PrincipalAmount;
		}
	}
}
