using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClubApp
{
    public class GroupTraining : TrainingSession
    {
        private string? _trainerName;
        private int _roomNumber;
        private int _maxParticipants;
        private int _currentParticipants;

        [Required]
        [MaxLength(100)]
        public string? TrainerName
        {
            get => _trainerName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя тренера не может быть пустым");
                _trainerName = value;
            }
        }

        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Номер зала должен быть больше 0");
                _roomNumber = value;
            }
        }

        public int MaxParticipants
        {
            get => _maxParticipants;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Максимум участников должен быть больше 0");
                _maxParticipants = value;
            }
        }

        public int CurrentParticipants
        {
            get => _currentParticipants;
            set
            {
                if (value < 0 || value > MaxParticipants)
                    throw new ArgumentException("Некорректное количество участников");
                _currentParticipants = value;
            }
        }

        public GroupTraining() : base()
        {
            TrainingType = "Group";
            _trainerName = string.Empty;
        }

        // Конструктор 1: базовый 
        public GroupTraining(string title, int duration, int maxParticipants, string trainerName, int roomNumber)
            : base(title, duration, "Group")
        {
            TrainerName = trainerName;
            RoomNumber = roomNumber;
            MaxParticipants = maxParticipants;
            CurrentParticipants = 0;
        }

        // Конструктор 2: с начальным количеством участников 
        public GroupTraining(string title, int duration, int maxParticipants, string trainerName, int roomNumber, int currentParticipants)
            : base(title, duration, "Group")
        {
            TrainerName = trainerName;
            RoomNumber = roomNumber;
            MaxParticipants = maxParticipants;
            CurrentParticipants = currentParticipants;
        }

        // Конструктор 3: копирование с изменением тренера и зала 
        public GroupTraining(GroupTraining other, string newTrainerName, int newRoomNumber)
            : base(other.Title, other.Duration, "Group")
        {
            TrainerName = newTrainerName;
            RoomNumber = newRoomNumber;
            MaxParticipants = other.MaxParticipants;
            CurrentParticipants = other.CurrentParticipants;
        }

        public void AddParticipant()
        {
            if (CurrentParticipants >= MaxParticipants)
                throw new InvalidOperationException($"Достигнут лимит участников! Максимум: {MaxParticipants}");

            CurrentParticipants++;
        }

        public void AddParticipants(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Количество должно быть положительным");
            if (CurrentParticipants + count > MaxParticipants)
                throw new InvalidOperationException($"Недостаточно мест. Свободно: {MaxParticipants - CurrentParticipants}");

            CurrentParticipants += count;
        }

        public override void Start()
        {
            base.Start();
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"\nТренер: {TrainerName}\nЗал: {RoomNumber}\nУчастники: {CurrentParticipants}/{MaxParticipants}";
        }

        public override string GetDescription()
        {
            return $"Групповая тренировка '{Title}' с тренером {TrainerName} (зал №{RoomNumber})";
        }

        public override string GetDetails()
        {
            return $"Групповая: {Title}, зал {RoomNumber}, тренер: {TrainerName}, участников: {CurrentParticipants}/{MaxParticipants}";
        }

        public string GetTrainerInfo()
        {
            return $"Инструктор: {TrainerName}, зал: {RoomNumber}";
        }
    }
}