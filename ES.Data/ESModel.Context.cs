﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ES.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ESEntities : DbContext
    {
        public ESEntities()
            : base("name=ESEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<certification> certifications { get; set; }
        public virtual DbSet<connection> connections { get; set; }
        public virtual DbSet<education> educations { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<users_details> users_details { get; set; }
    }
}