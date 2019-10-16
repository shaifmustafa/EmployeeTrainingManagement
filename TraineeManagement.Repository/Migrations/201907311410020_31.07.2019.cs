namespace TraineeManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31072019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvanceDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceId = c.Long(nullable: false),
                        PurposeId = c.Long(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advances", t => t.AdvanceId, cascadeDelete: true)
                .ForeignKey("dbo.Purposes", t => t.PurposeId, cascadeDelete: true)
                .Index(t => t.AdvanceId)
                .Index(t => t.PurposeId);
            
            CreateTable(
                "dbo.Advances",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceType = c.String(),
                        MemoNo = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        EmployeeId = c.Long(nullable: false),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubDistrictId = c.Long(),
                        DistrictId = c.Long(),
                        Trainer = c.String(),
                        Topic = c.String(),
                        AdvanceStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.SubDistricts", t => t.SubDistrictId)
                .Index(t => t.EmployeeId)
                .Index(t => t.SubDistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        GradeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: true)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Grades = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubDistricts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DistrictId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Purposes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Billeds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MemoNo = c.String(),
                        BillDate = c.DateTime(nullable: false),
                        Vendor = c.String(),
                        Description = c.String(),
                        BillStatus = c.String(),
                        AdvanceId = c.Long(nullable: false),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advances", t => t.AdvanceId, cascadeDelete: true)
                .Index(t => t.AdvanceId);
            
            CreateTable(
                "dbo.BilledDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BilledId = c.Long(nullable: false),
                        PurposeId = c.Long(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Billeds", t => t.BilledId, cascadeDelete: true)
                .ForeignKey("dbo.Purposes", t => t.PurposeId, cascadeDelete: true)
                .Index(t => t.BilledId)
                .Index(t => t.PurposeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BilledDetails", "PurposeId", "dbo.Purposes");
            DropForeignKey("dbo.BilledDetails", "BilledId", "dbo.Billeds");
            DropForeignKey("dbo.Billeds", "AdvanceId", "dbo.Advances");
            DropForeignKey("dbo.AdvanceDetails", "PurposeId", "dbo.Purposes");
            DropForeignKey("dbo.Advances", "SubDistrictId", "dbo.SubDistricts");
            DropForeignKey("dbo.SubDistricts", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Advances", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Advances", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.AdvanceDetails", "AdvanceId", "dbo.Advances");
            DropIndex("dbo.BilledDetails", new[] { "PurposeId" });
            DropIndex("dbo.BilledDetails", new[] { "BilledId" });
            DropIndex("dbo.Billeds", new[] { "AdvanceId" });
            DropIndex("dbo.SubDistricts", new[] { "DistrictId" });
            DropIndex("dbo.Employees", new[] { "GradeId" });
            DropIndex("dbo.Advances", new[] { "DistrictId" });
            DropIndex("dbo.Advances", new[] { "SubDistrictId" });
            DropIndex("dbo.Advances", new[] { "EmployeeId" });
            DropIndex("dbo.AdvanceDetails", new[] { "PurposeId" });
            DropIndex("dbo.AdvanceDetails", new[] { "AdvanceId" });
            DropTable("dbo.BilledDetails");
            DropTable("dbo.Billeds");
            DropTable("dbo.Purposes");
            DropTable("dbo.SubDistricts");
            DropTable("dbo.Grades");
            DropTable("dbo.Employees");
            DropTable("dbo.Districts");
            DropTable("dbo.Advances");
            DropTable("dbo.AdvanceDetails");
        }
    }
}
