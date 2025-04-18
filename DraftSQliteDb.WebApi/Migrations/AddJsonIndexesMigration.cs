using Microsoft.EntityFrameworkCore.Migrations;

namespace DraftSQliteDb.WebApi.Migrations;

public class AddJsonIndexesMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(@"
        //                    CREATE INDEX IF NOT EXISTS IX_Authors_Contact_City
        //                    ON Authors ( json_extract(Contact, '$.Address.City') 
        //                    )");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(@"DROP INDEX IF EXISTS IX_Authors_Contact_City;");
    }
}
