namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIndexOfEditFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EdiFilesItems", "index", c => c.Int(nullable: false));
            AddColumn("dbo.EdiFilesItems", "IsC", c => c.Boolean(nullable: false));
            AddColumn("dbo.EdiFilesItems", "IsA", c => c.Boolean(nullable: false));
            AddColumn("dbo.EdiFilesItems", "IsCA", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EdiFilesItems", "IsCA");
            DropColumn("dbo.EdiFilesItems", "IsA");
            DropColumn("dbo.EdiFilesItems", "IsC");
            DropColumn("dbo.EdiFilesItems", "index");
        }
    }
}
