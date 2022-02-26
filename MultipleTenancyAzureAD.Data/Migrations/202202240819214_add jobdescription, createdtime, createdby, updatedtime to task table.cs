namespace MultipleTenancyAzureAD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobdescriptioncreatedtimecreatedbyupdatedtimetotasktable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "JobDescription", c => c.String());
            AddColumn("dbo.Tasks", "CreatedBy", c => c.String());
            AddColumn("dbo.Tasks", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "UpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "UpdateTime");
            DropColumn("dbo.Tasks", "CreatedTime");
            DropColumn("dbo.Tasks", "CreatedBy");
            DropColumn("dbo.Tasks", "JobDescription");
        }
    }
}
