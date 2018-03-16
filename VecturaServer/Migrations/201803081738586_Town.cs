namespace VecturaServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Town : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Town", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Town");
        }
    }
}
