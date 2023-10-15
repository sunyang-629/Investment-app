using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Data
{
    public class InvestmentDBContext : DbContext
    {
        public DbSet<Investment> Investments { get; set; }

        public InvestmentDBContext(DbContextOptions<InvestmentDBContext> options) : base(options)
        {

        }


        #region investment
        public void AddInvestment(Investment investment)
        {
            Investments.Add(investment);
        }

        public Investment FindInvestmentByName(string investmentName)
        {
            return Investments.Where(i => i.Name == investmentName).FirstOrDefault();
        }

        public void UpdateInvestment(Investment investment)
        {
            var investmentToUpdate = FindInvestmentByName(investment.Name);
            if(investmentToUpdate != null)
            {
                investmentToUpdate.InterestRate = investment.InterestRate;
                investmentToUpdate.StartDate = investment.StartDate;
                investmentToUpdate.PrincipalAmount = investment.PrincipalAmount;

                Entry(investmentToUpdate).State = EntityState.Modified;
            }
        }

        public void DeleteInvestmentByName(string investmentName)
        {
            var investmentToDelete = FindInvestmentByName(investmentName);
            if (investmentToDelete != null)
            {
                Investments.Remove(investmentToDelete);
            }
        }
        #endregion investement

    }
}
