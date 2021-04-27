namespace FitnessAmeera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastedit : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.HeightWeightCompares");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
