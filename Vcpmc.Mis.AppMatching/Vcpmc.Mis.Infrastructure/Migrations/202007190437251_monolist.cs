namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class monolist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonopolyObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.Int(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        No = c.Int(nullable: false),
                        CodeOld = c.String(),
                        CodeNew = c.String(),
                        Name = c.String(),
                        NameType = c.String(),
                        Own = c.String(),
                        NoteMono = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        ReceiveTime = c.DateTime(nullable: false),
                        Note2 = c.String(),
                        Note3 = c.String(),
                        Tone = c.Boolean(nullable: false),
                        Web = c.Boolean(nullable: false),
                        Performances = c.Boolean(nullable: false),
                        PerformancesHCM = c.Boolean(nullable: false),
                        Cddvd = c.Boolean(nullable: false),
                        Kok = c.Boolean(nullable: false),
                        Broadcasting = c.Boolean(nullable: false),
                        Entertaiment = c.Boolean(nullable: false),
                        Film = c.Boolean(nullable: false),
                        Advertisement = c.Boolean(nullable: false),
                        PubMusicBook = c.Boolean(nullable: false),
                        Youtube = c.Boolean(nullable: false),
                        Other = c.Boolean(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MonopolyObjects");
        }
    }
}
