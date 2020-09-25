namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adContractObject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        No = c.Int(nullable: false),
                        Customer = c.String(),
                        Address = c.String(),
                        District = c.String(),
                        Phone = c.String(),
                        Contact = c.String(),
                        TaxCode = c.String(),
                        License = c.String(),
                        ContractNumber = c.String(),
                        Field = c.String(),
                        NameSign = c.String(),
                        ContractTime = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValueVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address2 = c.String(),
                        Note = c.String(),
                        NoteReSigned = c.String(),
                        IsReSigned = c.String(),
                        Ground = c.String(),
                        Badger = c.String(),
                        Floor1 = c.String(),
                        Floor2 = c.String(),
                        Floor3 = c.String(),
                        Floor4 = c.String(),
                        Floor5 = c.String(),
                        Floor6 = c.String(),
                        Floor7 = c.String(),
                        Floor8 = c.String(),
                        Floor9 = c.String(),
                        Floor10 = c.String(),
                        Terrace = c.String(),
                        CountGround = c.String(),
                        CountBadger = c.String(),
                        CountFloor1 = c.String(),
                        CountFloor2 = c.String(),
                        CountFloor3 = c.String(),
                        CountFloor4 = c.String(),
                        CountFloor5 = c.String(),
                        CountFloor6 = c.String(),
                        CountFloor7 = c.String(),
                        CountFloor8 = c.String(),
                        CountFloor9 = c.String(),
                        CountFloor10 = c.String(),
                        CountTerrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContractObjects");
        }
    }
}
