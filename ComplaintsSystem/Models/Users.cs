using System;
using System.Collections.Generic;

namespace ComplaintsSystem.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
