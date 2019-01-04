using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        [Display(Name = "Send To:")]
        public string ReceivingUserId { get; set; }

        public ApplicationUser ReceivingUser { get; set; }

        [Required]
        [Display(Name = "Sender:")]
        public string SendingUserId { get; set; }

        [Required]
        public ApplicationUser SendingUser { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}
