namespace FitnessClubApp
{
    public class AvailableTraining
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Duration { get; set; }
        public string? TrainingType { get; set; } 

        // для групповых
        public string? TrainerName { get; set; }
        public int? RoomNumber { get; set; }
        public int? MaxParticipants { get; set; }

        // для персональных
        public string? ClientName { get; set; }
        public string? Equipment { get; set; }
        public int? IntensityLevel { get; set; }

        // Конструктор по умолчанию
        public AvailableTraining() { }

        // Конструктор для групповой тренировки 
        public AvailableTraining(string title, int duration, string trainerName, int roomNumber, int maxParticipants)
        {
            Title = title;
            Duration = duration;
            TrainingType = "Group";
            TrainerName = trainerName;
            RoomNumber = roomNumber;
            MaxParticipants = maxParticipants;
        }

        // Конструктор для персональной тренировки 
        public AvailableTraining(string title, int duration, string clientName, string equipment, int intensityLevel)
        {
            Title = title;
            Duration = duration;
            TrainingType = "Personal";
            ClientName = clientName;
            Equipment = equipment;
            IntensityLevel = intensityLevel;
        }

        // Конструктор копирования с изменением названия
        public AvailableTraining(AvailableTraining other, string newTitle)
        {
            Title = newTitle;
            Duration = other.Duration;
            TrainingType = other.TrainingType;
            TrainerName = other.TrainerName;
            RoomNumber = other.RoomNumber;
            MaxParticipants = other.MaxParticipants;
            ClientName = other.ClientName;
            Equipment = other.Equipment;
            IntensityLevel = other.IntensityLevel;
        }
    }
}