//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollageDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollageDetail()
        {
            this.BranchDetails = new HashSet<BranchDetail>();
        }
    
        public int Id { get; set; }
        public string CollageName { get; set; }
        public string Addrress { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public Nullable<int> EstablishIn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BranchDetail> BranchDetails { get; set; }
    }
}
