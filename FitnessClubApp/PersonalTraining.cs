using System.ComponentModel.DataAnnotations;

namespace FitnessClubApp
{
    public class PersonalTraining : TrainingSession
    {
        private string? _clientName;
        private string? _equipment;
        private int _intensityLevel;

        [Required]
        [MaxLength(100)]
        public string? ClientName
        {
            get => _clientName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя клиента не может быть пустым");
                _clientName = value;
            }
        }

        [Required]
        [MaxLength(100)]
        public string? Equipment
        {
            get => _equipment;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Оборудование не может быть пустым");
                _equipment = value;
            }
        }

        public int IntensityLevel
        {
            get => _intensityLevel;
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentException("Интенсивность должна быть от 1 до 10");
                _intensityLevel = value;
            }
        }

        public PersonalTraining() : base()
        {
            TrainingType = "Personal";
            _clientName = string.Empty;
            _equipment = string.Empty;
        }

        // Конструктор 1: базовый 
        public PersonalTraining(string title, int duration, string clientName, string equipment, int intensityLevel)
            : base(title, duration, "Personal")
        {
            ClientName = clientName;
            Equipment = equipment;
            IntensityLevel = intensityLevel;
        }

        // Конструктор 2: с другим порядком параметров 
        public PersonalTraining(string title, string clientName, int duration, string equipment, int intensityLevel)
            : base(title, duration, "Personal")
        {
            ClientName = clientName;
            Equipment = equipment;
            IntensityLevel = intensityLevel;
        }

        public void SetIntensity(int level)
        {
            IntensityLevel = level;
        }

        public string GetClientProgress()
        {
            string recommendation = IntensityLevel switch
            {
                1 or 2 or 3 => "Рекомендация: увеличить нагрузку постепенно",
                4 or 5 => "Рекомендация: поддерживать текущий темп",
                6 or 7 or 8 or 9 or 10 => "Рекомендация: не забывать про разминку и заминку",
                _ => "Продолжайте в том же духе!"
            };

            return $"ПРОГРЕСС КЛИЕНТА\n" +
                   $"Клиент: {ClientName}\n" +
                   $"Тренировка: {Title}\n" +
                   $"Интенсивность: {IntensityLevel}/10\n" +
                   $"Длительность: {Duration} минут\n" +
                   $"Оборудование: {Equipment}\n" +
                   $"{recommendation}";
        }

        public override void Start()
        {
            base.Start();
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"\nКлиент: {ClientName}\nОборудование: {Equipment}\nИнтенсивность: {IntensityLevel}/10";
        }

        public override string GetDescription()
        {
            return $"Персональная тренировка для {ClientName} с оборудованием: {Equipment} (интенсивность: {IntensityLevel}/10)";
        }

        public override string GetDetails()
        {
            return $"Персональная: {Title}, клиент: {ClientName}, снаряд: {Equipment}, интенсивность: {IntensityLevel}";
        }
    }
}