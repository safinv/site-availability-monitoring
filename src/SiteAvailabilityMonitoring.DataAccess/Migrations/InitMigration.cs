using FluentMigrator;

namespace SiteAvailabilityMonitoring.DataAccess.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            Create.Table("website")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("address").AsString().NotNullable()
                .WithColumn("available").AsBoolean().NotNullable()
                .WithColumn("status_code").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("website");
        }
    }
}