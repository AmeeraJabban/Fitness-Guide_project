namespace FitnessAmeera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticalesTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleSummary = c.String(nullable: false),
                        ArticleTitle = c.String(nullable: false),
                        ArticleDetails = c.String(nullable: false),
                        ArticleImage = c.String(),
                        ArticleDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        ArticalesTypesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticalesTypes", t => t.ArticalesTypesID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ArticalesTypesID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Age = c.String(),
                        Sex = c.String(),
                        Weight = c.Int(nullable: false),
                        Heigh = c.Int(nullable: false),
                        Status = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FitnessTips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipSummary = c.String(nullable: false),
                        TipTitle = c.String(nullable: false),
                        TipDetails = c.String(nullable: false),
                        TipStatus = c.String(nullable: false),
                        TipImage = c.String(),
                        TipDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        ArticalesTypesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticalesTypes", t => t.ArticalesTypesID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ArticalesTypesID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.HeightWeightCompares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Sex = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MySavedArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        articlesId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.articlesId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.articlesId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MySavedArticles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MySavedArticles", "articlesId", "dbo.Articles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FitnessTips", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FitnessTips", "ArticalesTypesID", "dbo.ArticalesTypes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "ArticalesTypesID", "dbo.ArticalesTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MySavedArticles", new[] { "UserId" });
            DropIndex("dbo.MySavedArticles", new[] { "articlesId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.FitnessTips", new[] { "ArticalesTypesID" });
            DropIndex("dbo.FitnessTips", new[] { "UserID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Articles", new[] { "ArticalesTypesID" });
            DropIndex("dbo.Articles", new[] { "UserID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MySavedArticles");
            DropTable("dbo.HeightWeightCompares");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FitnessTips");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticalesTypes");
        }
    }
}
