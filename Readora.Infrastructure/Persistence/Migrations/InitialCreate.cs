using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Readora.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260923211546_InitialCreate")]
public class InitialCreate : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable("AspNetRoles", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 256;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 256;
			return new
			{
				Id = id,
				Name = name,
				NormalizedName = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				ConcurrencyStamp = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetRoles", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetUsers", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 50;
			OperationBuilder<AddColumnOperation> firstName = table.Column<string>("character varying(50)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 50;
			OperationBuilder<AddColumnOperation> lastName = table.Column<string>("character varying(50)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 256;
			OperationBuilder<AddColumnOperation> userName = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 256;
			OperationBuilder<AddColumnOperation> normalizedUserName = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 256;
			OperationBuilder<AddColumnOperation> email = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 256;
			return new
			{
				Id = id,
				FirstName = firstName,
				LastName = lastName,
				UserName = userName,
				NormalizedUserName = normalizedUserName,
				Email = email,
				NormalizedEmail = table.Column<string>("character varying(256)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				EmailConfirmed = table.Column<bool>("boolean", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				PasswordHash = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				SecurityStamp = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				ConcurrencyStamp = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				PhoneNumber = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				PhoneNumberConfirmed = table.Column<bool>("boolean", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				TwoFactorEnabled = table.Column<bool>("boolean", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				LockoutEnd = table.Column<DateTimeOffset>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				LockoutEnabled = table.Column<bool>("boolean", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				AccessFailedCount = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetUsers", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("Authors", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 100;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("character varying(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 1000;
			return new
			{
				Id = id,
				Name = name,
				Bio = table.Column<string>("character varying(1000)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Authors", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("Categories", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 100;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("character varying(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			return new
			{
				Id = id,
				Name = name,
				Description = table.Column<string>("character varying(500)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Categories", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("IdempotencyRecords", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 100;
			OperationBuilder<AddColumnOperation> key = table.Column<string>("character varying(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 100;
			return new
			{
				Id = id,
				Key = key,
				Name = table.Column<string>("character varying(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				RequestPayload = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				ResponsePayload = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				StatusCode = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_IdempotencyRecords", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetRoleClaims", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4),
			RoleId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ClaimType = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ClaimValue = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetRoleClaims", x => (object)x.Id);
			table.ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", x => (object)x.RoleId, "AspNetRoles", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetUserClaims", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4),
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ClaimType = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ClaimValue = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetUserClaims", x => (object)x.Id);
			table.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetUserLogins", (ColumnsBuilder table) => new
		{
			LoginProvider = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ProviderKey = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ProviderDisplayName = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetUserLogins", x => (object)new { x.LoginProvider, x.ProviderKey });
			table.ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetUserRoles", (ColumnsBuilder table) => new
		{
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			RoleId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetUserRoles", x => (object)new { x.UserId, x.RoleId });
			table.ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", x => (object)x.RoleId, "AspNetRoles", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
			table.ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("AspNetUserTokens", (ColumnsBuilder table) => new
		{
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			LoginProvider = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			Name = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			Value = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_AspNetUserTokens", x => (object)new { x.UserId, x.LoginProvider, x.Name });
			table.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("Notifications", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			OperationBuilder<AddColumnOperation> userId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 200;
			OperationBuilder<AddColumnOperation> title = table.Column<string>("character varying(200)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 1000;
			return new
			{
				Id = id,
				UserId = userId,
				Title = title,
				Message = table.Column<string>("character varying(1000)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				IsRead = table.Column<bool>("boolean", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				Type = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Notifications", x => (object)x.Id);
			table.ForeignKey("FK_Notifications_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("RefreshTokens", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			OperationBuilder<AddColumnOperation> userId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 200;
			return new
			{
				Id = id,
				UserId = userId,
				TokenHash = table.Column<string>("character varying(200)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				ExpiresAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				RevokedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_RefreshTokens", x => (object)x.Id);
			table.ForeignKey("FK_RefreshTokens_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("Wishlists", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4),
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Wishlists", x => (object)x.Id);
			table.ForeignKey("FK_Wishlists_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("Books", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			int? num = 255;
			OperationBuilder<AddColumnOperation> title = table.Column<string>("character varying(255)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 20;
			OperationBuilder<AddColumnOperation> iSBN = table.Column<string>("character varying(20)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 2000;
			return new
			{
				Id = id,
				Title = title,
				ISBN = iSBN,
				Description = table.Column<string>("character varying(2000)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				PublishedDate = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CoverImageUrl = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				FileKey = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				AuthorId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CategoryId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Books", x => (object)x.Id);
			table.ForeignKey("FK_Books_Authors_AuthorId", x => (object)x.AuthorId, "Authors", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)1);
			table.ForeignKey("FK_Books_Categories_CategoryId", x => (object)x.CategoryId, "Categories", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)1);
		}, (string)null);
		migrationBuilder.CreateTable("BookCopies", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			OperationBuilder<AddColumnOperation> bookId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 50;
			return new
			{
				Id = id,
				BookId = bookId,
				CopyNumber = table.Column<string>("character varying(50)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				Status = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				RowVersion = table.Column<byte[]>("bytea", (bool?)null, (int?)null, true, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_BookCopies", x => (object)x.Id);
			table.ForeignKey("FK_BookCopies_Books_BookId", x => (object)x.BookId, "Books", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("Reviews", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			OperationBuilder<AddColumnOperation> userId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			OperationBuilder<AddColumnOperation> bookId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			OperationBuilder<AddColumnOperation> rating = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 1000;
			return new
			{
				Id = id,
				UserId = userId,
				BookId = bookId,
				Rating = rating,
				Comment = table.Column<string>("character varying(1000)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Reviews", x => (object)x.Id);
			table.ForeignKey("FK_Reviews_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)1);
			table.ForeignKey("FK_Reviews_Books_BookId", x => (object)x.BookId, "Books", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("WishlistItems", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4),
			WishlistId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			BookId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			AddedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_WishlistItems", x => (object)x.Id);
			table.ForeignKey("FK_WishlistItems_Books_BookId", x => (object)x.BookId, "Books", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
			table.ForeignKey("FK_WishlistItems_Wishlists_WishlistId", x => (object)x.WishlistId, "Wishlists", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("Borrowings", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4),
			UserId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			BookCopyId = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			BorrowedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			DueDate = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			ReturnedAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			Status = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Borrowings", x => (object)x.Id);
			table.ForeignKey("FK_Borrowings_AspNetUsers_UserId", x => (object)x.UserId, "AspNetUsers", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)1);
			table.ForeignKey("FK_Borrowings_BookCopies_BookCopyId", x => (object)x.BookCopyId, "BookCopies", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)1);
		}, (string)null);
		migrationBuilder.CreateIndex("IX_AspNetRoleClaims_RoleId", "AspNetRoleClaims", "RoleId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("RoleNameIndex", "AspNetRoles", "NormalizedName", (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_AspNetUserClaims_UserId", "AspNetUserClaims", "UserId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_AspNetUserLogins_UserId", "AspNetUserLogins", "UserId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_AspNetUserRoles_RoleId", "AspNetUserRoles", "RoleId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("EmailIndex", "AspNetUsers", "NormalizedEmail", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("UserNameIndex", "AspNetUsers", "NormalizedUserName", (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_BookCopies_BookId_CopyNumber", "BookCopies", new string[2] { "BookId", "CopyNumber" }, (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Books_AuthorId", "Books", "AuthorId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Books_CategoryId", "Books", "CategoryId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Books_ISBN", "Books", "ISBN", (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Borrowings_BookCopyId", "Borrowings", "BookCopyId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Borrowings_UserId", "Borrowings", "UserId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Categories_Name", "Categories", "Name", (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_IdempotencyRecords_Key", "IdempotencyRecords", "Key", (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Notifications_UserId", "Notifications", "UserId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_RefreshTokens_UserId", "RefreshTokens", "UserId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Reviews_BookId", "Reviews", "BookId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Reviews_UserId_BookId", "Reviews", new string[2] { "UserId", "BookId" }, (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_WishlistItems_BookId", "WishlistItems", "BookId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_WishlistItems_WishlistId_BookId", "WishlistItems", new string[2] { "WishlistId", "BookId" }, (string)null, true, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Wishlists_UserId", "Wishlists", "UserId", (string)null, true, (string)null, (bool[])null);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("AspNetRoleClaims", (string)null);
		migrationBuilder.DropTable("AspNetUserClaims", (string)null);
		migrationBuilder.DropTable("AspNetUserLogins", (string)null);
		migrationBuilder.DropTable("AspNetUserRoles", (string)null);
		migrationBuilder.DropTable("AspNetUserTokens", (string)null);
		migrationBuilder.DropTable("Borrowings", (string)null);
		migrationBuilder.DropTable("IdempotencyRecords", (string)null);
		migrationBuilder.DropTable("Notifications", (string)null);
		migrationBuilder.DropTable("RefreshTokens", (string)null);
		migrationBuilder.DropTable("Reviews", (string)null);
		migrationBuilder.DropTable("WishlistItems", (string)null);
		migrationBuilder.DropTable("AspNetRoles", (string)null);
		migrationBuilder.DropTable("BookCopies", (string)null);
		migrationBuilder.DropTable("Wishlists", (string)null);
		migrationBuilder.DropTable("Books", (string)null);
		migrationBuilder.DropTable("AspNetUsers", (string)null);
		migrationBuilder.DropTable("Authors", (string)null);
		migrationBuilder.DropTable("Categories", (string)null);
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("ProductVersion", (object)"9.0.0").HasAnnotation("Relational:MaxIdentifierLength", (object)63);
		NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ConcurrencyStamp").IsConcurrencyToken(true), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").HasMaxLength(256), "character varying(256)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("NormalizedName").HasMaxLength(256), "character varying(256)");
			b.HasKey(new string[1] { "Id" });
			RelationalIndexBuilderExtensions.HasDatabaseName(b.HasIndex(new string[1] { "NormalizedName" }).IsUnique(true), "RoleNameIndex");
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetRoles", (string)null);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ClaimType"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ClaimValue"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("RoleId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "RoleId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetRoleClaims", (string)null);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ClaimType"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ClaimValue"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "UserId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetUserClaims", (string)null);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("LoginProvider"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ProviderKey"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ProviderDisplayName"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[2] { "LoginProvider", "ProviderKey" });
			b.HasIndex(new string[1] { "UserId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetUserLogins", (string)null);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("RoleId"), "integer");
			b.HasKey(new string[2] { "UserId", "RoleId" });
			b.HasIndex(new string[1] { "RoleId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetUserRoles", (string)null);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("LoginProvider"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Value"), "text");
			b.HasKey(new string[3] { "UserId", "LoginProvider", "Name" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetUserTokens", (string)null);
		}));
		modelBuilder.Entity("Readora.Domain.Entities.ApplicationUser", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("AccessFailedCount"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ConcurrencyStamp").IsConcurrencyToken(true), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Email").HasMaxLength(256), "character varying(256)");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("EmailConfirmed"), "boolean");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("FirstName").IsRequired(true).HasMaxLength(50), "character varying(50)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("LastName").IsRequired(true).HasMaxLength(50), "character varying(50)");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("LockoutEnabled"), "boolean");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTimeOffset?>(b.Property<DateTimeOffset?>("LockoutEnd"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("NormalizedEmail").HasMaxLength(256), "character varying(256)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("NormalizedUserName").HasMaxLength(256), "character varying(256)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("PasswordHash"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("PhoneNumber"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("PhoneNumberConfirmed"), "boolean");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("SecurityStamp"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("TwoFactorEnabled"), "boolean");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("UserName").HasMaxLength(256), "character varying(256)");
			b.HasKey(new string[1] { "Id" });
			RelationalIndexBuilderExtensions.HasDatabaseName(b.HasIndex(new string[1] { "NormalizedEmail" }), "EmailIndex");
			RelationalIndexBuilderExtensions.HasDatabaseName(b.HasIndex(new string[1] { "NormalizedUserName" }).IsUnique(true), "UserNameIndex");
			RelationalEntityTypeBuilderExtensions.ToTable(b, "AspNetUsers", (string)null);
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Author", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Bio").HasMaxLength(1000), "character varying(1000)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "character varying(100)");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Authors");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Book", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("AuthorId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("CategoryId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("CoverImageUrl"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Description").HasMaxLength(2000), "character varying(2000)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("FileKey"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ISBN").IsRequired(true).HasMaxLength(20), "character varying(20)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime?>(b.Property<DateTime?>("PublishedDate"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Title").IsRequired(true).HasMaxLength(255), "character varying(255)");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "AuthorId" });
			b.HasIndex(new string[1] { "CategoryId" });
			b.HasIndex(new string[1] { "ISBN" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Books");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.BookCopy", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("BookId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("CopyNumber").IsRequired(true).HasMaxLength(50), "character varying(50)");
			RelationalPropertyBuilderExtensions.HasColumnType<byte[]>(b.Property<byte[]>("RowVersion").IsConcurrencyToken(true).IsRequired(true)
				.ValueGeneratedOnAddOrUpdate(), "bytea");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Status"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[2] { "BookId", "CopyNumber" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "BookCopies");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Borrowing", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("BookCopyId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("BorrowedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("DueDate"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime?>(b.Property<DateTime?>("ReturnedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Status"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "BookCopyId" });
			b.HasIndex(new string[1] { "UserId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Borrowings");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Category", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Description").HasMaxLength(500), "character varying(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "character varying(100)");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "Name" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Categories");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.IdempotencyRecord", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Key").IsRequired(true).HasMaxLength(100), "character varying(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "character varying(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("RequestPayload"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ResponsePayload"), "text");
			RelationalPropertyBuilderExtensions.HasColumnType<int?>(b.Property<int?>("StatusCode"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "Key" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "IdempotencyRecords");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Notification", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsRead"), "boolean");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Message").IsRequired(true).HasMaxLength(1000), "character varying(1000)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Title").IsRequired(true).HasMaxLength(200), "character varying(200)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Type"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "UserId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Notifications");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.RefreshToken", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("ExpiresAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime?>(b.Property<DateTime?>("RevokedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("TokenHash").IsRequired(true).HasMaxLength(200), "character varying(200)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "UserId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "RefreshTokens");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Review", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("BookId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Comment").HasMaxLength(1000), "character varying(1000)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Rating"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "BookId" });
			b.HasIndex(new string[2] { "UserId", "BookId" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Reviews");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Wishlist", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "UserId" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Wishlists");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.WishlistItem", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "integer");
			NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn<int>(b.Property<int>("Id"));
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("AddedAt"), "timestamp with time zone");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("BookId"), "integer");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("WishlistId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "BookId" });
			b.HasIndex(new string[2] { "WishlistId", "BookId" }).IsUnique(true);
			RelationalEntityTypeBuilderExtensions.ToTable(b, "WishlistItems");
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "RoleId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "RoleId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.HasOne("Readora.Domain.Entities.ApplicationUser", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
		}));
		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", (string)null).WithMany((string)null).HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Book", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.Author", "Author").WithMany("Books").HasForeignKey(new string[1] { "AuthorId" })
				.OnDelete((DeleteBehavior)1)
				.IsRequired(true);
			b.HasOne("Readora.Domain.Entities.Category", "Category").WithMany("Books").HasForeignKey(new string[1] { "CategoryId" })
				.OnDelete((DeleteBehavior)1)
				.IsRequired(true);
			b.Navigation("Author");
			b.Navigation("Category");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.BookCopy", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.Book", "Book").WithMany("Copies").HasForeignKey(new string[1] { "BookId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("Book");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Borrowing", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.BookCopy", "BookCopy").WithMany("Borrowings").HasForeignKey(new string[1] { "BookCopyId" })
				.OnDelete((DeleteBehavior)1)
				.IsRequired(true);
			b.HasOne("Readora.Domain.Entities.ApplicationUser", "User").WithMany("Borrowings").HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)1)
				.IsRequired(true);
			b.Navigation("BookCopy");
			b.Navigation("User");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Notification", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", "User").WithMany("Notifications").HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("User");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.RefreshToken", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", "User").WithMany("RefreshTokens").HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("User");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Review", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.Book", "Book").WithMany("Reviews").HasForeignKey(new string[1] { "BookId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.HasOne("Readora.Domain.Entities.ApplicationUser", "User").WithMany("Reviews").HasForeignKey(new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)1)
				.IsRequired(true);
			b.Navigation("Book");
			b.Navigation("User");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Wishlist", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.ApplicationUser", "User").WithOne("Wishlist").HasForeignKey("Readora.Domain.Entities.Wishlist", new string[1] { "UserId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("User");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.WishlistItem", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Readora.Domain.Entities.Book", "Book").WithMany((string)null).HasForeignKey(new string[1] { "BookId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.HasOne("Readora.Domain.Entities.Wishlist", "Wishlist").WithMany("Items").HasForeignKey(new string[1] { "WishlistId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("Book");
			b.Navigation("Wishlist");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.ApplicationUser", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Borrowings");
			b.Navigation("Notifications");
			b.Navigation("RefreshTokens");
			b.Navigation("Reviews");
			b.Navigation("Wishlist");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Author", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Books");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Book", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Copies");
			b.Navigation("Reviews");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.BookCopy", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Borrowings");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Category", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Books");
		}));
		modelBuilder.Entity("Readora.Domain.Entities.Wishlist", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Items");
		}));
	}
}
