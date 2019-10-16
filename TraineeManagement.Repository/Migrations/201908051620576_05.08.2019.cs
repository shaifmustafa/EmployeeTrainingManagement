namespace TraineeManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05082019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjusts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BillingId = c.Long(nullable: false),
                        BillTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvanceId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Billeds", t => t.BillingId, cascadeDelete: true)
                .Index(t => t.BillingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adjusts", "BillingId", "dbo.Billeds");
            DropIndex("dbo.Adjusts", new[] { "BillingId" });
            DropTable("dbo.Adjusts");
        }
    }
}
