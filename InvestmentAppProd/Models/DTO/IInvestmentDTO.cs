using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestmentAppProd.Models.DTO
{
    public class IInvestmentDTO
    {
        public IInvestmentDTO()
        {

        }

        [Required]
        [Key]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string InterestType { get; set; }

        [Required]
        public double InterestRate { get; set; }

        [Required]
        public double PrincipalAmount { get; set; }
    }


}