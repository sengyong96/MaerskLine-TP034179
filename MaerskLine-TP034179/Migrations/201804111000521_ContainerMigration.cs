namespace MaerskLine_TP034179.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContainerMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        ContainerID = c.Int(nullable: false, identity: true),
                        ContainerType = c.String(nullable: false),
                        ContainerWeight = c.Double(nullable: false),
                        BookingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContainerID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .Index(t => t.BookingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Containers", "BookingID", "dbo.Bookings");
            DropIndex("dbo.Containers", new[] { "BookingID" });
            DropTable("dbo.Containers");
        }
    }
}
