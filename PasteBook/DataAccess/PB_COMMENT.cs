//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PB_COMMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PB_COMMENT()
        {
            this.PB_NOTIFICATION = new HashSet<PB_NOTIFICATION>();
        }
    
        public int ID { get; set; }
        public int POST_ID { get; set; }
        public int POSTER_ID { get; set; }
        public string CONTENT { get; set; }
        public System.DateTime DATE_CREATED { get; set; }
    
        public virtual PB_POST PB_POST { get; set; }
        public virtual PB_USER PB_USER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PB_NOTIFICATION> PB_NOTIFICATION { get; set; }
    }
}