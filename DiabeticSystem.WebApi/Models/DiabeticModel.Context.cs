﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiabeticSystem.WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DiabeticSystemEntities : DbContext
    {
        public DiabeticSystemEntities()
            : base("name=DiabeticSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PatientMembershipDetail> PatientMembershipDetails { get; set; }
        public virtual DbSet<PatientPersonal> PatientPersonals { get; set; }
        public virtual DbSet<PatientTestResult> PatientTestResults { get; set; }
        public virtual DbSet<DiabeticAdmin> DiabeticAdmins { get; set; }
    }
}
