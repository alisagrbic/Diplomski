namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        DateOfBirth = c.DateTime(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                        Approved = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.CreatorID, cascadeDelete: true)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.BranchOffices",
                c => new
                    {
                        BranchOfficeID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        Image = c.String(),
                        ServiceID = c.Int(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BranchOfficeID)
                .ForeignKey("dbo.AppUsers", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Text = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.AppUsers", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RateID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RateID)
                .ForeignKey("dbo.AppUsers", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VehicleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleTypeId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        Model = c.String(),
                        Producer = c.String(),
                        ProductionYear = c.Int(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Available = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        PriceVehicle = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.AppUsers", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.ServiceId)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeId = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        VehicleID = c.Int(nullable: false),
                        GetVehicleDate = c.DateTime(nullable: false),
                        ReturnVehicleDate = c.DateTime(nullable: false),
                        GetBranchId = c.Int(nullable: false),
                        ReturnBranchId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.BranchOffices", t => t.GetBranchId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.VehicleID)
                .Index(t => t.GetBranchId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AppUserId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .Index(t => t.AppUserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Reservations", "ClientID", "dbo.AppUsers");
            DropForeignKey("dbo.Reservations", "GetBranchId", "dbo.BranchOffices");
            DropForeignKey("dbo.Items", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Vehicles", "CreatorID", "dbo.AppUsers");
            DropForeignKey("dbo.Rates", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Rates", "ClientID", "dbo.AppUsers");
            DropForeignKey("dbo.Services", "CreatorID", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Comments", "ClientID", "dbo.AppUsers");
            DropForeignKey("dbo.BranchOffices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.BranchOffices", "CreatorID", "dbo.AppUsers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AppUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reservations", new[] { "GetBranchId" });
            DropIndex("dbo.Reservations", new[] { "VehicleID" });
            DropIndex("dbo.Reservations", new[] { "ClientID" });
            DropIndex("dbo.Vehicles", new[] { "CreatorID" });
            DropIndex("dbo.Vehicles", new[] { "ServiceId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropIndex("dbo.Items", new[] { "VehicleID" });
            DropIndex("dbo.Rates", new[] { "ServiceID" });
            DropIndex("dbo.Rates", new[] { "ClientID" });
            DropIndex("dbo.Comments", new[] { "ServiceID" });
            DropIndex("dbo.Comments", new[] { "ClientID" });
            DropIndex("dbo.BranchOffices", new[] { "CreatorID" });
            DropIndex("dbo.BranchOffices", new[] { "ServiceID" });
            DropIndex("dbo.Services", new[] { "CreatorID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reservations");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Items");
            DropTable("dbo.Rates");
            DropTable("dbo.Comments");
            DropTable("dbo.BranchOffices");
            DropTable("dbo.Services");
            DropTable("dbo.AppUsers");
        }
    }
}
