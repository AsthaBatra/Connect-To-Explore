//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class users_details
    {
        public int users_details_id { get; set; }
        public int id_user { get; set; }
        public string short_description { get; set; }
        public string organisation { get; set; }
        public string education { get; set; }
        public string experience { get; set; }
        public string projects { get; set; }
        public string certifications { get; set; }
        public string awards { get; set; }
        public string interests { get; set; }
        public string dp { get; set; }
    
        public virtual user user { get; set; }
    }
}
