using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksWeb.Models.ViewModel
{
    public class IndexVM
    {
        //for list of National parks and trails
        public IEnumerable<NationalPark> NationalParkList { get; set; }
        public IEnumerable<Trail> TrailList { get; set; }
    }
}
