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
        [Display(Name = "Send To:")]
        public string ReceivingUserId { get; set; }

        [Display(Name = "Sent To:")]
        public ApplicationUser ReceivingUser { get; set; }

        [Required]
        public string SendingUserId { get; set; }

        [Required]
        [Display(Name = "Sender")]
        public ApplicationUser SendingUser { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Messages { get; set; }
    }
}
