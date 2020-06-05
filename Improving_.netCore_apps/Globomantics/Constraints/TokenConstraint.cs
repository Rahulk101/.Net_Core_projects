using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Globomantics.Constraints
{
    public class TokenConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return ContainsRequiredChars(values["token"].ToString());
        }

        private bool ContainsRequiredChars(string token)
        {
            int letterCount = 0;
            int numCount = 0;
            double numSum = 0;
            
            foreach(var unit in token)
            {
                if (Char.IsLetter(unit))
                {
                    letterCount += 1;
                }
                else if (Char.IsDigit(unit))
                {
                    numCount += 1;
                    numSum += unit;
                }
            }

            return letterCount == 3 && numCount == 3 && numSum % 2 == 0;
        }
    }
}