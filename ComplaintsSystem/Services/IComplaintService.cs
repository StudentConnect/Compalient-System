using ComplaintsSystem.Models;
using ComplaintsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintsSystem.Services
{
    public interface IComplaintService
    {
        void Save(ComplaintModel _complaint);
        List<Complaint> Get();
    }
}
