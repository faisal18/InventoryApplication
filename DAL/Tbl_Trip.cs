//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Trip
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public string TotalDistance { get; set; }
        public string TollCharges { get; set; }
        public string WaitingCharges { get; set; }
        public string TotalCharges { get; set; }
        public string DriverCommission { get; set; }
        public Nullable<System.DateTime> DateofEntry { get; set; }
        public int DriverId { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] timestamp { get; set; }
        public Nullable<int> CreditStatusId { get; set; }
        public Nullable<int> DomainCompanyId { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
