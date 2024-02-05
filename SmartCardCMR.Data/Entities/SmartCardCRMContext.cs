using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class SmartCardCRMContext : DbContext
    {
        public SmartCardCRMContext(DbContextOptions<SmartCardCRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingObservationFiles> BookingObservationFiles { get; set; }
        public virtual DbSet<BookingObservations> BookingObservations { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientDebitCreditCards> ClientDebitCreditCards { get; set; }
        public virtual DbSet<ConfigurationSettings> ConfigurationSettings { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractBeneficiaries> ContractBeneficiaries { get; set; }
        public virtual DbSet<CustomerServiceObservationFiles> CustomerServiceObservationFiles { get; set; }
        public virtual DbSet<CustomerServiceObservations> CustomerServiceObservations { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLog { get; set; }
        public virtual DbSet<Quoter> Quoter { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingObservationFiles>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.BookingObservation)
                    .WithMany(p => p.BookingObservationFiles)
                    .HasForeignKey(d => d.BookingObservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingObservationFiles_BookingObservations");
            });

            modelBuilder.Entity<BookingObservations>(entity =>
            {
                entity.Property(e => e.ObservationDate).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.BookingObservations)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingObservations_Contract");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CarBrandModel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Closer)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerBirthDate).HasColumnType("date");

                entity.Property(e => e.CoOwnerCellPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerDebitCreditBanks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerDepartment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerDocumentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerDocumentType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerGender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerOffice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerProfession)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoOwnerTourismIndustry)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DebitCreditBanks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HasWorkedWithTourismIndustry).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.HousingType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Linner)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations).IsUnicode(false);

                entity.Property(e => e.Office)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Profession)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.Property(e => e.TlmkCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TourismIndustry)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Voucher)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientDebitCreditCards>(entity =>
            {
                entity.Property(e => e.BankName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CardType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientDebitCreditCards)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientDebitCreditCards_Client");
            });

            modelBuilder.Entity<ConfigurationSettings>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Adviser1Signature).IsUnicode(false);

                entity.Property(e => e.Adviser2Signature).IsUnicode(false);

                entity.Property(e => e.AuthorizationSignature).IsUnicode(false);

                entity.Property(e => e.CoOwnerSignature).IsUnicode(false);

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.ContractNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContractorSignature).IsUnicode(false);

                entity.Property(e => e.LendInstallmentDate).HasColumnType("date");

                entity.Property(e => e.Observations).IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Client");
            });

            modelBuilder.Entity<ContractBeneficiaries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastNames)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Names)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractBeneficiaries)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractBeneficiaries_Contract");
            });

            modelBuilder.Entity<CustomerServiceObservationFiles>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerServiceObservation)
                    .WithMany(p => p.CustomerServiceObservationFiles)
                    .HasForeignKey(d => d.CustomerServiceObservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerServiceObservationFiles_CustomerServiceObservations");
            });

            modelBuilder.Entity<CustomerServiceObservations>(entity =>
            {
                entity.Property(e => e.ObservationDate).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CustomerServiceObservations)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerServiceObservations_Contract");
            });

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.Property(e => e.ComponentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionMessage)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionTraceLog)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Quoter>(entity =>
            {
                entity.ToTable("Quoter");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cellphone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Scopes)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(d => d.IdQuoterNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdQuoter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_Quoter");

                entity.Property(e => e.KidsAges)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasDatabaseName("IX_User")
                    .IsUnique();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
