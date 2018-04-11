namespace MaerskLine_TP034179.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingAgent = c.String(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ShipID = c.Int(nullable: false),
                        ScheduleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleID, cascadeDelete: true)
                .ForeignKey("dbo.Ships", t => t.ShipID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.ShipID)
                .Index(t => t.ScheduleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ShipID", "dbo.Ships");
            DropForeignKey("dbo.Bookings", "ScheduleID", "dbo.Schedules");
            DropForeignKey("dbo.Bookings", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "ScheduleID" });
            DropIndex("dbo.Bookings", new[] { "ShipID" });
            DropIndex("dbo.Bookings", new[] { "CustomerID" });
            DropTable("dbo.Bookings");
        }
    }
}
