using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camionsito.Models.camion_model
{
    public class truck_get_update
    {
        public string placas { get; set; }  
        public int km_por_lt { get; set; }  
        public float capacidad_cc { get; set; } 
        public string deparatamento { get; set; }   
        public int tipo_carga { get; set; }
        public int id_conductor { get; set; }   
        public int id_ayudante { get; set; }

    }
}