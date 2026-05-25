using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FitnessClubApp
{
    public partial class MainForm : Form
    {
        private ListBox listBoxTrainings;
        private Button btnAdd;
        private Button btnOpen;
        private Button btnDelete;
        private Button btnDemoYoga;
        private Button btnDemoPilates;
        private Button btnDemoYogas;
        private Button btnDemoSila;
        private Button btnRefresh;
        private Button btnEdit;

        private AppDbContext _context;
        private List<TrainingSession> _availableTrainings;

        public MainForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadAvailableTrainingsList();
            LoadTrainings();
        }

        private void InitializeComponent()
        {
            listBoxTrainings = new ListBox();
            btnAdd = new Button();
            btnOpen = new Button();
            btnDelete = new Button();
            btnDemoYoga = new Button();
            btnDemoPilates = new Button();
            btnDemoYogas = new Button();
            btnDemoSila = new Button();
            btnRefresh = new Button();
            btnEdit = new Button();
            SuspendLayout();

            // listBoxTrainings
            listBoxTrainings.FormattingEnabled = true;
            listBoxTrainings.Location = new Point(12, 12);
            listBoxTrainings.Name = "listBoxTrainings";
            listBoxTrainings.Size = new Size(360, 184);
            listBoxTrainings.TabIndex = 0;

            // btnEdit - Редактировать 
            btnEdit.Location = new Point(12, 220);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(170, 35);
            btnEdit.TabIndex = 9;
            btnEdit.Text = "Редактировать";
            btnEdit.Click += btnEdit_Click;

            // btnOpen - Открыть 
            btnOpen.Location = new Point(190, 220);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(170, 35);
            btnOpen.TabIndex = 2;
            btnOpen.Text = "Открыть";
            btnOpen.Click += btnOpen_Click;

            // btnAdd - Добавить тренировку 
            btnAdd.Location = new Point(12, 260);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(170, 35);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.Click += btnAdd_Click;

            // btnRefresh - Обновить 
            btnRefresh.Location = new Point(190, 260);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(170, 35);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Обновить";
            btnRefresh.Click += btnRefresh_Click;

            // btnDelete - Удалить 
            btnDelete.Location = new Point(12, 300);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(170, 35);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.Click += btnDelete_Click;

            // btnDemoYoga 
            btnDemoYoga.Location = new Point(190, 300);
            btnDemoYoga.Name = "btnDemoYoga";
            btnDemoYoga.Size = new Size(170, 35);
            btnDemoYoga.TabIndex = 4;
            btnDemoYoga.Text = "Йога";
            btnDemoYoga.Click += btnDemoYoga_Click;

            // btnDemoPilates
            btnDemoPilates.Location = new Point(12, 340);
            btnDemoPilates.Name = "btnDemoPilates";
            btnDemoPilates.Size = new Size(170, 35);
            btnDemoPilates.TabIndex = 5;
            btnDemoPilates.Text = "Пилатес";
            btnDemoPilates.Click += btnDemoPilates_Click;

            // btnDemoYogas
            btnDemoYogas.Location = new Point(190, 340);
            btnDemoYogas.Name = "btnDemoYogas";
            btnDemoYogas.Size = new Size(170, 35);
            btnDemoYogas.TabIndex = 6;
            btnDemoYogas.Text = "Копия Йоги";
            btnDemoYogas.Click += btnDemoYogas_Click;

            // btnDemoSila 
            btnDemoSila.Location = new Point(12, 380);
            btnDemoSila.Name = "btnDemoSila";
            btnDemoSila.Size = new Size(348, 35);
            btnDemoSila.TabIndex = 7;
            btnDemoSila.Text = "Силовая (персональная)";
            btnDemoSila.Click += btnDemoSila_Click;

            // MainForm
            ClientSize = new Size(384, 440);  // Увеличили высоту формы
            Controls.Add(listBoxTrainings);
            Controls.Add(btnEdit);
            Controls.Add(btnOpen);
            Controls.Add(btnAdd);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnDemoYoga);
            Controls.Add(btnDemoPilates);
            Controls.Add(btnDemoYogas);
            Controls.Add(btnDemoSila);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Фитнес-клуб \"Атлетик\"";
            ResumeLayout(false);
        }

        // Обновление списка из базы данных
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Очищаем кэш контекста, чтобы получить свежие данные
                _context.Dispose();
                _context = new AppDbContext();
                LoadTrainings();

                MessageBox.Show("Список тренировок обновлен!", "Обновление",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Редактирование
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = listBoxTrainings.SelectedItem as TrainingSession;
            if (selected == null)
            {
                MessageBox.Show("Выберите тренировку для редактирования!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var groupTraining = _context.GroupTrainings.FirstOrDefault(g => g.Id == selected.Id);
            if (groupTraining != null)
            {
                var editForm = new EditGroupTrainingForm(groupTraining, _context);
                editForm.ShowDialog();
                LoadTrainings(); 
                return;
            }

            var personalTraining = _context.PersonalTrainings.FirstOrDefault(p => p.Id == selected.Id);
            if (personalTraining != null)
            {
                var editForm = new EditPersonalTrainingForm(personalTraining, _context);
                editForm.ShowDialog();
                LoadTrainings(); 
                return;
            }
        }

        private void LoadAvailableTrainingsList()
        {
            _availableTrainings = new List<TrainingSession>
            {
                // Групповые тренировки
                new GroupTraining("Утренняя Йога", 60, 15, "Анна Иванова", 3),
                new GroupTraining("Пилатес", 50, 20, "Мария Петрова", 2, 5),
                new GroupTraining("HIIT Тренировка", 45, 12, "Дмитрий Сидоров", 1),
                new GroupTraining(new GroupTraining("Кроссфит", 55, 10, "Алексей Смирнов", 4), "Ольга Новая", 5),
                
                // Персональные тренировки 
                new PersonalTraining("Персональная силовая", 60, "Владимир", "Штанга, гантели", 8),
                new PersonalTraining("Персональный стретчинг", "Елена", 45, "Коврик, эспандер", 5),
                new PersonalTraining("Персональное кардио", 40, "Мария", "Беговая дорожка", 7)
            };
        }

        private void LoadTrainings()
        {
            var groupTrainings = _context.GroupTrainings.ToList();
            var personalTrainings = _context.PersonalTrainings.ToList();

            var allTrainings = new List<TrainingSession>();
            allTrainings.AddRange(groupTrainings);
            allTrainings.AddRange(personalTrainings);

            listBoxTrainings.DataSource = null;
            listBoxTrainings.DataSource = allTrainings;
        }

        //  конструктор 1: GroupTraining с 5 параметрами 
        private void btnDemoYoga_Click(object sender, EventArgs e)
        {
            try
            {
                var yoga = new GroupTraining("Йога", 60, 15, "Анна Иванова", 3);

                if (_context.TrainingSessions.Any(t => t.Title == yoga.Title))
                {
                    MessageBox.Show("Тренировка 'Йога' уже существует в базе!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _context.GroupTrainings.Add(yoga);
                _context.SaveChanges();
                LoadTrainings();

                MessageBox.Show($"Тренировка добавлена!\n\n" +
                               $"Название: {yoga.Title}\n" +
                               $"Длительность: {yoga.Duration} мин\n" +
                               $"Тренер: {yoga.TrainerName}\n" +
                               $"Зал: {yoga.RoomNumber}\n" +
                               $"Макс. участников: {yoga.MaxParticipants}\n\n",
                               "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Конструктор 2: GroupTraining
        private void btnDemoPilates_Click(object sender, EventArgs e)
        {
            try
            {
                var pilates = new GroupTraining("Пилатес", 50, 20, "Мария Петрова", 2, 7);

                if (_context.TrainingSessions.Any(t => t.Title == pilates.Title))
                {
                    MessageBox.Show("Тренировка 'Пилатес' уже существует в базе!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _context.GroupTrainings.Add(pilates);
                _context.SaveChanges();
                LoadTrainings();

                MessageBox.Show($"Тренировка добавлена!\n\n" +
                               $"Название: {pilates.Title}\n" +
                               $"Тренер: {pilates.TrainerName}\n" +
                               $"Зал: {pilates.RoomNumber}\n" +
                               $"Участников (уже записано): {pilates.CurrentParticipants}/{pilates.MaxParticipants}\n\n",
                               "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // конструктор  3: копирование GroupTraining 
        private void btnDemoYogas_Click(object sender, EventArgs e)
        {
            try
            {
                var originalYoga = _context.GroupTrainings.FirstOrDefault(g => g.Title == "Йога");

                if (originalYoga == null)
                {
                    MessageBox.Show("Сначала создайте тренировку 'Йога' с помощью кнопки 'Йога'!",
                        "Не найдено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var yogas = new GroupTraining(originalYoga, "Ольга Смирнова", 5);
                yogas.Title = "Йога с Ольгой";

                if (_context.TrainingSessions.Any(t => t.Title == yogas.Title))
                {
                    MessageBox.Show("Тренировка с таким названием уже существует!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _context.GroupTrainings.Add(yogas);
                _context.SaveChanges();
                LoadTrainings();

                MessageBox.Show($"Тренировка добавлена!\n\n" +
                               $"Оригинал: {originalYoga.Title} (тренер: {originalYoga.TrainerName}, зал: {originalYoga.RoomNumber})\n" +
                               $"Копия: {yogas.Title}\n" +
                               $"Новый тренер: {yogas.TrainerName}\n" +
                               $"Новый зал: {yogas.RoomNumber}\n" +
                               $"Участников: {yogas.CurrentParticipants}/{yogas.MaxParticipants}\n\n",
                               "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  конструктор PersonalTraining 
        private void btnDemoSila_Click(object sender, EventArgs e)
        {
            try
            {
                var sila = new PersonalTraining("Силовая тренировка", 45, "Виктория", "Штанга, гантели", 8);

                if (_context.TrainingSessions.Any(t => t.Title == sila.Title))
                {
                    MessageBox.Show("Тренировка с таким названием уже существует!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _context.PersonalTrainings.Add(sila);
                _context.SaveChanges();
                LoadTrainings();

                string intensityText = sila.IntensityLevel switch
                {
                    1 or 2 => "Очень низкая",
                    3 or 4 => "Низкая",
                    5 or 6 => "Средняя",
                    7 or 8 => "Высокая",
                    9 or 10 => "Максимальная",
                    _ => "Средняя"
                };

                MessageBox.Show($"Тренировка добавлена!\n\n" +
                               $"Название: {sila.Title}\n" +
                               $"Клиент: {sila.ClientName}\n" +
                               $"Оборудование: {sila.Equipment}\n" +
                               $"Интенсивность: {sila.IntensityLevel}/10 ({intensityText})\n" +
                               $"Длительность: {sila.Duration} мин\n\n",
                               "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form selectForm = new Form();
            selectForm.Text = "Выберите тренировку для добавления";
            selectForm.Width = 400;
            selectForm.Height = 500;
            selectForm.StartPosition = FormStartPosition.CenterParent;

            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            listBox.DisplayMember = "Title";
            listBox.DataSource = _availableTrainings;

            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 60;

            Button btnAddTraining = new Button();
            btnAddTraining.Text = "Добавить";
            btnAddTraining.Size = new System.Drawing.Size(150, 35);
            btnAddTraining.Location = new System.Drawing.Point(10, 10);
            btnAddTraining.Click += (s, args) =>
            {
                var selected = listBox.SelectedItem as TrainingSession;
                if (selected == null)
                {
                    MessageBox.Show("Выберите тренировку!");
                    return;
                }

                try
                {
                    // Проверяем, групповая ли тренировка
                    if (selected is GroupTraining groupSelected)
                    {

                        var newTraining = new GroupTraining
                        {
                            Title = groupSelected.Title,
                            Duration = groupSelected.Duration,
                            TrainingType = "Group",
                            IsActive = false,
                            TrainerName = groupSelected.TrainerName,
                            RoomNumber = groupSelected.RoomNumber,
                            MaxParticipants = groupSelected.MaxParticipants,
                            CurrentParticipants = 0
                        };

                        _context.GroupTrainings.Add(newTraining);
                    }
                    // Проверяем, персональная ли тренировка
                    else if (selected is PersonalTraining personalSelected)
                    {

                        var newTraining = new PersonalTraining
                        {
                            Title = personalSelected.Title,
                            Duration = personalSelected.Duration,
                            TrainingType = "Personal",
                            IsActive = false,
                            ClientName = personalSelected.ClientName,
                            Equipment = personalSelected.Equipment,
                            IntensityLevel = personalSelected.IntensityLevel
                        };

                        _context.PersonalTrainings.Add(newTraining);
                    }

                    _context.SaveChanges();
                    LoadTrainings();
                    MessageBox.Show("Тренировка добавлена!");
                    selectForm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            };

            Button btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.Location = new System.Drawing.Point(170, 10);
            btnCancel.Click += (s, args) => selectForm.Close();

            buttonPanel.Controls.Add(btnAddTraining);
            buttonPanel.Controls.Add(btnCancel);
            selectForm.Controls.Add(listBox);
            selectForm.Controls.Add(buttonPanel);
            selectForm.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var selected = listBoxTrainings.SelectedItem as TrainingSession;
            if (selected == null)
            {
                MessageBox.Show("Выберите тренировку!");
                return;
            }

            var groupTraining = _context.GroupTrainings.FirstOrDefault(g => g.Id == selected.Id);
            if (groupTraining != null)
            {
                var form = new GroupTrainingForm(groupTraining);
                form.Show();
                return;
            }

            var personalTraining = _context.PersonalTrainings.FirstOrDefault(p => p.Id == selected.Id);
            if (personalTraining != null)
            {
                var form = new PersonalTrainingForm(personalTraining);
                form.Show();
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = listBoxTrainings.SelectedItem as TrainingSession;
            if (selected == null)
            {
                MessageBox.Show("Выберите тренировку!");
                return;
            }

            var result = MessageBox.Show($"Удалить {selected.Title}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var groupToDelete = _context.GroupTrainings.FirstOrDefault(g => g.Id == selected.Id);
                    if (groupToDelete != null)
                    {
                        _context.GroupTrainings.Remove(groupToDelete);
                    }
                    else
                    {
                        var personalToDelete = _context.PersonalTrainings.FirstOrDefault(p => p.Id == selected.Id);
                        if (personalToDelete != null)
                        {
                            _context.PersonalTrainings.Remove(personalToDelete);
                        }
                    }

                    _context.SaveChanges();
                    LoadTrainings();
                    MessageBox.Show("Тренировка удалена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }
    }
}