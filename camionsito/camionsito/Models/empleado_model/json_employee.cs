using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camionsito.Models.empleado_model
{
    public class json_employee
    {
        public string msm { get; set; }
        public List<get_employee> chofers { get; set; }
        public List<get_employee> helpers { get; set; }
    }
}