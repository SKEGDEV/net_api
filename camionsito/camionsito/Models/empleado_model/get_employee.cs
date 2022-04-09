using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camionsito.Models.empleado_model
{
    public class get_employee
    {
        public int id { get; set; }
        public string nombre_completo { get; set; } 
        public int DPI { get; set; }   
        public int NIT { get; set; }
        public int edad { get; set; }   

    }
}