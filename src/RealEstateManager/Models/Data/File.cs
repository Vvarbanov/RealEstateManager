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
    
    public partial class File
    {
        public System.Guid Id { get; set; }
        public string PathOnFileSystem { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.Guid> ContactId { get; set; }
        public Nullable<System.Guid> EstateId { get; set; }
    
        public virtual Contact Contact { get; set; }
        public virtual Estate Estate { get; set; }
    }
}
