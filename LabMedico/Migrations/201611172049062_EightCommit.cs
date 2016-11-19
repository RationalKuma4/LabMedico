namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EightCommit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sucursales", "NumeroInterior", c => c.Int(nullable: false));
            AlterColumn("dbo.Sucursales", "NumeroExterior", c => c.Int(nullable: false));
            AlterColumn("dbo.Sucursales", "CodigoPostal", c => c.Int(nullable: false));
            AlterColumn("dbo.Tecnicos", "NumeroInterior", c => c.Int(nullable: false));
            AlterColumn("dbo.Tecnicos", "NumeroExterior", c => c.Int(nullable: false));
            AlterColumn("dbo.Clientes", "NumeroInterior", c => c.Int(nullable: false));
            AlterColumn("dbo.Clientes", "NumeroExterior", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "NumeroExterior", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Clientes", "NumeroInterior", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Tecnicos", "NumeroExterior", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tecnicos", "NumeroInterior", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Sucursales", "CodigoPostal", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Sucursales", "NumeroExterior", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Sucursales", "NumeroInterior", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
