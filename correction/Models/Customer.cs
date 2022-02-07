using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace correction.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int IdCustomer { get; set; }

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Nom")]
        public string Lastname { get; set; } = null!;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Prénom")]
        public string Firstname { get; set; } = null!;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Mail")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string Mail { get; set; } = null!;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Numéro de Telephone")]
        [StringLength(10, ErrorMessage = "un numero de telephone doit contenir 10 caracteres.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Le champs doit être rempli."), Display(Name = "Budget")]
        [Range(1, 1000, ErrorMessage = "Vous devez indiquer un chiffre si vous pensez être hors budget indiquez 9999")]
        public int? Budget { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
