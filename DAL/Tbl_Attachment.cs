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
    
    public partial class Tbl_Attachment
    {
        public int Id { get; set; }
        public string filename { get; set; }
        public int AttachmentCategoryId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] timestamp { get; set; }
        public int ItemId { get; set; }
        public byte[] fileinByte { get; set; }
        public Nullable<int> DomainCompanyId { get; set; }
    }
}
