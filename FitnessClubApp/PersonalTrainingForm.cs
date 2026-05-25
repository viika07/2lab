using System;
using System.Windows.Forms;

namespace FitnessClubApp
{
    public partial class PersonalTrainingForm : Form
    {
        private PersonalTraining _training;
        private AppDbContext _context;

        private Label lblInfo;
        private Button btnStart;
        private Button btnFinish;
        private Button btnSetIntensity;
        private Button btnGetProgress;
        private Button btnGetDescription; 
        private TrackBar trackIntensity;
        private Label lblIntensity;
        private Label lblIntensityValue;

        public PersonalTrainingForm(PersonalTraining training)
        {
            _training = training;
            _context = new AppDbContext();
            InitializeComponent();
            LoadFromDatabase();
            UpdateUI();
        }

        private void InitializeComponent()
        {
            lblInfo = new Label();
            btnStart = new Button();
            btnFinish = new Button();
            btnSetIntensity = new Button();
            btnGetProgress = new Button();
            btnGetDescription = new Button();
            trackIntensity = new TrackBar();
            lblIntensityValue = new Label();
            lblIntensity = new Label();
            ((System.ComponentModel.ISupportInitialize)trackIntensity).BeginInit();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.Location = new Point(12, 12);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(460, 180);
            lblInfo.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 200);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(80, 40);
            btnStart.TabIndex = 1;
            btnStart.Text = "Начать";
            btnStart.Click += btnStart_Click;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(98, 200);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(99, 40);
            btnFinish.TabIndex = 2;
            btnFinish.Text = "Завершить";
            btnFinish.Click += btnFinish_Click;
            // 
            // btnSetIntensity
            // 
            btnSetIntensity.Location = new Point(12, 305);
            btnSetIntensity.Name = "btnSetIntensity";
            btnSetIntensity.Size = new Size(438, 40);
            btnSetIntensity.TabIndex = 9;
            btnSetIntensity.Text = "Применить интенсивность";
            btnSetIntensity.Click += btnSetIntensity_Click;
            // 
            // btnGetProgress
            // 
            btnGetProgress.Location = new Point(203, 200);
            btnGetProgress.Name = "btnGetProgress";
            btnGetProgress.Size = new Size(87, 40);
            btnGetProgress.TabIndex = 3;
            btnGetProgress.Text = "Прогресс";
            btnGetProgress.Click += btnGetProgress_Click;
            // 
            // btnGetDescription
            // 
            btnGetDescription.Location = new Point(296, 200);
            btnGetDescription.Name = "btnGetDescription";
            btnGetDescription.Size = new Size(88, 40);
            btnGetDescription.TabIndex = 4;
            btnGetDescription.Text = "Описание";
            btnGetDescription.Click += btnGetDescription_Click;
            // 
            // trackIntensity
            // 
            trackIntensity.Location = new Point(110, 252);
            trackIntensity.Minimum = 1;
            trackIntensity.Name = "trackIntensity";
            trackIntensity.Size = new Size(200, 56);
            trackIntensity.TabIndex = 7;
            trackIntensity.Value = 5;
            trackIntensity.Scroll += trackIntensity_Scroll;
            // 
            // lblIntensityValue
            // 
            lblIntensityValue.Location = new Point(320, 255);
            lblIntensityValue.Name = "lblIntensityValue";
            lblIntensityValue.Size = new Size(40, 25);
            lblIntensityValue.TabIndex = 8;
            lblIntensityValue.Text = "5";
            // 
            // lblIntensity
            // 
            lblIntensity.Location = new Point(12, 255);
            lblIntensity.Name = "lblIntensity";
            lblIntensity.Size = new Size(100, 25);
            lblIntensity.TabIndex = 6;
            lblIntensity.Text = "Интенсивность:";
            // 
            // PersonalTrainingForm
            // 
            ClientSize = new Size(464, 370);
            Controls.Add(lblInfo);
            Controls.Add(btnStart);
            Controls.Add(btnFinish);
            Controls.Add(btnGetProgress);
            Controls.Add(btnGetDescription);
            Controls.Add(lblIntensity);
            Controls.Add(trackIntensity);
            Controls.Add(lblIntensityValue);
            Controls.Add(btnSetIntensity);
            Name = "PersonalTrainingForm";
            Text = "Персональная тренировка";
            FormClosing += OnFormClosing;
            ((System.ComponentModel.ISupportInitialize)trackIntensity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnGetDescription_Click(object sender, EventArgs e)
        {
            try
            {
                string description = _training.GetDescription();
                MessageBox.Show(description, "Описание тренировки",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetProgress_Click(object sender, EventArgs e)
        {
            try
            {
                string progress = _training.GetClientProgress();
                MessageBox.Show(progress, "Прогресс клиента",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromDatabase()
        {
            var dbTraining = _context.PersonalTrainings.Find(_training.Id);
            if (dbTraining != null)
            {
                _training.Title = dbTraining.Title;
                _training.Duration = dbTraining.Duration;
                _training.IsActive = dbTraining.IsActive;
                _training.ClientName = dbTraining.ClientName;
                _training.Equipment = dbTraining.Equipment;
                _training.IntensityLevel = dbTraining.IntensityLevel;
            }
        }

        private void SaveToDatabase()
        {
            var dbTraining = _context.PersonalTrainings.Find(_training.Id);
            if (dbTraining != null)
            {
                dbTraining.IsActive = _training.IsActive;
                dbTraining.IntensityLevel = _training.IntensityLevel;
                _context.SaveChanges();
            }
        }

        private void UpdateUI()
        {
            lblInfo.Text = _training.GetInfo();

            btnStart.Enabled = !_training.IsActive;
            btnFinish.Enabled = _training.IsActive;

            trackIntensity.Value = _training.IntensityLevel;
            lblIntensityValue.Text = _training.IntensityLevel.ToString();

            btnSetIntensity.Enabled = !_training.IsActive;
            trackIntensity.Enabled = !_training.IsActive;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_training.IsActive)
                {
                    MessageBox.Show("Тренировка уже идёт!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _training.Start();
                SaveToDatabase();
                UpdateUI();

                string intensityText = GetIntensityText(_training.IntensityLevel);

                MessageBox.Show($"Тренировка началась!\n\n" +
                               $"Клиент: {_training.ClientName}\n" +
                               $"Интенсивность: {_training.IntensityLevel}/10 ({intensityText})\n" +
                               $"Оборудование: {_training.Equipment}\n\n" +
                               $"Удачной тренировки!",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_training.IsActive)
                {
                    MessageBox.Show("Тренировка ещё не началась!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _training.Stop();
                SaveToDatabase();
                UpdateUI();

                string intensityText = GetIntensityText(_training.IntensityLevel);

                MessageBox.Show($"Тренировка завершена!\n\n" +
                               $"Клиент: {_training.ClientName}\n" +
                               $"Интенсивность: {_training.IntensityLevel}/10 ({intensityText})\n" +
                               $"{_training.GetClientProgress()}\n\n" +
                               $"Отличная работа!",
                    "Завершение тренировки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackIntensity_Scroll(object sender, EventArgs e)
        {
            lblIntensityValue.Text = trackIntensity.Value.ToString();
        }

        private void btnSetIntensity_Click(object sender, EventArgs e)
        {
            try
            {
                if (_training.IsActive)
                {
                    MessageBox.Show("Нельзя изменить интенсивность во время тренировки!\n" +
                                   "Сначала завершите текущую тренировку.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int newIntensity = trackIntensity.Value;
                _training.SetIntensity(newIntensity);
                SaveToDatabase();
                UpdateUI();

                string intensityText = GetIntensityText(newIntensity);

                MessageBox.Show($"Интенсивность изменена на {newIntensity}/10 ({intensityText})\n\n" +
                               $"{_training.GetClientProgress()}",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetIntensityText(int level)
        {
            return level switch
            {
                1 or 2 => "Очень низкая",
                3 or 4 => "Низкая",
                5 or 6 => "Средняя",
                7 or 8 => "Высокая",
                9 or 10 => "Максимальная",
                _ => "Средняя"
            };
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToDatabase();
            _context.Dispose();
        }
    }
}