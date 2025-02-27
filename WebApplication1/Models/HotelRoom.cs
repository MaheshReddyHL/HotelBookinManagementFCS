//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HotelRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelRoom()
        {
            this.RoomBookings = new HashSet<RoomBooking>();
            this.RoomCharges = new HashSet<RoomCharge>();
        }
    
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool AvailabilityStatus { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomBooking> RoomBookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomCharge> RoomCharges { get; set; }
    }
}
