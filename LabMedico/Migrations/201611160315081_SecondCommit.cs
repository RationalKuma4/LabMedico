namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondCommit : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sucursals", newName: "Sucursales");
            RenameTable(name: "dbo.Tecnicoes", newName: "Tecnicos");
            RenameTable(name: "dbo.Historicoes", newName: "Historicos");
            CreateTable(
                "dbo.Zonas",
                c => new
                    {
                        ZonaId = c.Int(nullable: false, identity: true),
                        ZonaNombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ZonaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Zonas");
            RenameTable(name: "dbo.Historicos", newName: "Historicoes");
            RenameTable(name: "dbo.Tecnicos", newName: "Tecnicoes");
            RenameTable(name: "dbo.Sucursales", newName: "Sucursals");
        }
    }
}
