using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.System.Users
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } =false;
    }
}