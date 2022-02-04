using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace correction.Models
{
    public partial class Broker
    {
        public Broker()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int IdBroker { get; set; }

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Nom")]
        public string Lastname { get; set; } = null!;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Prenom")]
        public string Firstname { get; set; } = null;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Mail")]

        public string Mail { get; set; } = null;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Numéro de Telephone")]
        public string PhoneNumber { get; set; } = null;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
