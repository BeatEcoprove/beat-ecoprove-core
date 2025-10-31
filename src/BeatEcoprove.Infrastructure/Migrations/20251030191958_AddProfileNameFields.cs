using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeatEcoprove.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileNameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_auths_auth_id",
                table: "profiles");

            migrationBuilder.DropTable(
                name: "auths");

            migrationBuilder.DropTable(
                name: "group_invites");

            migrationBuilder.DropTable(
                name: "group_members");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropIndex(
                name: "IX_profiles_auth_id",
                table: "profiles");

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: new Guid("0d810f3b-2f17-49a0-9faf-320c30393e22"));

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: new Guid("1066f7be-78b8-4704-b444-bc6c11057b25"));

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: new Guid("a0a204c5-067c-46b8-8ad8-9f72a1100eeb"));

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: new Guid("ae921096-f72b-4b77-8562-ad49ab137d68"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("02e1621a-31de-428f-bfe8-12ce16303cfa"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("0dc52312-73b4-45cd-97c2-7ba0a0684195"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("20648049-1121-4037-912a-0b5ae4fe8ca1"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("21077620-0f70-4b79-adcb-26747d1f5d03"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("27b5e9e4-c2cc-487e-a47e-42ccc0ff7a82"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("460345a7-22c3-4739-92a5-bc15b7dbc8ec"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("607c29b2-e0a8-4c0e-9903-a9d95e335135"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("64332e3c-fbff-403d-b450-6cf764d67a98"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("679480a2-707b-45ca-9bd6-cc8643877fa6"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("737af9e9-d7e6-45a3-93db-66352b62d1af"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("9039f788-7265-474d-b352-302233dea969"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("9169de9c-87f0-40a7-8381-8a5b1107ad19"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("92957cca-6e3b-4512-a813-243f0da7b148"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("9b4ec4c5-cb66-4b10-a433-a85d72652593"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("a3895e31-4048-48fb-83d1-8315448b12c1"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("b1c577cc-498a-4f54-9c30-54a0eef365a3"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("c114ae9d-fe77-437e-a3ca-44d5a606efd2"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("c24acdb0-b7ff-4dd6-bd06-6f3f7bc13e01"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("dfb2b324-6205-4200-b617-7371495b9b9c"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("e18956f5-22a8-4a42-be44-a17227a05958"));

            migrationBuilder.DeleteData(
                table: "colors",
                keyColumn: "id",
                keyValue: new Guid("e72157a6-64f3-4d47-ac2b-e67dd9148f04"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("0489437f-e69d-46b4-8efd-71a49333550d"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("320d38a2-9067-49c4-849b-db85766a8c2b"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("344a0c07-eb30-490c-a7e8-eda8ac310d9f"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("50fdf9dd-a510-452f-9149-61db12696b79"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("636302c3-a457-400c-8352-2852bf610ebf"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("a41ee2b3-70ab-4574-9c32-0ee81b7b0e9f"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("a7910197-97ba-441a-a4b9-9353650b22ad"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("a85c6baa-0cea-441e-8545-bb99877118e1"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("aeef60ff-7006-49b0-bc9c-26b049411434"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("b8ff55b2-af07-47c0-9428-5e24596afc68"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("bab6b0f1-4841-4c0a-b31d-35cb0d794a82"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("bf2026c0-359c-423d-9216-0edd70b874dc"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("bf296acc-c032-42b3-8ae3-8b08addfd66b"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("c1794ceb-9750-411d-8a48-f047f52253c9"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("f11a3aef-3d2a-490c-b7ee-02e695d6c47c"));

            migrationBuilder.DeleteData(
                table: "maintenance_service_actions",
                keyColumn: "id",
                keyValue: new Guid("f50a6b8b-5861-40ba-9084-3d7f1ff98253"));

            migrationBuilder.DeleteData(
                table: "maintenance_services",
                keyColumn: "id",
                keyValue: new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"));

            migrationBuilder.DeleteData(
                table: "maintenance_services",
                keyColumn: "id",
                keyValue: new Guid("5cf609b9-227d-42e8-8dba-6a30da3fdab2"));

            migrationBuilder.DeleteData(
                table: "maintenance_services",
                keyColumn: "id",
                keyValue: new Guid("8aaa293b-bc68-426a-bafb-0a5fc1c4244e"));

            migrationBuilder.DeleteData(
                table: "maintenance_services",
                keyColumn: "id",
                keyValue: new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "profiles",
                newName: "last_name");

            migrationBuilder.AddColumn<string>(
                name: "biography",
                table: "profiles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "profiles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "profiles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "biography",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "display_name",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "profiles");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "profiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "profiles",
                newName: "user_name");

            migrationBuilder.CreateTable(
                name: "auths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    main_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    avatar_picture = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    members_count = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sustainable_points = table.Column<int>(type: "integer", nullable: false),
                    xp = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_groups_profiles_creator_id",
                        column: x => x.creator_id,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group_invites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    declined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inviter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission = table.Column<int>(type: "integer", nullable: false),
                    target_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_invites", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_invites_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_invites_profiles_inviter_id",
                        column: x => x.inviter_id,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_invites_profiles_target_id",
                        column: x => x.target_id,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission = table.Column<int>(type: "integer", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_members_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_members_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "id", "brand_avatar", "deleted_at", "name" },
                values: new object[,]
                {
                    { new Guid("0d810f3b-2f17-49a0-9faf-320c30393e22"), "public/default/brands/mo.png", null, "MO" },
                    { new Guid("1066f7be-78b8-4704-b444-bc6c11057b25"), "public/default/brands/zippy.png", null, "Zippy" },
                    { new Guid("a0a204c5-067c-46b8-8ad8-9f72a1100eeb"), "public/default/brands/salsa.png", null, "Salsa" },
                    { new Guid("ae921096-f72b-4b77-8562-ad49ab137d68"), "public/default/brands/losan.png", null, "Losan" }
                });

            migrationBuilder.InsertData(
                table: "colors",
                columns: new[] { "id", "deleted_at", "hex", "name" },
                values: new object[,]
                {
                    { new Guid("02e1621a-31de-428f-bfe8-12ce16303cfa"), null, "FF29394A", "Azul Escuro" },
                    { new Guid("0dc52312-73b4-45cd-97c2-7ba0a0684195"), null, "FFDA252E", "Vermelho" },
                    { new Guid("20648049-1121-4037-912a-0b5ae4fe8ca1"), null, "FF4C4C4C", "Cinzento Claro" },
                    { new Guid("21077620-0f70-4b79-adcb-26747d1f5d03"), null, "FFC3A572", "Amarelo Claro" },
                    { new Guid("27b5e9e4-c2cc-487e-a47e-42ccc0ff7a82"), null, "FFD2AAC5", "Roxo Claro" },
                    { new Guid("460345a7-22c3-4739-92a5-bc15b7dbc8ec"), null, "FFC2BC8B", "Verde Lima" },
                    { new Guid("607c29b2-e0a8-4c0e-9903-a9d95e335135"), null, "FF98B3C8", "Azul Claro" },
                    { new Guid("64332e3c-fbff-403d-b450-6cf764d67a98"), null, "FFD62598", "Roxo" },
                    { new Guid("679480a2-707b-45ca-9bd6-cc8643877fa6"), null, "FFFF6D6D", "Vermelho Claro" },
                    { new Guid("737af9e9-d7e6-45a3-93db-66352b62d1af"), null, "FFBE5967", "Rosa" },
                    { new Guid("9039f788-7265-474d-b352-302233dea969"), null, "FF8B5F3C", "Castanho Bebê" },
                    { new Guid("9169de9c-87f0-40a7-8381-8a5b1107ad19"), null, "FFFFE69F", "Amarelo" },
                    { new Guid("92957cca-6e3b-4512-a813-243f0da7b148"), null, "FFF9C7C4", "Rosa Claro" },
                    { new Guid("9b4ec4c5-cb66-4b10-a433-a85d72652593"), null, "FF948066", "Castanho Claro" },
                    { new Guid("a3895e31-4048-48fb-83d1-8315448b12c1"), null, "FFFFFFFF", "White" },
                    { new Guid("b1c577cc-498a-4f54-9c30-54a0eef365a3"), null, "FF000000", "Black" },
                    { new Guid("c114ae9d-fe77-437e-a3ca-44d5a606efd2"), null, "FFC0C0C0", "Cinzento Bebê" },
                    { new Guid("c24acdb0-b7ff-4dd6-bd06-6f3f7bc13e01"), null, "FFF58221", "Laranja" },
                    { new Guid("dfb2b324-6205-4200-b617-7371495b9b9c"), null, "FFF2E7D4", "Amarelo Bebê" },
                    { new Guid("e18956f5-22a8-4a42-be44-a17227a05958"), null, "FF509C75", "Verde" },
                    { new Guid("e72157a6-64f3-4d47-ac2b-e67dd9148f04"), null, "FF4A2D16", "Castanho" }
                });

            migrationBuilder.InsertData(
                table: "maintenance_services",
                columns: new[] { "id", "badge", "deleted_at", "description", "title" },
                values: new object[,]
                {
                    { new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"), "public/default/iron.png", null, "De que forma pretende engomar?", "Engomar" },
                    { new Guid("5cf609b9-227d-42e8-8dba-6a30da3fdab2"), "public/default/dry.png", null, "De que forma pretende secar?", "Secar" },
                    { new Guid("8aaa293b-bc68-426a-bafb-0a5fc1c4244e"), "public/default/repair.png", null, "De que forma pretende arranjar a peça?", "Reparar" },
                    { new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), "public/default/wash.png", null, "De que forma pretende lavar?", "Lavar" }
                });

            migrationBuilder.InsertData(
                table: "maintenance_service_actions",
                columns: new[] { "id", "badge", "deleted_at", "Description", "eco_score", "maintenance_service_id", "sustainable_points", "title" },
                values: new object[,]
                {
                    { new Guid("0489437f-e69d-46b4-8efd-71a49333550d"), "public/default/iron/less150.png", null, "Engomar a menos de 150ºC", -1, new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"), 1, "A menos de 150ºC" },
                    { new Guid("320d38a2-9067-49c4-849b-db85766a8c2b"), "public/default/wash/dry.png", null, "Lavar a seco", -3, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 0, "A seco" },
                    { new Guid("344a0c07-eb30-490c-a7e8-eda8ac310d9f"), "public/default/dry/air.png", null, "Secar ao ar livre", 0, new Guid("5cf609b9-227d-42e8-8dba-6a30da3fdab2"), 2, "Ao ar livre" },
                    { new Guid("50fdf9dd-a510-452f-9149-61db12696b79"), "public/default/iron/less110.png", null, "Engomar a menos de 110ºC", -1, new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"), 1, "A menos de 110ºC" },
                    { new Guid("636302c3-a457-400c-8352-2852bf610ebf"), "public/default/iron/less200.png", null, "Engomar a menos de 200ºC", -1, new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"), 1, "A menos de 200ºC" },
                    { new Guid("a41ee2b3-70ab-4574-9c32-0ee81b7b0e9f"), "public/default/repair.png", null, "Arranjar a peça pelo próprio", 2, new Guid("8aaa293b-bc68-426a-bafb-0a5fc1c4244e"), 3, "Pelo Próprio" },
                    { new Guid("a7910197-97ba-441a-a4b9-9353650b22ad"), "public/default/wash/less30.png", null, "Lavar a menos de 30ºC", -1, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 2, "A menos de 30ºC" },
                    { new Guid("a85c6baa-0cea-441e-8545-bb99877118e1"), "public/default/dry/machine.png", null, "Secar na máquina", -1, new Guid("5cf609b9-227d-42e8-8dba-6a30da3fdab2"), 1, "Na máquina" },
                    { new Guid("aeef60ff-7006-49b0-bc9c-26b049411434"), "public/default/wash/hand.png", null, "Lavar à mão com água e sabão", 10, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 100, "Lavar à mão" },
                    { new Guid("b8ff55b2-af07-47c0-9428-5e24596afc68"), "public/default/service.png", null, "Escolhe uma lavandaria", 10, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 100, "Serviço de lavandaria" },
                    { new Guid("bab6b0f1-4841-4c0a-b31d-35cb0d794a82"), "public/default/wash/less50.png", null, "Lavar a menos de 50ºC", -2, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 1, "A menos de 50ºC" },
                    { new Guid("bf2026c0-359c-423d-9216-0edd70b874dc"), "public/default/service.png", null, "Escolhe um serviço de secagem", 10, new Guid("5cf609b9-227d-42e8-8dba-6a30da3fdab2"), 100, "Serviço de Secagem" },
                    { new Guid("bf296acc-c032-42b3-8ae3-8b08addfd66b"), "public/default/wash/less95.png", null, "Lavar a menos de 95ºC", -2, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 1, "A menos de 95ºC" },
                    { new Guid("c1794ceb-9750-411d-8a48-f047f52253c9"), "public/default/wash/less70.png", null, "Lavar a menos de 70ºC", -2, new Guid("9197ddd7-6f01-4eb6-8ef4-4412fcb7cb1f"), 1, "A menos de 70ºC" },
                    { new Guid("f11a3aef-3d2a-490c-b7ee-02e695d6c47c"), "public/default/service.png", null, "Escolhe um serviço de engomadoria", 10, new Guid("2b78eb70-b39a-40c7-a9c4-0d6294c8da68"), 100, "Serviço de Engomadoria" },
                    { new Guid("f50a6b8b-5861-40ba-9084-3d7f1ff98253"), "public/default/service.png", null, "Escolhe um serviço de Reparação", 10, new Guid("8aaa293b-bc68-426a-bafb-0a5fc1c4244e"), 100, "Serviço de Reparação" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_profiles_auth_id",
                table: "profiles",
                column: "auth_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_invites_group_id",
                table: "group_invites",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_invites_inviter_id",
                table: "group_invites",
                column: "inviter_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_invites_target_id",
                table: "group_invites",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_members_group_id",
                table: "group_members",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_members_profile_id",
                table: "group_members",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_creator_id",
                table: "groups",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_auths_auth_id",
                table: "profiles",
                column: "auth_id",
                principalTable: "auths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
