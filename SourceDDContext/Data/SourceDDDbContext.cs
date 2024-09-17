using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SourceDDContext.Models;

namespace SourceDDContext.Data;

public partial class SourceDDDbContext : DbContext
{
    public SourceDDDbContext()
    {
    }

    public SourceDDDbContext(DbContextOptions<SourceDDDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<ColumnTarget> ColumnTargets { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS22;Initial Catalog=GreenfieldDD;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => new { e.TableSchema, e.TableName, e.ColumnName }).HasName("PK_Schema_Table_Column");

            entity.Property(e => e.TableSchema)
                .HasMaxLength(128)
                .HasColumnName("TABLE_SCHEMA");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("TABLE_NAME");
            entity.Property(e => e.ColumnName)
                .HasMaxLength(128)
                .HasColumnName("COLUMN_NAME");
            entity.Property(e => e.CharacterMaximumLength).HasColumnName("CHARACTER_MAXIMUM_LENGTH");
            entity.Property(e => e.CharacterOctetLength).HasColumnName("CHARACTER_OCTET_LENGTH");
            entity.Property(e => e.CharacterSetCatalog)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_CATALOG");
            entity.Property(e => e.CharacterSetName)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_NAME");
            entity.Property(e => e.CharacterSetSchema)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_SCHEMA");
            entity.Property(e => e.CollationCatalog)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_CATALOG");
            entity.Property(e => e.CollationName)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_NAME");
            entity.Property(e => e.CollationSchema)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_SCHEMA");
            entity.Property(e => e.ColumnDefault)
                .HasMaxLength(4000)
                .HasColumnName("COLUMN_DEFAULT");
            entity.Property(e => e.DataType)
                .HasMaxLength(128)
                .HasColumnName("DATA_TYPE");
            entity.Property(e => e.DatetimePrecision).HasColumnName("DATETIME_PRECISION");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DestinationColumn)
                .HasMaxLength(128)
                .HasColumnName("Destination_Column");
            entity.Property(e => e.DestinationTable)
                .HasMaxLength(128)
                .HasColumnName("Destination_Table");
            entity.Property(e => e.DistinctValues).HasColumnName("DISTINCT_VALUES");
            entity.Property(e => e.DomainCatalog)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_CATALOG");
            entity.Property(e => e.DomainName)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_NAME");
            entity.Property(e => e.DomainSchema)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_SCHEMA");
            entity.Property(e => e.IsNullable)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IS_NULLABLE");
            entity.Property(e => e.NonNulls).HasColumnName("NON_NULLS");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.NumericPrecision).HasColumnName("NUMERIC_PRECISION");
            entity.Property(e => e.NumericPrecisionRadix).HasColumnName("NUMERIC_PRECISION_RADIX");
            entity.Property(e => e.NumericScale).HasColumnName("NUMERIC_SCALE");
            entity.Property(e => e.OrdinalPosition).HasColumnName("ORDINAL_POSITION");
            entity.Property(e => e.TableCatalog)
                .HasMaxLength(128)
                .HasColumnName("TABLE_CATALOG");

            entity.HasOne(d => d.Table).WithMany(p => p.Columns)
                .HasForeignKey(d => new { d.TableSchema, d.TableName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schema_Table");
        });

        modelBuilder.Entity<ColumnTarget>(entity =>
        {
            entity.HasKey(e => new { e.SourceTable, e.SourceColumn, e.TargetTable, e.TargetColumn }).HasName("PK_Schema_Table_Column_Target");

            entity.Property(e => e.SourceTable)
                .HasMaxLength(128)
                .HasColumnName("Source_Table");
            entity.Property(e => e.SourceColumn)
                .HasMaxLength(128)
                .HasColumnName("Source_Column");
            entity.Property(e => e.TargetTable)
                .HasMaxLength(128)
                .HasColumnName("Target_Table");
            entity.Property(e => e.TargetColumn)
                .HasMaxLength(128)
                .HasColumnName("Target_Column");
            entity.Property(e => e.SourceSchema)
                .HasMaxLength(128)
                .HasColumnName("Source_Schema");
            entity.Property(e => e.TargetSchema)
                .HasMaxLength(128)
                .HasColumnName("Target_Schema");

            entity.HasOne(d => d.Column).WithMany(p => p.ColumnTargets)
                .HasForeignKey(d => new { d.SourceSchema, d.SourceTable, d.SourceColumn })
                .HasConstraintName("FK_Source_Column");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => new { e.TableSchema, e.TableName }).HasName("PK_Schema_Table");

            entity.Property(e => e.TableSchema)
                .HasMaxLength(128)
                .HasColumnName("TABLE_SCHEMA");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("TABLE_NAME");
            entity.Property(e => e.TableCatalog)
                .HasMaxLength(128)
                .HasColumnName("TABLE_CATALOG");
            entity.Property(e => e.ColCount).HasColumnName("COL_COUNT");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DestinationTable)
                .HasMaxLength(500)
                .HasColumnName("Destination_Table");
            entity.Property(e => e.RowCount).HasColumnName("ROW_COUNT");
            entity.Property(e => e.XStgTable)
                .HasMaxLength(500)
                .HasColumnName("xSTG_Table");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
