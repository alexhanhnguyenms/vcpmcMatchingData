namespace Vcpmc.Mis.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distibutions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        TotalRecord = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DistributionDataItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        StatusLoad = c.Boolean(nullable: false),
                        DistributionDataId = c.Guid(nullable: false),
                        No = c.Int(nullable: false),
                        WorkInNo = c.String(),
                        Title = c.String(),
                        Title2 = c.String(),
                        PoolName = c.String(),
                        PoolName2 = c.String(),
                        SourceName = c.String(),
                        SourceName2 = c.String(),
                        Role = c.String(),
                        Share = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Royalty = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Royalty2 = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Location = c.String(),
                        strContractTime = c.String(),
                        ContractTime = c.DateTime(nullable: false),
                        TotalAuthor = c.String(),
                        IsMapAuthor = c.Boolean(nullable: false),
                        BhAuthor = c.String(),
                        IsCondittionTime = c.Boolean(nullable: false),
                        SubMember = c.String(),
                        Beneficiary = c.String(),
                        GetPart = c.String(),
                        IsAlwaysGet = c.Boolean(nullable: false),
                        IsMapByGroup = c.Boolean(nullable: false),
                        returnDate = c.DateTime(),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsGiveBeneficiary = c.Boolean(nullable: false),
                        IsExcept = c.Boolean(nullable: false),
                        IsCreateReport = c.Boolean(nullable: false),
                        Note = c.String(),
                        Note2 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DistributionDatas", t => t.DistributionDataId, cascadeDelete: true)
                .Index(t => t.DistributionDataId);
            
            CreateTable(
                "dbo.DistributionDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        check = c.Boolean(nullable: false),
                        no = c.Int(nullable: false),
                        Name = c.String(),
                        TotalRecord = c.Int(nullable: false),
                        Note = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        StatusLoadData = c.Boolean(nullable: false),
                        StatusPrinter = c.Boolean(nullable: false),
                        StatusSaveDataToDatabase = c.Boolean(nullable: false),
                        MemberBHId = c.Guid(nullable: false),
                        ImportMapWorkMemberId = c.Guid(nullable: false),
                        ExceptionWorkId = c.Guid(nullable: false),
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
            
            CreateTable(
                "dbo.EdiFilesItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        User = c.String(),
                        EdiFilesId = c.Guid(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EdiFiles", t => t.EdiFilesId, cascadeDelete: true)
                .Index(t => t.EdiFilesId);
            
            CreateTable(
                "dbo.ExceptionWorkDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        ExceptionWorkId = c.Guid(nullable: false),
                        No = c.Int(nullable: false),
                        Member = c.String(),
                        Member2 = c.String(),
                        Title = c.String(),
                        Title2 = c.String(),
                        PoolName = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExceptionWorks", t => t.ExceptionWorkId, cascadeDelete: true)
                .Index(t => t.ExceptionWorkId);
            
            CreateTable(
                "dbo.ExceptionWorks",
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
            
            CreateTable(
                "dbo.ImportMapWorkMemberDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        ImportMapWorkMemberId = c.Guid(nullable: false),
                        No = c.Int(nullable: false),
                        Internal = c.String(),
                        Title = c.String(),
                        Title2 = c.String(),
                        Author = c.String(),
                        Author2 = c.String(),
                        Composer = c.String(),
                        Composer2 = c.String(),
                        Lyrics = c.String(),
                        Lyrics2 = c.String(),
                        Publisher = c.String(),
                        Publisher2 = c.String(),
                        Artistes = c.String(),
                        Artistes2 = c.String(),
                        strStatus = c.String(),
                        TotalAuthor = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportMapWorkMembers", t => t.ImportMapWorkMemberId, cascadeDelete: true)
                .Index(t => t.ImportMapWorkMemberId);
            
            CreateTable(
                "dbo.ImportMapWorkMembers",
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
            
            CreateTable(
                "dbo.MemberBHDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        MemberBHId = c.Guid(nullable: false),
                        No = c.Int(nullable: false),
                        Type = c.String(),
                        Member = c.String(),
                        MemberVN = c.String(),
                        StageName = c.String(),
                        SubMember = c.String(),
                        Beneficiary = c.String(),
                        GetPart = c.String(),
                        IsAlwaysGet = c.Boolean(nullable: false),
                        returnDate = c.DateTime(),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsGiveBeneficiary = c.Boolean(nullable: false),
                        IsCreateReport = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberBHs", t => t.MemberBHId, cascadeDelete: true)
                .Index(t => t.MemberBHId);
            
            CreateTable(
                "dbo.MemberBHs",
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
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.YoutubeTemps", t => t.YoutubeTempId, cascadeDelete: true)
                .Index(t => t.YoutubeTempId);
            
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
                        TotalScore = c.Single(nullable: false),
                        IsDetect1Vn = c.Boolean(nullable: false),
                        IsDetect2Algorithm = c.Boolean(nullable: false),
                        IsDetectAPI = c.Boolean(nullable: false),
                        IsISRC = c.Boolean(nullable: false),
                        IsLABEL = c.Boolean(nullable: false),
                        IsVn = c.Boolean(nullable: false),
                        Condition = c.Int(nullable: false),
                        Condition2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YoutubeFileId, t.Code })
                .ForeignKey("dbo.YoutubeFiles", t => t.YoutubeFileId, cascadeDelete: true)
                .Index(t => t.YoutubeFileId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.YoutubeFiles", t => t.YoutubeFile_Id)
                .Index(t => t.YoutubeFile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YoutubeFileItems", "YoutubeFileId", "dbo.YoutubeFiles");
            DropForeignKey("dbo.YoutubeFiles", "YoutubeFile_Id", "dbo.YoutubeFiles");
            DropForeignKey("dbo.YoutubeDataTemps", "YoutubeTempId", "dbo.YoutubeTemps");
            DropForeignKey("dbo.MemberBHDetails", "MemberBHId", "dbo.MemberBHs");
            DropForeignKey("dbo.ImportMapWorkMemberDetails", "ImportMapWorkMemberId", "dbo.ImportMapWorkMembers");
            DropForeignKey("dbo.ExceptionWorkDetails", "ExceptionWorkId", "dbo.ExceptionWorks");
            DropForeignKey("dbo.EdiFilesItems", "EdiFilesId", "dbo.EdiFiles");
            DropForeignKey("dbo.DistributionDataItems", "DistributionDataId", "dbo.DistributionDatas");
            DropIndex("dbo.YoutubeFiles", new[] { "YoutubeFile_Id" });
            DropIndex("dbo.YoutubeFileItems", new[] { "YoutubeFileId" });
            DropIndex("dbo.YoutubeDataTemps", new[] { "YoutubeTempId" });
            DropIndex("dbo.MemberBHDetails", new[] { "MemberBHId" });
            DropIndex("dbo.ImportMapWorkMemberDetails", new[] { "ImportMapWorkMemberId" });
            DropIndex("dbo.ExceptionWorkDetails", new[] { "ExceptionWorkId" });
            DropIndex("dbo.EdiFilesItems", new[] { "EdiFilesId" });
            DropIndex("dbo.DistributionDataItems", new[] { "DistributionDataId" });
            DropTable("dbo.YoutubeFiles");
            DropTable("dbo.YoutubeFileItems");
            DropTable("dbo.YoutubeTemps");
            DropTable("dbo.YoutubeDataTemps");
            DropTable("dbo.MemberBHs");
            DropTable("dbo.MemberBHDetails");
            DropTable("dbo.ImportMapWorkMembers");
            DropTable("dbo.ImportMapWorkMemberDetails");
            DropTable("dbo.ExceptionWorks");
            DropTable("dbo.ExceptionWorkDetails");
            DropTable("dbo.EdiFilesItems");
            DropTable("dbo.EdiFiles");
            DropTable("dbo.DistributionDatas");
            DropTable("dbo.DistributionDataItems");
            DropTable("dbo.Distibutions");
        }
    }
}
