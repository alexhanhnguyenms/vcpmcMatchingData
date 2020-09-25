using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ApplicationCore.Entities.makeData;

namespace Vcpmc.Mis.Infrastructure.data
{
    internal class DistributionDataItemEntityConfig : EntityTypeConfiguration<DistributionDataItem>
    {
        internal DistributionDataItemEntityConfig()
        {
            this.Property(e => e.Royalty).HasPrecision(18, 6);
            this.Property(e => e.Royalty2).HasPrecision(18, 6);
        }
    }
}

