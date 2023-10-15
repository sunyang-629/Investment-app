using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentAppProd.Models;
using InvestmentAppProd.Controllers;
using InvestmentAppProd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InvestmentAppProd.Tests
{
    public class TestInvestmentController : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly InvestmentController _controller;


        public TestInvestmentController(CustomWebApplicationFactory<Program> factory)
        {
            _scope = factory.Services.CreateScope();
            _controller = _scope.ServiceProvider.GetRequiredService<InvestmentController>();   
            SeedDatabase(_scope);
        }

        public void Dispose()
        {
            EmptyDatabase(_scope);
            _scope.Dispose();
        }

        private void SeedDatabase(IServiceScope scope)
        {
            var newInvestments = new List<Investment>();

            newInvestments.Add(
                new Investment
                {
                    Name = "Investment 1",
                    StartDate = DateTime.Parse("2022-03-01"),
                    InterestType = "Simple",
                    InterestRate = 3.875,
                    PrincipalAmount = 10000
                });
            newInvestments.Add(
                new Investment
                {
                    Name = "Investment 2",
                    StartDate = DateTime.Parse("2022-04-01"),
                    InterestType = "Simple",
                    InterestRate = 4,
                    PrincipalAmount = 15000
                });
            newInvestments.Add(
                new Investment
                {
                    Name = "Investment 3",
                    StartDate = DateTime.Parse("2022-05-01"),
                    InterestType = "Compound",
                    InterestRate = 5,
                    PrincipalAmount = 20000
                });

            var context = scope.ServiceProvider.GetRequiredService<InvestmentDBContext>();
            context.Investments.AddRange(newInvestments);
            context.SaveChanges();
        }

        private void EmptyDatabase(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<InvestmentDBContext>();
            context.Investments.RemoveRange(context.Investments.ToList());
            context.SaveChanges();
            
        }

        [Fact]
        public void GetInvestment_WithSingleItem_ShouldReturnSingleInvestment()
        {
            // Arrange
            
            var name = "Investment 1";

            // Act
            var result = _controller.CalculateInvestment(name);
            var obj = result.Result as ObjectResult;
            var objInvResult = obj.Value as Investment;

            // Assert   : Status code 200 ("Ok") + Object returned is of Type Investment + Object name is same.
            Assert.Equal(200, (obj.StatusCode));
            Assert.IsType<Investment>(objInvResult);
            Assert.Equal(name, objInvResult.Name);
        }

        [Fact]
        public void AddInvestment_SingleItem_ShouldAddInvestment()
        {
            // Arrange
            var newInvestnment = new Investment
            {
                Name = "Investment 4",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Simple",
                InterestRate = 7.7,
                PrincipalAmount = 25000
            };

            // Act
            var result = _controller.AddInvestment(newInvestnment);
            var obj = result.Result as ObjectResult;
            //var objInvResult = obj.Value as Investment;

            // Assert   : Status code 201 ("Created")
            Assert.Equal(201, (obj.StatusCode));
        }

        [Fact]
        public void UpdateInvestment_SingleItem_ShouldUpdateInvestment()
        {
            // Arrange
            var updateInvestment = "Investment 2";
            var newInvestment = new Investment
            {
                Name = "Investment 2",
                StartDate = DateTime.Parse("2022-06-01"),
                InterestType = "Compound",
                InterestRate = 8,
                PrincipalAmount = 30000
            };

            // Act
            var result = _controller.UpdateInvestment(updateInvestment, newInvestment);
            var obj = result as NoContentResult;

            // Assert   : Status code 204 ("No Content")
            Assert.Equal(204, obj.StatusCode);
        }

        [Fact]
        public void DeleteInvestment_SingleItem_ShouldDeleteInvestment()
        {
            // Arrange
            var deleteInvestment = "Investment 2";

            // Act
            var result = _controller.DeleteInvestment(deleteInvestment);
            var obj = result as NoContentResult;

            // Assert   : Status code 204 ("No Content")
            Assert.Equal(204, obj.StatusCode);
        }
    }
}
