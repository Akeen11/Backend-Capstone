using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class PetTreatment
    {
        [Key]
        public int PetTreatmentId { get; set; }

        [Required]
        public int PetId { get; set; }

        public Pet Pet { get; set; }

        [Required]
        public int TreatmentId { get; set; }

        public Treatment Treatment { get; set; }
    }
}
