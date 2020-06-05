using FluentMigrator;

using SiteAvailabilityMonitoring.DataAccess.Extensions;
using SiteAvailabilityMonitoring.Entities.DbModels;

namespace SiteAvailabilityMonitoring.DataAccess.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            Execute.CreateEnum<Status>("e_status");

            Create.Table("sites")
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