using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [Required]
        [Display(Name = "Choose Vet")]
        public string VetId { get; set; }

        public ApplicationUser Vet { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Status { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<PetTreatment> PetTreatments { get; set; }
    }
}
