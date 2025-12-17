using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviaCompany.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNamesToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_AircraftFamilies_FamilyId",
                table: "AircraftModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_AircraftModels_AircraftModelId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AircraftModels",
                table: "AircraftModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AircraftFamilies",
                table: "AircraftFamilies");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "tickets");

            migrationBuilder.RenameTable(
                name: "Passengers",
                newName: "passengers");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "flights");

            migrationBuilder.RenameTable(
                name: "AircraftModels",
                newName: "aircraft_models");

            migrationBuilder.RenameTable(
                name: "AircraftFamilies",
                newName: "aircraft_families");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tickets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "tickets",
                newName: "seat_number");

            migrationBuilder.RenameColumn(
                name: "PassengerId",
                table: "tickets",
                newName: "passenger_id");

            migrationBuilder.RenameColumn(
                name: "LuggageWeight",
                table: "tickets",
                newName: "luggage_weight");

            migrationBuilder.RenameColumn(
                name: "HasHandLuggage",
                table: "tickets",
                newName: "has_hand_luggage");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "tickets",
                newName: "flight_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PassengerId",
                table: "tickets",
                newName: "ix_tickets_passenger_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_FlightId",
                table: "tickets",
                newName: "ix_tickets_flight_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "passengers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PassportNumber",
                table: "passengers",
                newName: "passport_number");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "passengers",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "passengers",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "flights",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flights",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "FlightDuration",
                table: "flights",
                newName: "flight_duration");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "flights",
                newName: "departure_time");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "flights",
                newName: "departure_date");

            migrationBuilder.RenameColumn(
                name: "DepartureCity",
                table: "flights",
                newName: "departure_city");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "flights",
                newName: "arrival_date");

            migrationBuilder.RenameColumn(
                name: "ArrivalCity",
                table: "flights",
                newName: "arrival_city");

            migrationBuilder.RenameColumn(
                name: "AircraftModelId",
                table: "flights",
                newName: "aircraft_model_id");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_AircraftModelId",
                table: "flights",
                newName: "ix_flights_aircraft_model_id");

            migrationBuilder.RenameColumn(
                name: "Range",
                table: "aircraft_models",
                newName: "range");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aircraft_models",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aircraft_models",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PassengerCapacity",
                table: "aircraft_models",
                newName: "passenger_capacity");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "aircraft_models",
                newName: "family_id");

            migrationBuilder.RenameColumn(
                name: "CargoCapacity",
                table: "aircraft_models",
                newName: "cargo_capacity");

            migrationBuilder.RenameIndex(
                name: "IX_AircraftModels_FamilyId",
                table: "aircraft_models",
                newName: "ix_aircraft_models_family_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aircraft_families",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "aircraft_families",
                newName: "manufacturer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aircraft_families",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "seat_number",
                table: "tickets",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "luggage_weight",
                table: "tickets",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "has_hand_luggage",
                table: "tickets",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "passengers",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                table: "passengers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "flights",
                keyColumn: "code",
                keyValue: null,
                column: "code",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "flights",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "flight_duration",
                table: "flights",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "departure_time",
                table: "flights",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "departure_date",
                table: "flights",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "arrival_date",
                table: "flights",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tickets",
                table: "tickets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passengers",
                table: "passengers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_flights",
                table: "flights",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aircraft_models",
                table: "aircraft_models",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aircraft_families",
                table: "aircraft_families",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_passengers_passport_number",
                table: "passengers",
                column: "passport_number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_aircraft_models_family_id_aircraft_families",
                table: "aircraft_models",
                column: "family_id",
                principalTable: "aircraft_families",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_flights_aircraft_model_id_aircraft_models",
                table: "flights",
                column: "aircraft_model_id",
                principalTable: "aircraft_models",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_flight_id_flights",
                table: "tickets",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_passenger_id_passengers",
                table: "tickets",
                column: "passenger_id",
                principalTable: "passengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_aircraft_models_family_id_aircraft_families",
                table: "aircraft_models");

            migrationBuilder.DropForeignKey(
                name: "fk_flights_aircraft_model_id_aircraft_models",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "fk_tickets_flight_id_flights",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_tickets_passenger_id_passengers",
                table: "tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tickets",
                table: "tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passengers",
                table: "passengers");

            migrationBuilder.DropIndex(
                name: "IX_passengers_passport_number",
                table: "passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flights",
                table: "flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aircraft_models",
                table: "aircraft_models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aircraft_families",
                table: "aircraft_families");

            migrationBuilder.RenameTable(
                name: "tickets",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "passengers",
                newName: "Passengers");

            migrationBuilder.RenameTable(
                name: "flights",
                newName: "Flights");

            migrationBuilder.RenameTable(
                name: "aircraft_models",
                newName: "AircraftModels");

            migrationBuilder.RenameTable(
                name: "aircraft_families",
                newName: "AircraftFamilies");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "seat_number",
                table: "Tickets",
                newName: "SeatNumber");

            migrationBuilder.RenameColumn(
                name: "passenger_id",
                table: "Tickets",
                newName: "PassengerId");

            migrationBuilder.RenameColumn(
                name: "luggage_weight",
                table: "Tickets",
                newName: "LuggageWeight");

            migrationBuilder.RenameColumn(
                name: "has_hand_luggage",
                table: "Tickets",
                newName: "HasHandLuggage");

            migrationBuilder.RenameColumn(
                name: "flight_id",
                table: "Tickets",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "ix_tickets_passenger_id",
                table: "Tickets",
                newName: "IX_Tickets_PassengerId");

            migrationBuilder.RenameIndex(
                name: "ix_tickets_flight_id",
                table: "Tickets",
                newName: "IX_Tickets_FlightId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Passengers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "passport_number",
                table: "Passengers",
                newName: "PassportNumber");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Passengers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Passengers",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Flights",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Flights",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "flight_duration",
                table: "Flights",
                newName: "FlightDuration");

            migrationBuilder.RenameColumn(
                name: "departure_time",
                table: "Flights",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "departure_date",
                table: "Flights",
                newName: "DepartureDate");

            migrationBuilder.RenameColumn(
                name: "departure_city",
                table: "Flights",
                newName: "DepartureCity");

            migrationBuilder.RenameColumn(
                name: "arrival_date",
                table: "Flights",
                newName: "ArrivalDate");

            migrationBuilder.RenameColumn(
                name: "arrival_city",
                table: "Flights",
                newName: "ArrivalCity");

            migrationBuilder.RenameColumn(
                name: "aircraft_model_id",
                table: "Flights",
                newName: "AircraftModelId");

            migrationBuilder.RenameIndex(
                name: "ix_flights_aircraft_model_id",
                table: "Flights",
                newName: "IX_Flights_AircraftModelId");

            migrationBuilder.RenameColumn(
                name: "range",
                table: "AircraftModels",
                newName: "Range");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AircraftModels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AircraftModels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "passenger_capacity",
                table: "AircraftModels",
                newName: "PassengerCapacity");

            migrationBuilder.RenameColumn(
                name: "family_id",
                table: "AircraftModels",
                newName: "FamilyId");

            migrationBuilder.RenameColumn(
                name: "cargo_capacity",
                table: "AircraftModels",
                newName: "CargoCapacity");

            migrationBuilder.RenameIndex(
                name: "ix_aircraft_models_family_id",
                table: "AircraftModels",
                newName: "IX_AircraftModels_FamilyId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AircraftFamilies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "manufacturer",
                table: "AircraftFamilies",
                newName: "Manufacturer");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AircraftFamilies",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "Tickets",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "LuggageWeight",
                table: "Tickets",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasHandLuggage",
                table: "Tickets",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Passengers",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Passengers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Flights",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "FlightDuration",
                table: "Flights",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Flights",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                table: "Flights",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalDate",
                table: "Flights",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AircraftModels",
                table: "AircraftModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AircraftFamilies",
                table: "AircraftFamilies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_AircraftFamilies_FamilyId",
                table: "AircraftModels",
                column: "FamilyId",
                principalTable: "AircraftFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_AircraftModels_AircraftModelId",
                table: "Flights",
                column: "AircraftModelId",
                principalTable: "AircraftModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
