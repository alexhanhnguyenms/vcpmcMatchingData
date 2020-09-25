using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.System.Users
{
    public class UserChangePasswordRequest
    {
        //public string Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordOld { get; set; } = string.Empty;       
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
       
    }
}
