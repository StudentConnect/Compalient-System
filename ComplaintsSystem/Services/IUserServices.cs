using ComplaintsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintsSystem.Services
{
    public interface IUserServices
    {
        UserModel Authenticate(string email, string password);
    }
}
