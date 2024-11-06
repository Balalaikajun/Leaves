using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acces.Context
{
    public partial class LeavesDbContext : DbContext
    {

        //public LeavesDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        public LeavesDbContext(DbContextOptions<LeavesDbContext> options)
       : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Transaction> Tranasactions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        //private readonly string _connectionString;
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.PkAccountid).HasName("account_pkey");

                entity.ToTable("accounts");

                entity.Property(e => e.PkAccountid)
                    .HasDefaultValueSql("nextval('account_pk_accountid_seq'::regclass)")
                    .HasColumnName("pk_accountid");
                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");
                entity.Property(e => e.Created)
                    .HasPrecision(3)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("created");
                entity.Property(e => e.FkUserid).HasColumnName("fk_userid");

                entity.HasOne(d => d.FkUser).WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.FkUserid)
                    .HasConstraintName("account_fk_userid_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.PkTransactionid).HasName("tranasactions_pkey");

                entity.ToTable("tranasactions");

                entity.Property(e => e.PkTransactionid).HasColumnName("pk_transactionid");
                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");
                entity.Property(e => e.Created)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("created");
                entity.Property(e => e.FkAccountidfrom).HasColumnName("fk_accountidfrom");
                entity.Property(e => e.FkAccountidto).HasColumnName("fk_accountidto");

                entity.HasOne(d => d.FkAccountidfromNavigation).WithMany(p => p.TransactionFkAccountidfromNavigations)
                    .HasForeignKey(d => d.FkAccountidfrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tranasactions_fk_accountidfrom_fkey");

                entity.HasOne(d => d.FkAccountidtoNavigation).WithMany(p => p.TransactionFkAccountidtoNavigations)
                    .HasForeignKey(d => d.FkAccountidto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tranasactions_fk_accountidto_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PkUserid).HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.UqLogin, "users_uq_login_key").IsUnique();

                entity.Property(e => e.PkUserid).HasColumnName("pk_userid");
                entity.Property(e => e.Created)
                    .HasPrecision(0)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("created");
                entity.Property(e => e.UqLogin)
                    .HasMaxLength(100)
                    .HasColumnName("uq_login");
                entity.Property(e => e.Userpassword)
                    .HasMaxLength(128)
                    .HasColumnName("userpassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
