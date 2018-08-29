namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRentalTable : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Rental_RentalId", "dbo.Rentals");
            DropIndex("dbo.Movies", new[] { "Rental_RentalId" });
            DropColumn("dbo.Movies", "Rental_RentalId");
            DropTable("dbo.Rentals");
        }
    }
}
