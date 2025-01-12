using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLittleBookNest.Migrations
{
    public partial class UpdateInventoryISBN13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ta bort DEFAULT-constraint kopplat till "StockLevel"
            migrationBuilder.Sql(@"
                DECLARE @constraintName NVARCHAR(MAX);
                SELECT @constraintName = name
                FROM sys.default_constraints
                WHERE parent_object_id = OBJECT_ID('Inventory') AND parent_column_id = (
                    SELECT column_id 
                    FROM sys.columns 
                    WHERE name = 'StockLevel' AND object_id = OBJECT_ID('Inventory')
                );

                IF @constraintName IS NOT NULL
                BEGIN
                    EXEC('ALTER TABLE [Inventory] DROP CONSTRAINT [' + @constraintName + ']');
                END
            ");

            // Ta bort kolumnen "StockLevel" om den existerar
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 
                           FROM sys.columns 
                           WHERE name = 'StockLevel' AND object_id = OBJECT_ID('Inventory'))
                BEGIN
                    ALTER TABLE [Inventory] DROP COLUMN [StockLevel];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Återskapa kolumnen "StockLevel"
            migrationBuilder.AddColumn<int>(
                name: "StockLevel",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
