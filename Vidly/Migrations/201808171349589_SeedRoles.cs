namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'24c59219-f369-4b57-a0d1-fd4971b780bb', N'movie@test.com', 0, N'AHry91v08OLW/6itJm/VpTfszZ8OoQ8zl75Xgvl/7T8Ws9M01G1J8Ipvmekovg+fMA==', N'f9b48ed9-801c-4746-90a5-892b8a969206', NULL, 0, 0, NULL, 1, 0, N'movie@test.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f7207695-32a6-4a2b-8865-179746f53188', N'guest@test.com', 0, N'AMYY3wIscVqyyMtcVoPHjM6WOUxMACP12usMgDgqFsOQoo88gLWwoVBAMnho5OVS4Q==', N'ce50fa86-55af-4745-9097-f3a16df6866c', NULL, 0, 0, NULL, 1, 0, N'guest@test.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a7c6dd19-56b9-4bfa-bd0d-959fe9bcdf6c', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'24c59219-f369-4b57-a0d1-fd4971b780bb', N'a7c6dd19-56b9-4bfa-bd0d-959fe9bcdf6c')
            ");

        }
        
        public override void Down()
        {
        }
    }
}
