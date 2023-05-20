using System;
using System.Collections.Generic;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Data;

public partial class HomeFlooryDbContext : DbContext
{
    public HomeFlooryDbContext()
    {
    }

    public HomeFlooryDbContext(DbContextOptions<HomeFlooryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdresaIsporuke> AdreseIsporuke { get; set; }

    public virtual DbSet<DodatiProizvodi> DodatiProizvodi { get; set; }

    public virtual DbSet<Dostava> Dostave { get; set; }

    public virtual DbSet<Kategorija> Kategorije { get; set; }

    public virtual DbSet<Korisnik> Korisnici { get; set; }

    public virtual DbSet<Korpa> Korpe { get; set; }

    public virtual DbSet<Placanje> Placanja { get; set; }

    public virtual DbSet<Proizvod> Proizvodi { get; set; }

    public virtual DbSet<Proizvodjac> Proizvodjaci { get; set; }

    public virtual DbSet<Uloga> Uloge { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source= DESKTOP-V9A58S9\\SQLEXPRESS; Initial Catalog=HomeFloory;Integrated Security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdresaIsporuke>(entity =>
        {
            entity.HasKey(e => e.IdAdresaIsporuke);

            entity.ToTable("AdresaIsporuke");

            entity.Property(e => e.IdAdresaIsporuke).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Drzava)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Grad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PostanskiBroj)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Ulica)
                .HasMaxLength(35)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DodatiProizvodi>(entity =>
        {
            entity.HasKey(e => new { e.IdProizvod, e.IdKorpa });

            entity.ToTable("DodatiProizvodi", tb => tb.HasTrigger("triger_korpa"));
            entity.ToTable("DodatiProizvodi", tb => tb.HasTrigger("triger_uklanjanje_proizvoda"));

            entity.Property(e => e.IdProizvod).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.IdKorpa).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Cena).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Kolicina).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.KolicinaPoM2).HasColumnType("decimal(12, 3)");

            entity.HasOne(d => d.IdKorpaNavigation).WithMany(p => p.DodatiProizvodi)
                .HasForeignKey(d => d.IdKorpa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DodatiProizvodi_Korpa");

            entity.HasOne(d => d.IdProizvodNavigation).WithMany(p => p.DodatiProizvodi)
                .HasForeignKey(d => d.IdProizvod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DodatiProizvodi_Proizvod");
        });

        modelBuilder.Entity<Dostava>(entity =>
        {
            entity.HasKey(e => e.IdDostava);

            entity.ToTable("Dostava");

            entity.Property(e => e.IdDostava).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.CenaUsluge).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.NazivSluzbe)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.RokDostave)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TipDostave)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.HasKey(e => e.IdKategorija);

            entity.ToTable("Kategorija");

            entity.Property(e => e.IdKategorija).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.NazivKategorije)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.IdKorisnik);

            entity.ToTable("Korisnik");

            entity.Property(e => e.IdKorisnik).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.DatumRodjenja).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdAdresaIsporuke).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.IdUloga).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Ime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Kontakt)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Lozinka)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAdresaIsporukeNavigation).WithMany(p => p.Korisnici)
                .HasForeignKey(d => d.IdAdresaIsporuke)
                .HasConstraintName("FK_Korisnik_AdresaIsporuke");

            entity.HasOne(d => d.IdUlogaNavigation).WithMany(p => p.Korisnici)
                .HasForeignKey(d => d.IdUloga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korisnik_Uloga");
        });

        modelBuilder.Entity<Korpa>(entity =>
        {
            entity.HasKey(e => e.IdKorpa);

            entity.ToTable("Korpa", tb => tb.HasTrigger("trg_Isporuka"));

            entity.Property(e => e.IdKorpa).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.CenaDostave).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.IdDostava).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.IdPlacanje).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.UkupnaCena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDostavaNavigation).WithMany(p => p.Korpe)
                .HasForeignKey(d => d.IdDostava)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korpa_Dostava");

            entity.HasOne(d => d.IdPlacanjeNavigation).WithMany(p => p.Korpe)
                .HasForeignKey(d => d.IdPlacanje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korpa_Placanje");
        });

        modelBuilder.Entity<Placanje>(entity =>
        {
            entity.HasKey(e => e.IdPlacanje);

            entity.ToTable("Placanje");

            entity.Property(e => e.IdPlacanje).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Datum).HasColumnType("date");
            entity.Property(e => e.IdKorisnik).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdKorisnikNavigation).WithMany(p => p.Placanja)
                .HasForeignKey(d => d.IdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Placanje_Korisnik");
        });

        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.HasKey(e => e.IdProizvod);

            entity.ToTable("Proizvod");

            entity.Property(e => e.IdProizvod).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.CenaPoM2).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Dimenzija)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.IdKategorija).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.IdProizvodjac).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Naziv)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Nijansa)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Opis)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.UrlSlike)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PaketPoM2).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.KolicinaNaStanju).HasColumnType("numeric(10, 0)");

            entity.HasOne(d => d.IdKategorijaNavigation).WithMany(p => p.Proizvodi)
                .HasForeignKey(d => d.IdKategorija)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Kategorija");

            entity.HasOne(d => d.IdProizvodjacNavigation).WithMany(p => p.Proizvodi)
                .HasForeignKey(d => d.IdProizvodjac)
                .HasConstraintName("FK_Proizvod_Proizvodjac");
        });

        modelBuilder.Entity<Proizvodjac>(entity =>
        {
            entity.HasKey(e => e.IdProizvodjac);

            entity.ToTable("Proizvodjac");

            entity.Property(e => e.IdProizvodjac).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.NazivProizvodjaca)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Uloga>(entity =>
        {
            entity.HasKey(e => e.IdUloga);

            entity.ToTable("Uloga");

            entity.Property(e => e.IdUloga).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Uloga1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Uloga");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
