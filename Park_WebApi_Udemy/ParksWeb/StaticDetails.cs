using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksWeb
{
    public class StaticDetails
    {
        public static string ApiBaseUrl = "https://localhost:44311/";
        public static string NationalParkAPIPath = ApiBaseUrl + "api/v1/nationalparks/";
        public static string TrailAPIPath = ApiBaseUrl + "api/v1/trails/";
        public static string AccountAPIPath = ApiBaseUrl + "api/v1/Users/";
    }
}
