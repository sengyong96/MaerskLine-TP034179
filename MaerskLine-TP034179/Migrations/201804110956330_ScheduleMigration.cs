namespace MaerskLine_TP034179.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        Origin = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        DepartureDateTime = c.DateTime(nullable: false),
                        ArrivalDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schedules");
        }
    }
}
