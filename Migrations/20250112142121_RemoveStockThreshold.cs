using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLittleBookNest.Migrations
{
    public partial class RemoveStockThreshold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ta bort DEFAULT-constraint kopplat till "StockThreshold"
            migrationBuilder.Sql(@"
                DECLARE @constraintName NVARCHAR(MAX);
                SELECT @constraintName = name
                FROM sys.default_constraints
                WHERE parent_object_id = OBJECT_ID('Inventory') AND parent_column_id = (
                    SELECT column_id 
                    FROM sys.columns 
                    WHERE name = 'StockThreshold' AND object_id = OBJECT_ID('Inventory')
                );

                IF @constraintName IS NOT NULL
                BEGIN
                    EXEC('ALTER TABLE [Inventory] DROP CONSTRAINT [' + @constraintName + ']');
                END
            ");

            // Ta bort kolumnen "StockThreshold" om den existerar
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 
                           FROM sys.columns 
                           WHERE name = 'StockThreshold' AND object_id = OBJECT_ID('Inventory'))
                BEGIN
                    ALTER TABLE [Inventory] DROP COLUMN [StockThreshold];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Återskapa kolumnen "StockThreshold"
            migrationBuilder.AddColumn<int>(
                name: "StockThreshold",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
