namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdCommit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estudios", "Descripcion", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudios", "Descripcion", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
