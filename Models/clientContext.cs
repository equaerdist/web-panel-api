using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web_panel_api.Models
{
    public partial class clientContext : DbContext
    {
        public clientContext()
        {
        }

        public clientContext(DbContextOptions<clientContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PayHistory> PayHistories { get; set; } = null!;
        public virtual DbSet<Promocode> Promocodes { get; set; } = null!;
        public virtual DbSet<PromocodesPrice> PromocodesPrices { get; set; } = null!;
        public virtual DbSet<ReferralsTree> ReferralsTrees { get; set; } = null!;
        public virtual DbSet<SendMessage> SendMessages { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Tariff> Tariffs { get; set; } = null!;
        public virtual DbSet<UsePromocode> UsePromocodes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersKey> UsersKeys { get; set; } = null!;
        public virtual DbSet<UsersTariff> UsersTariffs { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("host=localhost;port=3306;database=client;uid=root;convertzerodatetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<PayHistory>(entity =>
            {
                entity.ToTable("pay_history");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TariffId, "tariff_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .HasColumnName("currency");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.PaidAt)
                    .HasColumnType("datetime")
                    .HasColumnName("paid_at");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(9)
                    .HasColumnName("payment_method");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(8)
                    .HasColumnName("payment_type");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.StatusPay)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status_pay");

                entity.Property(e => e.TariffId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tariff_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.PayHistories)
                    .HasForeignKey(d => d.TariffId)
                    .HasConstraintName("pay_history_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PayHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("pay_history_ibfk_1");
            });

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.ToTable("promocodes");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TariffId, "tariff_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.TariffId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tariff_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(5)
                    .HasColumnName("value_code");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.Promocodes)
                    .HasForeignKey(d => d.TariffId)
                    .HasConstraintName("promocodes_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Promocodes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("promocodes_ibfk_1");
            });

            modelBuilder.Entity<PromocodesPrice>(entity =>
            {
                entity.ToTable("promocodes_price");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.PromocodeId, "fk_promocode_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Currency)
                    .HasMaxLength(45)
                    .HasColumnName("currency");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.PromocodeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");

                entity.HasOne(d => d.Promocode)
                    .WithMany(p => p.PromocodesPrices)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_promocode");
            });

            modelBuilder.Entity<ReferralsTree>(entity =>
            {
                entity.ToTable("referrals_tree");

                entity.HasIndex(e => e.ChildrenId, "children_id");

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ChildrenId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("children_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id");

                entity.HasOne(d => d.Children)
                    .WithMany(p => p.ReferralsTreeChildren)
                    .HasPrincipalKey(p => p.ChatId)
                    .HasForeignKey(d => d.ChildrenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referrals_tree_ibfk_2");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ReferralsTreeParents)
                    .HasPrincipalKey(p => p.ChatId)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referrals_tree_ibfk_1");
            });

            modelBuilder.Entity<SendMessage>(entity =>
            {
                entity.ToTable("send_message");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("date_create");

                entity.Property(e => e.Send)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("servers");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ApiUrl)
                    .HasMaxLength(255)
                    .HasColumnName("api_url");

                entity.Property(e => e.CertSha256)
                    .HasMaxLength(255)
                    .HasColumnName("cert_sha256");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(45)
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("settings");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(250)
                    .HasColumnName("name_user");

                entity.Property(e => e.Value)
                    .HasColumnType("json")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Tariff>(entity =>
            {
                entity.ToTable("tariffs");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Duration)
                    .HasColumnType("int(11)")
                    .HasColumnName("duration");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.TariffName)
                    .HasMaxLength(45)
                    .HasColumnName("tariff_name");
            });

            modelBuilder.Entity<UsePromocode>(entity =>
            {
                entity.ToTable("use_promocodes");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.PromocodeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ChatId, "unique_chat")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ChatId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("chat_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsFree)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_free");

                entity.Property(e => e.IsReplay)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_replay");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .HasColumnName("last_name");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.StatusTariff)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status_tariff");

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UsersKey>(entity =>
            {
                entity.ToTable("users_keys");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TariffId, "tariff_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.DateEnd).HasColumnName("date_end");

                entity.Property(e => e.KeyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("key_id");

                entity.Property(e => e.KeyUrl)
                    .HasMaxLength(255)
                    .HasColumnName("key_url");

                entity.Property(e => e.ServerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("server_id");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.TariffId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tariff_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.UsersKeys)
                    .HasForeignKey(d => d.TariffId)
                    .HasConstraintName("users_keys_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersKeys)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("users_keys_ibfk_1");
            });

            modelBuilder.Entity<UsersTariff>(entity =>
            {
                entity.ToTable("users_tariff");

                entity.HasIndex(e => e.TariffId, "tariff_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Duration)
                    .HasColumnType("int(11)")
                    .HasColumnName("duration");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.TariffId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tariff_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.UsersTariffs)
                    .HasForeignKey(d => d.TariffId)
                    .HasConstraintName("users_tariff_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersTariffs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_tariff_ibfk_1");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallets");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Addresse)
                    .HasMaxLength(255)
                    .HasColumnName("addresse");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BalanceFact).HasColumnName("balance_fact");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(45)
                    .HasColumnName("currency");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .HasColumnName("type");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wallets_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
