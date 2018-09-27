namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategorieActivites", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes");
            DropIndex("dbo.CategorieActivites", new[] { "CategorieArticleParente_Id" });
            AddColumn("dbo.CategorieArticles", "CategorieArticleParente_Id", c => c.Int());
            CreateIndex("dbo.CategorieArticles", "CategorieArticleParente_Id");
            AddForeignKey("dbo.CategorieArticles", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes", "Id");
            DropColumn("dbo.CategorieActivites", "CategorieArticleParente_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategorieActivites", "CategorieArticleParente_Id", c => c.Int());
            DropForeignKey("dbo.CategorieArticles", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes");
            DropIndex("dbo.CategorieArticles", new[] { "CategorieArticleParente_Id" });
            DropColumn("dbo.CategorieArticles", "CategorieArticleParente_Id");
            CreateIndex("dbo.CategorieActivites", "CategorieArticleParente_Id");
            AddForeignKey("dbo.CategorieActivites", "CategorieArticleParente_Id", "dbo.CategorieArticleParentes", "Id");
        }
    }
}
