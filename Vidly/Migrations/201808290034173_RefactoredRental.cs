namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredRental : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "Rental_RentalId", "dbo.Rentals");
            DropIndex("dbo.Movies", new[] { "Rental_RentalId" });
            DropColumn("dbo.Movies", "Rental_RentalId");
            DropTable("dbo.Rentals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        DateRented = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(),
                    })
                .PrimaryKey(t => t.RentalId);
            
            AddColumn("dbo.Movies", "Rental_RentalId", c => c.Int());
            CreateIndex("dbo.Movies", "Rental_RentalId");
            AddForeignKey("dbo.Movies", "Rental_RentalId", "dbo.Rentals", "RentalId");
        }
    }
}
