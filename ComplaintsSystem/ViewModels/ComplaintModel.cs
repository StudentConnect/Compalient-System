using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintsSystem.ViewModels
{
    public class ComplaintModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
