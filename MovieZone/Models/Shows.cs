//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieZone.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shows
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shows()
        {
            this.Booking = new HashSet<Booking>();
        }
    
        public int ShowID { get; set; }
        public Nullable<System.DateTime> DateOfShow { get; set; }
        public Nullable<System.TimeSpan> TimeOfShow { get; set; }
        public Nullable<int> SalonID { get; set; }
        public Nullable<int> FilmID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }
        public virtual Film Film { get; set; }
        public virtual Salon Salon { get; set; }
    }
}
