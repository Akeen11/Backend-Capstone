using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: false),
                    IsVet = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.TreatmentId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceivingUserId = table.Column<string>(nullable: false),
                    SendingUserId = table.Column<string>(nullable: false),
                    Messages = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceivingUserId",
                        column: x => x.ReceivingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SendingUserId",
                        column: x => x.SendingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceivingUserId = table.Column<string>(nullable: false),
                    SendingUserId = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_ReceivingUserId",
                        column: x => x.ReceivingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_SendingUserId",
                        column: x => x.SendingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VetId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_AspNetUsers_VetId",
                        column: x => x.VetId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PetTreatments",
                columns: table => new
                {
                    PetTreatmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PetId = table.Column<int>(nullable: false),
                    TreatmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTreatments", x => x.PetTreatmentId);
                    table.ForeignKey(
                        name: "FK_PetTreatments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsVet", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", 0, "bd7e7d01-9d99-4ba9-ad90-1e817edfbf7b", "admin@admin.com", true, "admin", true, "admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEIMgpvq1poiycusrSapiyfPsl3+dI/f72Oysl1cOIvqLKt7WyapuxVK43R0GgUjQkg==", null, false, "c75a8974-7d24-4125-ad77-5584979ca077", "123 Infinity Way", false, "admin@admin.com" },
                    { "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b", 0, "1eb202a8-e46d-4269-9c74-aaed04f2b3fd", "aaron@aaron.com", true, "aaron", null, "aaron", false, null, "AARON@AARON.COM", "AARON@AARON.COM", "AQAAAAEAACcQAAAAEIjbiqecILyIrDV3PkgfOFTp5XBSn2yA8bFu4QxuG6bHh42wBDpZzW6UItHD4kQfpw==", null, false, "4e6a2d6a-b25e-48bf-b178-9a41b067ef89", "123 Infinity Way", false, "aaron@aaron.com" }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "TreatmentId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Attacks the lining of the intestinal tract and damages the heart of very young puppies; disease can be fatal", "Parvovirus Vaccine", 20.0 },
                    { 2, "Attacks the lungs and affects the function of the brain and spinal cord; disease can be fatal", "Canine Distemper Virus Vaccine", 20.0 },
                    { 3, "Affects the liver and can cause loss of vision", "Canine adenovirus", 15.0 },
                    { 4, "This is a fatal viral disease that can infect all warm-blooded animals, including dogs and humans", "Rabies", 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "Messages", "ReceivingUserId", "SendingUserId" },
                values: new object[,]
                {
                    { 1, "Hi", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" },
                    { 2, "Hello", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" },
                    { 3, "How are you", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" },
                    { 4, "I'm good", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NoteId", "Message", "ReceivingUserId", "SendingUserId" },
                values: new object[,]
                {
                    { 1, "Get shots", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" },
                    { 2, "Happy pupper", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "Age", "ImagePath", "Name", "Status", "UserId", "VetId" },
                values: new object[,]
                {
                    { 1, 1, null, "Gunner", "Healthy", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1" },
                    { 2, 1, null, "Marley", "Sick", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1" },
                    { 3, 1, null, "Whitley", "Beat up", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1" },
                    { 4, 1, null, "Rocky", "Healthy", "b8e8c2c0-a36f-45f6-8f9d-48693eb93f5b", "61577fe1-1fad-4ef8-97f2-c0659eaa95f1" }
                });

            migrationBuilder.InsertData(
                table: "PetTreatments",
                columns: new[] { "PetTreatmentId", "PetId", "TreatmentId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "PetTreatments",
                columns: new[] { "PetTreatmentId", "PetId", "TreatmentId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceivingUserId",
                table: "Messages",
                column: "ReceivingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SendingUserId",
                table: "Messages",
                column: "SendingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ReceivingUserId",
                table: "Notes",
                column: "ReceivingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SendingUserId",
                table: "Notes",
                column: "SendingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_VetId",
                table: "Pets",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetTreatments_PetId",
                table: "PetTreatments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetTreatments_TreatmentId",
                table: "PetTreatments",
                column: "TreatmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PetTreatments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
