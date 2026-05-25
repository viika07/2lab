using Microsoft.EntityFrameworkCore;

namespace FitnessClubApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }
        public DbSet<PersonalTraining> PersonalTrainings { get; set; }
        public DbSet<AvailableTraining> AvailableTrainings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=fitness_db;Username=postgres;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // базовая таблица training_sessions
            modelBuilder.Entity<TrainingSession>(entity =>
            {
                entity.ToTable("training_sessions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.TrainingType).HasColumnName("training_type").HasMaxLength(20);
                entity.UseTptMappingStrategy();
            });

            // таблица group_trainings (наследник)
            modelBuilder.Entity<GroupTraining>(entity =>
            {
                entity.ToTable("group_trainings");
                entity.Property(e => e.TrainerName).HasColumnName("trainer_name").HasMaxLength(100);
                entity.Property(e => e.RoomNumber).HasColumnName("room_number");
                entity.Property(e => e.MaxParticipants).HasColumnName("max_participants");
                entity.Property(e => e.CurrentParticipants).HasColumnName("current_participants");
            });

            // таблица personal_trainings (наследник)
            modelBuilder.Entity<PersonalTraining>(entity =>
            {
                entity.ToTable("personal_trainings");
                entity.Property(e => e.ClientName).HasColumnName("client_name").HasMaxLength(100);
                entity.Property(e => e.Equipment).HasColumnName("equipment").HasMaxLength(100);
                entity.Property(e => e.IntensityLevel).HasColumnName("intensity_level");
            });

            // таблица available_trainings
            modelBuilder.Entity<AvailableTraining>(entity =>
            {
                entity.ToTable("available_trainings");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.TrainingType).HasColumnName("training_type");
                entity.Property(e => e.TrainerName).HasColumnName("trainer_name");
                entity.Property(e => e.RoomNumber).HasColumnName("room_number");
                entity.Property(e => e.MaxParticipants).HasColumnName("max_participants");
                entity.Property(e => e.ClientName).HasColumnName("client_name");
                entity.Property(e => e.Equipment).HasColumnName("equipment");
                entity.Property(e => e.IntensityLevel).HasColumnName("intensity_level");
            });
        }
    }
}