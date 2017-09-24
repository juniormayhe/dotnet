namespace Ornithology.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TONT_AVES",
                c => new
                    {
                        CDAVE = c.String(nullable: false, maxLength: 5),
                        DSNOMBRE_COMUN = c.String(maxLength: 100),
                        DSNOMBRE_CIENTIFICO = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CDAVE);
            
            CreateTable(
                "dbo.TONT_AVES_PAIS",
                c => new
                    {
                        CDPAIS = c.String(nullable: false, maxLength: 5),
                        CDAVE = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.CDPAIS, t.CDAVE })
                .ForeignKey("dbo.TONT_AVES", t => t.CDAVE, cascadeDelete: true)
                .ForeignKey("dbo.TONT_PAISES", t => t.CDPAIS, cascadeDelete: true)
                .Index(t => t.CDPAIS)
                .Index(t => t.CDAVE);
            
            CreateTable(
                "dbo.TONT_PAISES",
                c => new
                    {
                        CDPAIS = c.String(nullable: false, maxLength: 5),
                        DSNOMBRE = c.String(maxLength: 100),
                        CDZONA = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.CDPAIS)
                .ForeignKey("dbo.TONT_ZONAS", t => t.CDZONA)
                .Index(t => t.CDZONA);
            
            CreateTable(
                "dbo.TONT_ZONAS",
                c => new
                    {
                        CDZONA = c.String(nullable: false, maxLength: 3),
                        DSNOMBRE = c.String(maxLength: 45),
                    })
                .PrimaryKey(t => t.CDZONA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TONT_AVES_PAIS", "CDPAIS", "dbo.TONT_PAISES");
            DropForeignKey("dbo.TONT_PAISES", "CDZONA", "dbo.TONT_ZONAS");
            DropForeignKey("dbo.TONT_AVES_PAIS", "CDAVE", "dbo.TONT_AVES");
            DropIndex("dbo.TONT_PAISES", new[] { "CDZONA" });
            DropIndex("dbo.TONT_AVES_PAIS", new[] { "CDAVE" });
            DropIndex("dbo.TONT_AVES_PAIS", new[] { "CDPAIS" });
            DropTable("dbo.TONT_ZONAS");
            DropTable("dbo.TONT_PAISES");
            DropTable("dbo.TONT_AVES_PAIS");
            DropTable("dbo.TONT_AVES");
        }
    }
}
