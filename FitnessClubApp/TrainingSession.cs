using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClubApp
{
    public abstract class TrainingSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private string? _title;
        private int _duration;
        private bool _isActive;
        private string? _trainingType;

        [Required]
        [MaxLength(200)]
        public string? Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название не может быть пустым");
                _title = value;
            }
        }

        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Длительность должна быть больше 0");
                _duration = value;
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        [MaxLength(20)]
        public string? TrainingType
        {
            get => _trainingType;
            set => _trainingType = value;
        }

        protected TrainingSession()
        {
            _title = string.Empty;
            _trainingType = string.Empty;
        }

        protected TrainingSession(string title, int duration, string trainingType)
        {
            Title = title;
            Duration = duration;
            TrainingType = trainingType;
            IsActive = false;
        }

        public virtual void Start()
        {
            IsActive = true;
        }

        public virtual void Stop()
        {
            IsActive = false;
        }

        public virtual string GetInfo()
        {
            return $"Название: {Title}\nДлительность: {Duration} мин\nСтатус: {(IsActive ? "Активна" : "Не активна")}\nТип: {TrainingType}";
        }

        public abstract string GetDescription();
        public abstract string GetDetails();

        public override string ToString()
        {
            return $"{Title} (ID: {Id})";
        }
    }
}