namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategorieArticleParentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategorieActiviteParentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CategorieActivites", "CategorieArticleParente_Id", c => c.Int());
            AddColumn("dbo.CategorieActivites", "CategorieActiviteParente_Id", c => c.Int());
            CreateIndex("dbo.CategorieActivites", "CategorieArticleParente_Id");
            CreateIndex("dbo.CategorieActivites", "CategorieActiviteParente_Id");
            AddForeignKey("dbo.CategorieActivites", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes", "Id");
            AddForeignKey("dbo.CategorieActivites", "CategorieActiviteParente_Id", "dbo.CategorieActiviteParentes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategorieActivites", "CategorieActiviteParente_Id", "dbo.CategorieActiviteParentes");
            DropForeignKey("dbo.CategorieActivites", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes");
            DropIndex("dbo.CategorieActivites", new[] { "CategorieActiviteParente_Id" });
            DropIndex("dbo.CategorieActivites", new[] { "CategorieArticleParente_Id" });
            DropColumn("dbo.CategorieActivites", "CategorieActiviteParente_Id");
            DropColumn("dbo.CategorieActivites", "CategorieArticleParente_Id");
            DropTable("dbo.CategorieActiviteParentes");
            DropTable("dbo.CategorieArticleParentes");
        }
    }
}
