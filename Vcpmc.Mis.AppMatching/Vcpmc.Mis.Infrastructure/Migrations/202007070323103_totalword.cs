namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YoutubeFileItems", "TotalWord", c => c.Int(nullable: false));
            AddColumn("dbo.YoutubeFileItems", "CorrectWord", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YoutubeFileItems", "CorrectWord");
            DropColumn("dbo.YoutubeFileItems", "TotalWord");
        }
    }
}
