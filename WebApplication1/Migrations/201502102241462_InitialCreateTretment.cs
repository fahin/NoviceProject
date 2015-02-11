namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateTretment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TreatmentServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VotersID = c.String(),
                        Dose = c.String(),
                        BeforAfter = c.String(),
                        QtyGiven = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TreatmentServices");
        }
    }
}
