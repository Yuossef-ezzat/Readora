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
[Migration("20260923225629_AddRefreshTokenHashIndex")]
public class AddRefreshTokenHashIndex : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("IdempotencyRecords", (string)null);
		int? num = 256;
		Type typeFromHandle = typeof(string);
		int? num2 = 200;
		migrationBuilder.AlterColumn<string>("TokenHash", "RefreshTokens", "character varying(256)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, typeFromHandle, "character varying(200)", (bool?)null, num2, false, false, (object)null, (string)null, (string)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (int?)null, (int?)null, (int?)null, (int?)null, (bool?)null, (bool?)null);
		migrationBuilder.CreateIndex("IX_RefreshTokens_TokenHash", "RefreshTokens", "TokenHash", (string)null, false, (string)null, (bool[])null);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex("IX_RefreshTokens_TokenHash", "RefreshTokens", (string)null);
		int? num = 200;
		Type typeFromHandle = typeof(string);
		int? num2 = 256;
		migrationBuilder.AlterColumn<string>("TokenHash", "RefreshTokens", "character varying(200)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, typeFromHandle, "character varying(256)", (bool?)null, num2, false, false, (object)null, (string)null, (string)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (int?)null, (int?)null, (int?)null, (int?)null, (bool?)null, (bool?)null);
		migrationBuilder.CreateTable("IdempotencyRecords", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("Npgsql:ValueGenerationStrategy", (object)(NpgsqlValueGenerationStrategy)4);
			OperationBuilder<AddColumnOperation> createdAt = table.Column<DateTime>("timestamp with time zone", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num3 = 100;
			OperationBuilder<AddColumnOperation> key = table.Column<string>("character varying(100)", (bool?)null, num3, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num3 = 100;
			return new
			{
				Id = id,
				CreatedAt = createdAt,
				Key = key,
				Name = table.Column<string>("character varying(100)", (bool?)null, num3, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				RequestPayload = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				ResponsePayload = table.Column<string>("text", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				StatusCode = table.Column<int>("integer", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_IdempotencyRecords", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateIndex("IX_IdempotencyRecords_Key", "IdempotencyRecords", "Key", (string)null, true, (string)null, (bool[])null);
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
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("TokenHash").IsRequired(true).HasMaxLength(256), "character varying(256)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("UserId"), "integer");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "TokenHash" });
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
