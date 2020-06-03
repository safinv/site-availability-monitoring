using FluentMigrator;

namespace SiteAvailabilityMonitoring.DataAccess.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            Create.Table("sites")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("address").AsString().NotNullable()
                .WithColumn("status").AsBoolean();
        }

        public override void Down()
        {
            Delete.Table("sites");
        }
    }
}