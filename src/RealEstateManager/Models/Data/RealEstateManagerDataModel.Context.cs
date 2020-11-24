﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RealEstateManagerDataModelContainer : DbContext
    {
        public RealEstateManagerDataModelContainer()
            : base("name=RealEstateManagerDataModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Estate> Estates { get; set; }
        public virtual DbSet<BuildingInfo> BuildingInfoes { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<EstateAgent> EstateAgents { get; set; }
        public virtual DbSet<PublicUser> PublicUsers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<SystemValue> SystemValues { get; set; }
    }
}
