//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace camionsito
{
    using System;
    using System.Collections.Generic;
    
    public partial class camion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public camion()
        {
            this.viajes = new HashSet<viaje>();
        }
    
        public int id { get; set; }
        public string placas { get; set; }
        public Nullable<int> km_por_lt { get; set; }
        public Nullable<double> capacidad_cc { get; set; }
        public string departamento { get; set; }
        public Nullable<int> tipo_carga { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> id_conductor { get; set; }
        public Nullable<int> id_ayudante { get; set; }
    
        public virtual ayudante ayudante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<viaje> viajes { get; set; }
        public virtual conductor conductor { get; set; }
        public virtual estado estado1 { get; set; }
        public virtual tipo_carga tipo_carga1 { get; set; }
    }
}