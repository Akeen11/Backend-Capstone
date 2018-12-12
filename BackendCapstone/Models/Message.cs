using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string VetId { get; set; }

        public ApplicationUser Vet { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string Messages { get; set; }
    }
}
