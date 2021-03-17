using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOM.IS4Host.Data.Migrations.AspNetIdentity.ApplicationDb
{
    public partial class add_application_event_stores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationEventStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ActivityId = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    ProcessId = table.Column<int>(nullable: false),
                    LocalIpAddress = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEventStores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationEventStores");
        }
    }
}
