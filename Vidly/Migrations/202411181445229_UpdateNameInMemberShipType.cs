namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNameInMemberShipType : DbMigration
    {
        public override void Up()
        {
            Sql("Update MembershipTypes Set Name = 'Pay As You Go' Where Id = 1");
            Sql("Update MembershipTypes Set Name = 'Weekly' Where Id = 2");
            Sql("Update MembershipTypes Set Name = 'Monthly' Where Id = 3");
            Sql("Update MembershipTypes Set Name = 'Yearly' Where Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
