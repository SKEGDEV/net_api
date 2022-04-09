using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camionsito.Models.camion_model
{
    public class truck_get
    {
        public int id { get; set; } 
        public string placas { get; set; }
        public float capacidad_cc { get; set; } 
        public string departamento { get; set; }
        public string estado { get; set; }  
        public int km_por_lt { get; set; }
        public string conductor { get; set; }
        public string ayudante { get; set; }
        public string tipo_carga { get; set; }

    }
}