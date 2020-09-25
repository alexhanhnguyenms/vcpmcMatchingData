namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTableFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EdiFilesItems", "EdiFilesId", "dbo.EdiFiles");
            DropForeignKey("dbo.YoutubeDataTemps", "YoutubeTempId", "dbo.YoutubeTemps");
            DropForeignKey("dbo.YoutubeFiles", "YoutubeFile_Id", "dbo.YoutubeFiles");
            DropForeignKey("dbo.YoutubeFileItems", "YoutubeFileId", "dbo.YoutubeFiles");
            DropIndex("dbo.EdiFilesItems", new[] { "EdiFilesId" });
            DropIndex("dbo.YoutubeDataTemps", new[] { "YoutubeTempId" });
            DropIndex("dbo.YoutubeFileItems", new[] { "YoutubeFileId" });
            DropIndex("dbo.YoutubeFiles", new[] { "YoutubeFile_Id" });
            DropTable("dbo.EdiFiles");
            DropTable("dbo.EdiFilesItems");
            DropTable("dbo.YoutubeDataTemps");
            DropTable("dbo.YoutubeTemps");
            DropTable("dbo.YoutubeFileItems");
            DropTable("dbo.YoutubeFiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.YoutubeFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        TotalRecord = c.Long(nullable: false),
                        YoutubeFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YoutubeFileItems",
                c => new
                    {
                        YoutubeFileId = c.Int(nullable: false),
                        Code = c.Int(nullable: false),
                        NO = c.Long(nullable: false),
                        ID = c.String(),
                        TITLE = c.String(),
                        TITLE2 = c.String(),
                        ARTIST = c.String(),
                        ARTIST2 = c.String(),
                        ALBUM = c.String(),
                        ALBUM2 = c.String(),
                        LABEL = c.String(),
                        LABEL2 = c.String(),
                        ISRC = c.String(),
                        COMP_ID = c.String(),
                        COMP_TITLE = c.String(),
                        COMP_ISWC = c.String(),
                        COMP_WRITERS = c.String(),
                        COMP_CUSTOM_ID = c.String(),
                        QUANTILE = c.Int(nullable: false),
                        IsReport1 = c.Boolean(nullable: false),
                        IsReport2 = c.Boolean(nullable: false),
                        IsReport3 = c.Boolean(nullable: false),
                        IsReport4 = c.Boolean(nullable: false),
                        IsReport5 = c.Boolean(nullable: false),
                        IsReport6 = c.Boolean(nullable: false),
                        IsReport7 = c.Boolean(nullable: false),
                        IsReport8 = c.Boolean(nullable: false),
                        IsReport9 = c.Boolean(nullable: false),
                        IsReport10 = c.Boolean(nullable: false),
                        ScoreDetect1Vn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScoreDetect2Algorithm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScoreDetect3API = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DetectLanguage = c.String(),
                        Note = c.String(),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDetect1Vn = c.Boolean(nullable: false),
                        IsDetect2Algorithm = c.Boolean(nullable: false),
                        IsDetectAPI = c.Boolean(nullable: false),
                        IsISRC = c.Boolean(nullable: false),
                        IsLABEL = c.Boolean(nullable: false),
                        IsVn = c.Boolean(nullable: false),
                        Condition = c.Int(nullable: false),
                        Condition2 = c.Int(nullable: false),
                        TotalWord = c.Int(nullable: false),
                        CorrectWord = c.Int(nullable: false),
                        IsConvertNotSignVN = c.Int(nullable: false),
                        IsAnalysic = c.Int(nullable: false),
                        Percents = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.YoutubeFileId, t.Code });
            
            CreateTable(
                "dbo.YoutubeTemps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YoutubeDataTemps",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        YoutubeTempId = c.Int(nullable: false),
                        ID = c.String(),
                        TITLE = c.String(),
                        TITLE2 = c.String(),
                        ARTIST = c.String(),
                        ARTIST2 = c.String(),
                        ALBUM = c.String(),
                        ALBUM2 = c.String(),
                        LABEL = c.String(),
                        LABEL2 = c.String(),
                        ISRC = c.String(),
                        COMP_ID = c.String(),
                        COMP_TITLE = c.String(),
                        COMP_ISWC = c.String(),
                        COMP_WRITERS = c.String(),
                        COMP_CUSTOM_ID = c.String(),
                        QUANTILE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.EdiFilesItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        EdiFilesId = c.Guid(nullable: false),
                        index = c.Int(nullable: false),
                        seqNo = c.Int(nullable: false),
                        Title = c.String(),
                        NoOfPerf = c.Int(nullable: false),
                        Composer = c.String(),
                        Artist = c.String(),
                        Publisher = c.String(),
                        WorkInternalNo = c.String(),
                        RegionalNo = c.String(),
                        WorkTitle = c.String(),
                        WorkArtist = c.String(),
                        WorkComposer = c.String(),
                        WorkStatus = c.String(),
                        IpSetNo = c.String(),
                        IpInNo = c.String(),
                        NameNo = c.String(),
                        IpNameType = c.String(),
                        IpWorkRole = c.String(),
                        IpName = c.String(),
                        IpNameLocal = c.String(),
                        Society = c.String(),
                        SpName = c.String(),
                        Society2 = c.String(),
                        PerOwnShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerColShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MecOwnShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MecColShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SpShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMecShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SynOwnShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SynColShr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDuplicate = c.Boolean(nullable: false),
                        CountDuplicate = c.Int(nullable: false),
                        Note = c.String(),
                        IsC = c.Boolean(nullable: false),
                        IsA = c.Boolean(nullable: false),
                        IsCA = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EdiFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        Note = c.String(),
                        TotalRecord = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.YoutubeFiles", "YoutubeFile_Id");
            CreateIndex("dbo.YoutubeFileItems", "YoutubeFileId");
            CreateIndex("dbo.YoutubeDataTemps", "YoutubeTempId");
            CreateIndex("dbo.EdiFilesItems", "EdiFilesId");
            AddForeignKey("dbo.YoutubeFileItems", "YoutubeFileId", "dbo.YoutubeFiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.YoutubeFiles", "YoutubeFile_Id", "dbo.YoutubeFiles", "Id");
            AddForeignKey("dbo.YoutubeDataTemps", "YoutubeTempId", "dbo.YoutubeTemps", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EdiFilesItems", "EdiFilesId", "dbo.EdiFiles", "Id", cascadeDelete: true);
        }
    }
}
