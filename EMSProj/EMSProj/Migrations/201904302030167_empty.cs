namespace EMSProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empty : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into AspNetRoles(Id, Name) Values(1,'Admin')");
            Sql("Insert Into AspNetRoles(Id, Name) Values(2,'Employee')");
        }
        
        public override void Down()
        {
        }
    }
}
