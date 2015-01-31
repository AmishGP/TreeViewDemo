namespace TreeViewDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initializeImageCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Image");
        }
    }
}
