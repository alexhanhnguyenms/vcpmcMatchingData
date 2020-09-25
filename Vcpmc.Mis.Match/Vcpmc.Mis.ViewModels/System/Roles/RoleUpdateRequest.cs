using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.System.Roles
{
    public  class RoleUpdateRequest
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach quyen
        /// </summary>
        public List<AppClaimViewModel> Rights { get; set; } = new List<AppClaimViewModel>();
    }
}
