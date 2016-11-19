namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FithCommit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Analisis", "Descripcion", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Analisis", "Requisitos", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Analisis", "Requisitos", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Analisis", "Descripcion", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
