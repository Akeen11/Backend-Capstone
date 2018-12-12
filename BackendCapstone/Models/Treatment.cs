using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual ICollection<PetTreatment> PetTreatments { get; set; }
    }
}
