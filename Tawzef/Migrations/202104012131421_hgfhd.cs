namespace Tawzef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hgfhd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String());
            AlterColumn("dbo.Jobs", "JobContent", c => c.String());
            AlterColumn("dbo.Jobs", "JobImage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobImage", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "JobContent", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "JobTitle", c => c.Int(nullable: false));
        }
    }
}
