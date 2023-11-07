using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations;

public partial class first : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Customer",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SerialNumber = table.Column<int>(type: "integer", nullable: false),
                Balance = table.Column<int>(type: "integer", nullable: false),
                FirstName = table.Column<string>(type: "text", nullable: false),
                LastName = table.Column<string>(type: "text", nullable: false),
                Category = table.Column<string>(type: "text", nullable: false),
                BelongsToDepartment = table.Column<string>(type: "text", nullable: false),
                Notes = table.Column<string>(type: "text", nullable: false),
                IsRegular = table.Column<bool>(type: "boolean", nullable: false),
                Eligible = table.Column<bool>(type: "boolean", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customer", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DummyMealInformation",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                NumberOfCalories = table.Column<int>(type: "integer", nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                ImagePath = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DummyMealInformation", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Meal",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                NumberOfCalories = table.Column<int>(type: "integer", nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                ImagePath = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Meal", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "NotificationMessage",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MessageSubject = table.Column<string>(type: "text", nullable: false),
                MessageContent = table.Column<string>(type: "text", nullable: false),
                SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NotificationMessage", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Permission",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permission", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PricingRecord",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Price = table.Column<int>(type: "integer", nullable: false),
                CustomerTypeValue = table.Column<string>(type: "text", nullable: false),
                MealTypeValue = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PricingRecord", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AccountTransaction",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Type = table.Column<string>(type: "text", nullable: false),
                Value = table.Column<int>(type: "integer", nullable: false),
                By = table.Column<string>(type: "text", nullable: false),
                CustomerId = table.Column<long>(type: "bigint", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AccountTransaction", x => x.Id);
                table.ForeignKey(
                    name: "FK_AccountTransaction_Customer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customer",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Feedback",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CustomerId = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Feedback", x => x.Id);
                table.ForeignKey(
                    name: "FK_Feedback_Customer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customer",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false),
                HashedPassword = table.Column<string>(type: "text", nullable: false),
                HiastMail = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_Customer_Id",
                    column: x => x.Id,
                    principalTable: "Customer",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "MealEntry",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MealInformationId = table.Column<long>(type: "bigint", nullable: false),
                CustomerCanCancel = table.Column<bool>(type: "boolean", nullable: false),
                AtDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                PreparedCount = table.Column<int>(type: "integer", nullable: false),
                LastNumberInQueue = table.Column<int>(type: "integer", nullable: false),
                ReservationsCount = table.Column<int>(type: "integer", nullable: false),
                ConsumedReservations = table.Column<int>(type: "integer", nullable: false),
                Version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false, defaultValue: 0L),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MealEntry", x => x.Id);
                table.ForeignKey(
                    name: "FK_MealEntry_Meal_MealInformationId",
                    column: x => x.MealInformationId,
                    principalTable: "Meal",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RolePermission",
            columns: table => new
            {
                RoleId = table.Column<int>(type: "integer", nullable: false),
                PermissionId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                table.ForeignKey(
                    name: "FK_RolePermission_Permission_PermissionId",
                    column: x => x.PermissionId,
                    principalTable: "Permission",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RolePermission_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ForgetPasswordEntry",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SerialNumber = table.Column<int>(type: "integer", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                ValidationToken = table.Column<string>(type: "text", nullable: false),
                AtDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ForgetPasswordEntry", x => x.Id);
                table.ForeignKey(
                    name: "FK_ForgetPasswordEntry_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserRole",
            columns: table => new
            {
                UserId = table.Column<long>(type: "bigint", nullable: false),
                RoleId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                table.ForeignKey(
                    name: "FK_UserRole_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRole_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reservation",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AtDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CustomerId = table.Column<long>(type: "bigint", nullable: false),
                MealEntryId = table.Column<long>(type: "bigint", nullable: false),
                ReservationStatus = table.Column<string>(type: "text", nullable: false),
                NumberInQueue = table.Column<int>(type: "integer", nullable: false),
                Price = table.Column<int>(type: "integer", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservation", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reservation_Customer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customer",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Reservation_MealEntry_MealEntryId",
                    column: x => x.MealEntryId,
                    principalTable: "MealEntry",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Permission",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "ReadContent" },
                { 2, "RegisterCustomer" },
                { 3, "CreateSystemInformation" },
                { 4, "OrderContent" },
                { 5, "ReadSystemInformation" },
                { 6, "ConsumeReservations" },
                { 7, "EditBalances" },
                { 8, "SeePublicContent" }
            });

        migrationBuilder.InsertData(
            table: "Role",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Manager" },
                { 2, "User" },
                { 3, "Accountant" },
                { 4, "Consumer" }
            });

        migrationBuilder.InsertData(
            table: "RolePermission",
            columns: new[] { "PermissionId", "RoleId" },
            values: new object[,]
            {
                { 2, 1 },
                { 3, 1 },
                { 5, 1 },
                { 7, 1 },
                { 8, 1 },
                { 1, 2 },
                { 4, 2 },
                { 8, 2 },
                { 7, 3 },
                { 8, 3 },
                { 6, 4 },
                { 8, 4 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_AccountTransaction_CustomerId",
            table: "AccountTransaction",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Customer_Id",
            table: "Customer",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Customer_SerialNumber",
            table: "Customer",
            column: "SerialNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Feedback_CustomerId",
            table: "Feedback",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_ForgetPasswordEntry_UserId",
            table: "ForgetPasswordEntry",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Meal_Id",
            table: "Meal",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MealEntry_AtDay",
            table: "MealEntry",
            column: "AtDay");

        migrationBuilder.CreateIndex(
            name: "IX_MealEntry_Id",
            table: "MealEntry",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MealEntry_MealInformationId",
            table: "MealEntry",
            column: "MealInformationId");

        migrationBuilder.CreateIndex(
            name: "IX_PricingRecord_Id",
            table: "PricingRecord",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PricingRecord_MealTypeValue_CustomerTypeValue",
            table: "PricingRecord",
            columns: new[] { "MealTypeValue", "CustomerTypeValue" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Reservation_AtDay",
            table: "Reservation",
            column: "AtDay");

        migrationBuilder.CreateIndex(
            name: "IX_Reservation_CustomerId",
            table: "Reservation",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Reservation_Id",
            table: "Reservation",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Reservation_MealEntryId",
            table: "Reservation",
            column: "MealEntryId");

        migrationBuilder.CreateIndex(
            name: "IX_Role_Id",
            table: "Role",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_RolePermission_PermissionId",
            table: "RolePermission",
            column: "PermissionId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRole_UserId",
            table: "UserRole",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AccountTransaction");

        migrationBuilder.DropTable(
            name: "DummyMealInformation");

        migrationBuilder.DropTable(
            name: "Feedback");

        migrationBuilder.DropTable(
            name: "ForgetPasswordEntry");

        migrationBuilder.DropTable(
            name: "NotificationMessage");

        migrationBuilder.DropTable(
            name: "PricingRecord");

        migrationBuilder.DropTable(
            name: "Reservation");

        migrationBuilder.DropTable(
            name: "RolePermission");

        migrationBuilder.DropTable(
            name: "UserRole");

        migrationBuilder.DropTable(
            name: "MealEntry");

        migrationBuilder.DropTable(
            name: "Permission");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "Meal");

        migrationBuilder.DropTable(
            name: "Customer");
    }
}
