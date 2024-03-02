namespace Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitNewTTT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleDetail",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("dbo.SaleMasters", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.SaleMasters",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        CustomeName = c.String(nullable: false),
                        Gender = c.String(),
                        Type = c.String(),
                        Photo = c.String(),
                        date = c.DateTime(),
                    })
                .PrimaryKey(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetail", "SaleId", "dbo.SaleMasters");
            DropIndex("dbo.SaleDetail", new[] { "SaleId" });
            DropTable("dbo.SaleMasters");
            DropTable("dbo.SaleDetail");
        }
    }
}
