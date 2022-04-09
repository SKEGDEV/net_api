using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camionsito.Models.camion_model
{
    public class json_truck
    {
        public string msm { get; set; }
        public List<truck_get> data { get; set; }
    }
}