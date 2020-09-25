using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Data.Entities
{
    /// <summary>
    /// Quyền
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
