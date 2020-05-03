using ComplaintsSystem.Models;
using ComplaintsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintsSystem.Services
{
    public class ComplaintService : IComplaintService
    {
        private CMSContext _db;

        public ComplaintService(CMSContext db)
        {
            _db = db;
        }

        public void Save(ComplaintModel _complaint)
        {
            Complaint complaint = new Complaint()
            {
                CreatedBy = _complaint.UserId,
                CreatedDate = DateTime.Now,
                Description = _complaint.Description,
                Department = _complaint.Department,
                Subject = _complaint.Subject
            };

            _db.Complaint.Add(complaint);
            _db.SaveChanges();
        }

        public List<Complaint> Get()
        {
            return _db.Complaint.ToList();
        }
    }
}
