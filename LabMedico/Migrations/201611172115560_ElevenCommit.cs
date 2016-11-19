namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElevenCommit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "SucursalId", "dbo.Sucursales");
            DropIndex("dbo.AspNetUsers", new[] { "SucursalId" });
            AlterColumn("dbo.AspNetUsers", "NumeroInterior", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "NumeroExterior", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "SucursalId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SucursalId");
            AddForeignKey("dbo.AspNetUsers", "SucursalId", "dbo.Sucursales", "SucursalId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SucursalId", "dbo.Sucursales");
            DropIndex("dbo.AspNetUsers", new[] { "SucursalId" });
            AlterColumn("dbo.AspNetUsers", "SucursalId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NumeroExterior", c => c.String());
            AlterColumn("dbo.AspNetUsers", "NumeroInterior", c => c.String());
            CreateIndex("dbo.AspNetUsers", "SucursalId");
            AddForeignKey("dbo.AspNetUsers", "SucursalId", "dbo.Sucursales", "SucursalId", cascadeDelete: true);
        }
    }
}
