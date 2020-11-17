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
    
    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            this.Files = new HashSet<File>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Number { get; set; }
        public string Outcome { get; set; }
        public System.Guid EstateId { get; set; }
        public System.Guid AgentId { get; set; }
        public System.Guid PublicUserId { get; set; }
    
        public virtual Estate Estate { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual PublicUser PublicUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }
    }
}
