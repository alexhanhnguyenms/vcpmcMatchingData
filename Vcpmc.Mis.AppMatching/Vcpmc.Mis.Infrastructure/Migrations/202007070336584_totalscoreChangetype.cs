namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalscoreChangetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.YoutubeFileItems", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.YoutubeFileItems", "TotalScore", c => c.Single(nullable: false));
        }
    }
}
