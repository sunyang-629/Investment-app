using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Investment> Investments { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
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
                //** assuming name as the unique identify which is not able to update here
                investmentToUpdate.InterestType = investment.InterestType;
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
