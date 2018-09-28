namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategorieActivites", "CategorieParente_", c => c.Int(nullable: false));
            AddColumn("dbo.CategorieArticles", "CategorieParente_", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategorieArticles", "CategorieParente_");
            DropColumn("dbo.CategorieActivites", "CategorieParente_");
        }
    }
}
