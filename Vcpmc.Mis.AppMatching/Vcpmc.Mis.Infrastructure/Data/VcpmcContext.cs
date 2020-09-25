using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Vcpmc.Mis.ApplicationCore.Entities.contract;
using Vcpmc.Mis.ApplicationCore.Entities.control;
using Vcpmc.Mis.ApplicationCore.Entities.dis;
using Vcpmc.Mis.ApplicationCore.Entities.makeData;
using Vcpmc.Mis.ApplicationCore.Entities.youtube;

namespace Vcpmc.Mis.Infrastructure.data
{
    public class VcpmcContext : DbContext
    {
        public VcpmcContext() : base("MyDBConnectionString")
        {
            //var initializer = new DropCreateDatabaseAlways<VcpmcContext>();
            //Database.SetInitializer(initializer);
            //this.SetCommandTimeOut(3000);
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 10 * 60; // value in seconds
        }
        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //18,6
            modelBuilder.Configurations.Add(new DistributionDataItemEntityConfig());
        }

        #region Phân phối BH
        /// <summary>
        /// Phiếu phân phối
        /// </summary>
        public DbSet<Distibution> Distibutions { get; set; }
        /// <summary>
        /// Phiếu phân phối
        /// </summary>
        public DbSet<DistributionData> DistributionDatas { get; set; }
        /// <summary>
        /// Phiếu phân phối(chi tiết)
        /// </summary>
        public DbSet<DistributionDataItem> DistributionDataItems { get; set; }
        /// <summary>
        /// Bai hat
        /// </summary>
        //public DbSet<Work> Works { get; set; }
        /// <summary>
        /// thanh vien
        /// </summary>
        //public DbSet<Member> Members { get; set; }
        /// <summary>
        /// Phieu xac nhan bai hat
        /// </summary>
        public DbSet<ImportMapWorkMember> importMapWorkMembers { get; set; }
        /// <summary>
        /// Phieu xac nhan bai hat chi tiet
        /// </summary>
        public DbSet<ImportMapWorkMemberDetail> ImportMapWorkMemberDetails { get; set; }
        /// <summary>
        /// Phiếu nhập các bài hat ngoai le
        /// </summary>
        public DbSet<ExceptionWork> ExceptionWorks { get; set; }
        /// <summary>
        /// Phiếu nhập các bài hat ngoai le- chi tiết
        /// </summary>
        public DbSet<ExceptionWorkDetail> ExceptionWorkDetails { get; set; }
        /// <summary>
        /// Phiếu nhập thành viên BH
        /// </summary>
        public DbSet<MemberBH> MemberBHs { get; set; }
        /// <summary>
        /// Phiếu nhập thành viên BH chi tiết
        /// </summary>
        public DbSet<MemberBHDetail> MemberBHDetails { get; set; }
        #endregion

        #region Youtube
        ///// <summary>
        ///// Phiếu Import dât từ SQL
        ///// </summary>
        //public DbSet<YoutubeTemp> YoutubeTemps { get; set; }
        ///// <summary>
        ///// Phiếu import data từ sql
        ///// </summary>
        //public DbSet<YoutubeDataTemp> YoutubeDataTemps { get; set; }
        ///// <summary>
        ///// Phiếu nhập youtube
        ///// </summary>
        //public DbSet<YoutubeFile> YoutubeFiles { get; set; }
        ///// <summary>
        ///// Chi tiết phiếu nhập youtube
        ///// </summary>
        //public DbSet<YoutubeFileItems> YoutubeFileItems { get; set; }
        #endregion        

        #region Hop dong
        /// <summary>
        /// Hop Đồng
        /// </summary>
        public DbSet<ContractObject> ContractObjects { get; set; }
        #endregion        
    }
}
