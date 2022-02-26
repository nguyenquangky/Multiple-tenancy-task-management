namespace MultipleTenancyAzureAD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeupdatedtimebenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "UpdateTime", c => c.DateTime(nullable: false));
        }
    }
}
