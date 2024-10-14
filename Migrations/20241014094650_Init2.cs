using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventureAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Events_EventId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Events_EventId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Ratings",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_EventId",
                table: "Ratings",
                newName: "IX_Ratings_ActivityId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Comments",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                newName: "IX_Comments_ActivityId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Attendances",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_EventId",
                table: "Attendances",
                newName: "IX_Attendances_ActivityId");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DateOfActivity = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    IsFamilyFriendly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityCategories",
                columns: table => new
                {
                    ActivityCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCategories", x => x.ActivityCategoryId);
                    table.ForeignKey(
                        name: "FK_ActivityCategories_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCategories_ActivityId",
                table: "ActivityCategories",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCategories_CategoryId",
                table: "ActivityCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Activities_ActivityId",
                table: "Attendances",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Activities_ActivityId",
                table: "Ratings",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Activities_ActivityId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Activities_ActivityId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "ActivityCategories");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Ratings",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ActivityId",
                table: "Ratings",
                newName: "IX_Ratings_EventId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Comments",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ActivityId",
                table: "Comments",
                newName: "IX_Comments_EventId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Attendances",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_ActivityId",
                table: "Attendances",
                newName: "IX_Attendances_EventId");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    IsFamilyFriendly = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    EventCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.EventCategoryId);
                    table.ForeignKey(
                        name: "FK_EventCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCategories_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_CategoryId",
                table: "EventCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_EventId",
                table: "EventCategories",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Events_EventId",
                table: "Attendances",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Events_EventId",
                table: "Ratings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
