using System;
using System.Windows.Forms;

namespace FitnessClubApp
{
    public partial class GroupTrainingForm : Form
    {
        private GroupTraining _training;
        private AppDbContext _context;

        private Label lblInfo;
        private Button btnStart;
        private Button btnFinish;
        private Button btnAddParticipant;
        private Button btnAddMultiple;
        private Button btnGetTrainerInfo;
        private Button btnGetDescription;  
        private Button btnGetDetails;     

        public GroupTrainingForm(GroupTraining training)
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
            btnAddParticipant = new Button();
            btnAddMultiple = new Button();
            btnGetTrainerInfo = new Button();
            btnGetDescription = new Button();
            btnGetDetails = new Button();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.Location = new Point(12, 12);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(460, 200);
            lblInfo.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 220);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 40);
            btnStart.TabIndex = 1;
            btnStart.Text = "Начать";
            btnStart.Click += btnStart_Click;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(120, 220);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(100, 40);
            btnFinish.TabIndex = 2;
            btnFinish.Text = "Завершить";
            btnFinish.Click += btnFinish_Click;
            // 
            // btnAddParticipant
            // 
            btnAddParticipant.Location = new Point(12, 270);
            btnAddParticipant.Name = "btnAddParticipant";
            btnAddParticipant.Size = new Size(100, 40);
            btnAddParticipant.TabIndex = 3;
            btnAddParticipant.Text = "+1 участник";
            btnAddParticipant.Click += btnAddParticipant_Click;
            // 
            // btnAddMultiple
            // 
            btnAddMultiple.Location = new Point(120, 270);
            btnAddMultiple.Name = "btnAddMultiple";
            btnAddMultiple.Size = new Size(134, 40);
            btnAddMultiple.TabIndex = 4;
            btnAddMultiple.Text = "+5 участников";
            btnAddMultiple.Click += btnAddMultiple_Click;
            // 
            // btnGetTrainerInfo
            // 
            btnGetTrainerInfo.Location = new Point(230, 220);
            btnGetTrainerInfo.Name = "btnGetTrainerInfo";
            btnGetTrainerInfo.Size = new Size(116, 40);
            btnGetTrainerInfo.TabIndex = 5;
            btnGetTrainerInfo.Text = "Информация";
            btnGetTrainerInfo.Click += btnGetTrainerInfo_Click;
            // 
            // btnGetDescription
            // 
            btnGetDescription.Location = new Point(277, 270);
            btnGetDescription.Name = "btnGetDescription";
            btnGetDescription.Size = new Size(100, 40);
            btnGetDescription.TabIndex = 6;
            btnGetDescription.Text = "Описание";
            btnGetDescription.Click += btnGetDescription_Click;
            // 
            // btnGetDetails
            // 
            btnGetDetails.Location = new Point(352, 220);
            btnGetDetails.Name = "btnGetDetails";
            btnGetDetails.Size = new Size(100, 40);
            btnGetDetails.TabIndex = 7;
            btnGetDetails.Text = "Детали";
            btnGetDetails.Click += btnGetDetails_Click;
            // 
            // GroupTrainingForm
            // 
            ClientSize = new Size(464, 340);
            Controls.Add(lblInfo);
            Controls.Add(btnStart);
            Controls.Add(btnFinish);
            Controls.Add(btnAddParticipant);
            Controls.Add(btnAddMultiple);
            Controls.Add(btnGetTrainerInfo);
            Controls.Add(btnGetDescription);
            Controls.Add(btnGetDetails);
            Name = "GroupTrainingForm";
            Text = "Групповая тренировка";
            FormClosing += OnFormClosing;
            ResumeLayout(false);
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

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string details = _training.GetDetails();
                MessageBox.Show(details, "Детали тренировки",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetTrainerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string trainerInfo = _training.GetTrainerInfo();
                MessageBox.Show(trainerInfo, "Информация о тренере",
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
            var dbTraining = _context.GroupTrainings.Find(_training.Id);
            if (dbTraining != null)
            {
                _training.Title = dbTraining.Title;
                _training.Duration = dbTraining.Duration;
                _training.IsActive = dbTraining.IsActive;
                _training.TrainerName = dbTraining.TrainerName;
                _training.RoomNumber = dbTraining.RoomNumber;
                _training.MaxParticipants = dbTraining.MaxParticipants;
                _training.CurrentParticipants = dbTraining.CurrentParticipants;
            }
        }

        private void SaveToDatabase()
        {
            var dbTraining = _context.GroupTrainings.Find(_training.Id);
            if (dbTraining != null)
            {
                dbTraining.IsActive = _training.IsActive;
                dbTraining.CurrentParticipants = _training.CurrentParticipants;
                _context.SaveChanges();
            }
        }

        private void UpdateUI()
        {
            lblInfo.Text = _training.GetInfo();

            btnStart.Enabled = !_training.IsActive;
            btnFinish.Enabled = _training.IsActive;

            bool canAdd = _training.IsActive && _training.CurrentParticipants < _training.MaxParticipants;
            btnAddParticipant.Enabled = canAdd;
            btnAddMultiple.Enabled = canAdd;
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
                MessageBox.Show($"Тренировка '{_training.Title}' началась!\n\n" +
                               $"Тренер: {_training.TrainerName}\n" +
                               $"Зал: {_training.RoomNumber}\n" +
                               $"Максимум участников: {_training.MaxParticipants}\n\n" +
                               $"Теперь можно добавлять участников.",
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

                string participantsMessage = _training.CurrentParticipants > 0
                    ? $"Всего участников сегодня: {_training.CurrentParticipants}"
                    : "К сожалению, никто не пришёл на тренировку";

                MessageBox.Show($"Тренировка '{_training.Title}' завершена!\n\n" +
                               $"Тренер: {_training.TrainerName}\n" +
                               $"{participantsMessage}\n\n" +
                               $"Хорошая работа!",
                    "Завершение тренировки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_training.IsActive)
                {
                    MessageBox.Show("Сначала начните тренировку!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _training.AddParticipant();
                SaveToDatabase();
                UpdateUI();

                int freeSpaces = _training.MaxParticipants - _training.CurrentParticipants;
                string message = $"Участник добавлен!\n\n" +
                                $"Теперь: {_training.CurrentParticipants}/{_training.MaxParticipants}\n";

                if (freeSpaces > 0)
                    message += $"Осталось мест: {freeSpaces}";
                else
                    message += $"Группа полностью укомплектована.";

                MessageBox.Show(message, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddMultiple_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_training.IsActive)
                {
                    MessageBox.Show("Сначала начните тренировку!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int freeSpaces = _training.MaxParticipants - _training.CurrentParticipants;

                if (freeSpaces < 5)
                {
                    MessageBox.Show($"Недостаточно свободных мест! Свободно только {freeSpaces} мест. Нужно 5.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _training.AddParticipants(5);
                SaveToDatabase();
                UpdateUI();

                MessageBox.Show($"Добавлено 5 участников!\n\n" +
                               $"Теперь: {_training.CurrentParticipants}/{_training.MaxParticipants}\n" +
                               $"Осталось мест: {_training.MaxParticipants - _training.CurrentParticipants}",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToDatabase();
            _context.Dispose();
        }
    }
}