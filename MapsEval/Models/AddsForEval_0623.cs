//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapsEval.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AddsForEval_0623
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AddsForEval_0623()
        {
            this.AddsForEval_0623Companies = new HashSet<AddsForEval_0623Companies>();
        }
    
        public int AddressID { get; set; }
        public string Address { get; set; }
        public Nullable<double> AddressLatitude { get; set; }
        public Nullable<double> AddressLongitude { get; set; }
        public Nullable<bool> UseOfPremises_Office { get; set; }
        public Nullable<bool> UseOfPremises_ProductionLogistics { get; set; }
        public Nullable<bool> UseOfPremises_Retail { get; set; }
        public Nullable<bool> UseOfPremises_Residential { get; set; }
        public Nullable<int> StandardOfPremises { get; set; }
        public Nullable<int> StandardOfParkedCars { get; set; }
        public Nullable<int> StandardOfSurroundingArea { get; set; }
        public Nullable<int> EstimatedAgeOfPremises { get; set; }
        public Nullable<bool> AddressNotFound { get; set; }
        public Nullable<bool> PremisesNotVisible { get; set; }
        public Nullable<bool> UnclearReviewNeeded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddsForEval_0623Companies> AddsForEval_0623Companies { get; set; }
    }
}
