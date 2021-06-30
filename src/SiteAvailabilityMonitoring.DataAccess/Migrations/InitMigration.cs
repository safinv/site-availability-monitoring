using FluentMigrator;

using SiteAvailabilityMonitoring.DataAccess.Extensions;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.DataAccess.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            Execute.CreateEnum<DbStatus>("e_status");

            Create.Table("websites")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("address").AsString().NotNullable()
                .WithColumn("status").AsCustom("e_status").Nullable();
        }

        public override void Down()
        {
            Delete.Table("sites");
        }
    }
}