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
    
    public partial class tblFrequentFlightSchedule
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int RouteID { get; set; }
        public System.DateTime DepartureTime { get; set; }
        public System.DateTime ArrivalTime { get; set; }
        public int FlightFrequency { get; set; }
    }
}
