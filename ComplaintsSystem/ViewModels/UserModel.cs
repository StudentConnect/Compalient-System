using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintsSystem.ViewModels
{
    public class UserModel
    {
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
