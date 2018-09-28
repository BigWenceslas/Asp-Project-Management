namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CategorieArticles", name: "CategorieArticleParente_Id", newName: "CategorieAr_Id");
            RenameColumn(table: "dbo.CategorieActivites", name: "CategorieActiviteParente_Id", newName: "CategoriePar_Id");
            RenameIndex(table: "dbo.CategorieActivites", name: "IX_CategorieActiviteParente_Id", newName: "IX_CategoriePar_Id");
            RenameIndex(table: "dbo.CategorieArticles", name: "IX_CategorieArticleParente_Id", newName: "IX_CategorieAr_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CategorieArticles", name: "IX_CategorieAr_Id", newName: "IX_CategorieArticleParente_Id");
            RenameIndex(table: "dbo.CategorieActivites", name: "IX_CategoriePar_Id", newName: "IX_CategorieActiviteParente_Id");
            RenameColumn(table: "dbo.CategorieActivites", name: "CategoriePar_Id", newName: "CategorieActiviteParente_Id");
            RenameColumn(table: "dbo.CategorieArticles", name: "CategorieAr_Id", newName: "CategorieArticleParente_Id");
        }
    }
}
