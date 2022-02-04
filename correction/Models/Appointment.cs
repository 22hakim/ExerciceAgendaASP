using System;
using System.Collections.Generic;

namespace correction.Models
{
    public partial class Appointment
    {
        public int IdAppointment { get; set; }
        public DateTime DateHour { get; set; }
        public string Subject { get; set; } = null!;
        public int IdCustomer { get; set; }
        public int IdBroker { get; set; }

        public virtual Broker IdBrokerNavigation { get; set; } = null!;
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
    }
}
