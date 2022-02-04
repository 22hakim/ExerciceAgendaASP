using System;
using System.Collections.Generic;

namespace correction.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int IdCustomer { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Budget { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
