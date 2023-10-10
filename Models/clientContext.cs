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
        public virtual DbSet<ReferralsTree> ReferralsTrees { get; set; } = null!;
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

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(5)
                    .HasColumnName("currency");

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

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PayHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pay_history_ibfk_1");
            });

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.ToTable("promocodes");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TariffId, "promocodes_ibfk_2");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.PriceDel).HasColumnName("price_del");

                entity.Property(e => e.PriceRub).HasColumnName("price_rub");

                entity.Property(e => e.PriceTon).HasColumnName("price_ton");

                entity.Property(e => e.PriceUsdt).HasColumnName("price_usdt");

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

            modelBuilder.Entity<ReferralsTree>(entity =>
            {
                entity.ToTable("referrals_tree");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ChildId, "child_id");

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.ChildId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("Child_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("Parent_id");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.ReferralsTreeChildren)
                    .HasForeignKey(d => d.ChildId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referrals_tree_ibfk_2");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ReferralsTreeParents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referrals_tree_ibfk_1");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("servers");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Apiurl)
                    .HasMaxLength(255)
                    .HasColumnName("apiurl");

                entity.Property(e => e.Certsha256)
                    .HasMaxLength(255)
                    .HasColumnName("certsha256");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(45)
                    .HasColumnName("created_at");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CommissionInputDel)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_input_del");

                entity.Property(e => e.CommissionInputTon)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_input_ton");

                entity.Property(e => e.CommissionInputUsdt)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_input_usdt");

                entity.Property(e => e.CommissionOutputDel)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_output_del");

                entity.Property(e => e.CommissionOutputRub)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_output_rub");

                entity.Property(e => e.CommissionOutputTon)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_output_ton");

                entity.Property(e => e.CommissionOutputUsdt)
                    .HasColumnType("int(11)")
                    .HasColumnName("commission_output_usdt");

                entity.Property(e => e.MinOutput)
                    .HasColumnType("int(11)")
                    .HasColumnName("min_output");

                entity.Property(e => e.ReferralRewardLvl1)
                    .HasColumnType("int(11)")
                    .HasColumnName("referral_reward_lvl_1");

                entity.Property(e => e.ReferralRewardLvl2)
                    .HasColumnType("int(11)")
                    .HasColumnName("referral_reward_lvl_2");

                entity.Property(e => e.ReferralRewardLvl3)
                    .HasColumnType("int(11)")
                    .HasColumnName("referral_reward_lvl_3");
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

                entity.HasIndex(e => e.PromocodeId, "promocode_id");

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
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Promocode)
                    .WithMany(p => p.UsePromocodes)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("use_promocodes_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.UseCollation("utf8mb4_unicode_ci");

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

                entity.HasIndex(e => e.ServerId, "server_Id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("date_end");

                entity.Property(e => e.Key)
                    .HasMaxLength(255)
                    .HasColumnName("key");

                entity.Property(e => e.ServerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("server_Id");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.UsersKeys)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("users_keys_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UsersKeys)
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
                entity.HasNoKey();

                entity.ToTable("wallets");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Addresse)
                    .HasMaxLength(255)
                    .HasColumnName("addresse");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .HasColumnName("type");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wallets_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
