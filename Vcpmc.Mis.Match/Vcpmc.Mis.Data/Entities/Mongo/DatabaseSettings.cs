using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Data.Entities.Mongo
{
    /// <summary>
    /// Cài đặt databases
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        /// <summary>
        /// Bảng preclaim
        /// </summary>
        public string PreclaimsCollectionName { get; set; }
        /// <summary>
        /// Work
        /// </summary>
        public string WorksCollectionName { get; set; }
        public string WorkTrackingsCollectionName { get; set; }
        /// <summary>
        /// Độc quyền
        /// </summary>
        public string MonopolysCollectionName { get; set; }
        public string MembersCollectionName { get; set; }
        public string MasterListsCollectionName { get; set; }
        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// tên database
        /// </summary>
        public string DatabaseName { get; set; }

        #region system
        public string AppUsersCollectionName { get; set; }
        public string AppRolesCollectionName { get; set; }
        public string AppClaimsCollectionName { get; set; }
        public string FixParametersCollectionName { get; set; }
        #endregion
    }
    public interface IDatabaseSettings
    {
        public string PreclaimsCollectionName { get; set; }
        public string WorksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string WorkTrackingsCollectionName { get; set; }
        public string MonopolysCollectionName { get; set; }
        public string MembersCollectionName { get; set; }
        #region system
        public string AppUsersCollectionName { get; set; }
        public string AppRolesCollectionName { get; set; }
        public string AppClaimsCollectionName { get; set; }
        public string MasterListsCollectionName { get; set; }        
        public string FixParametersCollectionName { get; set; }        
        #endregion
    }
}
