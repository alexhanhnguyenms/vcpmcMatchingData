namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsConvertSign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YoutubeFileItems", "IsConvertNotSignVN", c => c.Int(nullable: false));
            AddColumn("dbo.YoutubeFileItems", "IsAnalysic", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YoutubeFileItems", "IsAnalysic");
            DropColumn("dbo.YoutubeFileItems", "IsConvertNotSignVN");
        }
    }
}
