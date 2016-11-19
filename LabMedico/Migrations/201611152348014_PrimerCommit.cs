namespace LabMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimerCommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Nombre = c.String(),
                        ApellidoPaterno = c.String(),
                        ApellidoMaterno = c.String(),
                        Calle = c.String(),
                        NumeroInterior = c.String(),
                        NumeroExterior = c.String(),
                        Colonia = c.String(),
                        DelegacionMunicipio = c.String(),
                        CodigoPostal = c.Int(),
                        Estado = c.String(),
                        Edad = c.Int(),
                        Sexo = c.String(),
                        Notas = c.String(),
                        SucursalId = c.Int(nullable: false),
                        Estatus = c.String(),
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
                .ForeignKey("dbo.Sucursals", t => t.SucursalId, cascadeDelete: true)
                .Index(t => t.SucursalId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        FechaRegistro = c.DateTime(nullable: false),
                        FechaEntrega = c.DateTime(nullable: false),
                        FechaAplicacion = c.DateTime(nullable: false),
                        HoraAplicacion = c.String(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        AnalisisId = c.Int(nullable: false),
                        TecnicoId = c.Int(nullable: false),
                        Estatus = c.String(nullable: false, maxLength: 3),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Analisis_ServicioId = c.Int(),
                        Usuarios_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.Tecnicoes", t => t.TecnicoId, cascadeDelete: true)
                .ForeignKey("dbo.Analisis", t => t.Analisis_ServicioId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuarios_Id)
                .Index(t => t.ClienteId)
                .Index(t => t.TecnicoId)
                .Index(t => t.Analisis_ServicioId)
                .Index(t => t.Usuarios_Id);
            
            CreateTable(
                "dbo.Analisis",
                c => new
                    {
                        ServicioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        Requisitos = c.String(nullable: false, maxLength: 500),
                        Estatus = c.String(nullable: false, maxLength: 3),
                        CategoriaId = c.Int(nullable: false),
                        Estudios_EstudioId = c.Int(),
                    })
                .PrimaryKey(t => t.ServicioId)
                .ForeignKey("dbo.Estudios", t => t.Estudios_EstudioId)
                .Index(t => t.Estudios_EstudioId);
            
            CreateTable(
                "dbo.AnalisisSucursals",
                c => new
                    {
                        AnalisisSucursalId = c.Int(nullable: false, identity: true),
                        SucursalId = c.Int(nullable: false),
                        AnalisisId = c.Int(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estatus = c.String(nullable: false, maxLength: 3),
                        Analisis_ServicioId = c.Int(),
                    })
                .PrimaryKey(t => t.AnalisisSucursalId)
                .ForeignKey("dbo.Analisis", t => t.Analisis_ServicioId)
                .ForeignKey("dbo.Sucursals", t => t.SucursalId, cascadeDelete: true)
                .Index(t => t.SucursalId)
                .Index(t => t.Analisis_ServicioId);
            
            CreateTable(
                "dbo.Sucursals",
                c => new
                    {
                        SucursalId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Calle = c.String(nullable: false, maxLength: 100),
                        NumeroInterior = c.String(nullable: false, maxLength: 100),
                        NumeroExterior = c.String(nullable: false, maxLength: 100),
                        Colonia = c.String(nullable: false, maxLength: 100),
                        DelegacionMunicipio = c.String(nullable: false, maxLength: 100),
                        CodigoPostal = c.String(nullable: false, maxLength: 100),
                        Telefono = c.String(nullable: false, maxLength: 100),
                        Horario = c.String(nullable: false, maxLength: 100),
                        Estatus = c.String(nullable: false, maxLength: 3),
                        ZonaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SucursalId);
            
            CreateTable(
                "dbo.Tecnicoes",
                c => new
                    {
                        TecnicoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        ApellidoPaterno = c.String(nullable: false, maxLength: 100),
                        ApellidoMaterno = c.String(nullable: false, maxLength: 100),
                        Calle = c.String(nullable: false, maxLength: 100),
                        NumeroInterior = c.String(nullable: false, maxLength: 100),
                        NumeroExterior = c.String(nullable: false, maxLength: 100),
                        Colonia = c.String(nullable: false, maxLength: 100),
                        DelegacionMunicipio = c.String(nullable: false, maxLength: 100),
                        CodigoPostal = c.Int(nullable: false),
                        Estado = c.String(nullable: false, maxLength: 100),
                        Edad = c.Int(nullable: false),
                        Sexo = c.String(nullable: false, maxLength: 1),
                        Notas = c.String(),
                        Estatus = c.String(nullable: false, maxLength: 3),
                        SucursalId = c.Int(nullable: false),
                        EstudioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TecnicoId)
                .ForeignKey("dbo.Estudios", t => t.EstudioId, cascadeDelete: true)
                .ForeignKey("dbo.Sucursals", t => t.SucursalId, cascadeDelete: true)
                .Index(t => t.SucursalId)
                .Index(t => t.EstudioId);
            
            CreateTable(
                "dbo.Estudios",
                c => new
                    {
                        EstudioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false, maxLength: 100),
                        Estatus = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.EstudioId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        ApellidoPaterno = c.String(nullable: false, maxLength: 100),
                        ApellidoMaterno = c.String(nullable: false, maxLength: 100),
                        Telefono = c.String(nullable: false, maxLength: 100),
                        Celular = c.String(nullable: false, maxLength: 100),
                        Calle = c.String(nullable: false, maxLength: 100),
                        NumeroInterior = c.String(nullable: false, maxLength: 10),
                        NumeroExterior = c.String(nullable: false, maxLength: 10),
                        Colonia = c.String(nullable: false, maxLength: 100),
                        DelegacionMunicipio = c.String(nullable: false, maxLength: 100),
                        CodigoPostal = c.Int(nullable: false),
                        Sexo = c.String(nullable: false, maxLength: 1),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Edad = c.Int(nullable: false),
                        Estatus = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Historicoes",
                c => new
                    {
                        HistoricoId = c.Int(nullable: false, identity: true),
                        CitaId = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.HistoricoId)
                .ForeignKey("dbo.Citas", t => t.CitaId, cascadeDelete: true)
                .Index(t => t.CitaId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                        UserId = c.Int(nullable: false),
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
            DropForeignKey("dbo.Citas", "Usuarios_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Historicoes", "CitaId", "dbo.Citas");
            DropForeignKey("dbo.Citas", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Citas", "Analisis_ServicioId", "dbo.Analisis");
            DropForeignKey("dbo.AspNetUsers", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.Tecnicoes", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.Tecnicoes", "EstudioId", "dbo.Estudios");
            DropForeignKey("dbo.Analisis", "Estudios_EstudioId", "dbo.Estudios");
            DropForeignKey("dbo.Citas", "TecnicoId", "dbo.Tecnicoes");
            DropForeignKey("dbo.AnalisisSucursals", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.AnalisisSucursals", "Analisis_ServicioId", "dbo.Analisis");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Historicoes", new[] { "CitaId" });
            DropIndex("dbo.Tecnicoes", new[] { "EstudioId" });
            DropIndex("dbo.Tecnicoes", new[] { "SucursalId" });
            DropIndex("dbo.AnalisisSucursals", new[] { "Analisis_ServicioId" });
            DropIndex("dbo.AnalisisSucursals", new[] { "SucursalId" });
            DropIndex("dbo.Analisis", new[] { "Estudios_EstudioId" });
            DropIndex("dbo.Citas", new[] { "Usuarios_Id" });
            DropIndex("dbo.Citas", new[] { "Analisis_ServicioId" });
            DropIndex("dbo.Citas", new[] { "TecnicoId" });
            DropIndex("dbo.Citas", new[] { "ClienteId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "SucursalId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Historicoes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Estudios");
            DropTable("dbo.Tecnicoes");
            DropTable("dbo.Sucursals");
            DropTable("dbo.AnalisisSucursals");
            DropTable("dbo.Analisis");
            DropTable("dbo.Citas");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
