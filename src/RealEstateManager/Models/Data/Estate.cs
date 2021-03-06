//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstateManager.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Estate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estate()
        {
            this.EstateAccounts = new HashSet<EstateAccount>();
            this.Contacts = new HashSet<Contact>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public EstateType Type { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public EstateStatusType Status { get; set; }
        public string PublicDescription { get; set; }
        public string PrivateDescription { get; set; }
        public double Area { get; set; }
        public Nullable<System.Guid> BuildingInfoId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string FilePathsCSV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstateAccount> EstateAccounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual BuildingInfo BuildingInfo { get; set; }
    }
}
