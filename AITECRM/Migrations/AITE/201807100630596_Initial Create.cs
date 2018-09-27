namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projets", "EtatProjet_Id", "dbo.EtatProjets");
            DropIndex("dbo.Projets", new[] { "EtatProjet_Id" });
            AlterColumn("dbo.Projets", "EtatProjet_", c => c.String());
            DropColumn("dbo.Projets", "EtatProjet_Id");
            DropTable("dbo.EtatProjets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EtatProjets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomEtat = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projets", "EtatProjet_Id", c => c.Int());
            AlterColumn("dbo.Projets", "EtatProjet_", c => c.Int(nullable: false));
            CreateIndex("dbo.Projets", "EtatProjet_Id");
            AddForeignKey("dbo.Projets", "EtatProjet_Id", "dbo.EtatProjets", "Id");
        }
    }
}
