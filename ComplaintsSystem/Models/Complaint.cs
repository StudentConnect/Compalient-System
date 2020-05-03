using System;
using System.Collections.Generic;

namespace ComplaintsSystem.Models
{
    public partial class Complaint
    {
        public int ComplaintId { get; set; }
        public string Subject { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsResolved { get; set; }
        public int? ResolvedBy { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
