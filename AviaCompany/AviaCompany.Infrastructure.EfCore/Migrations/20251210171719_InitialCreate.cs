using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AviaCompany.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AircraftFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manufacturer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftFamilies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassportNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AircraftModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    Range = table.Column<double>(type: "double", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    CargoCapacity = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AircraftModels_AircraftFamilies_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "AircraftFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureCity = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalCity = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureTime = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    FlightDuration = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    AircraftModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_AircraftModels_AircraftModelId",
                        column: x => x.AircraftModelId,
                        principalTable: "AircraftModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasHandLuggage = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LuggageWeight = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AircraftFamilies",
                columns: new[] { "Id", "Manufacturer", "Name" },
                values: new object[,]
                {
                    { 1, "Airbus", "A320" },
                    { 2, "Boeing", "737" },
                    { 3, "Sukhoi", "Superjet 100" },
                    { 4, "Irkut", "MS-21" },
                    { 5, "Bombardier", "CRJ" },
                    { 6, "Airbus", "A330" },
                    { 7, "Boeing", "777" },
                    { 8, "Airbus", "A350" },
                    { 9, "Boeing", "787" },
                    { 10, "Embraer", "Embraer E-Jet" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "BirthDate", "FullName", "PassportNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иванов Иван Иванович", "45 01 123456" },
                    { 2, new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петров Петр Петрович", "45 02 234567" },
                    { 3, new DateTime(1988, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сидорова Анна Владимировна", "45 03 345678" },
                    { 4, new DateTime(1992, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козлов Дмитрий Сергеевич", "45 04 456789" },
                    { 5, new DateTime(1987, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Николаева Екатерина Андреевна", "45 05 567890" },
                    { 6, new DateTime(1995, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Федоров Алексей Викторович", "45 06 678901" },
                    { 7, new DateTime(1991, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Морозова Ольга Дмитриевна", "45 07 789012" },
                    { 8, new DateTime(1983, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волков Сергей Иванович", "45 08 890123" },
                    { 9, new DateTime(1993, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Павлова Мария Петровна", "45 09 901234" },
                    { 10, new DateTime(1989, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Семенов Андрей Николаевич", "45 10 012345" }
                });

            migrationBuilder.InsertData(
                table: "AircraftModels",
                columns: new[] { "Id", "CargoCapacity", "FamilyId", "Name", "PassengerCapacity", "Range" },
                values: new object[,]
                {
                    { 1, 20000.0, 1, "A320-200", 180, 6100.0 },
                    { 2, 25000.0, 1, "A321neo", 240, 7400.0 },
                    { 3, 22000.0, 2, "737-800", 189, 5765.0 },
                    { 4, 23000.0, 2, "737 MAX 8", 210, 6570.0 },
                    { 5, 12000.0, 3, "SSJ-100", 98, 3048.0 },
                    { 6, 24000.0, 4, "MS-21-300", 211, 6000.0 },
                    { 7, 8000.0, 5, "CRJ-200", 50, 3045.0 },
                    { 8, 18000.0, 1, "A319", 160, 6850.0 },
                    { 9, 19000.0, 2, "737-700", 149, 6230.0 },
                    { 10, 15000.0, 5, "CRJ-900", 90, 3400.0 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AircraftModelId", "ArrivalCity", "ArrivalDate", "Code", "DepartureCity", "DepartureDate", "DepartureTime", "FlightDuration" },
                values: new object[,]
                {
                    { 1, 1, "Санкт-Петербург", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1001", "Москва", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 1, 30, 0, 0) },
                    { 2, 2, "Сочи", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1002", "Москва", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 2, 30, 0, 0) },
                    { 3, 3, "Екатеринбург", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1003", "Москва", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 2, 15, 0, 0) },
                    { 4, 4, "Новосибирск", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1004", "Москва", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 30, 0, 0), new TimeSpan(0, 4, 0, 0, 0) },
                    { 5, 6, "Владивосток", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1005", "Москва", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 30, 0, 0) },
                    { 6, 1, "Москва", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1006", "Санкт-Петербург", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 1, 30, 0, 0) },
                    { 7, 5, "Сочи", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1007", "Санкт-Петербург", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 3, 0, 0, 0) },
                    { 8, 2, "Москва", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1008", "Сочи", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 2, 30, 0, 0) },
                    { 9, 3, "Москва", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1009", "Екатеринбург", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 30, 0, 0), new TimeSpan(0, 2, 15, 0, 0) },
                    { 10, 7, "Санкт-Петербург", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU1010", "Новосибирск", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 4, 30, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "FlightId", "HasHandLuggage", "LuggageWeight", "PassengerId", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, true, 15.5m, 1, "10A" },
                    { 2, 1, true, 10.0m, 2, "10B" },
                    { 3, 1, false, 0m, 3, "11A" },
                    { 4, 1, true, 8.0m, 4, "11B" },
                    { 5, 1, true, 0m, 5, "12A" },
                    { 6, 1, true, 12.0m, 6, "12B" },
                    { 7, 1, false, 0m, 7, "13A" },
                    { 8, 1, true, 9.5m, 8, "13B" },
                    { 9, 2, true, 20.0m, 9, "15C" },
                    { 10, 2, true, 12.5m, 10, "15D" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AircraftModels_FamilyId",
                table: "AircraftModels",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftModelId",
                table: "Flights",
                column: "AircraftModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "AircraftModels");

            migrationBuilder.DropTable(
                name: "AircraftFamilies");
        }
    }
}
