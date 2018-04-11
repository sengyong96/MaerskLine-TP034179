namespace MaerskLine_TP034179.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newSeedUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'1ac70ad8-c51d-447b-9852-ac2865474ce2', N'admin@maersk.com', 0, N'ABR3MKCYuaKee+Bf+KajI0WJoY9YQz6mGwkr2HVOjXMBtrNIyswCZH8frCATJVxGeg==', N'f9bdc682-1d5c-497f-959d-4dbe9472dc94', NULL, 0, 0, NULL, 1, 0, N'admin@maersk.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3440d229-f5d4-4da7-867d-b1ec6c09efad', N'Admin')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1ac70ad8-c51d-447b-9852-ac2865474ce2', N'3440d229-f5d4-4da7-867d-b1ec6c09efad')
            ");

        }

    public override void Down()
        {
        }
    }
}
