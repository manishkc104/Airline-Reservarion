//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblFlightSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblFlightSchedule()
        {
            this.tblFlightBookDetails = new HashSet<tblFlightBookDetail>();
        }
    
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int RouteID { get; set; }
        public System.DateTime DepartureDateTime { get; set; }
        public System.DateTime ArrivalDateTime { get; set; }
    
        public virtual tblFlight tblFlight { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFlightBookDetail> tblFlightBookDetails { get; set; }
        public virtual tblRoute tblRoute { get; set; }
    }
}
