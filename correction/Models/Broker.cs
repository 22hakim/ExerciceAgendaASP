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
        [EmailAddress(ErrorMessage = "Email invalide.")]

        public string Mail { get; set; } = null;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Numéro de Telephone")]
        [Range(10, 10, ErrorMessage = "un numero de telephone doit contenir 10 caracteres.")]       
        public string PhoneNumber { get; set; } = null;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
