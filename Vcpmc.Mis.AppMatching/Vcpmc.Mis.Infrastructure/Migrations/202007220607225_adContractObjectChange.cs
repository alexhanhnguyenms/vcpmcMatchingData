namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adContractObjectChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContractObjects", "IsReSigned", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountGround", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountBadger", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor1", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor2", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor3", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor4", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor5", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor6", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor7", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor8", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor9", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountFloor10", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractObjects", "CountTerrace", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContractObjects", "CountTerrace", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor10", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor9", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor8", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor7", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor6", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor5", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor4", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor3", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor2", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountFloor1", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountBadger", c => c.String());
            AlterColumn("dbo.ContractObjects", "CountGround", c => c.String());
            AlterColumn("dbo.ContractObjects", "IsReSigned", c => c.String());
        }
    }
}
