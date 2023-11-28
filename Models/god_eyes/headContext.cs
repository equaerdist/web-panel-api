using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web_panel_api.Models.god_eyes
{
    public partial class headContext : DbContext
    {
        public headContext()
        {
        }

        public headContext(DbContextOptions<headContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Balance> Balances { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Pay> Pays { get; set; } = null!;
        public virtual DbSet<Queue> Queues { get; set; } = null!;
        public virtual DbSet<Referral> Referrals { get; set; } = null!;
        public virtual DbSet<SendMessage> SendMessages { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("host=localhost;port=3306;database=head;uid=root;convertzerodatetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("balances");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Balance)
                    .HasForeignKey<Balance>(d => d.UserId)
                    .HasConstraintName("Balances_ibfk_1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("clients");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.IsUsedBy, "is_used_by");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ApiHash)
                    .HasMaxLength(100)
                    .HasColumnName("api_hash");

                entity.Property(e => e.ApiId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("api_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.IsUsedBy)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("is_used_by");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RequestsBalance)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("requests_balance");

                entity.Property(e => e.SessionString)
                    .HasMaxLength(1024)
                    .HasColumnName("session_string");

                entity.HasOne(d => d.IsUsedByNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IsUsedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Clients_ibfk_1");
            });

            modelBuilder.Entity<Pay>(entity =>
            {
                entity.ToTable("pays");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasColumnType("enum('USDT','TRX','BNB','TON','DEL')")
                    .HasColumnName("currency");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Method)
                    .HasColumnType("enum('BALANCE','REFERRALS','OUTPUT','MESSAGE')")
                    .HasColumnName("method");

                entity.Property(e => e.PaidAt)
                    .HasColumnType("datetime")
                    .HasColumnName("paid_at");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pays)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Pays_ibfk_1");
            });

            modelBuilder.Entity<Queue>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("queue");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.HasKey(e => e.ChildId)
                    .HasName("PRIMARY");

                entity.ToTable("referrals");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.ChildId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("child_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id");

                entity.HasOne(d => d.Child)
                    .WithOne(p => p.ReferralChild)
                    .HasForeignKey<Referral>(d => d.ChildId)
                    .HasConstraintName("Referrals_ibfk_1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ReferralParents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Referrals_ibfk_2");
            });

            modelBuilder.Entity<SendMessage>(entity =>
            {
                entity.ToTable("send_message");

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

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("settings");

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
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsReplay).HasColumnName("is_replay");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallets");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasColumnType("enum('USDT','TON','DEL','BNB','TRX')")
                    .HasColumnName("currency");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("wallets_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
