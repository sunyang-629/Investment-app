//using Microsoft.Extensions.DependencyInjection;
//using System;

 

//namespace InvestmentAppProd

//{



//    public interface IThirdPartyInterest

//    {

//        //

//        double CalculateCurrentValue(DateTime startDate, double principle, double interestRate);

//    }



//    public static class ThirdPartyServiceConfig

//    {

//        public static void UseThirdPartyService(this IServiceCollection services)

//        {

//            services.AddTransient<IThirdPartyInterest, ThirdPartyService>();

//        }

//    }





//    internal class ThirdPartyService : IThirdPartyInterest

//    {





//        public double CalculateCurrentValue(DateTime startDate, double principle, double interestRate)

//        {

//            return 10;

//        }

//    }

//}